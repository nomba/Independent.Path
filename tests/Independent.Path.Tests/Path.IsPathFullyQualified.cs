// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.IO.Independent.Tests
{
    public static class GetFullyQualifiedPathTests
    {
        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public static void IsPathFullyQualified_NullArgument(Platform platform)
        {
            Assert.Throws<ArgumentNullException>(() => Path.IsPathFullyQualified(platform, null!));
        }

        [Theory]
        [InlineData(Platform.Windows)]
        [InlineData(Platform.Unix)]
        public static void IsPathFullyQualified_Empty(Platform platform)
        {
            Assert.False(Path.IsPathFullyQualified(platform, ""));
            Assert.False(Path.IsPathFullyQualified(platform, ReadOnlySpan<char>.Empty));
        }

        [Theory]
        [InlineData("/")]
        [InlineData(@"\")]
        [InlineData(".")]
        [InlineData("C:")]
        [InlineData("C:foo.txt")]
        public static void IsPathFullyQualified_Windows_Invalid(string path)
        {
            Assert.False(Path.IsPathFullyQualified(Platform.Windows, path));
            Assert.False(Path.IsPathFullyQualified(Platform.Windows, path.AsSpan()));
        }
        
        [Theory]
        [InlineData(@"\\")]
        [InlineData(@"\\\")]
        [InlineData(@"\\Server")]
        [InlineData(@"\\Server\Foo.txt")]
        [InlineData(@"\\Server\Share\Foo.txt")]
        [InlineData(@"\\Server\Share\Test\Foo.txt")]
        [InlineData(@"C:\")]
        [InlineData(@"C:\foo1")]
        [InlineData(@"C:\\")]
        [InlineData(@"C:\\foo2")]
        [InlineData(@"C:/")]
        [InlineData(@"C:/foo1")]
        [InlineData(@"C://")]
        [InlineData(@"C://foo2")]
        public static void IsPathFullyQualified_Windows_Valid(string path)
        {
            Assert.True(Path.IsPathFullyQualified(Platform.Windows, path));
            Assert.True(Path.IsPathFullyQualified(Platform.Windows, path.AsSpan()));
        }
        
        [Theory]
        [InlineData(@"\")]
        [InlineData(@"\\")]
        [InlineData(".")]
        [InlineData("./foo.txt")]
        [InlineData("..")]
        [InlineData("../foo.txt")]
        [InlineData(@"C:")]
        [InlineData(@"C:/")]
        [InlineData(@"C://")]
        public static void IsPathFullyQualified_Unix_Invalid(string path)
        {
            Assert.False(Path.IsPathFullyQualified(Platform.Unix, path));
            Assert.False(Path.IsPathFullyQualified(Platform.Unix, path.AsSpan()));
        }
        
        [Theory]
        [InlineData("/")]
        [InlineData("/foo.txt")]
        [InlineData("/..")]
        [InlineData("//")]
        [InlineData("//foo.txt")]
        [InlineData("//..")]
        public static void IsPathFullyQualified_Unix_Valid(string path)
        {
            Assert.True(Path.IsPathFullyQualified(Platform.Unix, path));
            Assert.True(Path.IsPathFullyQualified(Platform.Unix, path.AsSpan()));
        }
    }
}