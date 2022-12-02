@ECHO OFF
dotnet test ..\src\Umbrella.Infrastructure.Firestore.Tests\Umbrella.Infrastructure.Firestore.Tests.csproj --configuration Release --collect "XPlat Code coverage"
ECHO .
IF %ERRORLEVEL% NEQ 0 GOTO BuildError

reportgenerator -reports:..\src\Umbrella.Infrastructure.Firestore.Tests\**\coverage.cobertura.xml -targetdir:..\reports\codeCoverage -reporttypes:"HTML"
ECHO .
IF %ERRORLEVEL% NEQ 0 GOTO BuildError

REM all successful
GOTO :BuildSucceded

:BuildError
REM An error occurred
REM Return error status to the caller
ECHO .
ECHO .
ECHO *********  Failed *********
ECHO .
EXIT /B 1

:BuildSucceded
ECHO .
ECHO . ------- Succeded -------
ECHO .
