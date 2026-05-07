namespace LibrarySystem
{
    partial class MainDeskForm
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
            this.grpIssue = new System.Windows.Forms.GroupBox();
            this.lblIssueMemberId = new System.Windows.Forms.Label();
            this.txtIssueMemberId = new System.Windows.Forms.TextBox();
            this.lblIssueISBN = new System.Windows.Forms.Label();
            this.txtIssueISBN = new System.Windows.Forms.TextBox();
            this.lblIssueCopyNum = new System.Windows.Forms.Label();
            this.txtIssueCopyNum = new System.Windows.Forms.TextBox();
            this.btnIssue = new System.Windows.Forms.Button();

            this.grpReturn = new System.Windows.Forms.GroupBox();
            this.lblReturnISBN = new System.Windows.Forms.Label();
            this.txtReturnISBN = new System.Windows.Forms.TextBox();
            this.lblReturnCopyNum = new System.Windows.Forms.Label();
            this.txtReturnCopyNum = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();

            this.grpAvailable = new System.Windows.Forms.GroupBox();
            this.dgvBookCopies = new System.Windows.Forms.DataGridView();

            this.grpRadar = new System.Windows.Forms.GroupBox();
            this.dgvActiveLoans = new System.Windows.Forms.DataGridView();
            
            this.btnBack = new System.Windows.Forms.Button();

            this.grpIssue.SuspendLayout();
            this.grpReturn.SuspendLayout();
            this.grpAvailable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookCopies)).BeginInit();
            this.grpRadar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveLoans)).BeginInit();
            this.SuspendLayout();

            // ==========================================
            // TOP LEFT: CHECKOUT (ISSUE)
            // ==========================================
            this.grpIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grpIssue.Location = new System.Drawing.Point(20, 20);
            this.grpIssue.Size = new System.Drawing.Size(350, 320);
            this.grpIssue.Name = "grpIssue";
            this.grpIssue.Text = "Check-Out / Issue Book";
            this.grpIssue.TabIndex = 0;

            this.lblIssueMemberId.AutoSize = true; this.lblIssueMemberId.Location = new System.Drawing.Point(20, 40); this.lblIssueMemberId.Text = "Member ID:";
            this.txtIssueMemberId.Location = new System.Drawing.Point(120, 37); this.txtIssueMemberId.Size = new System.Drawing.Size(200, 22);

            this.lblIssueISBN.AutoSize = true; this.lblIssueISBN.Location = new System.Drawing.Point(20, 80); this.lblIssueISBN.Text = "ISBN:";
            this.txtIssueISBN.Location = new System.Drawing.Point(120, 77); this.txtIssueISBN.Size = new System.Drawing.Size(200, 22);

            this.lblIssueCopyNum.AutoSize = true; this.lblIssueCopyNum.Location = new System.Drawing.Point(20, 120); this.lblIssueCopyNum.Text = "Copy Num:";
            this.txtIssueCopyNum.Location = new System.Drawing.Point(120, 117); this.txtIssueCopyNum.Size = new System.Drawing.Size(200, 22);

            this.btnIssue.Location = new System.Drawing.Point(120, 165);
            this.btnIssue.Size = new System.Drawing.Size(200, 40);
            this.btnIssue.Text = "ISSUE BOOK";
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);

            this.grpIssue.Controls.Add(this.lblIssueMemberId);
            this.grpIssue.Controls.Add(this.txtIssueMemberId);
            this.grpIssue.Controls.Add(this.lblIssueISBN);
            this.grpIssue.Controls.Add(this.txtIssueISBN);
            this.grpIssue.Controls.Add(this.lblIssueCopyNum);
            this.grpIssue.Controls.Add(this.txtIssueCopyNum);
            this.grpIssue.Controls.Add(this.btnIssue);

            // ==========================================
            // TOP RIGHT: AVAILABLE INVENTORY
            // ==========================================
            this.grpAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAvailable.Location = new System.Drawing.Point(390, 20);
            this.grpAvailable.Size = new System.Drawing.Size(770, 320);
            this.grpAvailable.Name = "grpAvailable";
            this.grpAvailable.Text = "Available Inventory (Click to select)";
            this.grpAvailable.TabIndex = 1;

            this.dgvBookCopies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookCopies.Name = "dgvBookCopies";
            this.dgvBookCopies.ReadOnly = true;
            this.dgvBookCopies.AllowUserToAddRows = false;
            this.dgvBookCopies.AllowUserToDeleteRows = false;
            this.dgvBookCopies.AllowUserToResizeColumns = false;
            this.dgvBookCopies.AllowUserToResizeRows = false;
            this.dgvBookCopies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookCopies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookCopies.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBookCopies_CellClick);
            
            this.grpAvailable.Controls.Add(this.dgvBookCopies);

            // ==========================================
            // BOTTOM LEFT: RETURN DROP BOX
            // ==========================================
            this.grpReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grpReturn.Location = new System.Drawing.Point(20, 350);
            this.grpReturn.Size = new System.Drawing.Size(350, 260);
            this.grpReturn.Name = "grpReturn";
            this.grpReturn.Text = "Drop Box / Return Book";
            this.grpReturn.TabIndex = 2;

            this.lblReturnISBN.AutoSize = true; this.lblReturnISBN.Location = new System.Drawing.Point(20, 40); this.lblReturnISBN.Text = "ISBN:";
            this.txtReturnISBN.Location = new System.Drawing.Point(120, 37); this.txtReturnISBN.Size = new System.Drawing.Size(200, 22);

            this.lblReturnCopyNum.AutoSize = true; this.lblReturnCopyNum.Location = new System.Drawing.Point(20, 80); this.lblReturnCopyNum.Text = "Copy Num:";
            this.txtReturnCopyNum.Location = new System.Drawing.Point(120, 77); this.txtReturnCopyNum.Size = new System.Drawing.Size(200, 22);

            this.btnReturn.Location = new System.Drawing.Point(120, 125);
            this.btnReturn.Size = new System.Drawing.Size(200, 40);
            this.btnReturn.Text = "PROCESS RETURN";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);

            this.grpReturn.Controls.Add(this.lblReturnISBN);
            this.grpReturn.Controls.Add(this.txtReturnISBN);
            this.grpReturn.Controls.Add(this.lblReturnCopyNum);
            this.grpReturn.Controls.Add(this.txtReturnCopyNum);
            this.grpReturn.Controls.Add(this.btnReturn);

            // ==========================================
            // BOTTOM RIGHT: ACTIVE LOANS RADAR
            // ==========================================
            this.grpRadar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRadar.Location = new System.Drawing.Point(390, 350);
            this.grpRadar.Size = new System.Drawing.Size(770, 300);
            this.grpRadar.Name = "grpRadar";
            this.grpRadar.Text = "Active Loans Radar (Click to select return)";
            this.grpRadar.TabIndex = 3;

            this.dgvActiveLoans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActiveLoans.Name = "dgvActiveLoans";
            this.dgvActiveLoans.ReadOnly = true;
            this.dgvActiveLoans.AllowUserToAddRows = false;
            this.dgvActiveLoans.AllowUserToDeleteRows = false;
            this.dgvActiveLoans.AllowUserToResizeColumns = false;
            this.dgvActiveLoans.AllowUserToResizeRows = false;
            this.dgvActiveLoans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActiveLoans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveLoans.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvActiveLoans_DataBindingComplete);
            this.dgvActiveLoans.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvActiveLoans_CellClick);
            
            this.grpRadar.Controls.Add(this.dgvActiveLoans);

            // ==========================================
            // GLOBAL
            // ==========================================
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(20, 620);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "< Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 680);
            this.Controls.Add(this.grpRadar);
            this.Controls.Add(this.grpAvailable);
            this.Controls.Add(this.grpReturn);
            this.Controls.Add(this.grpIssue);
            this.Controls.Add(this.btnBack);
            this.Name = "MainDeskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Circulation Desk";
            this.Load += new System.EventHandler(this.MainDeskForm_Load);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
            this.grpIssue.ResumeLayout(false);
            this.grpIssue.PerformLayout();
            this.grpReturn.ResumeLayout(false);
            this.grpReturn.PerformLayout();
            this.grpAvailable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookCopies)).EndInit();
            this.grpRadar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveLoans)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpIssue;
        private System.Windows.Forms.Label lblIssueMemberId;
        private System.Windows.Forms.TextBox txtIssueMemberId;
        private System.Windows.Forms.Label lblIssueISBN;
        private System.Windows.Forms.TextBox txtIssueISBN;
        private System.Windows.Forms.Label lblIssueCopyNum;
        private System.Windows.Forms.TextBox txtIssueCopyNum;
        private System.Windows.Forms.Button btnIssue;

        private System.Windows.Forms.GroupBox grpReturn;
        private System.Windows.Forms.Label lblReturnISBN;
        private System.Windows.Forms.TextBox txtReturnISBN;
        private System.Windows.Forms.Label lblReturnCopyNum;
        private System.Windows.Forms.TextBox txtReturnCopyNum;
        private System.Windows.Forms.Button btnReturn;

        private System.Windows.Forms.GroupBox grpAvailable;
        private System.Windows.Forms.DataGridView dgvBookCopies;

        private System.Windows.Forms.GroupBox grpRadar;
        private System.Windows.Forms.DataGridView dgvActiveLoans;
        private System.Windows.Forms.Button btnBack;
    }
}