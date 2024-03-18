// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public class PathTests : PathTestsBase
    {
        [Theory,
            InlineData(Platform.Windows, null, null, null),
            InlineData(Platform.Windows, null, "exe", null),
            InlineData(Platform.Windows, "", "", ""),
            InlineData(Platform.Windows, "file.exe", null, "file"),
            InlineData(Platform.Windows, "file.exe", "", "file."),
            InlineData(Platform.Windows, "file", "exe", "file.exe"),
            InlineData(Platform.Windows, "file", ".exe", "file.exe"),
            InlineData(Platform.Windows, "file.txt", "exe", "file.exe"),
            InlineData(Platform.Windows, "file.txt", ".exe", "file.exe"),
            InlineData(Platform.Windows, "file.txt.bin", "exe", "file.txt.exe"),
            InlineData(Platform.Windows, "dir/file.t", "exe", "dir/file.exe"),
            InlineData(Platform.Windows, "dir/file.exe", "t", "dir/file.t"),
            InlineData(Platform.Windows, "dir/file", "exe", "dir/file.exe"),
            InlineData(Platform.Unix, null, null, null),
            InlineData(Platform.Unix, null, "exe", null),
            InlineData(Platform.Unix, "", "", ""),
            InlineData(Platform.Unix, "file.exe", null, "file"),
            InlineData(Platform.Unix, "file.exe", "", "file."),
            InlineData(Platform.Unix, "file", "exe", "file.exe"),
            InlineData(Platform.Unix, "file", ".exe", "file.exe"),
            InlineData(Platform.Unix, "file.txt", "exe", "file.exe"),
            InlineData(Platform.Unix, "file.txt", ".exe", "file.exe"),
            InlineData(Platform.Unix, "file.txt.bin", "exe", "file.txt.exe"),
            InlineData(Platform.Unix, "dir/file.t", "exe", "dir/file.exe"),
            InlineData(Platform.Unix, "dir/file.exe", "t", "dir/file.t"),
            InlineData(Platform.Unix, "dir/file", "exe", "dir/file.exe")]
        public void ChangeExtension(Platform platform, string path, string newExtension, string expected)
        {
            if (expected != null)
                expected = expected.Replace('/', Path.GetDirectorySeparatorChar(platform));
            if (path != null)
                path = path.Replace('/', Path.GetDirectorySeparatorChar(platform));
            Assert.Equal(expected, Path.ChangeExtension(platform, path, newExtension));
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public void GetDirectoryName_NullReturnsNull(Platform platform)
        {
            Assert.Null(Path.GetDirectoryName(platform, null));
        }

        [Theory] 
        [MemberData(nameof(Get_TestData_GetDirectoryName), Platform.Windows)]
        [MemberData(nameof(Get_TestData_GetDirectoryName), Platform.Unix)]
        public void GetDirectoryName(Platform platform, string path, string expected)
        {
            Assert.Equal(expected, Path.GetDirectoryName(platform, path));
        }
         
         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetExtension_Null(Platform platform)
         {
             Assert.Null(Path.GetExtension(platform, null));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetExtension), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetExtension), Platform.Unix)]
         public void GetExtension(Platform platform, string path, string expected)
         {
             Assert.Equal(expected, Path.GetExtension(platform, path));
             Assert.Equal(!string.IsNullOrEmpty(expected), Path.HasExtension(platform, path));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetFileName_Null(Platform platform)
         {
             Assert.Null(Path.GetFileName(platform, null));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetFileName_Empty(Platform platform)
         {
             Assert.Empty(Path.GetFileName(platform, string.Empty));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetFileName), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetFileName), Platform.Unix)]
         public void GetFileName(Platform platform, string path, string expected)
         {
             Assert.Equal(expected, Path.GetFileName(platform, path));
             Assert.Equal(expected, Path.GetFileName(platform, Path.Combine(platform, "whizzle", path)));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetFileNameWithoutExtension_Null(Platform platform)
         {
             Assert.Null(Path.GetFileNameWithoutExtension(platform, null));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetFileNameWithoutExtension), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetFileNameWithoutExtension), Platform.Unix)]
         public void GetFileNameWithoutExtension(Platform platform, string path, string expected)
         {
             Assert.Equal(expected, Path.GetFileNameWithoutExtension(platform, path));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetPathRoot_Null(Platform platform)
         {
             Assert.Null(Path.GetPathRoot(platform, null));
         }

         [Theory]
         [InlineData(Platform.Windows, @"c:\users\user")]
         [InlineData(Platform.Unix, @"/home/user")]
         public void GetPathRoot_Basic(Platform platform, string cwd)
         {
             string substring = cwd.Substring(0, cwd.IndexOf(Path.GetDirectorySeparatorChar(platform)) + 1);

             Assert.Equal(substring, Path.GetPathRoot(platform, cwd));
             PathAssert.Equal(substring.AsSpan(), Path.GetPathRoot(platform, cwd.AsSpan()));

             Assert.True(Path.IsPathRooted(platform, cwd));

             Assert.Equal(string.Empty, Path.GetPathRoot(platform, @"file.exe"));
             Assert.True(Path.GetPathRoot(platform, @"file.exe".AsSpan()).IsEmpty);

             Assert.False(Path.IsPathRooted(platform, "file.exe"));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetInvalidPathChars_Invariants(Platform platform)
         {
             Assert.NotNull(Path.GetInvalidPathChars(platform));
             Assert.NotSame(Path.GetInvalidPathChars(platform), Path.GetInvalidPathChars(platform));
             Assert.Equal((IEnumerable<char>) Path.GetInvalidPathChars(platform), Path.GetInvalidPathChars(platform));
             Assert.True(Path.GetInvalidPathChars(platform).Length > 0);
         }
         
         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetInvalidFileNameChars_Invariants(Platform platform)
         {
             Assert.NotNull(Path.GetInvalidFileNameChars(platform));
             Assert.NotSame(Path.GetInvalidFileNameChars(platform), Path.GetInvalidFileNameChars(platform));
             Assert.Equal((IEnumerable<char>)Path.GetInvalidFileNameChars(platform), Path.GetInvalidFileNameChars(platform));
             Assert.True(Path.GetInvalidFileNameChars(platform).Length > 0);
         }
         
         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetDirectoryName_EmptyReturnsNull(Platform platform)
         {
             // In .NET Framework this throws argument exception
             Assert.Null(Path.GetDirectoryName(platform, string.Empty));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_Spaces), Platform.Windows)]
         [MemberData(nameof(Get_TestData_Spaces), Platform.Unix)]
         public void GetDirectoryName_Spaces(Platform platform, string path)
         {
             if (platform == Platform.Windows)
             {
                 // In Windows spaces are eaten by Win32, making them effectively empty
                 Assert.Null(Path.GetDirectoryName(platform, path));
             }
             else
             {
                 Assert.Empty(Path.GetDirectoryName(platform, path));
             }
         }

         [Theory]
         [MemberData(nameof(Get_TestData_Spaces), Platform.Windows)]
         [MemberData(nameof(Get_TestData_Spaces), Platform.Unix)]
         public void GetDirectoryName_Span_Spaces(Platform platform, string path)
         {
             PathAssert.Empty(Path.GetDirectoryName(platform, path.AsSpan()));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_EmbeddedNull), Platform.Windows)]
         [MemberData(nameof(Get_TestData_ControlChars), Platform.Windows)]
         [MemberData(nameof(Get_TestData_UnicodeWhiteSpace), Platform.Windows)]
         public void GetDirectoryName_NetFxInvalid(Platform platform, string path)
         {
             Assert.Empty(Path.GetDirectoryName(platform, path));
             Assert.Equal(path, Path.GetDirectoryName(platform, Path.Combine(platform, path, path)));
             PathAssert.Empty(Path.GetDirectoryName(platform, path.AsSpan()));
             PathAssert.Equal(path, new string(Path.GetDirectoryName(platform, Path.Combine(platform, path, path).AsSpan())));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetDirectoryName), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetDirectoryName), Platform.Unix)]
         public void GetDirectoryName_Span(Platform platform, string path, string expected)
         {
             PathAssert.Equal(expected ?? ReadOnlySpan<char>.Empty, Path.GetDirectoryName(platform, path.AsSpan()));
         }

         [Theory]
         [InlineData(Platform.Windows, @"c:\users\user")]
         [InlineData(Platform.Unix, @"/home/user")]
         public void GetDirectoryName_Span_CurrentDirectory(Platform platform, string curDir)
         {
             PathAssert.Equal(curDir, Path.GetDirectoryName(platform, Path.Combine(platform, curDir, "baz").AsSpan()));
             PathAssert.Empty(Path.GetDirectoryName(platform, Path.GetPathRoot(platform, curDir).AsSpan()));
         }

         [Theory]
         [InlineData(Platform.Windows, @" C:\dir/baz", @" C:\dir")]
         [InlineData(Platform.Unix, @" C:\dir/baz", @" C:\dir")]
         public void GetDirectoryName_SkipSpaces(Platform platform, string path, string expected)
         {
             // We no longer trim leading spaces for any path
             Assert.Equal(expected, Path.GetDirectoryName(platform, path));
         }

         [Theory] 
         [MemberData(nameof(Get_TestData_GetExtension), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetExtension), Platform.Unix)]
         public void GetExtension_Span(Platform platform, string path, string expected)
         {
             PathAssert.Equal(expected, Path.GetExtension(platform, path.AsSpan()));
             Assert.Equal(!string.IsNullOrEmpty(expected), Path.HasExtension(platform, path.AsSpan()));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetFileName), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetFileName), Platform.Unix)]
         public void GetFileName_Span(Platform platform, string path, string expected)
         {
             PathAssert.Equal(expected, Path.GetFileName(platform, path.AsSpan()));
         }

         public static IEnumerable<object[]> Get_TestData_GetFileName_Volume(Platform platform)
         {
             yield return new object[] {platform, ":", ":" };
             yield return new object[] {platform, ".:", ".:" };
             yield return new object[] {platform, ".:.", ".:." };     // Not a valid drive letter
             yield return new object[] {platform, "file:", "file:" };
             yield return new object[] {platform, ":file", ":file" };
             yield return new object[] {platform, "file:exe", "file:exe" };
             yield return new object[] {platform, Path.Combine(platform, "baz", "file:exe"), "file:exe" };
             yield return new object[] {platform, Path.Combine(platform, "bar", "baz", "file:exe"), "file:exe" };
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetFileName_Volume), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetFileName_Volume), Platform.Unix)]
         public void GetFileName_Volume(Platform platform, string path, string expected)
         {
             // We used to break on ':' on Windows. This is a valid file name character for alternate data streams.
             // Additionally the character can show up on unix volumes mounted to Windows.
             Assert.Equal(expected, Path.GetFileName(platform, path));
             PathAssert.Equal(expected, Path.GetFileName(platform, path.AsSpan()));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_GetFileNameWithoutExtension), Platform.Windows)]
         [MemberData(nameof(Get_TestData_GetFileNameWithoutExtension), Platform.Unix)]
         public void GetFileNameWithoutExtension_Span(Platform platform, string path, string expected)
         {
             PathAssert.Equal(expected, Path.GetFileNameWithoutExtension(platform, path.AsSpan()));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetPathRoot_Empty(Platform platform)
         {
             Assert.Null(Path.GetPathRoot(platform, string.Empty));
         }

         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetPathRoot_Empty_Span(Platform platform)
         {
             PathAssert.Empty(Path.GetPathRoot(platform, ReadOnlySpan<char>.Empty));
         }

         [Theory]
         [MemberData(nameof(Get_TestData_Spaces), Platform.Windows)]
         [MemberData(nameof(Get_TestData_ControlChars), Platform.Windows)]
         [MemberData(nameof(Get_TestData_EmbeddedNull), Platform.Windows)]
         [MemberData(nameof(Get_TestData_InvalidDriveLetters), Platform.Windows)]
         [MemberData(nameof(Get_TestData_UnicodeWhiteSpace), Platform.Windows)]
         [MemberData(nameof(Get_TestData_EmptyString), Platform.Windows)]  
         [MemberData(nameof(Get_TestData_Spaces), Platform.Unix)]
         [MemberData(nameof(Get_TestData_ControlChars), Platform.Unix)]
         [MemberData(nameof(Get_TestData_EmbeddedNull), Platform.Unix)]
         [MemberData(nameof(Get_TestData_InvalidDriveLetters), Platform.Unix)]
         [MemberData(nameof(Get_TestData_UnicodeWhiteSpace), Platform.Unix)]
         [MemberData(nameof(Get_TestData_EmptyString), Platform.Unix)]
         public void IsPathRooted_NegativeCases(Platform platform, string path)
         {
             Assert.False(Path.IsPathRooted(platform, path));
             Assert.False(Path.IsPathRooted(platform, path.AsSpan()));
         }
         
         [Theory]
         [InlineData(Platform.Windows)]
         [InlineData(Platform.Unix)]
         public void GetInvalidPathChars_Span(Platform platform)
         {
             Assert.All(Path.GetInvalidPathChars(platform), c =>
             {
                 string bad = c.ToString();
                 Assert.Equal(string.Empty, new string(Path.GetDirectoryName(platform, bad.AsSpan())));
                 Assert.Equal(string.Empty, new string(Path.GetExtension(platform, bad.AsSpan())));
                 Assert.Equal(bad, new string(Path.GetFileName(platform, bad.AsSpan())));
                 Assert.Equal(bad, new string(Path.GetFileNameWithoutExtension(platform, bad.AsSpan())));
                 Assert.True(Path.GetPathRoot(platform, bad.AsSpan()).IsEmpty);
                 Assert.False(Path.IsPathRooted(platform, bad.AsSpan()));
             });
         }
    }
}
