namespace LibrarySystem
{
    partial class MainDeskForm
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
            this.gbIssueBook = new System.Windows.Forms.GroupBox();
            this.gbIssueReturn = new System.Windows.Forms.GroupBox();
            this.txtIssueCopyNumber = new System.Windows.Forms.TextBox();
            this.txtIssueIsbn = new System.Windows.Forms.TextBox();
            this.txtIssueMemberId = new System.Windows.Forms.TextBox();
            this.btnIssueBook = new System.Windows.Forms.Button();
            this.txtReturnCopyNumber = new System.Windows.Forms.TextBox();
            this.txtReturnISBN = new System.Windows.Forms.TextBox();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.dgvActiveLoans = new System.Windows.Forms.DataGridView();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.gbIssueBook.SuspendLayout();
            this.gbIssueReturn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveLoans)).BeginInit();
            this.SuspendLayout();
            // 
            // gbIssueBook
            // 
            this.gbIssueBook.Controls.Add(this.btnIssueBook);
            this.gbIssueBook.Controls.Add(this.txtIssueMemberId);
            this.gbIssueBook.Controls.Add(this.txtIssueIsbn);
            this.gbIssueBook.Controls.Add(this.txtIssueCopyNumber);
            this.gbIssueBook.Location = new System.Drawing.Point(12, 12);
            this.gbIssueBook.Name = "gbIssueBook";
            this.gbIssueBook.Size = new System.Drawing.Size(200, 177);
            this.gbIssueBook.TabIndex = 0;
            this.gbIssueBook.TabStop = false;
            this.gbIssueBook.Text = "Issue Book (Check Out)";
            // 
            // gbIssueReturn
            // 
            this.gbIssueReturn.Controls.Add(this.btnReturnBook);
            this.gbIssueReturn.Controls.Add(this.txtReturnCopyNumber);
            this.gbIssueReturn.Controls.Add(this.txtReturnISBN);
            this.gbIssueReturn.Location = new System.Drawing.Point(571, 12);
            this.gbIssueReturn.Name = "gbIssueReturn";
            this.gbIssueReturn.Size = new System.Drawing.Size(200, 177);
            this.gbIssueReturn.TabIndex = 1;
            this.gbIssueReturn.TabStop = false;
            this.gbIssueReturn.Text = "Process Return (Check In)";
            // 
            // txtIssueCopyNumber
            // 
            this.txtIssueCopyNumber.Location = new System.Drawing.Point(6, 105);
            this.txtIssueCopyNumber.Name = "txtIssueCopyNumber";
            this.txtIssueCopyNumber.Size = new System.Drawing.Size(167, 22);
            this.txtIssueCopyNumber.TabIndex = 0;
            this.txtIssueCopyNumber.Text = "Copy Number / Barcode";
            // 
            // txtIssueIsbn
            // 
            this.txtIssueIsbn.Location = new System.Drawing.Point(6, 63);
            this.txtIssueIsbn.Name = "txtIssueIsbn";
            this.txtIssueIsbn.Size = new System.Drawing.Size(100, 22);
            this.txtIssueIsbn.TabIndex = 1;
            this.txtIssueIsbn.Text = "Book ISBN";
            // 
            // txtIssueMemberId
            // 
            this.txtIssueMemberId.Location = new System.Drawing.Point(6, 21);
            this.txtIssueMemberId.Name = "txtIssueMemberId";
            this.txtIssueMemberId.Size = new System.Drawing.Size(100, 22);
            this.txtIssueMemberId.TabIndex = 2;
            this.txtIssueMemberId.Text = "Member ID";
            // 
            // btnIssueBook
            // 
            this.btnIssueBook.Location = new System.Drawing.Point(6, 148);
            this.btnIssueBook.Name = "btnIssueBook";
            this.btnIssueBook.Size = new System.Drawing.Size(100, 23);
            this.btnIssueBook.TabIndex = 3;
            this.btnIssueBook.Text = "Issue Book";
            this.btnIssueBook.UseVisualStyleBackColor = true;
            // 
            // txtReturnCopyNumber
            // 
            this.txtReturnCopyNumber.Location = new System.Drawing.Point(6, 63);
            this.txtReturnCopyNumber.Name = "txtReturnCopyNumber";
            this.txtReturnCopyNumber.Size = new System.Drawing.Size(160, 22);
            this.txtReturnCopyNumber.TabIndex = 5;
            this.txtReturnCopyNumber.Text = "Copy Number / Barcode";
            // 
            // txtReturnISBN
            // 
            this.txtReturnISBN.Location = new System.Drawing.Point(6, 21);
            this.txtReturnISBN.Name = "txtReturnISBN";
            this.txtReturnISBN.Size = new System.Drawing.Size(100, 22);
            this.txtReturnISBN.TabIndex = 6;
            this.txtReturnISBN.Text = "Book ISBN";
            // 
            // btnReturnBook
            // 
            this.btnReturnBook.Location = new System.Drawing.Point(6, 148);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(100, 23);
            this.btnReturnBook.TabIndex = 4;
            this.btnReturnBook.Text = "Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = true;
            // 
            // dgvActiveLoans
            // 
            this.dgvActiveLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveLoans.Location = new System.Drawing.Point(18, 195);
            this.dgvActiveLoans.Name = "dgvActiveLoans";
            this.dgvActiveLoans.RowHeadersWidth = 51;
            this.dgvActiveLoans.RowTemplate.Height = 24;
            this.dgvActiveLoans.Size = new System.Drawing.Size(770, 214);
            this.dgvActiveLoans.TabIndex = 2;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(18, 415);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(167, 23);
            this.btnDashboard.TabIndex = 3;
            this.btnDashboard.Text = "Return To Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // MainDeskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDashboard);
            this.Controls.Add(this.dgvActiveLoans);
            this.Controls.Add(this.gbIssueReturn);
            this.Controls.Add(this.gbIssueBook);
            this.Name = "MainDeskForm";
            this.Text = "MainDeskForm";
            this.gbIssueBook.ResumeLayout(false);
            this.gbIssueBook.PerformLayout();
            this.gbIssueReturn.ResumeLayout(false);
            this.gbIssueReturn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveLoans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbIssueBook;
        private System.Windows.Forms.GroupBox gbIssueReturn;
        private System.Windows.Forms.TextBox txtIssueMemberId;
        private System.Windows.Forms.TextBox txtIssueIsbn;
        private System.Windows.Forms.TextBox txtIssueCopyNumber;
        private System.Windows.Forms.Button btnIssueBook;
        private System.Windows.Forms.TextBox txtReturnCopyNumber;
        private System.Windows.Forms.TextBox txtReturnISBN;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.DataGridView dgvActiveLoans;
        private System.Windows.Forms.Button btnDashboard;
    }
}