// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public class PathTests_Join : PathTestsBase
    {
        public static TheoryData<Platform, string, string> Get_TestData_JoinOnePath(Platform platform)
        {
            var Sep = PathTestsBase.Sep(platform);
            var AltSep = PathTestsBase.AltSep(platform);

            return new TheoryData<Platform, string, string>
            {
                {platform, "", ""},
                {platform, Sep, Sep},
                {platform, AltSep, AltSep},
                {platform, "a", "a"},
                {platform, null, ""}
            };
        }

        public static TheoryData<Platform, string, string, string> Get_TestData_JoinTwoPaths(Platform platform)
        {
            var Sep = PathTestsBase.Sep(platform);
            var AltSep = PathTestsBase.AltSep(platform);

            return new TheoryData<Platform, string, string, string>
            {
                {platform, "", "", ""},
                {platform, Sep, "", Sep},
                {platform, AltSep, "", AltSep},
                {platform, "", Sep, Sep},
                {platform, "", AltSep, AltSep},
                {platform, Sep, Sep, $"{Sep}{Sep}"},
                {platform, AltSep, AltSep, $"{AltSep}{AltSep}"},
                {platform, "a", "", "a"},
                {platform, "", "a", "a"},
                {platform, "a", "a", $"a{Sep}a"},
                {platform, $"a{Sep}", "a", $"a{Sep}a"},
                {platform, "a", $"{Sep}a", $"a{Sep}a"},
                {platform, $"a{Sep}", $"{Sep}a", $"a{Sep}{Sep}a"},
                {platform, "a", $"a{Sep}", $"a{Sep}a{Sep}"},
                {platform, $"a{AltSep}", "a", $"a{AltSep}a"},
                {platform, "a", $"{AltSep}a", $"a{AltSep}a"},
                {platform, $"a{Sep}", $"{AltSep}a", $"a{Sep}{AltSep}a"},
                {platform, $"a{AltSep}", $"{AltSep}a", $"a{AltSep}{AltSep}a"},
                {platform, "a", $"a{AltSep}", $"a{Sep}a{AltSep}"},
                {platform, null, null, ""},
                {platform, null, "a", "a"},
                {platform, "a", null, "a"}
            };
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinTwoPaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinTwoPaths), Platform.Unix)]
        public void JoinTwoPaths(Platform platform, string path1, string path2, string expected)
        {
            Assert.Equal(expected, Path.Join(platform, path1.AsSpan(), path2.AsSpan()));
            Assert.Equal(expected, Path.Join(platform, path1, path2));
        }

        [Theory] 
        [MemberData(nameof(Get_TestData_JoinTwoPaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinTwoPaths), Platform.Unix)]
        public void TryJoinTwoPaths(Platform platform, string path1, string path2, string expected)
        {
            char[] output = new char[expected.Length];

            Assert.True(Path.TryJoin(platform, path1, path2, output, out int written));
            Assert.Equal(expected.Length, written);
            Assert.Equal(expected, new string(output));

            if (expected.Length > 0)
            {
                Assert.False(Path.TryJoin(platform, path1, path2, Span<char>.Empty, out written));
                Assert.Equal(0, written);

                output = new char[expected.Length - 1];
                Assert.False(Path.TryJoin(platform, path1, path2, output, out written));
                Assert.Equal(0, written);
                Assert.Equal(output, new char[output.Length]);
            }
        }

        public static TheoryData<Platform, string, string, string, string> Get_TestData_JoinThreePaths(Platform platform)
        {
            var Sep = PathTestsBase.Sep(platform);
            var AltSep = PathTestsBase.AltSep(platform);

            return new TheoryData<Platform, string, string, string, string>
            {
                {platform, "", "", "", ""},
                {platform, Sep, Sep, Sep, $"{Sep}{Sep}{Sep}"},
                {platform, AltSep, AltSep, AltSep, $"{AltSep}{AltSep}{AltSep}"},
                {platform, "a", "", "", "a"},
                {platform, "", "a", "", "a"},
                {platform, "", "", "a", "a"},
                {platform, "a", "", "a", $"a{Sep}a"},
                {platform, "a", "a", "", $"a{Sep}a"},
                {platform, "", "a", "a", $"a{Sep}a"},
                {platform, "a", "a", "a", $"a{Sep}a{Sep}a"},
                {platform, "a", Sep, "a", $"a{Sep}a"},
                {platform, $"a{Sep}", "", "a", $"a{Sep}a"},
                {platform, $"a{Sep}", "a", "", $"a{Sep}a"},
                {platform, "", $"a{Sep}", "a", $"a{Sep}a"},
                {platform, "a", "", $"{Sep}a", $"a{Sep}a"},
                {platform, $"a{AltSep}", "", "a", $"a{AltSep}a"},
                {platform, $"a{AltSep}", "a", "", $"a{AltSep}a"},
                {platform, "", $"a{AltSep}", "a", $"a{AltSep}a"},
                {platform, "a", "", $"{AltSep}a", $"a{AltSep}a"},
                {platform, null, null, null, ""},
                {platform, "a", null, null, "a"},
                {platform, null, "a", null, "a"},
                {platform, null, null, "a", "a"},
                {platform, "a", null, "a", $"a{Sep}a"}
            };
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinThreePaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinThreePaths), Platform.Unix)]
        public void JoinThreePaths(Platform platform, string path1, string path2, string path3, string expected)
        {
            // TODO: Implement
            // Assert.Equal(expected, Path.Join(platform, path1.AsSpan(), path2.AsSpan(), path3.AsSpan()));
            Assert.Equal(expected, Path.Join(platform, path1, path2, path3));
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinThreePaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinThreePaths), Platform.Unix)]
        public void TryJoinThreePaths(Platform platform, string path1, string path2, string path3, string expected)
        {
            char[] output = new char[expected.Length];

            Assert.True(Path.TryJoin(platform, path1, path2, path3, output, out int written));
            Assert.Equal(expected.Length, written);
            Assert.Equal(expected, new string(output));

            if (expected.Length > 0)
            {
                Assert.False(Path.TryJoin(platform, path1, path2, path3, Span<char>.Empty, out written));
                Assert.Equal(0, written);

                output = new char[expected.Length - 1];
                Assert.False(Path.TryJoin(platform, path1, path2, path3, output, out written));
                Assert.Equal(0, written);
                Assert.Equal(output, new char[output.Length]);
            }
        }

        public static TheoryData<Platform, string, string, string, string, string> Get_TestData_JoinFourPaths(Platform platform)
        {
            var Sep = PathTestsBase.Sep(platform);
            var AltSep = PathTestsBase.AltSep(platform);

            return new TheoryData<Platform, string, string, string, string, string>
            {
                {platform, "", "", "", "", ""},
                {platform, Sep, Sep, Sep, Sep, $"{Sep}{Sep}{Sep}{Sep}"},
                {platform, AltSep, AltSep, AltSep, AltSep, $"{AltSep}{AltSep}{AltSep}{AltSep}"},
                {platform, "a", "", "", "", "a"},
                {platform, "", "a", "", "", "a"},
                {platform, "", "", "a", "", "a"},
                {platform, "", "", "", "a", "a"},
                {platform, "a", "b", "", "", $"a{Sep}b"},
                {platform, "a", "", "b", "", $"a{Sep}b"},
                {platform, "a", "", "", "b", $"a{Sep}b"},
                {platform, "a", "b", "c", "", $"a{Sep}b{Sep}c"},
                {platform, "a", "b", "", "c", $"a{Sep}b{Sep}c"},
                {platform, "a", "", "b", "c", $"a{Sep}b{Sep}c"},
                {platform, "", "a", "b", "c", $"a{Sep}b{Sep}c"},
                {platform, "a", "b", "c", "d", $"a{Sep}b{Sep}c{Sep}d"},
                {platform, "a", Sep, "b", "", $"a{Sep}b"},
                {platform, "a", Sep, "", "b", $"a{Sep}b"},
                {platform, "a", "", Sep, "b", $"a{Sep}b"},
                {platform, $"a{Sep}", "b", "", "", $"a{Sep}b"},
                {platform, $"a{Sep}", "", "b", "", $"a{Sep}b"},
                {platform, $"a{Sep}", "", "", "b", $"a{Sep}b"},
                {platform, "", $"a{Sep}", "b", "", $"a{Sep}b"},
                {platform, "", $"a{Sep}", "", "b", $"a{Sep}b"},
                {platform, "", "", $"a{Sep}", "b", $"a{Sep}b"},
                {platform, "a", $"{Sep}b", "", "", $"a{Sep}b"},
                {platform, "a", "", $"{Sep}b", "", $"a{Sep}b"},
                {platform, "a", "", "", $"{Sep}b", $"a{Sep}b"},
                {platform, $"{Sep}a", "", "", "", $"{Sep}a"},
                {platform, "", $"{Sep}a", "", "", $"{Sep}a"},
                {platform, "", "", $"{Sep}a", "", $"{Sep}a"},
                {platform, "", "", "", $"{Sep}a", $"{Sep}a"},
                {platform, $"{Sep}a", "b", "", "", $"{Sep}a{Sep}b"},
                {platform, "", $"{Sep}a", "b", "", $"{Sep}a{Sep}b"},
                {platform, "", "", $"{Sep}a", "b", $"{Sep}a{Sep}b"},
                {platform, $"a{Sep}", $"{Sep}b", "", "", $"a{Sep}{Sep}b"},
                {platform, $"a{Sep}", "", $"{Sep}b", "", $"a{Sep}{Sep}b"},
                {platform, $"a{Sep}", "", "", $"{Sep}b", $"a{Sep}{Sep}b"},
                {platform, $"a{AltSep}", "b", "", "", $"a{AltSep}b"},
                {platform, $"a{AltSep}", "", "b", "", $"a{AltSep}b"},
                {platform, $"a{AltSep}", "", "", "b", $"a{AltSep}b"},
                {platform, "", $"a{AltSep}", "b", "", $"a{AltSep}b"},
                {platform, "", $"a{AltSep}", "", "b", $"a{AltSep}b"},
                {platform, "", "", $"a{AltSep}", "b", $"a{AltSep}b"},
                {platform, "a", $"{AltSep}b", "", "", $"a{AltSep}b"},
                {platform, "a", "", $"{AltSep}b", "", $"a{AltSep}b"},
                {platform, "a", "", "", $"{AltSep}b", $"a{AltSep}b"},
                {platform, null, null, null, null, ""},
                {platform, "a", null, null, null, "a"},
                {platform, null, "a", null, null, "a"},
                {platform, null, null, "a", null, "a"},
                {platform, null, null, null, "a", "a"},
                {platform, "a", null, "b", null, $"a{Sep}b"},
                {platform, "a", null, null, "b", $"a{Sep}b"}
            };
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinFourPaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinFourPaths), Platform.Unix)]
        public void JoinFourPaths(Platform platform, string path1, string path2, string path3, string path4, string expected)
        {
            Assert.Equal(expected, Path.Join(platform, path1, path2, path3, path4));
            // TODO: Implement
            // Assert.Equal(expected, Path.Join(platform, path1.AsSpan(), path2.AsSpan(), path3.AsSpan(), path4.AsSpan()));
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public void JoinStringArray_ThrowsArgumentNullException(Platform platform)
        {
            Assert.Throws<ArgumentNullException>(() => Path.Join(platform, null));
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public void JoinStringArray_ZeroLengthArray(Platform platform)
        {
            Assert.Equal(string.Empty, Path.Join(platform, new string[0]));
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinOnePath), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinOnePath), Platform.Unix)]
        public void JoinStringArray_1(Platform platform, string path1, string expected)
        {
            Assert.Equal(expected, Path.Join(platform, new string[] {path1}));
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinTwoPaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinTwoPaths), Platform.Unix)]
        public void JoinStringArray_2(Platform platform, string path1, string path2, string expected)
        {
            Assert.Equal(expected, Path.Join(platform, new string[] {path1, path2}));
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinThreePaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinThreePaths), Platform.Unix)]
        public void JoinStringArray_3(Platform platform, string path1, string path2, string path3, string expected)
        {
            Assert.Equal(expected, Path.Join(platform, new string[] {path1, path2, path3}));
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinFourPaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinFourPaths), Platform.Unix)]
        public void JoinStringArray_4(Platform platform, string path1, string path2, string path3, string path4, string expected)
        {
            Assert.Equal(expected, Path.Join(platform, new string[] {path1, path2, path3, path4}));
        }

        [Theory]
        [MemberData(nameof(Get_TestData_JoinFourPaths), Platform.Windows)]
        [MemberData(nameof(Get_TestData_JoinFourPaths), Platform.Unix)]
        public void JoinStringArray_8(Platform platform, string path1, string path2, string path3, string path4, string fourJoined)
        {
            Assert.Equal(Path.Join(platform, fourJoined, fourJoined), Path.Join(platform, new string[] {path1, path2, path3, path4, path1, path2, path3, path4}));
        }
    }
}