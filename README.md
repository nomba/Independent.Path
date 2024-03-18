# <img src="docs/icon.svg" width="150">

`Independent.Path` extends `System.IO.Path` class to specify a platform for independent members.
For example, you **can run on any platform**:

```csharp
Path.Combine(Platform.Unix, "home", "user")
    
// Output: "home/user"
```

or 

```csharp
Path.Combine(Platform.Windows, "C:", "Users", "User")

// Output: "C:\\Users\\User"
```

In fact, it's original [dotnet/runtime](https://github.com/dotnet/runtime/tree/main/src/libraries/System.Private.CoreLib/src/System/IO) code that is copied and a bit modified to provides platform independent running. There is a structure below:

- [`System.IO.Independent.Windows`](src/Independent.Path/Windows) namespace contains the original code for Windows
- [`System.IO.Independent.Unix`](src/Independent.Path/Unix) namespace contains the original code for Unix
- [`System.IO.Independent.Path`](src/Independent.Path/Path.cs) class is a facade of `System.IO.Path` that enables choosing platform

Current upstream commit (`main`): [Fix System.Runtime.InteropServices.UnitTests on Windows set to UTF8 encoding (#99393)](https://github.com/dotnet/runtime/commit/49a8bb7eb31f2aea9c895f4e9f6c2f6567281bdd)

## Why?

The current `System.IO.Path` implementation is tightly coupled to the platform it's running on. For example, you can't combine path or get file name in Windows style on Unix runtime and vice versa. 
It's kinda a rare scenario but it can be useful:
- In distributed app for communication logic based on IPC (Inter-Process Communication) or RPC (Remote procedure call)
- In Shared Data architecture where there is one database for a few application. When a file must be shared between different platforms


## Extracted API list (included in [`System.IO.Independent.Path`](src/Independent.Path/Path.cs))

- DirectorySeparatorChar -> GetDirectorySeparatorChar()
- AltDirectorySeparatorChar -> GetAltDirectorySeparatorChar()
- VolumeSeparatorChar -> GetVolumeSeparatorChar()
- PathSeparator -> GetPathSeparator()
- ChangeExtension()
- GetDirectoryName()
- GetExtension()
- GetFileName()
- GetFileNameWithoutExtension()
- GetInvalidFileNameChars()
- GetInvalidPathChars()
- IsPathFullyQualified()
- IsPathRooted()
- GetPathRoot()
- HasExtension()
- Combine()
- Join()
- TryJoin()

## Platform-dependent API, that skipped (not extracted)

- Exists()
- GetFullPath()
- GetRandomFileName()
- GetRelativePath()
- GetTempFileName()
- GetTempPath()

## Naming

The package name `Independent.Path` originates from the class namespace `System.IO.Independent.Path`. It could have been named "as is," but the `System.IO` prefix is reserved by Microsoft. Using this prefix makes code navigation easier because it follows the familiar convention where file system code is located in `System.IO`. Therefore, the root namespace of the package is `System.IO.Independent` despite the package name.  

## Samples

### Windows format

```csharp
// Via the facade
System.IO.Independent.Path.Combine(Platform.Windows, "C:", "Users", "User")
// Output: "C:\\Users\\User"
    
// Directly using System.IO.Independent.Windows
System.IO.Independent.Windows.Path.Combine( "C:", "Users", "User");
// Output: "C:\\Users\\User"

// Original. Only on Windows runtime then result is the same
Path.Combine( "C:", "Users", "User");
// Output: "C:\\Users\\User"
```

### Unix format

```csharp
// Via the facade
System.IO.Independent.Path.Combine(Platform.Unix, "home", "user");
// Output: "home/user"

// Directly System.IO.Independent.Unix
System.IO.Independent.Unix.Path.Combine("home", "user");
// Output: "home/user"

// Original. Only on any Unix runtime then result is the same
Path.Combine("home", "user");
// Output: "home/user"
```

