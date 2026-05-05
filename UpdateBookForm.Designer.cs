namespace LibrarySystem
{
    partial class UpdateBookForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPubDate = new System.Windows.Forms.DateTimePicker();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.txtStaffId = new System.Windows.Forms.TextBox();
            this.txtPublisherId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblISBN = new System.Windows.Forms.Label();
            this.chkISBN = new System.Windows.Forms.CheckBox();
            this.chkTitle = new System.Windows.Forms.CheckBox();
            this.chkCategory = new System.Windows.Forms.CheckBox();
            this.chkAuthor = new System.Windows.Forms.CheckBox();
            this.chkPages = new System.Windows.Forms.CheckBox();
            this.chkStaffId = new System.Windows.Forms.CheckBox();
            this.chkPublishedId = new System.Windows.Forms.CheckBox();
            this.chkPubDate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(354, 353);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 57;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Location = new System.Drawing.Point(187, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 56;
            this.label2.Text = "Publication Date";
            // 
            // dtpPubDate
            // 
            this.dtpPubDate.Enabled = false;
            this.dtpPubDate.Location = new System.Drawing.Point(454, 270);
            this.dtpPubDate.Name = "dtpPubDate";
            this.dtpPubDate.Size = new System.Drawing.Size(259, 22);
            this.dtpPubDate.TabIndex = 55;
            // 
            // txtISBN
            // 
            this.txtISBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtISBN.Enabled = false;
            this.txtISBN.Location = new System.Drawing.Point(503, 74);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(128, 22);
            this.txtISBN.TabIndex = 46;
            // 
            // txtStaffId
            // 
            this.txtStaffId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStaffId.Enabled = false;
            this.txtStaffId.Location = new System.Drawing.Point(503, 214);
            this.txtStaffId.Name = "txtStaffId";
            this.txtStaffId.Size = new System.Drawing.Size(128, 22);
            this.txtStaffId.TabIndex = 54;
            // 
            // txtPublisherId
            // 
            this.txtPublisherId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPublisherId.Enabled = false;
            this.txtPublisherId.Location = new System.Drawing.Point(503, 242);
            this.txtPublisherId.Name = "txtPublisherId";
            this.txtPublisherId.Size = new System.Drawing.Size(128, 22);
            this.txtPublisherId.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.Location = new System.Drawing.Point(187, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 20);
            this.label7.TabIndex = 52;
            this.label7.Text = "PublisherID";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.Location = new System.Drawing.Point(187, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 20);
            this.label6.TabIndex = 51;
            this.label6.Text = "StaffID";
            // 
            // txtCategory
            // 
            this.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCategory.Enabled = false;
            this.txtCategory.Location = new System.Drawing.Point(503, 130);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(128, 22);
            this.txtCategory.TabIndex = 50;
            // 
            // txtAuthor
            // 
            this.txtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthor.Enabled = false;
            this.txtAuthor.Location = new System.Drawing.Point(503, 158);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(128, 22);
            this.txtAuthor.TabIndex = 49;
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(503, 102);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(128, 22);
            this.txtTitle.TabIndex = 48;
            // 
            // txtPages
            // 
            this.txtPages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPages.Enabled = false;
            this.txtPages.Location = new System.Drawing.Point(503, 186);
            this.txtPages.Name = "txtPages";
            this.txtPages.Size = new System.Drawing.Size(128, 22);
            this.txtPages.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.Location = new System.Drawing.Point(187, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Author Name";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.Location = new System.Drawing.Point(187, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "Number of Pages";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Location = new System.Drawing.Point(187, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "Title";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(187, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "Category";
            // 
            // lblISBN
            // 
            this.lblISBN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblISBN.Location = new System.Drawing.Point(187, 76);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(62, 20);
            this.lblISBN.TabIndex = 41;
            this.lblISBN.Text = "ISBN";
            // 
            // chkISBN
            // 
            this.chkISBN.AutoSize = true;
            this.chkISBN.Location = new System.Drawing.Point(354, 76);
            this.chkISBN.Name = "chkISBN";
            this.chkISBN.Size = new System.Drawing.Size(18, 17);
            this.chkISBN.TabIndex = 58;
            this.chkISBN.UseVisualStyleBackColor = true;
            this.chkISBN.CheckedChanged += new System.EventHandler(this.chkISBN_CheckedChanged);
            // 
            // chkTitle
            // 
            this.chkTitle.AutoSize = true;
            this.chkTitle.Location = new System.Drawing.Point(354, 104);
            this.chkTitle.Name = "chkTitle";
            this.chkTitle.Size = new System.Drawing.Size(18, 17);
            this.chkTitle.TabIndex = 59;
            this.chkTitle.UseVisualStyleBackColor = true;
            this.chkTitle.CheckedChanged += new System.EventHandler(this.chkTitle_CheckedChanged);
            // 
            // chkCategory
            // 
            this.chkCategory.AutoSize = true;
            this.chkCategory.Location = new System.Drawing.Point(354, 132);
            this.chkCategory.Name = "chkCategory";
            this.chkCategory.Size = new System.Drawing.Size(18, 17);
            this.chkCategory.TabIndex = 60;
            this.chkCategory.UseVisualStyleBackColor = true;
            this.chkCategory.CheckedChanged += new System.EventHandler(this.chkCategory_CheckedChanged);
            // 
            // chkAuthor
            // 
            this.chkAuthor.AutoSize = true;
            this.chkAuthor.Location = new System.Drawing.Point(354, 160);
            this.chkAuthor.Name = "chkAuthor";
            this.chkAuthor.Size = new System.Drawing.Size(18, 17);
            this.chkAuthor.TabIndex = 61;
            this.chkAuthor.UseVisualStyleBackColor = true;
            this.chkAuthor.CheckedChanged += new System.EventHandler(this.chkAuthor_CheckedChanged);
            // 
            // chkPages
            // 
            this.chkPages.AutoSize = true;
            this.chkPages.Location = new System.Drawing.Point(354, 188);
            this.chkPages.Name = "chkPages";
            this.chkPages.Size = new System.Drawing.Size(18, 17);
            this.chkPages.TabIndex = 62;
            this.chkPages.UseVisualStyleBackColor = true;
            this.chkPages.CheckedChanged += new System.EventHandler(this.chkPages_CheckedChanged);
            // 
            // chkStaffId
            // 
            this.chkStaffId.AutoSize = true;
            this.chkStaffId.Location = new System.Drawing.Point(354, 216);
            this.chkStaffId.Name = "chkStaffId";
            this.chkStaffId.Size = new System.Drawing.Size(18, 17);
            this.chkStaffId.TabIndex = 63;
            this.chkStaffId.UseVisualStyleBackColor = true;
            this.chkStaffId.CheckedChanged += new System.EventHandler(this.chkStaffId_CheckedChanged);
            // 
            // chkPublishedId
            // 
            this.chkPublishedId.AutoSize = true;
            this.chkPublishedId.Location = new System.Drawing.Point(354, 242);
            this.chkPublishedId.Name = "chkPublishedId";
            this.chkPublishedId.Size = new System.Drawing.Size(18, 17);
            this.chkPublishedId.TabIndex = 64;
            this.chkPublishedId.UseVisualStyleBackColor = true;
            this.chkPublishedId.CheckedChanged += new System.EventHandler(this.chkPublishedId_CheckedChanged);
            // 
            // chkPubDate
            // 
            this.chkPubDate.AutoSize = true;
            this.chkPubDate.Location = new System.Drawing.Point(353, 270);
            this.chkPubDate.Name = "chkPubDate";
            this.chkPubDate.Size = new System.Drawing.Size(18, 17);
            this.chkPubDate.TabIndex = 65;
            this.chkPubDate.UseVisualStyleBackColor = true;
            this.chkPubDate.CheckedChanged += new System.EventHandler(this.chkPubDate_CheckedChanged);
            // 
            // UpdateBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkPubDate);
            this.Controls.Add(this.chkPublishedId);
            this.Controls.Add(this.chkStaffId);
            this.Controls.Add(this.chkPages);
            this.Controls.Add(this.chkAuthor);
            this.Controls.Add(this.chkCategory);
            this.Controls.Add(this.chkTitle);
            this.Controls.Add(this.chkISBN);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpPubDate);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.txtStaffId);
            this.Controls.Add(this.txtPublisherId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtPages);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblISBN);
            this.Name = "UpdateBookForm";
            this.Text = "UpdateBookForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPubDate;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.TextBox txtStaffId;
        private System.Windows.Forms.TextBox txtPublisherId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.CheckBox chkISBN;
        private System.Windows.Forms.CheckBox chkTitle;
        private System.Windows.Forms.CheckBox chkCategory;
        private System.Windows.Forms.CheckBox chkAuthor;
        private System.Windows.Forms.CheckBox chkPages;
        private System.Windows.Forms.CheckBox chkStaffId;
        private System.Windows.Forms.CheckBox chkPublishedId;
        private System.Windows.Forms.CheckBox chkPubDate;
    }
}