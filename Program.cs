using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Http.Headers;

namespace LibrarySystem
{
    internal static class Program
    {
        public static string ConnectionString = "Data Source=.;Initial Catalog=LibrarySystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
