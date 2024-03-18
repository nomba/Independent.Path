using System.Diagnostics.CodeAnalysis;

namespace System.IO.Independent;

// Publish NuGet
// https://learn.microsoft.com/en-us/nuget/create-packages/package-authoring-best-practices#readme
// https://github.com/NuGet/docs.microsoft.com-nuget/blob/main/docs/nuget-org/package-readme-on-nuget-org.md

// About README.md
// https://devblogs.microsoft.com/nuget/add-a-readme-to-your-nuget-package/

// Getting started to cross platform targeting
// https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/cross-platform-targeting
public static class Path
{
    /// <summary>
    ///  Enables getting property <see cref="IO.Path.DirectorySeparatorChar"/> for specified platform
    /// </summary>
    public static char GetDirectorySeparatorChar(Platform platform) => platform switch
    {
        Platform.Windows => Windows.Path.DirectorySeparatorChar,
        Platform.Unix => Unix.Path.DirectorySeparatorChar,
        _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
    }; 
    
    /// <summary>
    ///  Enables getting property <see cref="IO.Path.AltDirectorySeparatorChar"/> for specified platform
    /// </summary>
    public static char GetAltDirectorySeparatorChar(Platform platform) => platform switch
    {
        Platform.Windows => Windows.Path.AltDirectorySeparatorChar,
        Platform.Unix => Unix.Path.AltDirectorySeparatorChar,
        _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
    };
    
    /// <summary>
    ///  Enables getting property <see cref="IO.Path.VolumeSeparatorChar"/> for specified platform
    /// </summary>
    public static char GetVolumeSeparatorChar(Platform platform) => platform switch
    {
        Platform.Windows => Windows.Path.VolumeSeparatorChar,
        Platform.Unix => Unix.Path.VolumeSeparatorChar,
        _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
    }; 
    
