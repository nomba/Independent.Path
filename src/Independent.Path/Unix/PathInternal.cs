// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;

namespace System.IO.Independent.Unix
{
    /// <summary>Contains internal path helpers that are shared between many projects.</summary>
    internal static partial class PathInternal
    {
        /// <summary>
        /// Returns true if the path starts in a directory separator.
        /// </summary>
        internal static bool StartsWithDirectorySeparator(ReadOnlySpan<char> path) => path.Length > 0 && IsDirectorySeparator(path[0]);

        internal static string EnsureTrailingSeparator(string path)
            => EndsInDirectorySeparator(path.AsSpan()) ? path : path + DirectorySeparatorCharAsString;

        internal static bool IsRoot(ReadOnlySpan<char> path)
            => path.Length == GetRootLength(path);
        
        /// <summary>
        /// Trims one trailing directory separator beyond the root of the path.
        /// </summary>
        [return: NotNullIfNotNull(nameof(path))]
        internal static string? TrimEndingDirectorySeparator(string? path) =>
            EndsInDirectorySeparator(path) && !IsRoot(path.AsSpan()) ?
                path!.Substring(0, path.Length - 1) :
                path;

        /// <summary>
        /// Returns true if the path ends in a directory separator.
        /// </summary>
        internal static bool EndsInDirectorySeparator([NotNullWhen(true)] string? path) =>
              !string.IsNullOrEmpty(path) && IsDirectorySeparator(path[path.Length - 1]);

        /// <summary>
        /// Trims one trailing directory separator beyond the root of the path.
        /// </summary>
        internal static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path) =>
            EndsInDirectorySeparator(path) && !IsRoot(path) ?
                path.Slice(0, path.Length - 1) :
                path;

        /// <summary>
        /// Returns true if the path ends in a directory separator.
        /// </summary>
        internal static bool EndsInDirectorySeparator(ReadOnlySpan<char> path) =>
            path.Length > 0 && IsDirectorySeparator(path[path.Length - 1]);
    }
}
