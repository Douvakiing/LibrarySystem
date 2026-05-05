using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text="LibrarySystem";
        }

        private void btnManageBooksPage_Click_1(object sender, EventArgs e)
        {
            // This creates the new window and pops it up
            BookManagerForm bookForm = new BookManagerForm();
            bookForm.Show();
            this.Hide();
        }
        private void btnAdminPanelPage_Click(object sender, EventArgs e)
        {
            AdminPanelForm adminpanel = new AdminPanelForm();
            adminpanel.Show();
            this.Hide();
        }

        private void btnManageMembersPage_Click(object sender, EventArgs e)
        {
            MemberDirectoryForm memberForm = new MemberDirectoryForm();
            memberForm.Show();
            this.Hide();
        }

        private void btnMainDeskPage_Click(object sender, EventArgs e)
        {
            MainDeskForm maindesk = new MainDeskForm();
            maindesk.Show();
            this.Hide();
        }
    }
}

