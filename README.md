# Tools for creating AWS C# Code Examples

A collection of small tools I use when creating C# examples for Amazon Web
Tools. These are not official tools, just those I've created for myself to
simplify the work flow.

## Example Workflow

    1. Check out the master branch of my local repo.
    2. Update the GitHub fork to include the latest changes.
    3. Run the git pull command to update the local repo.
    4. Create a new local branch: git checkout -b new-branch-name
    5. Navigate to the parent directory where the new example will be created.
    6. Run the file to create the example (details below.)
    7. Add code and unit tests.
    8. Commit the changes to the local branch.
    9. Push the branch to GitHub - push origin new-branch-name
    10. Open GitHub in the browser and create a pull request.
    11. The PR should have the labels ".NET" and "Ready for review'
    12. Assign to the on call person for review and merge.
    13. Let the on call person know that the PR is ready for review/merge.

## Create Project using create_project

This is step 6 of the workflow described above. The file create_project.cmd must be
located in a directory that is included in your environment PATH. I use this tool for single
service examples only.

In Windows, you can navigate to the local folder, click in the address bar, and then enter cmd.
When you press <ENTER> the command line interpreter will be opened in that directory.

Now type the following command:

    create_project <ProjectName> <AWSService>

where PrjectName is the name you want the application to have and AWSService is the name of
the NuGet package for the service for which you are creating an example application.

For example: create_project CopyObjectExample S3

will create a console application called "CopyObjectExample" for the Amazon Simple Storage
Service.

Add the packages for the AWS Services the project will use.

