
[![Build Status](https://ci.appveyor.com/api/projects/status/github/FlukeFan/Generazor?svg=true)](https://ci.appveyor.com/project/FlukeFan/Generazor) <pre>

Generazor
=========

Template based code generation using the Razor SDK.


Building
========

Pre-requisites
--------------

* .NET SDK specified in global.json.

To build:

1. Open CommandPrompt.bat as administrator;
3. Type 'br' (restores NuGet packages);
4. Type 'b' to build.

Build commands
--------------

br                                      Restore dependencies (execute this first)
b                                       Dev-build
ba                                      Build all (including slow tests and coverage)
bw                                      Watch dev-build
bt [test]                               Run tests with filter Name~[test]
btw [test]                              Watch run tests with filter Name~[test]
bc                                      Clean the build outputs
