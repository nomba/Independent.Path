// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;

namespace System.IO.Independent.Tests
{
    public static class PathFeatures
    {
        private enum State
        {
            Uninitialized,
            True,
            False
        }

        // Note that this class is using APIs that allow it to run on all platforms (including Core 5.0)
        // That is why we have .GetTypeInfo(), don't use the Registry, etc...

        private static State s_osEnabled;
        private static State s_onCore;
        
        public static bool IsUsingLegacyPathNormalization()
        {
            return HasLegacyIoBehavior("UseLegacyPathHandling");
        }
        
        private static bool HasLegacyIoBehavior(string propertyName)
        {
            // Core doesn't have legacy behaviors
            if (RunningOnCoreLib)
                return false;

            Type t = typeof(object).GetTypeInfo().Assembly.GetType("System.AppContextSwitches");
            var p = t.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public);

            // If the switch actually exists use it, otherwise we predate the switch and are effectively on
            return (bool)(p?.GetValue(null) ?? true);
        }

        private static bool RunningOnCoreLib
        {
            get
            {
                // Not particularly elegant
                if (s_onCore == State.Uninitialized)
                    s_onCore = typeof(object).GetTypeInfo().Assembly.GetName().Name == "System.Private.CoreLib" ? State.True : State.False;

                return s_onCore == State.True;
            }
        }
    }
}
