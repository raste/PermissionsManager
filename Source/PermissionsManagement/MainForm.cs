// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using DataAccess;
using CustomControls;

namespace PermissionManagement
{
    public partial class MainForm : Form
    {
        private Entities objectContext = new Entities();
        private User currUser = null;
        private UserOrRoleRights currUserRights = new UserOrRoleRights();

        private BUsers bUsers = new BUsers();
        private BRoles bRoles = new BRoles();
        private BReports bReports = new BReports();

        private User userReports = null;                // Which user reports should show
        private bool onlyVisibleReports = true;         // What type of reports should show - if false : only deleted reports

        private bool showAllUsersOnSearch = false;      // IF true..it`ll show all non admin users after search

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            SetPositions();
            ShowFirstComponents();
            ResetStatus();
        }

        /// <summary>
        /// Sets positions of all panels
        /// </summary>
        private void SetPositions()
        {
            flpOptions.Top = 40;
            flpOptions.Left = 495;

            flpSearch.Top = 40;
            flpSearch.Left = 8;

            Size controlSize = new Size();
            controlSize.Width = 485;
            controlSize.Height = 390;

            flpSearch.Size = controlSize;
            flpReports.Size = controlSize;

            flpReports.Top = 40;
            flpReports.Left = 8;

            gbFirstAdminReg.Top = 130;
            gbFirstAdminReg.Left = 80;

            gbLogIn.Top = 140;
            gbLogIn.Left = 80;

            gbRegUser.Top = 130;
            gbRegUser.Left = 85;

            gbAddRole.Top = 105;
            gbAddRole.Left = 85;

            gbEditRoles.Top = 90;
            gbEditRoles.Left = 85;

            gbAddReport.Top = 125;
            gbAddReport.Left = 20;

            gbChangeUserRole.Top = 175;
            gbChangeUserRole.Left = 25;

        }

        private void ShowFirstComponents()
        {
            if (bUsers.RegisteredUsersCount(objectContext) > 0)
            {
                ShowLogInPanel();
            }
            else
            {
                ShowAdminRegPanel();
            }

            flpOptions.Visible = false;

            tsmLogOut.Enabled = false;
            tsDDBtnOptions.Enabled = false;
            tsDDlView.Enabled = false;

            tsBtnSearch.Visible = true;
            tsBtnSearch.Enabled = false;

            tsTbSearch.Visible = true;
            tsTbSearch.Enabled = false;

            tsTbSearch.Text = "Търси потребител";
        }

        private void ShowAdminRegPanel()
        {
            HideAllPanels();

            gbFirstAdminReg.Visible = true;
        }

        private void ShowLogInPanel()
        {
            HideAllPanels();
            gbLogIn.Visible = true;


            tbUsername.Text = string.Empty;
            tbPassword.Text = string.Empty;
        }

        private void ShowReports()
        {
            HideAllPanels();

            FillReports();
        }

        private void ShowChangeUserRolePanel(User user)
        {
            HideAllPanels();

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            FillChangeUserRoleData(user);
        }

        /// <summary>
        /// Sets all menus/buttons for actions to visible/enable = true
        /// </summary>
        private void ResetButtonsStateAfterLogIn()
        {
            tsDDBtnOptions.Visible = true;
            tsDDBtnOptions.Enabled = true;

            tsDDlView.Enabled = true;
            tsDDlView.Visible = true;

            tsmLogOut.Visible = true;
            tsmLogOut.Enabled = true;

            tsmAddReport.Visible = true;
            tsmAddReport.Enabled = true;

            tsmShowVisReports.Visible = true;
            tsmShowVisReports.Enabled = true;

            tsmShowDeletedReports.Visible = true;
            tsmShowDeletedReports.Enabled = true;

            tsmRegisterUser.Visible = true;
            tsmRegisterUser.Enabled = true;

            tsmAddRole.Visible = true;
            tsmAddRole.Enabled = true;

            tsmChangeRole.Visible = true;
            tsmChangeRole.Enabled = true;

            tsmChangeUserRole.Visible = true;
            tsmChangeUserRole.Enabled = true;

            btnShowModifyRole.Enabled = true;
            btnShowChangeUserRole.Enabled = true;

            btnShowReports.Visible = true;
            btnShowReports.Enabled = true;

            btnShowAddReport.Visible = true;
            btnShowAddReport.Enabled = true;

            btnShowDeletedReports.Visible = true;
            btnShowDeletedReports.Enabled = true;

            tsTbSearch.Visible = true;
            tsTbSearch.Enabled = true;

            tsBtnSearch.Visible = true;
            tsBtnSearch.Enabled = true;

            tsmUserOptions.Visible = true;
            tsmUserOptions.Enabled = true;

            tsmAdminOptions.Visible = true;
            tsmAdminOptions.Enabled = true;
        }

