@ECHO OFF

..\..\_Utils\protogen\protogen.exe --proto_path=..  --csharp_out=..\Templates\ **\*.proto

IF ERRORLEVEL 1 (
	ECHO ERRORS!!!
PAUSE
)
