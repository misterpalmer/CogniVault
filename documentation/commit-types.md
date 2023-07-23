# Instructions for Submitting Changes and Creating Pull Requests

The process of submitting changes and creating pull requests involves a few steps. Here is a guide on how to do it.

## Step 1: Write a Clear Commit Message

The commit message should be clear and informative. Use the following template:

```
<type>(<scope>): <subject>

<body>
<footer>
```

```
feat(authentication): add 'remember me' feature

Implement the 'remember me' functionality in the authentication module. This allows users to remain authenticated to the application even after closing the browser.

Closes #123
```

## Components of a Commit Message

### Type
This refers to the type of change. It could be:
| Type       | Description                                                                                     |
|------------|-------------------------------------------------------------------------------------------------|
| feat       | A new feature                                                                                   |
| fix        | A bug fix                                                                                       |
| docs       | Changes to documentation                                                                        |
| style      | Formatting, not changing the code                                                               |
| refactor   | Changing the code but not its functionality                                                     |
| test       | Adding tests                                                                                    |
| chore      | Updating build processes and other tasks that do not modify the source code but are necessary   |

### Scope
This refers to what the commit is changing specifically, like a component or a file name.

### Subject
A brief description of the change, typically no more than 50 characters. It should be written in the imperative mood ("change" not "changed" or "changes").

### Body
A more detailed description of the change, explaining why it was necessary and how it was implemented. This part is optional and not always necessary for small changes.

### Footer
Any issues, pull requests, or action items related to the commit can be referenced here. For example, you could say "Closes #123" or "Related to #123".


## Step 2: Push Your Changes to GitHub
Use the `git push` command to push the changes to your GitHub repository.

## Step 3: Create a Pull Request
Navigate to your repository on GitHub and click the "Pull requests" tab. Click "New pull request", choose the branches for the comparison, review your changes, and then click "Create pull request".

Ensure your pull request has a clear title and a descriptive comment. After you create the pull request, others can review your changes and provide feedback. Once your pull request has been reviewed and approved, you can merge your changes.

Remember to pull the latest changes from the repository before starting new work. This can be done using the `git pull` command.