        /// <summary>
        /// Sets buttons to visible/enabled depending user rights
        /// </summary>
        private void ShowOptionsPanelsAfterLogIn()
        {
            ResetButtonsStateAfterLogIn();

            if (currUser == null)
            {
                throw new ArgumentNullException("currUser");
            }
            if (currUserRights == null)
            {
                throw new ArgumentNullException("currUserRights");
            }

            if (bUsers.IsUserAdmin(currUser) == true)
            {
                if (tsmAdminOptions.Checked == true || tsmUserOptions.Checked == true)
                {
                    flpOptions.Visible = true;
                }
                else
                {
                    flpOptions.Visible = false;
                }

                if (tsmAdminOptions.Checked == true)
                {
                    flpAdminOptions.Visible = true;
                }
                else
                {
                    flpAdminOptions.Visible = false;
                }

                if (tsmUserOptions.Checked == true)
                {
                    flpMainOptions.Visible = true;
                }
                else
                {
                    flpMainOptions.Visible = false;
                }


                List<Role> roles = bRoles.GetRoles(objectContext);
                if (roles != null && roles.Count < 1)
                {
                    btnShowModifyRole.Enabled = false;
                    tsmChangeRole.Enabled = false;

                    btnShowAddUser.Enabled = false;
                    tsmRegisterUser.Enabled = false;
                }

                if (roles != null && roles.Count > 1 && bUsers.CountUsers(objectContext) > 0)
                {
                    btnShowChangeUserRole.Enabled = true;
                    tsmChangeUserRole.Enabled = true;
                }
                else
                {
                    btnShowChangeUserRole.Enabled = false;
                    tsmChangeUserRole.Enabled = false;
                }

            }
            else
            {
                tsmAdminOptions.Visible = false;
                flpAdminOptions.Visible = false;

                tsmRegisterUser.Visible = false;
                tsmAddRole.Visible = false;
                tsmChangeRole.Visible = false;
                tsmChangeUserRole.Visible = false;

                if (currUserRights.SeeVisibleReportsVisible == true || currUserRights.AddReportVisible == true
                    || currUserRights.SeeDeletedReportVisible == true)
                {
                    tsmUserOptions.Enabled = true;

                    if (tsmUserOptions.Checked == true)
                    {
                        flpOptions.Visible = true;
                        flpMainOptions.Visible = true;
                    }
                    else
                    {
                        flpOptions.Visible = false;
                    }
                }
                else
                {
                    tsmUserOptions.Enabled = false;
                    tsmUserOptions.Checked = false;
                }

                btnShowAddReport.Visible = currUserRights.AddReportVisible;
                btnShowAddReport.Enabled = currUserRights.AddReportEnabled;

                btnShowReports.Visible = currUserRights.SeeVisibleReportsVisible;
                btnShowReports.Enabled = currUserRights.SeeVisibleReportsEnabled;

                btnShowDeletedReports.Visible = currUserRights.SeeDeletedReportVisible;
                btnShowDeletedReports.Enabled = currUserRights.SeeDeletedReportsEnabled;

                tsmAddReport.Visible = currUserRights.AddReportVisible;
                tsmAddReport.Enabled = currUserRights.AddReportEnabled;

                tsmShowVisReports.Visible = currUserRights.SeeVisibleReportsVisible;
                tsmShowVisReports.Enabled = currUserRights.SeeVisibleReportsEnabled;

                tsmShowDeletedReports.Visible = currUserRights.SeeDeletedReportVisible;
                tsmShowDeletedReports.Enabled = currUserRights.SeeDeletedReportsEnabled;

                tsTbSearch.Visible = currUserRights.SearchForUsersVisible;
                tsTbSearch.Enabled = currUserRights.SearchForUsersEnabled;

                tsBtnSearch.Visible = currUserRights.SearchForUsersVisible;
                tsBtnSearch.Enabled = currUserRights.SearchForUsersEnabled;

            }

            if (currUserRights.SeeVisibleReportsVisible == true && currUserRights.SeeVisibleReportsEnabled == true)
            {
                userReports = null;
                onlyVisibleReports = true;

                FillReports();
            }

        }

        private void ShowAddRolePanel()
        {
            HideAllPanels();

            gbAddRole.Visible = true;

            tbRoleName.Text = string.Empty;

            role1enabled.Checked = false;
            role2enabled.Checked = false;
            role5enabled.Checked = false;
            role6enabled.Checked = false;
            role3enabled.Checked = false;
            role4enabled.Checked = false;

            role1visible.Checked = false;
            role2visible.Checked = false;
            role5visible.Checked = false;
            role6visible.Checked = false;
            role3visible.Checked = false;
            role4visible.Checked = false;
        }

        private void ShowAddReportPanel()
        {
            HideAllPanels();

            gbAddReport.Visible = true;

            tbReportDescr.Text = string.Empty;
            rbReportAbout1.Checked = true;
        }


        private void ShowRegNewUserPanel()
        {
            HideAllPanels();

            gbRegUser.Visible = true;

            tbNewUserName.Text = string.Empty;
            tbNewUserPassword.Text = string.Empty;
            tbNewUserUsername.Text = string.Empty;

            rbNewUserNormal.Checked = true;
            ddlNewUserRoles.Enabled = true;

            FillDdlNewUserRoles();
        }

        private void ShowEditRolePanel()
        {
            HideAllPanels();

            gbEditRoles.Visible = true;
            tbEditRoleNewName.Text = string.Empty;

            FillEditRoleDDL();
        }

        private void UnCheckEditRoleCheckboxes()
        {
            cbEditRole1Enabled.Checked = false;
            cbEditRole2Enabled.Checked = false;
            cbEditRole3Enabled.Checked = false;
            cbEditRole4Enabled.Checked = false;
            cbEditRole5Enabled.Checked = false;
            cbEditRole6Enabled.Checked = false;

            cbEditRole1Visible.Checked = false;
            cbEditRole2Visible.Checked = false;
            cbEditRole3Visible.Checked = false;
            cbEditRole4Visible.Checked = false;
            cbEditRole5Visible.Checked = false;
            cbEditRole6Visible.Checked = false;
        }

