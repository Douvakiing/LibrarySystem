namespace LibrarySystem
{
    partial class StockManagerForm
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSelectedBook = new System.Windows.Forms.Label();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.lblCopyNum = new System.Windows.Forms.Label();
            this.txtCopyNum = new System.Windows.Forms.TextBox();
            this.btnAddCopy = new System.Windows.Forms.Button();
            this.btnDeleteCopy = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.SuspendLayout();

            // lblHeader
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(760, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Master Inventory Stock Manager";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // dgvStock
            this.dgvStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStock.AllowUserToAddRows = false;
            this.dgvStock.AllowUserToDeleteRows = false;
            this.dgvStock.ReadOnly = true;
            this.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Location = new System.Drawing.Point(12, 45);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.Size = new System.Drawing.Size(760, 360);
            this.dgvStock.TabIndex = 1;
            this.dgvStock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStock_CellClick);

            // lblSelectedBook (Shows what book is currently targeted)
            this.lblSelectedBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectedBook.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedBook.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblSelectedBook.Location = new System.Drawing.Point(12, 420);
            this.lblSelectedBook.Name = "lblSelectedBook";
            this.lblSelectedBook.Size = new System.Drawing.Size(760, 25);
            this.lblSelectedBook.TabIndex = 2;
            this.lblSelectedBook.Text = "Target Book: (None Selected - Click a row above)";

            // Copy Num Input
            this.lblCopyNum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCopyNum.AutoSize = true;
            this.lblCopyNum.Location = new System.Drawing.Point(150, 465);
            this.lblCopyNum.Name = "lblCopyNum";
            this.lblCopyNum.Text = "Specific Copy Num (Optional):";

            this.txtCopyNum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtCopyNum.Location = new System.Drawing.Point(350, 462);
            this.txtCopyNum.Name = "txtCopyNum";
            this.txtCopyNum.Size = new System.Drawing.Size(60, 22);

            // Buttons
            this.btnAddCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddCopy.Location = new System.Drawing.Point(430, 458);
            this.btnAddCopy.Name = "btnAddCopy";
            this.btnAddCopy.Size = new System.Drawing.Size(120, 30);
            this.btnAddCopy.Text = "+ Add Copy";
            this.btnAddCopy.Click += new System.EventHandler(this.btnAddCopy_Click);

            this.btnDeleteCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeleteCopy.Location = new System.Drawing.Point(565, 458);
            this.btnDeleteCopy.Name = "btnDeleteCopy";
            this.btnDeleteCopy.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteCopy.Text = "- Delete Copy";
            this.btnDeleteCopy.Click += new System.EventHandler(this.btnDeleteCopy_Click);

            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(12, 510);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "< Close";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // Form Info
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDeleteCopy);
            this.Controls.Add(this.btnAddCopy);
            this.Controls.Add(this.txtCopyNum);
            this.Controls.Add(this.lblCopyNum);
            this.Controls.Add(this.lblSelectedBook);
            this.Controls.Add(this.dgvStock);
            this.Controls.Add(this.lblHeader);
            this.Name = "StockManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Book Inventory";
            this.Load += new System.EventHandler(this.StockManagerForm_Load);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSelectedBook;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Label lblCopyNum;
        private System.Windows.Forms.TextBox txtCopyNum;
        private System.Windows.Forms.Button btnAddCopy;
        private System.Windows.Forms.Button btnDeleteCopy;
        private System.Windows.Forms.Button btnBack;
    }
}