echo off

set OpenCoverPath=C:\Users\ueb\.nuget\packages\opencover\4.7.922\tools
set XUnitPath=C:\Users\ueb\.nuget\packages\xunit.runner.console\2.4.1\tools\net472
set ReportGeneratorPath=C:\Users\ueb\.nuget\packages\reportgenerator\4.8.7\tools\net47
set AssemblyPath=C:\Users\ueb\source\repos\ConsoleViews\ConsoleViews.Tests\bin\debug\net472
set ReportXmlPath=C:\Users\ueb\source\repos\ConsoleViews\ConsoleViews.Tests
set ReportOutputPath=C:\Users\ueb\source\repos\ConsoleViews\ConsoleViews.Tests\Coverage

%OpenCoverPath%\OpenCover.Console.exe -register:user -output:%ReportXmlPath%\coverage.xml -target:"%XUnitPath%\xunit.console.exe" -targetargs:"%AssemblyPath%\ConsoleViews.Tests.dll -noshadow" -filter:"+[ConsoleViews]* -[ConsoleViews]ConsoleViews.Program -[ConsoleViews]ConsoleViews.Cells.Cell -[ConsoleViews]ConsoleViews.Display.Entities.ConsoleString"
%ReportGeneratorPath%\ReportGenerator.exe -reports:%ReportXmlPath%\coverage.xml -targetdir:%ReportOutputPath%
start %ReportOutputPath%\index.htm