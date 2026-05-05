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

            this.dgvActiveLoans = new System.Windows.Forms.DataGridView();
            this.lblRadar = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();

            this.grpIssue.SuspendLayout();
            this.grpReturn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveLoans)).BeginInit();
            this.SuspendLayout();

            // ==========================================
            // ZONE A: ISSUE BOOK (Left Side)
            // ==========================================
            this.grpIssue.Controls.Add(this.lblIssueMemberId);
            this.grpIssue.Controls.Add(this.txtIssueMemberId);
            this.grpIssue.Controls.Add(this.lblIssueISBN);
            this.grpIssue.Controls.Add(this.txtIssueISBN);
            this.grpIssue.Controls.Add(this.lblIssueCopyNum);
            this.grpIssue.Controls.Add(this.txtIssueCopyNum);
            this.grpIssue.Controls.Add(this.btnIssue);
            this.grpIssue.Location = new System.Drawing.Point(16, 16);
            this.grpIssue.Name = "grpIssue";
            this.grpIssue.Size = new System.Drawing.Size(350, 200);
            this.grpIssue.TabIndex = 0;
            this.grpIssue.TabStop = false;
            this.grpIssue.Text = "Check-Out / Issue Book";

            this.lblIssueMemberId.AutoSize = true; this.lblIssueMemberId.Location = new System.Drawing.Point(20, 40); this.lblIssueMemberId.Text = "Member ID:";
            this.txtIssueMemberId.Location = new System.Drawing.Point(120, 37); this.txtIssueMemberId.Size = new System.Drawing.Size(200, 22);

            this.lblIssueISBN.AutoSize = true; this.lblIssueISBN.Location = new System.Drawing.Point(20, 80); this.lblIssueISBN.Text = "ISBN:";
            this.txtIssueISBN.Location = new System.Drawing.Point(120, 77); this.txtIssueISBN.Size = new System.Drawing.Size(200, 22);

            this.lblIssueCopyNum.AutoSize = true; this.lblIssueCopyNum.Location = new System.Drawing.Point(20, 120); this.lblIssueCopyNum.Text = "Copy Number:";
            this.txtIssueCopyNum.Location = new System.Drawing.Point(120, 117); this.txtIssueCopyNum.Size = new System.Drawing.Size(200, 22);

            this.btnIssue.Location = new System.Drawing.Point(120, 155);
            this.btnIssue.Size = new System.Drawing.Size(200, 30);
            this.btnIssue.Text = "Issue Book";
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);

            // ==========================================
            // ZONE B: RETURN BOOK (Right Side)
            // ==========================================
            this.grpReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReturn.Controls.Add(this.lblReturnISBN);
            this.grpReturn.Controls.Add(this.txtReturnISBN);
            this.grpReturn.Controls.Add(this.lblReturnCopyNum);
            this.grpReturn.Controls.Add(this.txtReturnCopyNum);
            this.grpReturn.Controls.Add(this.btnReturn);
            this.grpReturn.Location = new System.Drawing.Point(418, 16);
            this.grpReturn.Name = "grpReturn";
            this.grpReturn.Size = new System.Drawing.Size(350, 200);
            this.grpReturn.TabIndex = 1;
            this.grpReturn.TabStop = false;
            this.grpReturn.Text = "Drop Box / Return Book";

            this.lblReturnISBN.AutoSize = true; this.lblReturnISBN.Location = new System.Drawing.Point(20, 40); this.lblReturnISBN.Text = "ISBN:";
            this.txtReturnISBN.Location = new System.Drawing.Point(120, 37); this.txtReturnISBN.Size = new System.Drawing.Size(200, 22);

            this.lblReturnCopyNum.AutoSize = true; this.lblReturnCopyNum.Location = new System.Drawing.Point(20, 80); this.lblReturnCopyNum.Text = "Copy Number:";
            this.txtReturnCopyNum.Location = new System.Drawing.Point(120, 77); this.txtReturnCopyNum.Size = new System.Drawing.Size(200, 22);

            this.btnReturn.Location = new System.Drawing.Point(120, 155);
            this.btnReturn.Size = new System.Drawing.Size(200, 30);
            this.btnReturn.Text = "Process Return";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);

            // ==========================================
            // ZONE C: ACTIVE LOANS RADAR (Bottom)
            // ==========================================
            this.lblRadar.AutoSize = true;
            this.lblRadar.Location = new System.Drawing.Point(16, 235);
            this.lblRadar.Text = "Active Loans Radar (Books currently out of library):";

            this.dgvActiveLoans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvActiveLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveLoans.Location = new System.Drawing.Point(16, 260);
            this.dgvActiveLoans.Name = "dgvActiveLoans";
            this.dgvActiveLoans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveLoans.Size = new System.Drawing.Size(752, 240);
            this.dgvActiveLoans.TabIndex = 2;
            this.dgvActiveLoans.ReadOnly = true;

            // Global Controls
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(16, 515);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "< Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // Main Form Settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblRadar);
            this.Controls.Add(this.dgvActiveLoans);
            this.Controls.Add(this.grpReturn);
            this.Controls.Add(this.grpIssue);
            this.Controls.Add(this.btnBack);
            this.Name = "MainDeskForm";
            this.Text = "Main Circulation Desk";
            this.Load += new System.EventHandler(this.MainDeskForm_Load);
            
            this.grpIssue.ResumeLayout(false);
            this.grpIssue.PerformLayout();
            this.grpReturn.ResumeLayout(false);
            this.grpReturn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveLoans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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

        private System.Windows.Forms.Label lblRadar;
        private System.Windows.Forms.DataGridView dgvActiveLoans;
        private System.Windows.Forms.Button btnBack;
    }
}
