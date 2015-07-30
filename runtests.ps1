#requires -version 2.0

$tests = (Get-ChildItem -Filter "*.Tests.dll" -Recurse | Where-Object { $_.Directory -NotMatch "obj" }).FullName
$opencover = Get-ChildItem -Filter "OpenCover.Console.exe" -Recurse | Resolve-Path -Relative
$xunit = (Get-ChildItem -Filter "xunit.console.exe" -Recurse).FullName

$target = "-target:$xunit"
$targetargs = "-targetargs:$tests -noshadow"
$filter = "-filter:+[SimpleConf*]* -[SimpleConf.Tests*]*"
&$opencover $target $targetargs -register:user $filter -output:coverage.xml

If (Test-Path env:APPVEYOR) {
    $coverage_result = Get-ChildItem -Filter coverage.xml -Recurse | Resolve-Path -Relative
    $coverall_args = "--opencover $coverage_result -r $env:COVERALL_TOKEN"
    $coverall = (Get-ChildItem -Filter "coverall.net.exe" -Recurse | Resolve-Path -Relative).FullName
    
    &$coverall $coverall_args 
} else {
    Write-Host -ForegroundColor Green "Not running in CI"
}

