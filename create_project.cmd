echo off
IF [%1]==[]  goto missing-params
IF [%2]==[]  goto missing-params
mkdir %1
cd %1
dotnet new sln
dotnet new console --name %1
dotnet new xunit -o %1Tests
cd %1
dotnet add package AWSSDK.Core
dotnet add package AWSSDK.%2
dotnet add package StyleCop.Analyzers
cd ../%1Tests
dotnet add package AWSSDK.Core
dotnet add package AWSSDK.%2
dotnet add package moq
cd ..
dotnet add ./%1Tests/%1Tests.csproj reference ./%1/%1.csproj
dotnet sln add %1
dotnet sln add %1Tests
echo Done creating solution for %1
exit /b

:missing-params
echo Usage:
echo   c:/tools/create_project ExampleName ServicePackageName
exit /b


