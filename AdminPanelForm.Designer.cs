namespace LibrarySystem
{
    partial class AdminPanelForm
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

        private void InitializeComponent()
        {
            this.tabControlAdmin = new System.Windows.Forms.TabControl();
            this.tabStaff = new System.Windows.Forms.TabPage();
            this.tabPublishers = new System.Windows.Forms.TabPage();
            
            // Staff Controls
            this.dgvStaff = new System.Windows.Forms.DataGridView();
            this.txtStaffId = new System.Windows.Forms.TextBox();
            this.txtStaffFirst = new System.Windows.Forms.TextBox();
            this.txtStaffLast = new System.Windows.Forms.TextBox();
            this.txtStaffEmail = new System.Windows.Forms.TextBox();
            this.txtStaffPhone = new System.Windows.Forms.TextBox();
            this.txtStaffPassword = new System.Windows.Forms.TextBox();
            this.btnAddStaff = new System.Windows.Forms.Button();
            this.btnUpdateStaff = new System.Windows.Forms.Button();
            this.btnDeleteStaff = new System.Windows.Forms.Button();
            this.btnSearchStaff = new System.Windows.Forms.Button();
            this.chkSearchMode = new System.Windows.Forms.CheckBox();
            
            // Publisher Controls
            this.dgvPublishers = new System.Windows.Forms.DataGridView();
            this.txtPubId = new System.Windows.Forms.TextBox();
            this.txtPubName = new System.Windows.Forms.TextBox();
            this.txtPubEmail = new System.Windows.Forms.TextBox();
            this.txtPubPhone = new System.Windows.Forms.TextBox();
            this.txtPubAddress = new System.Windows.Forms.TextBox();
            this.btnAddPub = new System.Windows.Forms.Button();
            this.btnUpdatePub = new System.Windows.Forms.Button();
            this.btnDeletePub = new System.Windows.Forms.Button();
            this.btnSearchPub = new System.Windows.Forms.Button();
            this.chkSearchPub = new System.Windows.Forms.CheckBox();
            
            this.btnBack = new System.Windows.Forms.Button();

            System.Windows.Forms.Label lblStaffId = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblStaffFirst = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblStaffLast = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblStaffEmail = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblStaffPhone = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblStaffPassword = new System.Windows.Forms.Label();
            
            System.Windows.Forms.Label lblPubId = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblPubName = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblPubEmail = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblPubPhone = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblPubAddress = new System.Windows.Forms.Label();

            this.tabControlAdmin.SuspendLayout();
            this.tabStaff.SuspendLayout();
            this.tabPublishers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublishers)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControlAdmin
            // 
            this.tabControlAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAdmin.Controls.Add(this.tabStaff);
            this.tabControlAdmin.Controls.Add(this.tabPublishers);
            this.tabControlAdmin.Location = new System.Drawing.Point(12, 12);
            this.tabControlAdmin.Name = "tabControlAdmin";
            this.tabControlAdmin.SelectedIndex = 0;
            this.tabControlAdmin.Size = new System.Drawing.Size(760, 500);
            this.tabControlAdmin.TabIndex = 0;

            // ==========================================
            // TAB 1: STAFF MANAGEMENT
            // ==========================================
            this.tabStaff.Controls.Add(this.dgvStaff);
            this.tabStaff.Controls.Add(this.txtStaffId);
            this.tabStaff.Controls.Add(this.txtStaffFirst);
            this.tabStaff.Controls.Add(this.txtStaffLast);
            this.tabStaff.Controls.Add(this.txtStaffEmail);
            this.tabStaff.Controls.Add(this.txtStaffPhone);
            this.tabStaff.Controls.Add(this.txtStaffPassword);
            this.tabStaff.Controls.Add(this.btnAddStaff);
            this.tabStaff.Controls.Add(this.btnUpdateStaff);
            this.tabStaff.Controls.Add(this.btnDeleteStaff);
            this.tabStaff.Controls.Add(this.btnSearchStaff);
            this.tabStaff.Controls.Add(this.chkSearchMode);
            this.tabStaff.Controls.Add(lblStaffId);
            this.tabStaff.Controls.Add(lblStaffFirst);
            this.tabStaff.Controls.Add(lblStaffLast);
            this.tabStaff.Controls.Add(lblStaffEmail);
            this.tabStaff.Controls.Add(lblStaffPhone);
            this.tabStaff.Controls.Add(lblStaffPassword);
            this.tabStaff.Location = new System.Drawing.Point(4, 25);
            this.tabStaff.Name = "tabStaff";
            this.tabStaff.Size = new System.Drawing.Size(752, 471);
            this.tabStaff.TabIndex = 0;
            this.tabStaff.Text = "Staff Management";
            this.tabStaff.UseVisualStyleBackColor = true;

            // dgvStaff
            this.dgvStaff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStaff.Location = new System.Drawing.Point(16, 16);
            this.dgvStaff.Name = "dgvStaff";
            this.dgvStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStaff.Size = new System.Drawing.Size(720, 250);
            this.dgvStaff.TabIndex = 0;

            // Staff Inputs
            lblStaffId.Text = "Staff ID:"; lblStaffId.Location = new System.Drawing.Point(16, 290); lblStaffId.AutoSize = true;
            this.txtStaffId.Location = new System.Drawing.Point(90, 287); this.txtStaffId.Name = "txtStaffId"; 

            lblStaffFirst.Text = "First Name:"; lblStaffFirst.Location = new System.Drawing.Point(16, 320); lblStaffFirst.AutoSize = true;
            this.txtStaffFirst.Location = new System.Drawing.Point(90, 317); this.txtStaffFirst.Name = "txtStaffFirst";

            lblStaffLast.Text = "Last Name:"; lblStaffLast.Location = new System.Drawing.Point(16, 350); lblStaffLast.AutoSize = true;
            this.txtStaffLast.Location = new System.Drawing.Point(90, 347); this.txtStaffLast.Name = "txtStaffLast";

            lblStaffEmail.Text = "Email:"; lblStaffEmail.Location = new System.Drawing.Point(230, 290); lblStaffEmail.AutoSize = true;
            this.txtStaffEmail.Location = new System.Drawing.Point(300, 287); this.txtStaffEmail.Name = "txtStaffEmail";

            lblStaffPhone.Text = "Phone:"; lblStaffPhone.Location = new System.Drawing.Point(230, 320); lblStaffPhone.AutoSize = true;
            this.txtStaffPhone.Location = new System.Drawing.Point(300, 317); this.txtStaffPhone.Name = "txtStaffPhone";

            lblStaffPassword.Text = "Password:"; lblStaffPassword.Location = new System.Drawing.Point(230, 350); lblStaffPassword.AutoSize = true;
            this.txtStaffPassword.Location = new System.Drawing.Point(300, 347); this.txtStaffPassword.Name = "txtStaffPassword"; this.txtStaffPassword.UseSystemPasswordChar = true;

            this.chkSearchMode.AutoSize = true;
            this.chkSearchMode.Location = new System.Drawing.Point(16, 385);
            this.chkSearchMode.Name = "chkSearchMode";
            this.chkSearchMode.Text = "Search by ID";
            this.chkSearchMode.UseVisualStyleBackColor = true;
            this.chkSearchMode.CheckedChanged += new System.EventHandler(this.chkSearchMode_CheckedChanged);

            // Staff Buttons
            this.btnAddStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStaff.Location = new System.Drawing.Point(620, 287); this.btnAddStaff.Name = "btnAddStaff"; this.btnAddStaff.Size = new System.Drawing.Size(100, 30); this.btnAddStaff.Text = "Add Staff"; this.btnAddStaff.Click += new System.EventHandler(this.btnAddStaff_Click);

            this.btnUpdateStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateStaff.Location = new System.Drawing.Point(620, 327); this.btnUpdateStaff.Name = "btnUpdateStaff"; this.btnUpdateStaff.Size = new System.Drawing.Size(100, 30); this.btnUpdateStaff.Text = "Update Staff"; this.btnUpdateStaff.Click += new System.EventHandler(this.btnUpdateStaff_Click);

            this.btnDeleteStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteStaff.Location = new System.Drawing.Point(620, 367); this.btnDeleteStaff.Name = "btnDeleteStaff"; this.btnDeleteStaff.Size = new System.Drawing.Size(100, 30); this.btnDeleteStaff.Text = "Delete Staff"; this.btnDeleteStaff.Click += new System.EventHandler(this.btnDeleteStaff_Click);

            this.btnSearchStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchStaff.Location = new System.Drawing.Point(620, 287); this.btnSearchStaff.Name = "btnSearchStaff"; this.btnSearchStaff.Size = new System.Drawing.Size(100, 30); this.btnSearchStaff.Text = "Search"; this.btnSearchStaff.Visible = false; this.btnSearchStaff.Click += new System.EventHandler(this.btnSearchStaff_Click);

            // ==========================================
            // TAB 2: PUBLISHERS
            // ==========================================
            this.tabPublishers.Controls.Add(this.dgvPublishers);
            this.tabPublishers.Controls.Add(this.txtPubId);
            this.tabPublishers.Controls.Add(this.txtPubName);
            this.tabPublishers.Controls.Add(this.txtPubEmail);
            this.tabPublishers.Controls.Add(this.txtPubPhone);
            this.tabPublishers.Controls.Add(this.txtPubAddress);
            this.tabPublishers.Controls.Add(this.btnAddPub);
            this.tabPublishers.Controls.Add(this.btnUpdatePub);
            this.tabPublishers.Controls.Add(this.btnDeletePub);
            this.tabPublishers.Controls.Add(this.btnSearchPub);
            this.tabPublishers.Controls.Add(this.chkSearchPub);
            this.tabPublishers.Controls.Add(lblPubId);
            this.tabPublishers.Controls.Add(lblPubName);
            this.tabPublishers.Controls.Add(lblPubEmail);
            this.tabPublishers.Controls.Add(lblPubPhone);
            this.tabPublishers.Controls.Add(lblPubAddress);
            this.tabPublishers.Location = new System.Drawing.Point(4, 25);
            this.tabPublishers.Name = "tabPublishers";
            this.tabPublishers.Size = new System.Drawing.Size(752, 471);
            this.tabPublishers.TabIndex = 1;
            this.tabPublishers.Text = "Publishers";
            this.tabPublishers.UseVisualStyleBackColor = true;

            // dgvPublishers
            this.dgvPublishers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPublishers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPublishers.Location = new System.Drawing.Point(16, 16);
            this.dgvPublishers.Name = "dgvPublishers";
            this.dgvPublishers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPublishers.Size = new System.Drawing.Size(720, 250);
            this.dgvPublishers.TabIndex = 0;

            // Publisher Inputs
            lblPubId.Text = "Pub ID:"; lblPubId.Location = new System.Drawing.Point(16, 290); lblPubId.AutoSize = true;
            this.txtPubId.Location = new System.Drawing.Point(90, 287); this.txtPubId.Name = "txtPubId"; 

            lblPubName.Text = "Name:"; lblPubName.Location = new System.Drawing.Point(16, 320); lblPubName.AutoSize = true;
            this.txtPubName.Location = new System.Drawing.Point(90, 317); this.txtPubName.Name = "txtPubName";

            lblPubEmail.Text = "Email:"; lblPubEmail.Location = new System.Drawing.Point(16, 350); lblPubEmail.AutoSize = true;
            this.txtPubEmail.Location = new System.Drawing.Point(90, 347); this.txtPubEmail.Name = "txtPubEmail";

            lblPubPhone.Text = "Phone:"; lblPubPhone.Location = new System.Drawing.Point(230, 290); lblPubPhone.AutoSize = true;
            this.txtPubPhone.Location = new System.Drawing.Point(300, 287); this.txtPubPhone.Name = "txtPubPhone";

            lblPubAddress.Text = "Address:"; lblPubAddress.Location = new System.Drawing.Point(230, 320); lblPubAddress.AutoSize = true;
            this.txtPubAddress.Location = new System.Drawing.Point(300, 317); this.txtPubAddress.Name = "txtPubAddress"; this.txtPubAddress.Size = new System.Drawing.Size(200, 22);

            this.chkSearchPub.AutoSize = true;
            this.chkSearchPub.Location = new System.Drawing.Point(16, 385);
            this.chkSearchPub.Name = "chkSearchPub";
            this.chkSearchPub.Text = "Search by ID";
            this.chkSearchPub.UseVisualStyleBackColor = true;
            this.chkSearchPub.CheckedChanged += new System.EventHandler(this.chkSearchPub_CheckedChanged);

            // Publisher Buttons
            this.btnAddPub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPub.Location = new System.Drawing.Point(620, 287); this.btnAddPub.Name = "btnAddPub"; this.btnAddPub.Size = new System.Drawing.Size(100, 30); this.btnAddPub.Text = "Add Pub"; this.btnAddPub.Click += new System.EventHandler(this.btnAddPub_Click);

            this.btnUpdatePub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdatePub.Location = new System.Drawing.Point(620, 327); this.btnUpdatePub.Name = "btnUpdatePub"; this.btnUpdatePub.Size = new System.Drawing.Size(100, 30); this.btnUpdatePub.Text = "Update Pub"; this.btnUpdatePub.Click += new System.EventHandler(this.btnUpdatePub_Click);

            this.btnDeletePub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePub.Location = new System.Drawing.Point(620, 367); this.btnDeletePub.Name = "btnDeletePub"; this.btnDeletePub.Size = new System.Drawing.Size(100, 30); this.btnDeletePub.Text = "Delete Pub"; this.btnDeletePub.Click += new System.EventHandler(this.btnDeletePub_Click);

            this.btnSearchPub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchPub.Location = new System.Drawing.Point(620, 287); this.btnSearchPub.Name = "btnSearchPub"; this.btnSearchPub.Size = new System.Drawing.Size(100, 30); this.btnSearchPub.Text = "Search"; this.btnSearchPub.Visible = false; this.btnSearchPub.Click += new System.EventHandler(this.btnSearchPub_Click);

            // Global Navigation
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(12, 520);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // Form Attributes
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561); // 800x600 equivalent
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.tabControlAdmin);
            this.Name = "AdminPanelForm";
            this.Text = "System Administrator Panel";
            this.Load += new System.EventHandler(this.AdminPanelForm_Load);
            
            this.tabControlAdmin.ResumeLayout(false);
            this.tabStaff.ResumeLayout(false);
            this.tabStaff.PerformLayout();
            this.tabPublishers.ResumeLayout(false);
            this.tabPublishers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublishers)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabStaff;
        private System.Windows.Forms.TabPage tabPublishers;
        
        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.TextBox txtStaffId;
        private System.Windows.Forms.TextBox txtStaffFirst;
        private System.Windows.Forms.TextBox txtStaffLast;
        private System.Windows.Forms.TextBox txtStaffEmail;
        private System.Windows.Forms.TextBox txtStaffPhone;
        private System.Windows.Forms.TextBox txtStaffPassword;
        private System.Windows.Forms.Button btnAddStaff;
        private System.Windows.Forms.Button btnUpdateStaff;
        private System.Windows.Forms.Button btnDeleteStaff;
        private System.Windows.Forms.Button btnSearchStaff;
        private System.Windows.Forms.CheckBox chkSearchMode;

        private System.Windows.Forms.DataGridView dgvPublishers;
        private System.Windows.Forms.TextBox txtPubId;
        private System.Windows.Forms.TextBox txtPubName;
        private System.Windows.Forms.TextBox txtPubEmail;
        private System.Windows.Forms.TextBox txtPubPhone;
        private System.Windows.Forms.TextBox txtPubAddress;
        private System.Windows.Forms.Button btnAddPub;
        private System.Windows.Forms.Button btnUpdatePub;
        private System.Windows.Forms.Button btnDeletePub;
        private System.Windows.Forms.Button btnSearchPub;
        private System.Windows.Forms.CheckBox chkSearchPub;

        private System.Windows.Forms.Button btnBack;
    }
}