using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.IO;

namespace LiveSwitch.TextControl
{
    public partial class Email : Form
    {
        //VARIABLES
        string connectionString;
        SqlConnection sqlConnection;
        private MailMessage mail;
        //END

        public Email(MailMessage mail)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["LiveSwitch.TextControl.Properties.Settings.PersoneneConnectionString"].ConnectionString;
            this.mail = mail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendMail(mail);
        }

        private void sendMail(MailMessage mail)
        {
            try
            {
                string query = "SELECT * FROM personenDatenbank";
                SqlDataReader dtReader;
                using (sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    lstBoxEmail.Items.Clear();
                    sqlConnection.Open();
                    sqlCommand.ExecuteScalar();
                    dtReader = sqlCommand.ExecuteReader();
                    while (dtReader.Read())
                    {
                        lstBoxEmail.Items.Add(dtReader["E-Mail"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GC.Collect();
            try
            {
                foreach (string usrName in lstBoxEmail.Items)
                {
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.Host = SMTP_Server;
                    client.Port = SMTP_Port;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Username, Password);

                    mail.From = new MailAddress(Von);
                    mail.To.Add(new MailAddress(usrName));
                    mail.Subject = Betreff;

                    client.Send(mail);
                }
                MessageBox.Show("Newsletter gesendet");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string Username
        {
            get
            {
                return txtUsrName.Text.Trim();
            }
        }

        public string Password
        {
            get
            {
                return txtPasswd.Text.Trim();
            }
        }

        public string SMTP_Server
        {
            get
            {
                return txtSMTPC.Text.Trim();
            }
        }

        public int SMTP_Port
        {
            get
            {
                return Convert.ToInt32(txtSMTPPort.Text.Trim());
            }
        }

        public string Betreff
        {
            get
            {
                return txtBetreff.Text.Trim();
            }
        }

        public string Von
        {
            get
            {
                return txtVon.Text.Trim();
            }
        }
    }
}
