@echo off
set migrationName=%1
set projectName=src/DataAccess/PFCom.Selfhosted.DataAccess.EFCore
set startupProjectName=src/Host/Web/PFCom.Selfhosted.Host.Web
set outputDirectory=Migrations/

echo Mssql
dotnet ef migrations add -c MssqlContext -p "%projectName%" -s "%startupProjectName%" -o "%outputDirectory%Mssql" %migrationName%
echo -----------------

echo Mysql
dotnet ef migrations add -c MysqlContext -p "%projectName%" -s "%startupProjectName%" -o "%outputDirectory%Mysql" %migrationName%
echo -----------------

echo Postgre
dotnet ef migrations add -c PostgreContext -p "%projectName%" -s "%startupProjectName%" -o "%outputDirectory%Postgre" %migrationName%
echo -----------------

echo Sqlite
dotnet ef migrations add -c SqliteContext -p "%projectName%" -s "%startupProjectName%" -o "%outputDirectory%Sqlite" %migrationName%
echo -----------------
