namespace CustomControls
{
    partial class SearchUserResultControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblReports = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnSeeReports = new System.Windows.Forms.Button();
            this.btnChangeRole = new System.Windows.Forms.Button();
            this.btnSeeDeletedReports = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(4, 4);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(29, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Име";
            // 
            // lblReports
            // 
            this.lblReports.AutoSize = true;
            this.lblReports.Location = new System.Drawing.Point(143, 4);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new System.Drawing.Size(55, 13);
            this.lblReports.TabIndex = 1;
            this.lblReports.Text = "Доклади:";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(234, 4);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(35, 13);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Роля:";
            // 
            // btnSeeReports
            // 
            this.btnSeeReports.Location = new System.Drawing.Point(347, 23);
            this.btnSeeReports.Name = "btnSeeReports";
            this.btnSeeReports.Size = new System.Drawing.Size(108, 23);
            this.btnSeeReports.TabIndex = 3;
            this.btnSeeReports.Text = "Виж докладите му";
            this.btnSeeReports.UseVisualStyleBackColor = true;
            this.btnSeeReports.Click += new System.EventHandler(this.btnSeeReports_Click);
            // 
            // btnChangeRole
            // 
            this.btnChangeRole.Location = new System.Drawing.Point(133, 23);
            this.btnChangeRole.Name = "btnChangeRole";
            this.btnChangeRole.Size = new System.Drawing.Size(75, 23);
            this.btnChangeRole.TabIndex = 4;
            this.btnChangeRole.Text = "Смени роля";
            this.btnChangeRole.UseVisualStyleBackColor = true;
            this.btnChangeRole.Click += new System.EventHandler(this.btnChangeRole_Click);
            // 
            // btnSeeDeletedReports
            // 
            this.btnSeeDeletedReports.Location = new System.Drawing.Point(210, 23);
            this.btnSeeDeletedReports.Name = "btnSeeDeletedReports";
            this.btnSeeDeletedReports.Size = new System.Drawing.Size(135, 23);
            this.btnSeeDeletedReports.TabIndex = 5;
            this.btnSeeDeletedReports.Text = "Виж изтритите доклади";
            this.btnSeeDeletedReports.UseVisualStyleBackColor = true;
            this.btnSeeDeletedReports.Click += new System.EventHandler(this.btnSeeDeletedReports_Click);
            // 
            // SearchUserResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnSeeDeletedReports);
            this.Controls.Add(this.btnChangeRole);
            this.Controls.Add(this.btnSeeReports);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblReports);
            this.Controls.Add(this.lblUserName);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.MinimumSize = new System.Drawing.Size(460, 25);
            this.Name = "SearchUserResultControl";
            this.Size = new System.Drawing.Size(458, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnSeeReports;
        private System.Windows.Forms.Button btnChangeRole;
        private System.Windows.Forms.Button btnSeeDeletedReports;
    }
}
