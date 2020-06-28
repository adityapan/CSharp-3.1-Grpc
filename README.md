# CSharp-3.1-Grpc
Somethings to note when working with gRPC->
1. when adding protobuf file, need to make sure client and server are in sync, but server must have server only and client must have client only
    also make sure the protobuf files are chosen to be compiled
2. need to make sure server csproj file has the services mentioned otherwise the service.base generated file WILL NOT appear
3. in startup.cs and add the service for the server in the grpc pipeline
4. install the netcore packages, and also the tools for grpc. Incase you need to specify the msbuild during nuget restore, the following command may help->
    nuget restore -MSBuildVersion 16.6 
    where the vs2019 install path is D:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin (contains MsBuild.exe, check the version from details and run)
