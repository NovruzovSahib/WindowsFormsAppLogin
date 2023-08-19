using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppLogin.Model;

namespace WindowsFormsAppLogin
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            using (MYDATABASEEntities dbcontext = new MYDATABASEEntities())
            {

                string username = usertxt.Text.Trim();
                string password = PasswordHash(passwordtxt.Text.Trim());

                var user = dbcontext.REGISTRATIONs.FirstOrDefault(s => s.USERNAME == username && s.USERPASSWORD == password);

                if (user!=null)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Username or password is not correct");
                }

            }
        }
        private string PasswordHash(string pass)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(pass);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}
