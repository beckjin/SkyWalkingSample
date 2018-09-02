setlocal

cd /d %~dp0

set TOOLS_PATH=.\Grpc.Tools.1.14.2\tools\windows_x64

%TOOLS_PATH%\protoc.exe -I./protos --csharp_out . ./protos/userService.proto --grpc_out . --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe

endlocal