        private void FillReports()
        {
            flpReports.Visible = true;
            flpReports.Controls.Clear();

            List<Report> reports = new List<Report>();

            if (userReports == null)
            {
                reports = bReports.GetAllReports(objectContext, onlyVisibleReports);
            }
            else
            {
                reports = bReports.GetUserReports(objectContext, userReports, onlyVisibleReports);
            }

            Label lblReports = new Label();
            flpReports.Controls.Add(lblReports);
            lblReports.Width = 450;

            if (reports != null && reports.Count > 0)
            {
               
                if (onlyVisibleReports == false)
                {
                    if (userReports == null)
                    {
                        lblReports.Text = "     Изтрити доклади:";
                    }
                    else
                    {
                        lblReports.Text = string.Format("     Изтрити доклади на {0}:", userReports.name);
                    }
                }
                else
                {
                    if (userReports == null)
                    {
                        lblReports.Text = "     Доклади:";
                    }
                    else
                    {
                        lblReports.Text = string.Format("     Доклади на {0}:", userReports.name);
                    }
                }

                foreach (Report report in reports)
                {
                    ReportControl reportControl = new ReportControl();
                    reportControl.SetData(report, currUserRights);

                    reportControl.DeleteButtonClick += new ReportEventHandler(reportControl_DeleteButtonClick);
                    reportControl.SolveButtonClick += new ReportEventHandler(reportControl_SolveButtonClick);

                    flpReports.Controls.Add(reportControl);
                }

                flpReports.Controls.Add(new Label());
            }
            else
            {
                if (userReports == null)
                {
                    lblReports.Text = "     Не са открити доклади.";
                }
                else
                {
                    lblReports.Text = string.Format("     Не са открити доклади на {0}.", userReports.name);
                }
                
            }

        }

    

      

        private void FillDdlNewUserRoles()
        {
            ddlNewUserRoles.Items.Clear();

            List<Role> roles = bRoles.GetRoles(objectContext);
            if (roles == null || roles.Count < 1)
            {
                throw new InvalidOperationException("There are no roles, which can be added to new user");
            }

            ComboBoxItem item = new ComboBoxItem("...избери", "0");
            ddlNewUserRoles.Items.Add(item);

            foreach (Role role in roles)
            {
                item = new ComboBoxItem(role.name, role.ID.ToString());
                ddlNewUserRoles.Items.Add(item);
            }

            ddlNewUserRoles.SelectedIndex = 0;
        }


