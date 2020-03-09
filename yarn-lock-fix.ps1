# Dependabot is currently not supporting Yarn v2, and generates an invalid lock file.
# Running this on a PR branch should resolve the conflict.

$branch = &git rev-parse --abbrev-ref HEAD

# Clear yarn.lock file
Set-Content -Path .\yarn.lock -Value ''

# re-run yarn install to generate new lock file
yarn

# unstage all changes so only yarn.lock is automatically commited
git restore . --staged

# stand lockfile
git add yarn.lock

# commit with standard commit message
git commit -m "Fix v2 yarn.lock"

git push origin

git checkout develop

git branch -D $branch