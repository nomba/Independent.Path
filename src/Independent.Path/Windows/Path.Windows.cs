// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;

namespace System.IO.Independent.Windows
{
    public static unsafe partial class Path
    {
        public static char[] GetInvalidFileNameChars() => new char[]
        {
            '\"', '<', '>', '|', '\0',
            (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9, (char)10,
            (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19, (char)20,
            (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29, (char)30,
            (char)31, ':', '*', '?', '\\', '/'
        };

        public static char[] GetInvalidPathChars() => new char[]
        {
            '|', '\0',
            (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9, (char)10,
            (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19, (char)20,
            (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29, (char)30,
            (char)31
        };
        
        // Tests if the given path contains a root. A path is considered rooted
        // if it starts with a backslash ("\") or a valid drive letter and a colon (":").
        public static bool IsPathRooted([NotNullWhen(true)] string? path)
        {
            return path != null && IsPathRooted(path.AsSpan());
        }

        public static bool IsPathRooted(ReadOnlySpan<char> path)
        {
            int length = path.Length;
            return (length >= 1 && PathInternal.IsDirectorySeparator(path[0]))
                || (length >= 2 && PathInternal.IsValidDriveChar(path[0]) && path[1] == PathInternal.VolumeSeparatorChar);
        }

        // Returns the root portion of the given path. The resulting string
        // consists of those rightmost characters of the path that constitute the
        // root of the path. Possible patterns for the resulting string are: An
        // empty string (a relative path on the current drive), "\" (an absolute
        // path on the current drive), "X:" (a relative path on a given drive,
        // where X is the drive letter), "X:\" (an absolute path on a given drive),
        // and "\\server\share" (a UNC path for a given server and share name).
        // The resulting string is null if path is null. If the path is empty or
        // only contains whitespace characters an ArgumentException gets thrown.
        public static string? GetPathRoot(string? path)
        {
            if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
                return null;

            ReadOnlySpan<char> result = GetPathRoot(path.AsSpan());
            if (path!.Length == result.Length)
                return PathInternal.NormalizeDirectorySeparators(path);

            return PathInternal.NormalizeDirectorySeparators(result.ToString());
        }

        /// <remarks>
        /// Unlike the string overload, this method will not normalize directory separators.
        /// </remarks>
        public static ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
        {
            if (PathInternal.IsEffectivelyEmpty(path))
                return ReadOnlySpan<char>.Empty;

            int pathRoot = PathInternal.GetRootLength(path);
            return pathRoot <= 0 ? ReadOnlySpan<char>.Empty : path.Slice(0, pathRoot);
        }
    }
}