        private void btnAdminReg_Click(object sender, EventArgs e)
        {
            StringBuilder sbErrors = new StringBuilder();
            bool errorOccured = false;

            string error = string.Empty;
            string username = tbAdminUsername.Text;
            string name = tbAdminName.Text;
            string password = tbAdminPass.Text;

            if (Tools.ValidationPassed(ValidationField.Username, ref username, 3, 50, out error, false) == false)
            {
                sbErrors.Append(error);
                errorOccured = true;
            }

            if (Tools.ValidationPassed(ValidationField.Name, ref name, 3, 100, out error, false) == false)
            {
                sbErrors.Append(error);
                errorOccured = true;
            }

            if (Tools.ValidationPassed(ValidationField.Password, ref password, 3, 100, out error, false) == false)
            {
                sbErrors.Append(error);
                errorOccured = true;
            }

            if (!string.IsNullOrEmpty(username))
            {
                User user = bUsers.GetUser(objectContext, username, false);
                if (user != null)
                {
                    sbErrors.Append("Това потребителско име е заето.\n");
                    errorOccured = true;
                }
            }

            if (errorOccured == false)
            {
                lblAdminRegError.Visible = false;

                bUsers.AddUser(objectContext, username, name, password, true);

                ChangeStatus("Регистрацията успешна!");

                ShowLogInPanel();

            }
            else
            {
                sbErrors.Insert(0, "  Грешки в попълването:\n");
                sbErrors.Replace("\n", System.Environment.NewLine);
                lblAdminRegError.Visible = true;
                lblAdminRegError.Text = sbErrors.ToString();

            }

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string error = string.Empty;
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblLogInError.Visible = true;
                lblLogInError.Text = "Въведете потребителско име и/или парола!";
            }
            else
            {
                currUser = bUsers.GetUser(objectContext, username, password, false);

                if (currUser != null)
                {
                    lblLogInError.Visible = false;
                    gbLogIn.Visible = false;

                    ChangeStatus("Влязохте успешно!");

                    currUserRights = new UserOrRoleRights(objectContext, currUser);

                    ShowOptionsPanelsAfterLogIn();
                }
                else
                {
                    lblLogInError.Visible = true;
                    lblLogInError.Text = "Грешно потребителско име или парола!";
                }
            }

        }

        private void btnShowAddRole_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            ShowAddRolePanel();
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            StringBuilder sbErrors = new StringBuilder();
            string error = string.Empty;
            string name = tbRoleName.Text;
            bool errorsOccured = false;

            if (Tools.ValidationPassed(ValidationField.RoleName, ref name, 1, 100, out error, false) == false)
            {
                errorsOccured = true;
                sbErrors.Append(error);
            }

            if (role1visible.Checked == false && role2visible.Checked == false && role5visible.Checked == false
                && role6visible.Checked == false && role3visible.Checked == false && role4visible.Checked == false)
            {
                errorsOccured = true;
                sbErrors.Append("Минимум едно право трябва да е чекнато за видимост!\n");
            }

            if (!string.IsNullOrEmpty(name))
            {
                Role role = bRoles.GetRole(objectContext, name, false);
                if (role != null)
                {
                    errorsOccured = true;
                    sbErrors.Append("Има роля с такова име!\n");
                }
            }

            if (errorsOccured == false)
            {
                lblAddRoleError.Visible = false;

                NewRoleRights newRoleRights = new NewRoleRights();

                newRoleRights.SearchForUsersVisible = role1visible.Checked;
                newRoleRights.SearchForUsersEnabled = role1enabled.Checked;

                newRoleRights.AddReportVisible = role2visible.Checked;
                newRoleRights.AddReportEnabled = role2enabled.Checked;

                newRoleRights.SeeVisibleReportsVisible = role3visible.Checked;
                newRoleRights.SeeVisibleReportsEnabled = role3enabled.Checked;

                newRoleRights.SeeDeletedReportVisible = role4visible.Checked;
                newRoleRights.SeeDeletedReportsEnabled = role4enabled.Checked;

                newRoleRights.MarkReportAsResolvedVisible = role5visible.Checked;
                newRoleRights.MarkReportAsResolvedEnabled = role5enabled.Checked;

                newRoleRights.DeleteReportVisible = role6visible.Checked;
                newRoleRights.DeleteReportEnabled = role6enabled.Checked;

                bRoles.AddRole(objectContext, name, newRoleRights);

                ChangeStatus("Ролята е добавена!");

                int rolesCount = bRoles.CountRoles(objectContext);

                if (rolesCount == 1)
                {
                    tsmChangeRole.Enabled = true;
                    btnShowModifyRole.Enabled = true;

                    tsmRegisterUser.Enabled = true;
                    btnShowAddUser.Enabled = true;
                }

                if (rolesCount > 1 && bUsers.CountUsers(objectContext) > 0)
                {
                    btnShowChangeUserRole.Enabled = true;
                    tsmChangeUserRole.Enabled = true;
                }

                ShowAddRolePanel();
            }
            else
            {
                sbErrors.Insert(0, "  Грешки в попълването:\n");
                sbErrors.Replace("\n", System.Environment.NewLine);
                lblAddRoleError.Visible = true;
                lblAddRoleError.Text = sbErrors.ToString();
            }


        }

        private void btnShowAddUser_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            ShowRegNewUserPanel();
        }

        private void btnNewUserReg_Click(object sender, EventArgs e)
        {
            StringBuilder sbErrors = new StringBuilder();
            string error = string.Empty;
            string name = tbNewUserName.Text;
            string username = tbNewUserUsername.Text;
            string password = tbNewUserPassword.Text;
            bool errorsOccured = false;
            bool isAdmin = rbNewUserAdmin.Checked;

            if (Tools.ValidationPassed(ValidationField.Username, ref username, 3, 50, out error, false) == false)
            {
                errorsOccured = true;
                sbErrors.Append(error);
            }
            if (Tools.ValidationPassed(ValidationField.Name, ref name, 3, 100, out error, false) == false)
            {
                errorsOccured = true;
                sbErrors.Append(error);
            }
            if (Tools.ValidationPassed(ValidationField.Password, ref password, 3, 100, out error, false) == false)
            {
                errorsOccured = true;
                sbErrors.Append(error);
            }

            if (!string.IsNullOrEmpty(username))
            {
                User user = bUsers.GetUser(objectContext, username, false);
                if (user != null)
                {
                    sbErrors.Append("Това потребителско име е заето.\n");
                    errorsOccured = true;
                }
            }

            Role chosenRole = null;

            if (isAdmin == false)
            {
                if (ddlNewUserRoles.Items.Count < 1)
                {
                    throw new ArgumentException("No roles added to ddlNewUserRoles.");
                }

                ComboBoxItem selectedItem = (ComboBoxItem)ddlNewUserRoles.SelectedItem;
                if (selectedItem == null)
                {
                    throw new ArgumentException("Couldn't parse ddlNewUserRoles.SelectedItem to ComboBoxItem.");
                }

                string strRoleId = selectedItem.DataValue;
                long id = 0;
                if (long.TryParse(strRoleId, out id) == false)
                {
                    throw new ArgumentException("Couldn't parse ddlNewUserRoles.SelectedValue to long.");
                }

                if (id < 1)
                {
                    errorsOccured = true;
                    sbErrors.Append("Избери роля.");
                }
                else
                {
                    chosenRole = bRoles.GetRole(objectContext, id, true);
                }
            }

            if (errorsOccured == false)
            {
                lblNewUserError.Visible = false;

                bUsers.AddUser(objectContext, username, name, password, isAdmin);
                User newUser = bUsers.GetUser(objectContext, username, true);

                if (isAdmin == false)
                {
                    bRoles.AddRoleToUser(objectContext, chosenRole, newUser);
                }

                ChangeStatus("Потребителят е регистриран!");


                if (isAdmin == false && bRoles.CountRoles(objectContext) > 1)
                {
                    btnShowChangeUserRole.Enabled = true;
                    tsmChangeUserRole.Enabled = true;
                }

                ShowRegNewUserPanel();
            }
            else
            {
                sbErrors.Insert(0, "  Грешки в попълването:\n");
                sbErrors.Replace("\n", System.Environment.NewLine);
                lblNewUserError.Visible = true;
                lblNewUserError.Text = sbErrors.ToString();
            }

        }

        private void rbNewUserNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewUserNormal.Checked == true)
            {
                ddlNewUserRoles.Enabled = true;
            }
        }

        private void rbNewUserAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewUserAdmin.Checked == true)
            {
                ddlNewUserRoles.Enabled = false;
            }
        }

       

       

        private void btnShowAddReport_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            ShowAddReportPanel();
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            StringBuilder sbErrors = new StringBuilder();
            string error = string.Empty;
            string description = tbReportDescr.Text;
            bool errorsOccured = false;
            string about = string.Empty;

            if (Tools.ValidationPassed(ValidationField.Description, ref description, 1, long.MaxValue, out error, true) == false)
            {
                errorsOccured = true;
                sbErrors.Append(error);
            }

            if (rbReportAbout1.Checked == false && rbReportAbout2.Checked == false && rbReportAbout3.Checked == false)
            {
                errorsOccured = true;
                sbErrors.Append("Изберете относно!");
            }
            else
            {
                if (rbReportAbout1.Checked == true)
                {
                    about = "проблем";
                }
                else if (rbReportAbout2.Checked == true)
                {
                    about = "идея";
                }
                else
                {
                    about = "друго";
                }
            }


            if (errorsOccured == false)
            {
                lblAddReportError.Visible = false;

                bReports.AddReport(objectContext, currUser, about, description);

                ChangeStatus("Докладът е написан!");

                ShowAddReportPanel();

            }
            else
            {
                sbErrors.Insert(0, "  Грешки в попълването:\n");
                sbErrors.Replace("\n", System.Environment.NewLine);
                lblAddReportError.Visible = true;
                lblAddReportError.Text = sbErrors.ToString();
            }

            
        }

        private void btnShowModifyRole_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            ShowEditRolePanel();
        }

        private void ddlEditRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbEditRoleNewName.Text = string.Empty;

            if (ddlEditRole.SelectedIndex == 0)
            {
                pnlEditRoleRights.Enabled = false;
                UnCheckEditRoleCheckboxes();
                lblEditRoleError.Visible = false;
            }
            else
            {
                if (ddlEditRole.Items.Count < 1)
                {
                    throw new ArgumentException("No roles added to ddlEditRole.");
                }

                ComboBoxItem selectedItem = (ComboBoxItem)ddlEditRole.SelectedItem;
                if (selectedItem == null)
                {
                    throw new ArgumentException("Couldn't parse ddlEditRole.SelectedItem to ComboBoxItem.");
                }

                string strRoleId = selectedItem.DataValue;
                long id = 0;
                if (long.TryParse(strRoleId, out id) == false)
                {
                    throw new ArgumentException("Couldn't parse ddlEditRole.SelectedValue to long.");
                }

                if (id < 1)
                {
                    throw new ArgumentOutOfRangeException("Selected role id is < 1.");
                }

                Role selectedRole = bRoles.GetRole(objectContext, id, true);
                UserOrRoleRights roleRights = new UserOrRoleRights(objectContext, selectedRole);

                pnlEditRoleRights.Enabled = true;

                cbEditRole1Enabled.Checked = roleRights.SearchForUsersEnabled;
                cbEditRole2Enabled.Checked = roleRights.AddReportEnabled;
                cbEditRole3Enabled.Checked = roleRights.SeeVisibleReportsEnabled;
                cbEditRole4Enabled.Checked = roleRights.SeeDeletedReportsEnabled;
                cbEditRole5Enabled.Checked = roleRights.MarkReportAsResolvedEnabled;
                cbEditRole6Enabled.Checked = roleRights.DeleteReportEnabled;

                cbEditRole1Visible.Checked = roleRights.SearchForUsersVisible;
                cbEditRole2Visible.Checked = roleRights.AddReportVisible;
                cbEditRole3Visible.Checked = roleRights.SeeVisibleReportsVisible;
                cbEditRole4Visible.Checked = roleRights.SeeDeletedReportVisible;
                cbEditRole5Visible.Checked = roleRights.MarkReportAsResolvedVisible;
                cbEditRole6Visible.Checked = roleRights.DeleteReportVisible;
            }

           
        }


        private void FillEditRoleDDL()
        {
            ddlEditRole.Items.Clear();

            List<Role> roles = bRoles.GetRoles(objectContext);

            if (roles == null || roles.Count < 1)
            {
                throw new InvalidOperationException("There are no (added) roles, which can be edited.");
            }

            ComboBoxItem item = new ComboBoxItem("...избери", "0");
            ddlEditRole.Items.Add(item);

            foreach (Role role in roles)
            {
                item = new ComboBoxItem(role.name, role.ID.ToString());
                ddlEditRole.Items.Add(item);
            }

            ddlEditRole.SelectedIndex = 0;

        }

        private void btnEditRole_Click(object sender, EventArgs e)
        {

            StringBuilder sbErrors = new StringBuilder();
            string error = string.Empty;
            string newName = tbEditRoleNewName.Text;
            bool errorsOccured = false;

            if (Tools.ValidationPassed(ValidationField.RoleName, ref newName, 0, 100, out error, false) == false)
            {
                errorsOccured = true;
                sbErrors.Append(error);
            }

            if (cbEditRole1Visible.Checked == false && cbEditRole2Visible.Checked == false && cbEditRole3Visible.Checked == false
                && cbEditRole4Visible.Checked == false && cbEditRole5Visible.Checked == false && cbEditRole6Visible.Checked == false)
            {
                errorsOccured = true;
                sbErrors.Append("Минимум едно право трябва да е чекнато за видимост!\n");
            }

            if (!string.IsNullOrEmpty(newName))
            {
                Role role = bRoles.GetRole(objectContext, newName, false);
                if (role != null)
                {
                    errorsOccured = true;
                    sbErrors.Append("Има роля с такова име!\n");
                }
            }

            Role chosenRole = null;

            if (ddlEditRole.Items.Count < 1)
            {
                throw new ArgumentException("No roles added to ddlEditRole.");
            }

            ComboBoxItem selectedItem = (ComboBoxItem)ddlEditRole.SelectedItem;
            if (selectedItem == null)
            {
                throw new ArgumentException("Couldn't parse ddlEditRole.SelectedItem to ComboBoxItem.");
            }

            string strRoleId = selectedItem.DataValue;
            long id = 0;
            if (long.TryParse(strRoleId, out id) == false)
            {
                throw new ArgumentException("Couldn't parse ddlEditRole.SelectedValue to long.");
            }

            if (id < 1)
            {
                errorsOccured = true;
                sbErrors.Append("Избери роля.\n");
            }
            else
            {
                chosenRole = bRoles.GetRole(objectContext, id, true);
            }

            NewRoleRights editRoleRights = new NewRoleRights();

            if (errorsOccured == false)
            {
                editRoleRights.SearchForUsersVisible = cbEditRole1Visible.Checked;
                editRoleRights.SearchForUsersEnabled = cbEditRole1Enabled.Checked;

                editRoleRights.AddReportVisible = cbEditRole2Visible.Checked;
                editRoleRights.AddReportEnabled = cbEditRole2Enabled.Checked;

                editRoleRights.SeeVisibleReportsVisible = cbEditRole3Visible.Checked;
                editRoleRights.SeeVisibleReportsEnabled = cbEditRole3Enabled.Checked;

                editRoleRights.SeeDeletedReportVisible = cbEditRole4Visible.Checked;
                editRoleRights.SeeDeletedReportsEnabled = cbEditRole4Enabled.Checked;

                editRoleRights.MarkReportAsResolvedVisible = cbEditRole5Visible.Checked;
                editRoleRights.MarkReportAsResolvedEnabled = cbEditRole5Enabled.Checked;

                editRoleRights.DeleteReportVisible = cbEditRole6Visible.Checked;
                editRoleRights.DeleteReportEnabled = cbEditRole6Enabled.Checked;

                if (string.IsNullOrEmpty(newName))
                {
                    if (AreThereDiffsBwRoleRightsAndItsEditedVariant(chosenRole, editRoleRights) == false)
                    {
                        errorsOccured = true;
                        sbErrors.Append("Няма промени.\n");
                    }
                }
            }


            if (errorsOccured == false)
            {
                lblEditRoleError.Visible = false;

                bRoles.EditRole(objectContext, chosenRole, newName, editRoleRights);

                ChangeStatus("Ролята е променена!");

                ShowEditRolePanel();
            }
            else
            {
                sbErrors.Insert(0, "  Грешки в промяната:\n");
                sbErrors.Replace("\n", System.Environment.NewLine);
                lblEditRoleError.Visible = true;
                lblEditRoleError.Text = sbErrors.ToString();
            }

        }

        private bool AreThereDiffsBwRoleRightsAndItsEditedVariant(Role roleBeingEdited ,NewRoleRights editedRights)
        {
            bool differences = false;

            UserOrRoleRights editedRoleRights = new UserOrRoleRights(objectContext, roleBeingEdited);

            if (editedRoleRights.AddReportEnabled != editedRights.AddReportEnabled ||
                editedRoleRights.AddReportVisible != editedRights.AddReportVisible ||
                
                editedRoleRights.DeleteReportEnabled != editedRights.DeleteReportEnabled ||
                editedRoleRights.DeleteReportVisible != editedRights.DeleteReportVisible ||
                
                editedRoleRights.MarkReportAsResolvedEnabled != editedRights.MarkReportAsResolvedEnabled ||
                editedRoleRights.MarkReportAsResolvedVisible != editedRights.MarkReportAsResolvedVisible ||
                
                editedRoleRights.SearchForUsersEnabled != editedRights.SearchForUsersEnabled ||
                editedRoleRights.SearchForUsersVisible != editedRights.SearchForUsersVisible ||
                
                editedRoleRights.SeeDeletedReportsEnabled != editedRights.SeeDeletedReportsEnabled ||
                editedRoleRights.SeeDeletedReportVisible != editedRights.SeeDeletedReportVisible ||
                
                editedRoleRights.SeeVisibleReportsEnabled != editedRights.SeeVisibleReportsEnabled ||
                editedRoleRights.SeeVisibleReportsVisible != editedRights.SeeVisibleReportsVisible)
            {
                differences = true;
            }

            return differences;
        }

        private void btnShowReports_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            userReports = null;
            onlyVisibleReports = true;

            ShowReports();
        }

        private void btnShowDeletedReports_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            userReports = null;
            onlyVisibleReports = false;

            ShowReports();
        }

        void reportControl_DeleteButtonClick(object sender, ReportEventArgs args)
        {
            HideAllPanels();

            Report currReport = args.report;
            if (currReport == null)
            {
                throw new ArgumentNullException("currReport");
            }

            bReports.DeleteReport(objectContext, currReport);

            FillReports();

            ChangeStatus("Докладът е изтрит!");
        }

        void reportControl_SolveButtonClick(object sender, ReportEventArgs args)
        {
            HideAllPanels();

            Report currReport = args.report;
            if (currReport == null)
            {
                throw new ArgumentNullException("currReport");
            }

            bReports.SolveReport(objectContext, currReport);

            FillReports();

            ChangeStatus("Докладът е маркиран като решен!");
        }

        private void FillChangeUserRoleData(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.isAdmin == true)
            {
                throw new InvalidOperationException("admins dont have roles");
            }

            gbChangeUserRole.Visible = true;

            Role userRole = bRoles.GetUserRole(objectContext, user);

            lblChangeUserRoleName.Text = user.name;
            lblUserRole.Text = userRole.name;

            lblChangeUserRoleHidden.Text = user.ID.ToString();

            FillDDlChangeUserRole(userRole);
        }

        private void FillDDlChangeUserRole(Role usersRole)
        {
            ddlChangeUserRole.Items.Clear();

            if (usersRole == null)
            {
                throw new ArgumentNullException("usersRole");
            }

            List<Role> allRoles = bRoles.GetRoles(objectContext);
            if (allRoles == null || allRoles.Count < 2)
            {
                throw new InvalidOperationException("Not enough roles in database (need atleast 2).");
            }

            ComboBoxItem item = new ComboBoxItem("...избери", "0");
            ddlChangeUserRole.Items.Add(item);

            foreach (Role role in allRoles)
            {
                if (role.ID != usersRole.ID)
                {
                    item = new ComboBoxItem(role.name, role.ID.ToString());
                    ddlChangeUserRole.Items.Add(item);
                }
            }

            ddlChangeUserRole.SelectedIndex = 0;
        }

        void sUserControl_ChangeRoleClick(object sender, ReportEventArgs args)
        {
            HideAllPanels();

            User selectedUser = args.user;
            if (selectedUser == null)
            {
                throw new ArgumentNullException("selectedUser");
            }

            flpSearch.Visible = false;

            ShowChangeUserRolePanel(selectedUser);
        }

        void sUserControl_SeeDeletedReportsClick(object sender, ReportEventArgs args)
        {
            HideAllPanels();

            User selectedUser = args.user;
            if (selectedUser == null)
            {
                throw new ArgumentNullException("selectedUser");
            }

            flpSearch.Visible = false;

            userReports = selectedUser;
            onlyVisibleReports = false;

            ShowReports();
        }

        void sUserControl_SeeReportsClick(object sender, ReportEventArgs args)
        {
            HideAllPanels();

            User selectedUser = args.user;
            if (selectedUser == null)
            {
                throw new ArgumentNullException("selectedUser");
            }

            flpSearch.Visible = false;

            userReports = selectedUser;
            onlyVisibleReports = true;

            ShowReports();
        }

        private void btnChangeUserRole_Click(object sender, EventArgs e)
        {
            if (ddlChangeUserRole.SelectedIndex > 0)
            {
                if (string.IsNullOrEmpty(lblChangeUserRoleHidden.Text))
                {
                    throw new ArgumentNullException("lblChangeUserRoleHidden");
                }

                ComboBoxItem selectedItem = (ComboBoxItem)ddlChangeUserRole.SelectedItem;
                if (selectedItem == null)
                {
                    throw new ArgumentException("Couldn't parse ddlChangeUserRole.SelectedItem to ComboBoxItem.");
                }

                string strRoleId = selectedItem.DataValue;
                long roleId = 0;
                if (long.TryParse(strRoleId, out roleId) == false)
                {
                    throw new ArgumentException("Couldn't parse ddlChangeUserRole.SelectedValue to long.");
                }

                if (roleId < 1)
                {
                    throw new ArgumentOutOfRangeException("Selected role id is < 1.");
                }

                string strUserId = lblChangeUserRoleHidden.Text;
                long userId = 0;
                if (long.TryParse(strUserId, out userId) == false)
                {
                    throw new ArgumentException("Couldn't parse lblChangeUserRoleHidden.Text to long.");
                }

                if (userId < 1)
                {
                    throw new ArgumentOutOfRangeException("Selected user id is < 1.");
                }

                Role selectedRole = bRoles.GetRole(objectContext, roleId, true);
                User selectedUser = bUsers.GetUser(objectContext, userId, true);

                bRoles.AddRoleToUser(objectContext, selectedRole, selectedUser);

                lblChangeUserRoleError.Visible = true;

                ChangeStatus("Ролята е сменена!");

                ShowChangeUserRolePanel(selectedUser);

            }
            else
            {
                lblChangeUserRoleError.Visible = true;
                lblChangeUserRoleError.Text = "Избери нова роля!";
            }


        }

        private void ddlChangeUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChangeUserRole.SelectedIndex == 0)
            {
                btnChangeUserRole.Enabled = false;
            }
            else
            {
                if (ddlChangeUserRole.Items.Count < 1)
                {
                    throw new ArgumentException("No roles added to ddlChangeUserRole.");
                }

                ComboBoxItem selectedItem = (ComboBoxItem)ddlChangeUserRole.SelectedItem;
                if (selectedItem == null)
                {
                    throw new ArgumentException("Couldn't parse ddlChangeUserRole.SelectedItem to ComboBoxItem.");
                }

                string strRoleId = selectedItem.DataValue;
                long id = 0;
                if (long.TryParse(strRoleId, out id) == false)
                {
                    throw new ArgumentException("Couldn't parse ddlChangeUserRole.SelectedValue to long.");
                }

                if (id < 1)
                {
                    throw new ArgumentOutOfRangeException("Selected role id is < 1.");
                }

                Role selectedRole = bRoles.GetRole(objectContext, id, true);

                btnChangeUserRole.Enabled = true;
            }
        }

        /// <summary>
        /// Hides all visible panels (without the 2 with action buttons) and resets status msg
        /// </summary>
        private void HideAllPanels()
        {
            //ResetStatus();

            gbAddReport.Visible = false;
            gbAddRole.Visible = false;
            gbChangeUserRole.Visible = false;
            gbEditRoles.Visible = false;
            gbFirstAdminReg.Visible = false;
            gbLogIn.Visible = false;
            gbRegUser.Visible = false;

            flpReports.Visible = false;
            flpSearch.Visible = false;

            lblAddReportError.Visible = false;
            lblAddRoleError.Visible = false;
            lblAdminRegError.Visible = false;
            lblChangeUserRoleError.Visible = false;
            lblEditRoleError.Visible = false;
            lblLogInError.Visible = false;
            lblNewUserError.Visible = false;
        }

        private void tsmLogOut_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            currUser = null;
            currUserRights = null;

            ChangeStatus("Отписахте се успешно!");

            ShowFirstComponents();
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmAddReport_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            ShowAddReportPanel();
        }

        private void tsmShowVisReports_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            userReports = null;
            onlyVisibleReports = true;

            ShowReports();
        }

        private void tsmShowDeletedReports_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            userReports = null;
            onlyVisibleReports = false;

            ShowReports();
        }

        private void tsmRegisterUser_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            ShowRegNewUserPanel();
        }

        private void tsmAddRole_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            ShowAddRolePanel();
        }

        private void tsmChangeRole_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            ShowEditRolePanel();
        }

        private void tsmUserOptions_Click(object sender, EventArgs e)
        {
            ResetStatus();

            if (tsmUserOptions.Checked == true)
            {
                flpOptions.Visible = true;
                flpMainOptions.Visible = true;
            }
            else
            {
                flpMainOptions.Visible = false;

                if (flpAdminOptions.Visible == false)
                {
                    flpOptions.Visible = false;
                }
            }
        
        }

        private void tsmAdminOptions_Click(object sender, EventArgs e)
        {
            ResetStatus();

            if (tsmAdminOptions.Checked == true)
            {
                flpOptions.Visible = true;
                flpAdminOptions.Visible = true;
            }
            else
            {
                flpAdminOptions.Visible = false;

                if (flpMainOptions.Visible == false)
                {
                    flpOptions.Visible = false;
                }
            }

        }

        private void tsBtnSearch_Click(object sender, EventArgs e)
        {
            showAllUsersOnSearch = false;
            FillSearchResults();
        }

        private void FillSearchResults()
        {
            string searchText = tsTbSearch.Text;

            if ((!string.IsNullOrEmpty(searchText) && searchText != "Търси потребител") || showAllUsersOnSearch == true)
            {

                HideAllPanels();

                flpSearch.Visible = true;
                flpSearch.Controls.Clear();

                Label lblResults = new Label();
                flpSearch.Controls.Add(lblResults);
                lblResults.Width = 450;

                List<User> users = new List<User>();
                if (showAllUsersOnSearch == true)
                {
                    users = bUsers.GetAllNonAdminUsers(objectContext);
                }
                else
                {
                    users = bUsers.SearchForUser(objectContext, searchText);
                }

                if (users != null && users.Count > 0)
                {


                    lblResults.Text = "     Открити потребители:";

                    foreach (User user in users)
                    {
                        SearchUserResultControl sUserControl = new SearchUserResultControl();
                        sUserControl.SetData(objectContext, user, currUserRights);

                        sUserControl.SeeReportsClick += new ReportEventHandler(sUserControl_SeeReportsClick);
                        sUserControl.SeeDeletedReportsClick += new ReportEventHandler(sUserControl_SeeDeletedReportsClick);
                        sUserControl.ChangeRoleClick += new ReportEventHandler(sUserControl_ChangeRoleClick);

                        flpSearch.Controls.Add(sUserControl);
                    }

                    flpSearch.Controls.Add(new Label());
                }
                else
                {
                    lblResults.Text = "     Не са открити потребители отговарящи на условието.";
                }
            }
        }

 
        private void tsTbSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tsTbSearch.Text))
            {
                tsTbSearch.Text = "Търси потребител";
            }
        }

        private void tsTbSearch_Click(object sender, EventArgs e)
        {
            if (tsTbSearch.Text == "Търси потребител")
            {
                tsTbSearch.Text = string.Empty;
            }
        }

        private void ChangeStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentNullException(status);
            }

            lblStatus.Text = status;
        }

        private void ResetStatus()
        {
            lblStatus.Text = string.Empty;
        }

        private void btnShowChangeUserRole_Click(object sender, EventArgs e)
        {
            showAllUsersOnSearch = true;
            FillSearchResults();
        }

        private void tsmChangeUserRole_Click(object sender, EventArgs e)
        {
            showAllUsersOnSearch = true;
            FillSearchResults();
        }

    }
}
