using System.Windows.Forms;

namespace LibrarySystem
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.btnManageBooksPage = new System.Windows.Forms.Button();
            this.btnManageMembersPage = new System.Windows.Forms.Button();
            this.btnMainDeskPage = new System.Windows.Forms.Button();
            this.btnAdminPanelPage = new System.Windows.Forms.Button();
            this.tbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label.Location = new System.Drawing.Point(152, 20);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(429, 100);
            this.label.TabIndex = 0;
            this.label.Text = "Library Management System\n(Team 4)";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BackColor = System.Drawing.Color.AliceBlue;
            
            //
            // tbox (SQL Connection Status)
            //
            this.tbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbox.Location = new System.Drawing.Point(216, 140);
            this.tbox.Name = "tbox";
            this.tbox.ReadOnly = true;
            this.tbox.Multiline=true;
            this.tbox.WordWrap=true;
            this.tbox.Enabled=false;
            this.tbox.Size = new System.Drawing.Size(300, 40);
            this.tbox.TabIndex = 5;
            this.tbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbox.Text = "Checking SQL connection...";
            this.tbox.ReadOnly=true;
            
            // 
            // btnManageBooksPage
            // 
            this.btnManageBooksPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManageBooksPage.Location = new System.Drawing.Point(34, 500);
            this.btnManageBooksPage.Name = "btnManageBooksPage";
            this.btnManageBooksPage.Size = new System.Drawing.Size(127, 46);
            this.btnManageBooksPage.TabIndex = 1;
            this.btnManageBooksPage.Text = "Manage Books";
            this.btnManageBooksPage.UseVisualStyleBackColor = true;
            this.btnManageBooksPage.Click += new System.EventHandler(this.btnManageBooksPage_Click_1);
            
            // 
            // btnManageMembersPage
            // 
            this.btnManageMembersPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManageMembersPage.Location = new System.Drawing.Point(208, 500);
            this.btnManageMembersPage.Name = "btnManageMembersPage";
            this.btnManageMembersPage.Size = new System.Drawing.Size(127, 46);
            this.btnManageMembersPage.TabIndex = 2;
            this.btnManageMembersPage.Text = "Manage Members";
            this.btnManageMembersPage.UseVisualStyleBackColor = true;
            this.btnManageMembersPage.Click += new System.EventHandler(this.btnManageMembersPage_Click);
            
            // 
            // btnMainDeskPage
            // 
            this.btnMainDeskPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainDeskPage.Location = new System.Drawing.Point(383, 500);
            this.btnMainDeskPage.Name = "btnMainDeskPage";
            this.btnMainDeskPage.Size = new System.Drawing.Size(127, 46);
            this.btnMainDeskPage.TabIndex = 3;
            this.btnMainDeskPage.Text = "Main Desk";
            this.btnMainDeskPage.UseVisualStyleBackColor = true;
            this.btnMainDeskPage.Click += new System.EventHandler(this.btnMainDeskPage_Click);
            
            // 
            // btnAdminPanelPage
            // 
            this.btnAdminPanelPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAdminPanelPage.Location = new System.Drawing.Point(566, 500);
            this.btnAdminPanelPage.Name = "btnAdminPanelPage";
            this.btnAdminPanelPage.Size = new System.Drawing.Size(127, 46);
            this.btnAdminPanelPage.TabIndex = 4;
            this.btnAdminPanelPage.Text = "Admin Panel";
            this.btnAdminPanelPage.UseVisualStyleBackColor = true;
            this.btnAdminPanelPage.Click += new System.EventHandler(this.btnAdminPanelPage_Click);
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 600);
            
            // ADDED: Make sure all controls are actually added to the form!
            this.Controls.Add(this.tbox); 
            this.Controls.Add(this.btnAdminPanelPage);
            this.Controls.Add(this.btnMainDeskPage);
            this.Controls.Add(this.btnManageMembersPage);
            this.Controls.Add(this.btnManageBooksPage);
            this.Controls.Add(this.label);
            
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox tbox;
        private System.Windows.Forms.Button btnManageBooksPage;
        private System.Windows.Forms.Button btnManageMembersPage;
        private System.Windows.Forms.Button btnMainDeskPage;
        private System.Windows.Forms.Button btnAdminPanelPage;
    }
}