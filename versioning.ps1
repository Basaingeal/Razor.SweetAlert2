$pkgPath = "$Env:BUILD_SOURCESDIRECTORY\package.json";
$pkg = Get-Content "$pkgPath" | Out-String | ConvertFrom-Json
$version = $pkg.version;
$buildNumber = $Env:BUILD_BUILDNUMBER;

If ($Env:BUILD_REASON -eq "IndividualCI" -and $BUILD_SOURCEBRANCHNAME -eq "master") {
  Write-Host "##vso[build.updatebuildnumber]$version"
} 
ElseIf ($Env:BUILD_REASON -eq "PullRequest") {
  Write-Host "##vso[build.updatebuildnumber]$version-$BUILD_SOURCEBRANCHNAME-pr-$buildNumber"
}
Else {
  Write-Host "##vso[build.updatebuildnumber]$version-$BUILD_SOURCEBRANCHNAME-$buildNumber"
}