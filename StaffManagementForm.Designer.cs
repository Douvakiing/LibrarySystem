namespace LibrarySystem
{
    partial class StaffManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvStaff = new System.Windows.Forms.DataGridView();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkSearchMode = new System.Windows.Forms.CheckBox();
            this.btnBack = new System.Windows.Forms.Button();

            System.Windows.Forms.Label lblId = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblName = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblPhone = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblPassword = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblEmail = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).BeginInit();
            this.SuspendLayout();

            this.dgvStaff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStaff.Location = new System.Drawing.Point(16, 16);
            this.dgvStaff.Name = "dgvStaff";
            this.dgvStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStaff.Size = new System.Drawing.Size(752, 250);
            this.dgvStaff.ReadOnly=true;
            this.dgvStaff.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStaff_CellClick);

            lblId.Text = "Staff ID:"; lblId.Location = new System.Drawing.Point(16, 290); lblId.AutoSize = true;
            this.txtId.Location = new System.Drawing.Point(90, 287); 

            lblName.Text = "Name:"; lblName.Location = new System.Drawing.Point(16, 320); lblName.AutoSize = true;
            this.txtName.Location = new System.Drawing.Point(90, 317); 

            lblEmail.Text = "Email:"; lblEmail.Location = new System.Drawing.Point(16, 350); lblEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(90, 347); 

            lblPhone.Text = "Phone:"; lblPhone.Location = new System.Drawing.Point(230, 290); lblPhone.AutoSize = true;
            this.txtPhone.Location = new System.Drawing.Point(300, 287); 

            lblPassword.Text = "Password:"; lblPassword.Location = new System.Drawing.Point(230, 320); lblPassword.AutoSize = true;
            this.txtPassword.Location = new System.Drawing.Point(300, 317); this.txtPassword.UseSystemPasswordChar = true;

            this.chkSearchMode.AutoSize = true;
            this.chkSearchMode.Location = new System.Drawing.Point(16, 385);
            this.chkSearchMode.Text = "Search by ID";
            this.chkSearchMode.CheckedChanged += new System.EventHandler(this.chkSearchMode_CheckedChanged);

            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(620, 287); this.btnAdd.Size = new System.Drawing.Size(100, 30); this.btnAdd.Text = "Add Staff"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(620, 327); this.btnUpdate.Size = new System.Drawing.Size(100, 30); this.btnUpdate.Text = "Update Staff"; this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(620, 367); this.btnDelete.Size = new System.Drawing.Size(100, 30); this.btnDelete.Text = "Delete Staff"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(620, 407); this.btnSearch.Size = new System.Drawing.Size(100, 30); this.btnSearch.Text = "Search"; this.btnSearch.Visible = false; this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(16, 515);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "< Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(lblId); this.Controls.Add(this.txtId);
            this.Controls.Add(lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(lblPhone); this.Controls.Add(this.txtPhone);
            this.Controls.Add(lblPassword); this.Controls.Add(this.txtPassword);
            this.Controls.Add(lblEmail); this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.dgvStaff); this.Controls.Add(this.chkSearchMode);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnBack);
            this.Name = "StaffManagementForm";
            this.Text = "Staff Management";
            this.Load += new System.EventHandler(this.StaffManagementForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkSearchMode;
        private System.Windows.Forms.Button btnBack;
    }
}