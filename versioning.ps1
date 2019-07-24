$pkgPath = "$Env:BUILD_SOURCESDIRECTORY\package.json";
$pkg = Get-Content "$pkgPath" | Out-String | ConvertFrom-Json
$version = $pkg.version;
$buildNumber = $Env:BUILD_BUILDNUMBER;

If ($Env:BUILD_REASON -eq "IndividualCI" -and $Env:BUILD_SOURCEBRANCH -eq "refs/heads/master") {
  Write-Host "##vso[build.updatebuildnumber]$version"
} Else {
  Write-Host "Build Reason: $Env:BUILD_REASON"
  Write-Host "Source Branch: $Env:BUILD_SOURCEBRANCH"
  Write-Host "##vso[build.updatebuildnumber]$version-ci$buildNumber"
}