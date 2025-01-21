# SDRSharp.RemoteControl

This is a tool created to help different projects that are trying to integrate with [SDRSharp](https://airspy.com/download/)

At the time, there's no released version of the plugin, although you are welcome to clone the repo \
and compile the project, to use it with your build of SDR#.

- Current supported SDR# version: **v1.0.0.1921**
- .NET version: **net9.0-windows7.0**

# Build and Installation
If these badges show that the build and testing are passing,\
then you can follow next instructions...

![Build workflow](https://github.com/ZecosMAX/SDRSharp.RemoteControl/actions/workflows/dotnet.yml/badge.svg)
![Test workflow](https://github.com/ZecosMAX/SDRSharp.RemoteControl/actions/workflows/dotnet.yml/badge.svg)

---
- Make sure you have latest [.Net 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) version installed
- Also make sure that SDRSharp is closed
- Clone the repo via the most convenient method you have \
  Or use the following command inside the folder you want to place the sources:
  
	```
	git clone https://github.com/ZecosMAX/SDRSharp.RemoteControl.git
	```

- Enter the cloned folder
	```
	cd SDRSharp.RemoteControl
	```

- Build the project with `dotnet` command:
	```
	dotnet build -o bin\		
	```
- If the build succeeds, then copy only the .dll file to your SDR# installation folder:
	```
	robocopy bin\ {SDRSharpPath}\Plugins\Remote Control\ *.dll /s /e
	```
- Launch the SDRSharp and be happy :>
---

If by any reason the current build fails and/or you can't compile the project,\
grab latest [release](https://github.com/ZecosMAX/SDRSharp.RemoteControl/releases) and copy files to your SDR# installation, into the `plugins` folder

# Usage

TBA...