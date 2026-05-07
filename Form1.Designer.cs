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
            this.btnManageStaff = new System.Windows.Forms.Button();
            this.btnManagePublishers = new System.Windows.Forms.Button();
            this.tbox = new System.Windows.Forms.TextBox();
            
            
            
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label.Location = new System.Drawing.Point(160, 20);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(429, 100);
            this.label.TabIndex = 0;
            this.label.Text = "Library Management System\n(Team 4)";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            //
            // tbox (SQL Connection Status)
            //
            this.tbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbox.Location = new System.Drawing.Point(225, 140); 
            this.tbox.Name = "tbox";
            this.tbox.ReadOnly = true;
            this.tbox.Multiline = true;
            this.tbox.WordWrap = true;
            this.tbox.Enabled = false;
            this.tbox.Size = new System.Drawing.Size(300, 40);
            this.tbox.TabIndex = 5;
            this.tbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbox.Text = "Checking SQL connection...";

            
            
            // 
            // btnManageBooksPage
            // 
            this.btnManageBooksPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManageBooksPage.Location = new System.Drawing.Point(25, 500);
            this.btnManageBooksPage.Name = "btnManageBooksPage";
            this.btnManageBooksPage.Size = new System.Drawing.Size(120, 46);
            this.btnManageBooksPage.TabIndex = 1;
            this.btnManageBooksPage.Text = "Manage Books";
            this.btnManageBooksPage.UseVisualStyleBackColor = true;
            this.btnManageBooksPage.Click += new System.EventHandler(this.btnManageBooksPage_Click_1);
            
            // 
            // btnManageMembersPage
            // 
            this.btnManageMembersPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManageMembersPage.Location = new System.Drawing.Point(170, 500);
            this.btnManageMembersPage.Name = "btnManageMembersPage";
            this.btnManageMembersPage.Size = new System.Drawing.Size(120, 46);
            this.btnManageMembersPage.TabIndex = 2;
            this.btnManageMembersPage.Text = "Manage Members";
            this.btnManageMembersPage.UseVisualStyleBackColor = true;
            this.btnManageMembersPage.Click += new System.EventHandler(this.btnManageMembersPage_Click);
            
            // 
            // btnMainDeskPage
            // 
            this.btnMainDeskPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainDeskPage.Location = new System.Drawing.Point(315, 500);
            this.btnMainDeskPage.Name = "btnMainDeskPage";
            this.btnMainDeskPage.Size = new System.Drawing.Size(120, 46);
            this.btnMainDeskPage.TabIndex = 3;
            this.btnMainDeskPage.Text = "Main Desk";
            this.btnMainDeskPage.UseVisualStyleBackColor = true;
            this.btnMainDeskPage.Click += new System.EventHandler(this.btnMainDeskPage_Click);
            
            // 
            // btnManageStaff
            // 
            this.btnManageStaff.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManageStaff.Location = new System.Drawing.Point(460, 500);
            this.btnManageStaff.Name = "btnManageStaff";
            this.btnManageStaff.Size = new System.Drawing.Size(120, 46);
            this.btnManageStaff.TabIndex = 4;
            this.btnManageStaff.Text = "Manage Staff";
            this.btnManageStaff.UseVisualStyleBackColor = true;
            this.btnManageStaff.Click += new System.EventHandler(this.btnManageStaff_Click);

            // 
            // btnManagePublishers
            // 
            this.btnManagePublishers.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManagePublishers.Location = new System.Drawing.Point(605, 500);
            this.btnManagePublishers.Name = "btnManagePublishers";
            this.btnManagePublishers.Size = new System.Drawing.Size(120, 46);
            this.btnManagePublishers.TabIndex = 5;
            this.btnManagePublishers.Text = "Manage Publishers";
            this.btnManagePublishers.AutoSize=true;
            this.btnManagePublishers.UseVisualStyleBackColor = true;
            this.btnManagePublishers.Click += new System.EventHandler(this.btnManagePublishers_Click);
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue; // Set background to white to match the logo background seamlessly
            this.ClientSize = new System.Drawing.Size(750, 600);
            this.MinimumSize = new System.Drawing.Size(760, 600); 
            
            this.Controls.Add(this.tbox); 
            this.Controls.Add(this.btnManagePublishers);
            this.Controls.Add(this.btnManageStaff);
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
        private System.Windows.Forms.Button btnManageStaff;
        private System.Windows.Forms.Button btnManagePublishers;
    }
}