    /// <summary>
    ///  Enables getting property <see cref="IO.Path.VolumeSeparatorChar"/> for specified platform
    /// </summary>
    public static char GetPathSeparator(Platform platform) => platform switch
    {
        Platform.Windows => Windows.Path.PathSeparator,
        Platform.Unix => Unix.Path.PathSeparator,
        _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
    };

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.ChangeExtension(string, string)"/>
    /// </summary>
    public static string? ChangeExtension(Platform platform, string? path, string? extension)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.ChangeExtension(path, extension),
            Platform.Unix => Unix.Path.ChangeExtension(path, extension),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetDirectoryName(string)"/>
    /// </summary>
    public static string? GetDirectoryName(Platform platform, string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetDirectoryName(path),
            Platform.Unix => Unix.Path.GetDirectoryName(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }    
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetDirectoryName(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static ReadOnlySpan<char> GetDirectoryName(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetDirectoryName(path),
            Platform.Unix => Unix.Path.GetDirectoryName(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    } 
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetExtension(string)"/>
    /// </summary>
    public static string? GetExtension(Platform platform, string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetExtension(path),
            Platform.Unix => Unix.Path.GetExtension(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }   
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetExtension(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static ReadOnlySpan<char> GetExtension(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetExtension(path),
            Platform.Unix => Unix.Path.GetExtension(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }    
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetFileName(string)"/>
    /// </summary>
    [return: NotNullIfNotNull(nameof(path))]
    public static string? GetFileName(Platform platform, string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetFileName(path),
            Platform.Unix => Unix.Path.GetFileName(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetFileNameWithoutExtension(string)"/>
    /// </summary>
    [return: NotNullIfNotNull(nameof(path))]
    public static string? GetFileNameWithoutExtension(Platform platform, string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetFileNameWithoutExtension(path),
            Platform.Unix => Unix.Path.GetFileNameWithoutExtension(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    } 
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetFileNameWithoutExtension(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static ReadOnlySpan<char> GetFileNameWithoutExtension(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetFileNameWithoutExtension(path),
            Platform.Unix => Unix.Path.GetFileNameWithoutExtension(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }    
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetFileName(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static ReadOnlySpan<char> GetFileName(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetFileName(path),
            Platform.Unix => Unix.Path.GetFileName(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetInvalidFileNameChars()"/>
    /// </summary>
    public static char[] GetInvalidFileNameChars(Platform platform)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetInvalidFileNameChars(),
            Platform.Unix => Unix.Path.GetInvalidFileNameChars(),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetInvalidPathChars()"/>
    /// </summary>
    public static char[] GetInvalidPathChars(Platform platform)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetInvalidPathChars(),
            Platform.Unix => Unix.Path.GetInvalidPathChars(),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.IsPathFullyQualified(string)"/>
    /// </summary>
    public static bool IsPathFullyQualified(Platform platform, string path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.IsPathFullyQualified(path),
            Platform.Unix => Unix.Path.IsPathFullyQualified(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.IsPathFullyQualified(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static bool IsPathFullyQualified(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.IsPathFullyQualified(path),
            Platform.Unix => Unix.Path.IsPathFullyQualified(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.IsPathRooted(string)"/>
    /// </summary>
    public static bool IsPathRooted(Platform platform, [NotNullWhen(true)] string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.IsPathRooted(path),
            Platform.Unix => Unix.Path.IsPathRooted(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    } 
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.IsPathRooted(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static bool IsPathRooted(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.IsPathRooted(path),
            Platform.Unix => Unix.Path.IsPathRooted(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }    
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetPathRoot(string)"/>
    /// </summary>
    public static string? GetPathRoot(Platform platform, string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetPathRoot(path),
            Platform.Unix => Unix.Path.GetPathRoot(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    } 
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.GetPathRoot(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static ReadOnlySpan<char> GetPathRoot(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.GetPathRoot(path),
            Platform.Unix => Unix.Path.GetPathRoot(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.HasExtension(string?)"/>
    /// </summary>
    public static bool HasExtension(Platform platform, [NotNullWhen(true)] string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.HasExtension(path),
            Platform.Unix => Unix.Path.HasExtension(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.HasExtension(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static bool HasExtension(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.HasExtension(path),
            Platform.Unix => Unix.Path.HasExtension(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.Combine(string,string)"/>
    /// </summary>
    public static string Combine(Platform platform, string path1, string path2)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.Combine(path1, path2),
            Platform.Unix => Unix.Path.Combine(path1, path2),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.Combine(string[])"/>
    /// </summary>
    public static string Combine(Platform platform, params string[] paths)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.Combine(paths),
            Platform.Unix => Unix.Path.Combine(paths),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.Join(System.ReadOnlySpan{char},System.ReadOnlySpan{char})"/>
    /// </summary>
    public static string Join(Platform platform, ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.Join(path1, path2),
            Platform.Unix => Unix.Path.Join(path1, path2),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.Join(string?, string?)"/>
    /// </summary>
    public static string Join(Platform platform, string? path1, string? path2)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.Join(path1, path2),
            Platform.Unix => Unix.Path.Join(path1, path2),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.Join(string?, string?, string?, string?)"/>
    /// </summary>
    public static string Join(Platform platform, string? path1, string? path2, string? path3, string? path4)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.Join(path1, path2, path3, path4),
            Platform.Unix => Unix.Path.Join(path1, path2, path3, path4),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.Join(string[])"/>
    /// </summary>
    public static string Join(Platform platform, params string?[] paths)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.Join(paths),
            Platform.Unix => Unix.Path.Join(paths),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.TryJoin(System.ReadOnlySpan{char},System.ReadOnlySpan{char}, Span{char}, out int)"/>
    /// </summary>
    public static bool TryJoin(Platform platform, ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, Span<char> destination, out int charsWritten)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.TryJoin(path1, path2, destination, out charsWritten),
            Platform.Unix => Unix.Path.TryJoin(path1, path2, destination, out charsWritten),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.TryJoin(System.ReadOnlySpan{char},System.ReadOnlySpan{char},System.ReadOnlySpan{char}, Span{char}, out int)"/>
    /// </summary>
    public static bool TryJoin(Platform platform, ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, Span<char> destination, out int charsWritten)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.TryJoin(path1, path2, path3, destination, out charsWritten),
            Platform.Unix => Unix.Path.TryJoin(path1, path2, path3, destination, out charsWritten),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }

    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.TrimEndingDirectorySeparator(string)"/>
    /// </summary>
    public static string TrimEndingDirectorySeparator(Platform platform, string path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.TrimEndingDirectorySeparator(path),
            Platform.Unix => Unix.Path.TrimEndingDirectorySeparator(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.TrimEndingDirectorySeparator(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static ReadOnlySpan<char> TrimEndingDirectorySeparator(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.TrimEndingDirectorySeparator(path),
            Platform.Unix => Unix.Path.TrimEndingDirectorySeparator(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.EndsInDirectorySeparator(System.ReadOnlySpan{char})"/>
    /// </summary>
    public static bool EndsInDirectorySeparator(Platform platform, ReadOnlySpan<char> path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.EndsInDirectorySeparator(path),
            Platform.Unix => Unix.Path.EndsInDirectorySeparator(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    } 
    
    /// <summary>
    /// Enables specifying platform for core method <see cref="IO.Path.EndsInDirectorySeparator(string)"/>
    /// </summary>
    public static bool EndsInDirectorySeparator(Platform platform, [NotNullWhen(true)] string? path)
    {
        return platform switch
        {
            Platform.Windows => Windows.Path.EndsInDirectorySeparator(path),
            Platform.Unix => Unix.Path.EndsInDirectorySeparator(path),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, "Unknown platform.")
        };
    }
}