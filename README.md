# Permissions Manager

### About

Prototype of application which manages user roles. 

It was given as recruitment task with the following objectives:
- Application, which uses the following controls: GroupBox, Label, TextBox, Button, ToolStrip
- Roles, which are created and assigned to users. Roles determine which controls users can use. For each role it must be specified which controls will be enabled and visible. 
- There has to be User Interface, in which: users and roles are created; roles are assigned to users; permissions are asigned to roles.
- Development period of up to 2 weeks.

It was done in 4 days. The final variant was made as Administration application, in which administrators can create roles (and modify them), and assign roles to registered (by administrators) users. Users could create/delete/solve reports, see other user's reports, search for users, based on their roles.

Interface language is Bulgarian. 
The application was approved and I was recruited :) 

### Technologies

.NET 3.5, Windows Forms, LINQ, Entity Framework, C#

### Poke/Edit

In order to see the code you will have to open the [Source/PermissionsManagement.sln](https://github.com/raste/PermissionsManager/blob/master/Source/PermissionsManagement.sln) file with Visual Studio 2008 or greater.

To run the project: 
- Make sure tou have Microsoft SQL Server 2008 or greater installed. 
- Create the database from the script ([DB\DbScript.sql](https://github.com/raste/PermissionsManager/blob/master/DB/DbScript.sql)) or restore it from the backup file ([DbBackup.bak](https://github.com/raste/PermissionsManager/blob/master/DB/DbBackup.bak))
- Update the database connection string in [Source/PermissionsManagement/App.Config file](https://github.com/raste/PermissionsManager/blob/master/Source/PermissionsManagement/App.Config). Replace "NAME" in `Data Source=NAME;` with the name of your SQL Server. Replace "PermissionsManagement" in `Initial Catalog=PermissionsManagement;` with the application database name. If the database is password protected add `user id=dbUser;password=userPass;` right after `Initial Catalog=PermissionsManagement;` section and replace "dbUser" with the database user and "userPass" with his password.

### Images

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/admin_reg.png "First start admin registration")

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/login.png "Log in")

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/add_role.png "Role creation")

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/add_user.png "User registration")

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/add_report.png "Report writing")

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/reports.png "All reports")

![alt text](https://github.com/raste/PermissionsManager/blob/master/screenshots/search.png "Search")
