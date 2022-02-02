# Project-Crispy-Toast
Only for the toastiest floor mats.

We are working on a project for OGPC 15 at Ida B. Wells HS.

# Working With Git

## Start a New Task
create and checkout a new branch `git checkout -b name-of-task` <br />
commit any code changes `git commit -a -m "name of completed stuff"` <br />
push changes on branch to remote `git push origin name-of-branch`

## Sync Fork With Upstream
switch to main branch `git checkout main` <br />
pull upstream into main `git pull upstream main` <br />
push new main to origin `git push origin main` <br />

## Sync Branch With Current Main
checkout branch to sync into `git checkout branch-to-update` <br />
merge main into branch `git merge main` <br />
resolve all merge conflicts if needed <br />

## Complete a Task
sync fork with upstream <br />
sync branch with current main <br />
switch to task branch `git checkout name-of-branch` <br />
push your code to remote `git push origin name-of-branch` <br />
switch to main `git checkout main` <br />
merge task branch to main `git merge name-of-branch` <br />
notify me of your changes and I will pull them
