@echo off

:: Copyright (c) Philipp Wagner. All rights reserved.
:: Licensed under the MIT license. See LICENSE file in the project root for full license information.

set SQLCMD_EXECUTABLE="C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE"
set STDOUT=stdout.log
set STDERR=stderr.log
set LOGFILE=query_output.log

set ServerName=.
set DatabaseName=AirlineOnTimePerformance

call :AskQuestionWithYdefault "Use Server (%ServerName%) [Y,n]?" reply_
if /i [%reply_%] NEQ [y] (
	set /p ServerName="Enter Server: "
)

call :AskQuestionWithYdefault "Use Database (%DatabaseName%) [Y,n]?" reply_
if /i [%reply_%] NEQ [y]  (
	set /p DatabaseName="Enter Database: "
)

1>%STDOUT% 2>%STDERR% (

	:: Database
    %SQLCMD_EXECUTABLE% -S %ServerName% -i "01_create_database.sql" -v dbname=%DatabaseName% -o %LOGFILE%
	%SQLCMD_EXECUTABLE% -S %ServerName% -i "02_create_nodes.sql" -v dbname=%DatabaseName% -o %LOGFILE%
    %SQLCMD_EXECUTABLE% -S %ServerName% -i "03_create_edges.sql" -v dbname=%DatabaseName% -o %LOGFILE%
    %SQLCMD_EXECUTABLE% -S %ServerName% -i "04_create_indexes.sql" -v dbname=%DatabaseName% -o %LOGFILE%
    %SQLCMD_EXECUTABLE% -S %ServerName% -i "05_create_data.sql" -v dbname=%DatabaseName% -o %LOGFILE%
    %SQLCMD_EXECUTABLE% -S %ServerName% -i "06_create_procedures.sql" -v dbname=%DatabaseName% -o %LOGFILE%

)

goto :end

:: The question as a subroutine
:AskQuestionWithYdefault
	setlocal enableextensions
	:_asktheyquestionagain
	set return_=
	set ask_=
	set /p ask_="%~1"
	if "%ask_%"=="" set return_=y
	if /i "%ask_%"=="Y" set return_=y
	if /i "%ask_%"=="n" set return_=n
	if not defined return_ goto _asktheyquestionagain
	endlocal & set "%2=%return_%" & goto :EOF

:end
pause