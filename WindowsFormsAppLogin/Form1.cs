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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsAppLogin
{
    public partial class REGISTER : Form
    {
        public REGISTER()
        {
            InitializeComponent();
        }
        private async void registerbtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(usertxt.Text.Trim()) && !string.IsNullOrWhiteSpace(passwordtxt.Text.Trim()))
                {
                    using (MYDATABASEEntities dbcontext = new MYDATABASEEntities())
                    {
                        string username = usertxt.Text.Trim();
                        bool userexist = dbcontext.REGISTRATIONs.Any(s => s.USERNAME == username);
                        if (userexist)
                        {
                            MessageBox.Show("This user already exists in the database");
                        }
                        else
                        {
                            REGISTRATION user = new REGISTRATION
                            {
                                USERNAME = usertxt.Text.Trim(),
                                USERPASSWORD = PasswordHash(passwordtxt.Text.Trim())
                            };
                            dbcontext.REGISTRATIONs.Add(user);
                            await dbcontext.SaveChangesAsync();
                            MessageBox.Show("Successfully added");
                        }
                    }
                } 
                else
                {
                    MessageBox.Show("Username and password cannot be null");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured "+ ex.Message);
            }

        }

        private string PasswordHash(string pass)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(pass);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            LOGIN login = new LOGIN();
            login.Show();
            this.Hide();
        }
    }
}
