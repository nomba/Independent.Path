// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public class PathTests_Unix : PathTestsBase
    {
        [Theory,
            MemberData(nameof(TestData_GetPathRoot_Unc)),
            MemberData(nameof(TestData_GetPathRoot_DevicePaths))]
        public static void GetPathRoot(string value, string expected)
        {
            // UNCs and device paths have no special meaning in Unix
            _ = expected;
            Assert.Empty(Path.GetPathRoot(Platform.Unix, value));
            Assert.True(Path.GetPathRoot(Platform.Unix, value.AsSpan()).IsEmpty);
        }

        [Theory,
            InlineData("B:", "B:"),
            InlineData("A:.", "A:.")]
        public void GetFileName_Volume(string path, string expected)
        {
            // No such thing as a drive relative path on Unix.
            Assert.Equal(expected, Path.GetFileName(Platform.Unix, path));
            Assert.Equal(expected, new string(Path.GetFileName(Platform.Unix, path.AsSpan())));
        }
        
        [Theory]
        [InlineData(@"/../../.././tmp/..")]
        [InlineData(@"/../../../")]
        [InlineData(@"/../../../tmp/bar/..")]
        [InlineData(@"/../.././././bar/../../../")]
        [InlineData(@"/../../././tmp/..")]
        [InlineData(@"/../../tmp/../../")]
        [InlineData(@"/../../tmp/bar/..")]
        [InlineData(@"/../tmp/../..")]
        [InlineData(@"/././../../../../")]
        [InlineData(@"/././../../../")]
        [InlineData(@"/./././bar/../../../")]
        [InlineData(@"/")]
        [InlineData(@"/bar")]
        [InlineData(@"/bar/././././../../..")]
        [InlineData(@"/bar/tmp")]
        [InlineData(@"/tmp/..")]
        [InlineData(@"/tmp/../../../../../bar")]
        [InlineData(@"/tmp/../../../bar")]
        [InlineData(@"/tmp/../bar/../..")]
        [InlineData(@"/tmp/bar")]
        [InlineData(@"/tmp/bar/..")]
        public static void GePathRoot_Unix(string path)
        {
            string expected = @"/";
            Assert.Equal(expected, Path.GetPathRoot(Platform.Unix, path));
            PathAssert.Equal(expected.AsSpan(), Path.GetPathRoot(Platform.Unix, path.AsSpan()));
        }
        
        public static TheoryData<string, string> TestData_TrimEndingDirectorySeparator => new TheoryData<string, string>
        {
            { @"/folder/", @"/folder" },
            { @"folder/", @"folder" },
            { @"", @"" },
            { @"/", @"/" },
            { null, null }
        };

        public static TheoryData<string, bool> TestData_EndsInDirectorySeparator => new TheoryData<string, bool>
        {
            { @"/", true },
            { @"/folder/", true },
            { @"//", true },
            { @"folder", false },
            { @"folder/", true },
            { @"", false },
            { null, false }
        };

        [Theory,
            MemberData(nameof(TestData_TrimEndingDirectorySeparator))]
        public void TrimEndingDirectorySeparator_String(string path, string expected)
        {
            string trimmed = Path.TrimEndingDirectorySeparator(Platform.Unix, path);
            Assert.Equal(expected, trimmed);
            Assert.Same(trimmed, Path.TrimEndingDirectorySeparator(Platform.Unix, trimmed));
        }

        [Theory,
            MemberData(nameof(TestData_TrimEndingDirectorySeparator))]
        public void TrimEndingDirectorySeparator_ReadOnlySpan(string path, string expected)
        {
            ReadOnlySpan<char> trimmed = Path.TrimEndingDirectorySeparator(Platform.Unix, path.AsSpan());
            PathAssert.Equal(expected, trimmed);
            PathAssert.Equal(trimmed, Path.TrimEndingDirectorySeparator(Platform.Unix, trimmed));
        }

        [Theory,
            MemberData(nameof(TestData_EndsInDirectorySeparator))]
        public void EndsInDirectorySeparator_String(string path, bool expected)
        {
            Assert.Equal(expected, Path.EndsInDirectorySeparator(Platform.Unix, path));
        }

        [Theory,
            MemberData(nameof(TestData_EndsInDirectorySeparator))]
        public void EndsInDirectorySeparator_ReadOnlySpan(string path, bool expected)
        {
            Assert.Equal(expected, Path.EndsInDirectorySeparator(Platform.Unix, path.AsSpan()));
        }
    }
}
