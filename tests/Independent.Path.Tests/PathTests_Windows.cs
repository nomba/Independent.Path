// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public class PathTests_Windows : PathTestsBase
    {
        [Fact]
        public void GetDirectoryName_DevicePath()
        {
            if (PathFeatures.IsUsingLegacyPathNormalization())
            {
                Assert.Equal(@"\\?\C:", Path.GetDirectoryName(Platform.Windows, @"\\?\C:\foo"));
            }
            else
            {
                Assert.Equal(@"\\?\C:\", Path.GetDirectoryName(Platform.Windows, @"\\?\C:\foo"));
            }
        }

        [Theory, MemberData(nameof(TestData_GetDirectoryName_Windows))]
        public void GetDirectoryName(string path, string expected)
        {
            Assert.Equal(expected, Path.GetDirectoryName(Platform.Windows, path));
        }

        [Theory,
            InlineData("B:", ""),
            InlineData("A:.", ".")]
        public static void GetFileName_Volume(string path, string expected)
        {
            // With a valid drive letter followed by a colon, we have a root, but only on Windows.
            Assert.Equal(expected, Path.GetFileName(Platform.Windows, path));
        }

        [Theory,
            MemberData(nameof(TestData_GetPathRoot_Windows)),
            MemberData(nameof(TestData_GetPathRoot_Unc)),
            MemberData(nameof(TestData_GetPathRoot_DevicePaths))]
        public void GetPathRoot_Windows(string value, string expected)
        {
            Assert.Equal(expected, Path.GetPathRoot(Platform.Windows, value));
        
            if (value.Length != expected.Length)
            {
                // The string overload normalizes the separators
                Assert.Equal(expected, Path.GetPathRoot(Platform.Windows, value.Replace(Path.GetDirectorySeparatorChar(Platform.Windows), Path.GetAltDirectorySeparatorChar(Platform.Windows))));
        
                // UNCs and device paths will have their semantics changed if we double up separators
                if (!value.StartsWith(@"\\"))
                    Assert.Equal(expected, Path.GetPathRoot(Platform.Windows, value.Replace(@"\", @"\\")));
            }
        }
        
        [Theory,
            MemberData(nameof(TestData_GetPathRoot_Windows)),
            MemberData(nameof(TestData_GetPathRoot_Unc)),
            MemberData(nameof(TestData_GetPathRoot_DevicePaths))]
        public void GetPathRoot_Span(string value, string expected)
        {
            Assert.Equal(expected, new string(Path.GetPathRoot(Platform.Windows, value.AsSpan())));
            Assert.True(Path.IsPathRooted(Platform.Windows, value.AsSpan()));
        }

        public static TheoryData<string, string> TestData_TrimEndingDirectorySeparator => new TheoryData<string, string>
        {
            { @"C:\folder\", @"C:\folder" },
            { @"C:/folder/", @"C:/folder" },
            { @"/folder/", @"/folder" },
            { @"\folder\", @"\folder" },
            { @"folder\", @"folder" },
            { @"folder/", @"folder" },
            { @"C:\", @"C:\" },
            { @"C:/", @"C:/" },
            { @"", @"" },
            { @"/", @"/" },
            { @"\", @"\" },
            { @"\\server\share\", @"\\server\share" },
            { @"\\server\share\folder\", @"\\server\share\folder" },
            { @"\\?\C:\", @"\\?\C:\" },
            { @"\\?\C:\folder\", @"\\?\C:\folder" },
            { @"\\?\UNC\", @"\\?\UNC\" },
            { @"\\?\UNC\a\", @"\\?\UNC\a\" },
            { @"\\?\UNC\a\folder\", @"\\?\UNC\a\folder" },
            { null, null }
        };

        public static TheoryData<string, bool> TestData_EndsInDirectorySeparator => new TheoryData<string, bool>
        {
            { @"\", true },
            { @"/", true },
            { @"C:\folder\", true },
            { @"C:/folder/", true },
            { @"C:\", true },
            { @"C:/", true },
            { @"\\", true },
            { @"//", true },
            { @"\\server\share\", true },
            { @"\\?\UNC\a\", true },
            { @"\\?\C:\", true },
            { @"\\?\UNC\", true },
            { @"folder\", true },
            { @"folder", false },
            { @"", false },
            { null, false }
        };

        [Theory,
            MemberData(nameof(TestData_TrimEndingDirectorySeparator))]
        public void TrimEndingDirectorySeparator_String(string path, string expected)
        {
            string trimmed = Path.TrimEndingDirectorySeparator(Platform.Windows, path);
            Assert.Equal(expected, trimmed);
            Assert.Same(trimmed, Path.TrimEndingDirectorySeparator(Platform.Windows, trimmed));
        }

        [Theory,
            MemberData(nameof(TestData_TrimEndingDirectorySeparator))]
        public void TrimEndingDirectorySeparator_ReadOnlySpan(string path, string expected)
        {
            ReadOnlySpan<char> trimmed = Path.TrimEndingDirectorySeparator(Platform.Windows, path.AsSpan());
            PathAssert.Equal(expected, trimmed);
            PathAssert.Equal(trimmed, Path.TrimEndingDirectorySeparator(Platform.Windows, trimmed));
        }

        [Theory,
            MemberData(nameof(TestData_EndsInDirectorySeparator))]
        public void EndsInDirectorySeparator_String(string path, bool expected)
        {
            Assert.Equal(expected, Path.EndsInDirectorySeparator(Platform.Windows, path));
        }

        [Theory,
            MemberData(nameof(TestData_EndsInDirectorySeparator))]
        public void EndsInDirectorySeparator_ReadOnlySpan(string path, bool expected)
        {
            Assert.Equal(expected, Path.EndsInDirectorySeparator(Platform.Windows, path.AsSpan()));
        }
    }
}
