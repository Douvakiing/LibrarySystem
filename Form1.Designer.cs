namespace LibrarySystem
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.btnManageBooksPage = new System.Windows.Forms.Button();
            this.btnManageMembersPage = new System.Windows.Forms.Button();
            this.btnMainDeskPage = new System.Windows.Forms.Button();
            this.btnAdminPanelPage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label.Location = new System.Drawing.Point(152, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(429, 46);
            this.label.TabIndex = 0;
            this.label.Text = "Library Management System";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnManageBooksPage
            // 
            this.btnManageBooksPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnManageBooksPage.Location = new System.Drawing.Point(34, 222);
            this.btnManageBooksPage.Name = "btnManageBooksPage";
            this.btnManageBooksPage.Size = new System.Drawing.Size(127, 46);
            this.btnManageBooksPage.TabIndex = 1;
            this.btnManageBooksPage.Text = "Manage Books";
            this.btnManageBooksPage.UseVisualStyleBackColor = true;
            this.btnManageBooksPage.Click += new System.EventHandler(this.btnManageBooksPage_Click_1);
            // 
            // btnManageMembersPage
            // 
            this.btnManageMembersPage.Location = new System.Drawing.Point(208, 222);
            this.btnManageMembersPage.Name = "btnManageMembersPage";
            this.btnManageMembersPage.Size = new System.Drawing.Size(127, 46);
            this.btnManageMembersPage.TabIndex = 2;
            this.btnManageMembersPage.Text = "Manage Members";
            this.btnManageMembersPage.UseVisualStyleBackColor = true;
            this.btnManageMembersPage.Click += new System.EventHandler(this.btnManageMembersPage_Click);
            // 
            // btnMainDeskPage
            // 
            this.btnMainDeskPage.Location = new System.Drawing.Point(383, 222);
            this.btnMainDeskPage.Name = "btnMainDeskPage";
            this.btnMainDeskPage.Size = new System.Drawing.Size(127, 46);
            this.btnMainDeskPage.TabIndex = 3;
            this.btnMainDeskPage.Text = "Main Desk";
            this.btnMainDeskPage.UseVisualStyleBackColor = true;
            // 
            // btnAdminPanelPage
            // 
            this.btnAdminPanelPage.Location = new System.Drawing.Point(566, 222);
            this.btnAdminPanelPage.Name = "btnAdminPanelPage";
            this.btnAdminPanelPage.Size = new System.Drawing.Size(127, 46);
            this.btnAdminPanelPage.TabIndex = 4;
            this.btnAdminPanelPage.Text = "Admin Panel";
            this.btnAdminPanelPage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(717, 412);
            this.Controls.Add(this.btnAdminPanelPage);
            this.Controls.Add(this.btnMainDeskPage);
            this.Controls.Add(this.btnManageMembersPage);
            this.Controls.Add(this.btnManageBooksPage);
            this.Controls.Add(this.label);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btnManageBooksPage;
        private System.Windows.Forms.Button btnManageMembersPage;
        private System.Windows.Forms.Button btnMainDeskPage;
        private System.Windows.Forms.Button btnAdminPanelPage;
    }
}

