$pkgPath = "$Env:BUILD_SOURCESDIRECTORY\package.json";
$pkg = Get-Content "$pkgPath" | Out-String | ConvertFrom-Json
$version = $pkg.version;
$buildNumber = $Env:BUILD_BUILDNUMBER;
$branchName = $Env:BUILD_SOURCEBRANCHNAME
$branch = $Env:BUILD_SOURCEBRANCH
$buildReason = $Env:BUILD_REASON

If ($buildReason -eq "IndividualCI" -and $branch -eq "refs/heads/master") {
  Write-Host "##vso[build.updatebuildnumber]$version"
} 
ElseIf ($buildReason -eq "PullRequest") {
  Write-Host "##vso[build.updatebuildnumber]$version-pr-$buildNumber"
}
Else {
  Write-Host "##vso[build.updatebuildnumber]$version-$branchName-$buildNumber"
}