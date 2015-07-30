#requires -version 2.0

$tests = (Get-ChildItem -Filter "*.Tests.dll" -Recurse | Where-Object { $_.Directory -NotMatch "obj" }).FullName
$opencover = Get-ChildItem -Filter "OpenCover.Console.exe" -Recurse | Resolve-Path -Relative
$xunit = (Get-ChildItem -Filter "xunit.console.exe" -Recurse).FullName

$target = "-target:$xunit"
$targetargs = "-targetargs:$tests -noshadow"
$filter = "-filter:+[SimpleConf*]* -[SimpleConf.Tests*]*"
&$opencover $target $targetargs -register:user $filter -output:coverage.xml

If (Test-Path env:APPVEYOR) {
    $coverage_file = (Get-ChildItem -Filter coverage.xml -Recurse).FullName
    $coveralls = (Get-ChildItem -Filter "coveralls.net.exe" -Recurse).FullName
    
    &$coveralls `--opencover $coverage_file `-r $env:COVERALL_TOKEN
} else {
    Write-Host -ForegroundColor Green "Not running in CI, skipping upload of coverage data"
}