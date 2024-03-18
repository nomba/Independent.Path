// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public class PathTestsBase
    {
        protected static string Sep(Platform platform) => Path.GetDirectorySeparatorChar(platform).ToString();
        protected static string AltSep(Platform platform) => Path.GetAltDirectorySeparatorChar(platform).ToString();
        
        public static TheoryData<Platform, string> Get_TestData_EmbeddedNull(Platform platform) => new()
        {
            {platform, "a\0b"}
        };

        public static TheoryData<Platform, string> Get_TestData_EmptyString(Platform platform) => new()
        {
            {platform, ""}
        };

        public static TheoryData<Platform, string> Get_TestData_ControlChars(Platform platform) => new()
        {
            {platform, "\t"},
            {platform, "\r\n"},
            {platform, "\b"},
            {platform, "\v"},
            {platform, "\n"}
        };

        public static TheoryData<Platform, string> Get_TestData_Spaces(Platform platform) => new()
        {
            {platform, " "},
            {platform, "   "}
        };

        public static TheoryData<Platform, string> Get_TestData_UnicodeWhiteSpace(Platform platform) => new()
        {
            {platform, "\u00A0"}, // Non-breaking Space
            {platform, "\u2028"}, // Line separator
            {platform, "\u2029"}, // Paragraph separator
        };

        public static TheoryData<Platform, string> Get_TestData_InvalidDriveLetters(Platform platform) => new()
        {
            { platform, @"@:\foo" },  // 064 = @     065 = A
            { platform, @"[:\\" },    // 091 = [     090 = Z
            { platform, @"`:\foo "},  // 096 = `     097 = a
            { platform, @"{:\\" },    // 123 = {     122 = z
            { platform, @"@:/foo" },
            { platform, @"[://" },
            { platform, @"`:/foo "},
            { platform, @"{:/" },
            { platform, @"]:" }
        };

        public static TheoryData<Platform, string, string> Get_TestData_GetDirectoryName(Platform platform) => new()
        {
            {platform, ".", ""},
            {platform, "..", ""},
            {platform, "baz", ""},
            {platform, Path.Combine(platform, "dir", "baz"), "dir"},
            {platform, "dir.foo" + Path.GetAltDirectorySeparatorChar(platform) + "baz.txt", "dir.foo"},
            {platform, Path.Combine(platform, "dir", "baz", "bar"), Path.Combine(platform, "dir", "baz")},
            {platform, Path.Combine(platform, "..", "..", "files.txt"), Path.Combine(platform, "..", "..")},
            {platform, Path.GetDirectorySeparatorChar(platform) + "foo", Path.GetDirectorySeparatorChar(platform).ToString()},
            {platform, Path.GetDirectorySeparatorChar(platform).ToString(), null}
        };

        public static TheoryData<string, string> TestData_GetDirectoryName_Windows => new TheoryData<string, string>
        {
            { @"C:\", null },
            { @"C:/", null },
            { @"C:", null },
            { @"dir\\baz", "dir" },
            { @"dir//baz", "dir" },
            { @"C:\foo", @"C:\" },
            { @"C:foo", "C:" }
        };

        public static TheoryData<Platform, string, string> Get_TestData_GetExtension(Platform platform) => new()
        {
            {platform, @"file.exe", ".exe"},
            {platform, @"file", ""},
            {platform, @"file.", ""},
            {platform, @"file.s", ".s"},
            {platform, @"test/file", ""},
            {platform, @"test/file.extension", ".extension"},
            {platform, @"test\file", ""},
            {platform, @"test\file.extension", ".extension"},
            {platform, "file.e xe", ".e xe"},
            {platform, "file. ", ". "},
            {platform, " file. ", ". "},
            {platform, " file.extension", ".extension"}
        };

        public static TheoryData<Platform, string, string> Get_TestData_GetFileName(Platform platform) => new()
        {
            {platform, ".", "." },
            {platform, "..", ".." },
            {platform, "file", "file" },
            {platform, "file.", "file." },
            {platform, "file.exe", "file.exe" },
            {platform, " . ", " . " },
            {platform, " .. ", " .. " },
            {platform, "fi le", "fi le" },
            {platform, Path.Combine(platform, "baz", "file.exe"), "file.exe" },
            {platform, Path.Combine(platform, "baz", "file.exe") + Path.GetAltDirectorySeparatorChar(platform), "" },
            {platform, Path.Combine(platform, "bar", "baz", "file.exe"), "file.exe" },
            {platform, Path.Combine(platform, "bar", "baz", "file.exe") + Path.GetDirectorySeparatorChar(platform), "" }
        };

        public static TheoryData<Platform, string, string> Get_TestData_GetFileNameWithoutExtension(Platform platform) => new()
        {
            {platform, "", ""},
            {platform, "file", "file"},
            {platform, "file.exe", "file"},
            {platform, Path.Combine(platform, "bar", "baz", "file.exe"), "file"},
            {platform, Path.Combine(platform, "bar", "baz") + Path.GetDirectorySeparatorChar(platform), ""}
        };

        public static TheoryData<string, string> TestData_GetPathRoot_Unc => new TheoryData<string, string>
        {
            { @"\\test\unc\path\to\something", @"\\test\unc" },
            { @"\\a\b\c\d\e", @"\\a\b" },
            { @"\\a\b\", @"\\a\b" },
            { @"\\a\b", @"\\a\b" },
            { @"\\test\unc", @"\\test\unc" },
        };

        // TODO: Include \\.\ as well
        public static TheoryData<string, string> TestData_GetPathRoot_DevicePaths => new TheoryData<string, string>
        {
            { @"\\?\UNC\test\unc\path\to\something", PathFeatures.IsUsingLegacyPathNormalization() ? @"\\?\UNC" : @"\\?\UNC\test\unc" },
            { @"\\?\UNC\test\unc", PathFeatures.IsUsingLegacyPathNormalization() ? @"\\?\UNC" : @"\\?\UNC\test\unc" },
            { @"\\?\UNC\a\b1", PathFeatures.IsUsingLegacyPathNormalization() ? @"\\?\UNC" : @"\\?\UNC\a\b1" },
            { @"\\?\UNC\a\b2\", PathFeatures.IsUsingLegacyPathNormalization() ? @"\\?\UNC" : @"\\?\UNC\a\b2" },
            { @"\\?\C:\foo\bar.txt", PathFeatures.IsUsingLegacyPathNormalization() ? @"\\?\C:" : @"\\?\C:\" }
        };

        public static TheoryData<string, string> TestData_GetPathRoot_Windows => new TheoryData<string, string>
        {
            { @"C:", @"C:" },
            { @"C:\", @"C:\" },
            { @"C:\\", @"C:\" },
            { @"C:\foo1", @"C:\" },
            { @"C:\\foo2", @"C:\" },
        };

        protected static class PathAssert
        {
            public static void Equal(ReadOnlySpan<char> expected, ReadOnlySpan<char> actual)
            {
                if (!actual.SequenceEqual(expected))
                    throw new Xunit.Sdk.EqualException(new string(expected), new string(actual));
            }
        
            public static void Empty(ReadOnlySpan<char> actual)
            {
                if (actual.Length > 0)
                    throw new Xunit.Sdk.NotEmptyException();
            }
        }
    }
}
