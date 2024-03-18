// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public class PathTests_Combine
    {
        public static IEnumerable<object[]> Combine_Basic_TestData(Platform platform)
        {
            var s_separator = Path.GetDirectorySeparatorChar(platform);
            
            yield return new object[] { platform, new string[0] };
            yield return new object[] { platform, new string[] { "abc" } };
            yield return new object[] { platform, new string[] { "abc", "def" } };
            yield return new object[] { platform, new string[] { "abc", "def", "ghi", "jkl", "mno" } };
            yield return new object[] { platform, new string[] { "abc" + s_separator + "def", "def", "ghi", "jkl", "mno" } };

            // All paths are empty
            yield return new object[] { platform, new string[] { "" } };
            yield return new object[] { platform, new string[] { "", "" } };
            yield return new object[] { platform, new string[] { "", "", "" } };
            yield return new object[] { platform, new string[] { "", "", "", "" } };
            yield return new object[] { platform, new string[] { "", "", "", "", "" } };

            // Elements are all separated
            yield return new object[] { platform, new string[] { "abc" + s_separator, "def" + s_separator } };
            yield return new object[] { platform, new string[] { "abc" + s_separator, "def" + s_separator, "ghi" + s_separator } };
            yield return new object[] { platform, new string[] { "abc" + s_separator, "def" + s_separator, "ghi" + s_separator, "jkl" + s_separator } };
            yield return new object[] { platform, new string[] { "abc" + s_separator, "def" + s_separator, "ghi" + s_separator, "jkl" + s_separator, "mno" + s_separator } };
        }

        public static IEnumerable<string> Combine_CommonCases_Input_TestData(Platform platform)
        {
            var s_separator = Path.GetDirectorySeparatorChar(platform);
            
            // Any path is rooted (starts with \, \\, A:)
            yield return s_separator + "abc";
            yield return s_separator + s_separator + "abc";

            // Any path is empty (skipped)
            yield return "";

            // Any path is single element
            yield return "abc";
            yield return "abc" + s_separator;

            // Any path is multiple element
            yield return Path.Combine(platform, "abc", Path.Combine(platform, "def", "ghi"));

            // Wildcard characters
            yield return "*";
            yield return "?";

            // Obscure wildcard characters
            yield return "\"";
            yield return "<";
            yield return ">";
        }

        public static IEnumerable<object[]> Combine_CommonCases_TestData(Platform platform)
        {
            foreach (string testPath in Combine_CommonCases_Input_TestData(platform))
            {
                yield return new object[] { platform, new string[] { testPath } };

                yield return new object[] { platform, new string[] { "abc", testPath } };
                yield return new object[] { platform, new string[] { testPath, "abc" } };

                yield return new object[] { platform, new string[] { "abc", "def", testPath } };
                yield return new object[] { platform, new string[] { "abc", testPath, "def" } };
                yield return new object[] { platform, new string[] { testPath, "abc", "def" } };

                yield return new object[] { platform, new string[] { "abc", "def", "ghi", testPath } };
                yield return new object[] { platform, new string[] { "abc", "def", testPath, "ghi" } };
                yield return new object[] { platform, new string[] { "abc", testPath, "def", "ghi" } };
                yield return new object[] { platform, new string[] { testPath, "abc", "def", "ghi" } };

                yield return new object[] { platform, new string[] { "abc", "def", "ghi", "jkl", testPath } };
                yield return new object[] { platform, new string[] { "abc", "def", "ghi", testPath, "jkl" } };
                yield return new object[] { platform, new string[] { "abc", "def", testPath, "ghi", "jkl" } };
                yield return new object[] { platform, new string[] { "abc", testPath, "def", "ghi", "jkl" } };
                yield return new object[] { platform, new string[] { testPath, "abc", "def", "ghi", "jkl" } };
            }
        }

        [Theory]
        [MemberData(nameof(Combine_Basic_TestData), Platform.Windows)]
        [MemberData(nameof(Combine_CommonCases_TestData), Platform.Windows)]
        [MemberData(nameof(Combine_Basic_TestData), Platform.Unix)]
        [MemberData(nameof(Combine_CommonCases_TestData), Platform.Unix)]
        public static void Combine(Platform platform, string[] paths)
        {
            string expected = string.Empty;
            if (paths.Length > 0) expected = paths[0];
            for (int i = 1; i < paths.Length; i++)
            {
                expected = Path.Combine(platform, expected, paths[i]);
            }

            // Combine(string[])
            Assert.Equal(expected, Path.Combine(platform, paths));

            // Verify special cases
            switch (paths.Length)
            {
                case 2:
                    // Combine(string, string)
                    Assert.Equal(expected, Path.Combine(platform, paths[0], paths[1]));
                    break;

                case 3:
                    // Combine(string, string, string)
                    Assert.Equal(expected, Path.Combine(platform, paths[0], paths[1], paths[2]));
                    break;

                case 4:
                    // Combine(string, string, string, string)
                    Assert.Equal(expected, Path.Combine(platform, paths[0], paths[1], paths[2], paths[3]));
                    break;
            }
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public static void PathIsNull(Platform platform)
        {
            VerifyException<ArgumentNullException>(platform, null);
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public static void PathIsNullWithoutRooted(Platform platform)
        {
            //any path is null without rooted after (ANE)
            CommonCasesException<ArgumentNullException>(platform, null);
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public static void ContainsInvalidCharWithoutRooted_Core(Platform platform)
        {
            Assert.Equal("ab\0cd", Path.Combine(platform, "ab\0cd"));
        }

        [Fact]
        public static void ContainsInvalidCharWithoutRooted_Windows_Core()
        {
            Assert.Equal("ab|cd", Path.Combine(Platform.Windows, "ab|cd"));
            Assert.Equal("ab\bcd", Path.Combine(Platform.Windows, "ab\bcd"));
            Assert.Equal("ab\0cd", Path.Combine(Platform.Windows, "ab\0cd"));
            Assert.Equal("ab\tcd", Path.Combine(Platform.Windows, "ab\tcd"));
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public static void ContainsInvalidCharWithRooted_Core(Platform platform)
        {
            var s_separator = Path.GetDirectorySeparatorChar(platform);
            Assert.Equal(s_separator + "abc", Path.Combine(platform, "ab\0cd", s_separator + "abc"));
        }

        [Fact]
        public static void ContainsInvalidCharWithRooted_Windows_core()
        {
            var s_separator = Path.GetDirectorySeparatorChar(Platform.Windows);
            Assert.Equal(s_separator + "abc", Path.Combine(Platform.Windows, "ab|cd", s_separator + "abc"));
            Assert.Equal(s_separator + "abc", Path.Combine(Platform.Windows, "ab\bcd", s_separator + "abc"));
            Assert.Equal(s_separator + "abc", Path.Combine(Platform.Windows, "ab\tcd", s_separator + "abc"));
        }

        private static void VerifyException<T>(Platform platform, string[] paths) where T : Exception
        {
            Assert.Throws<T>(() => Path.Combine(platform, paths));

            //verify passed as elements case
            if (paths != null)
            {
                Assert.InRange(paths.Length, 1, 5);

                Assert.Throws<T>(() =>
                {
                    switch (paths.Length)
                    {
                        case 0:
                            Path.Combine(platform);
                            break;
                        case 1:
                            Path.Combine(platform, paths[0]);
                            break;
                        case 2:
                            Path.Combine(platform, paths[0], paths[1]);
                            break;
                        case 3:
                            Path.Combine(platform, paths[0], paths[1], paths[2]);
                            break;
                        case 4:
                            Path.Combine(platform, paths[0], paths[1], paths[2], paths[3]);
                            break;
                        case 5:
                            Path.Combine(platform, paths[0], paths[1], paths[2], paths[3], paths[4]);
                            break;
                    }
                });
            }
        }

        private static void CommonCasesException<T>(Platform platform, string testing) where T : Exception
        {
            VerifyException<T>(platform, new string[] { testing });

            VerifyException<T>(platform, new string[] { "abc", testing });
            VerifyException<T>(platform, new string[] { testing, "abc" });

            VerifyException<T>(platform, new string[] { "abc", "def", testing });
            VerifyException<T>(platform, new string[] { "abc", testing, "def" });
            VerifyException<T>(platform, new string[] { testing, "abc", "def" });

            VerifyException<T>(platform, new string[] { "abc", "def", "ghi", testing });
            VerifyException<T>(platform, new string[] { "abc", "def", testing, "ghi" });
            VerifyException<T>(platform, new string[] { "abc", testing, "def", "ghi" });
            VerifyException<T>(platform, new string[] { testing, "abc", "def", "ghi" });

            VerifyException<T>(platform, new string[] { "abc", "def", "ghi", "jkl", testing });
            VerifyException<T>(platform, new string[] { "abc", "def", "ghi", testing, "jkl" });
            VerifyException<T>(platform, new string[] { "abc", "def", testing, "ghi", "jkl" });
            VerifyException<T>(platform, new string[] { "abc", testing, "def", "ghi", "jkl" });
            VerifyException<T>(platform, new string[] { testing, "abc", "def", "ghi", "jkl" });
        }
    }
}
