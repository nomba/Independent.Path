// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;

namespace System.IO.Independent.Unix
{
    public static partial class Path
    {
        public static char[] GetInvalidFileNameChars() => new char[] { '\0', '/' };

        public static char[] GetInvalidPathChars() => new char[] { '\0' };
        
        public static bool IsPathRooted([NotNullWhen(true)] string? path)
        {
            if (path == null)
                return false;

            return IsPathRooted(path.AsSpan());
        }

        public static bool IsPathRooted(ReadOnlySpan<char> path)
        {
            return path.Length > 0 && path[0] == PathInternal.DirectorySeparatorChar;
        }

        /// <summary>
        /// Returns the path root or null if path is empty or null.
        /// </summary>
        public static string? GetPathRoot(string? path)
        {
            if (PathInternal.IsEffectivelyEmpty(path)) return null;
            return IsPathRooted(path) ? PathInternal.DirectorySeparatorCharAsString : string.Empty;
        }

        public static ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
        {
            return IsPathRooted(path) ? PathInternal.DirectorySeparatorCharAsString.AsSpan() : ReadOnlySpan<char>.Empty;
        }

    }
}
