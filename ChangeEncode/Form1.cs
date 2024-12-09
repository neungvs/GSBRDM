using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace ChangeEncode
{
    public partial class FChangeEncode : Form
    {
        public FChangeEncode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void FChangeEncode_Load(object sender, EventArgs e)
        {
            if (File.Exists("LOG.txt"))
            {
                File.Delete("LOG.txt");
            }



            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBSQL"].ToString());
                SqlCommand cmddlx = new SqlCommand();
                DataTable table = new DataTable();
                string formatdate = "";

                string sqlStatementdx = "select Period from rdm_system where SystemId = 9";
                con.Open();
                cmddlx.CommandText = sqlStatementdx;
                cmddlx.Connection = con;

                SqlDataAdapter adapter = new SqlDataAdapter(cmddlx);
                adapter.Fill(table);
                adapter.Dispose();
                adapter = null;

                if (table.Rows.Count > 0)
                {
                    formatdate = table.Rows[0][0].ToString();
                }


                String localPathIN = ConfigurationManager.AppSettings["ftpPathLocal"].ToString() + ConfigurationManager.AppSettings["Filename1"].ToString() + "_" + formatdate + ".csv";  //@"D:/Webapp/RDM/MNF_CORP_BIS_RDM_CIR5144_20210930.csv";

                if (File.Exists(localPathIN))
                {
                    string[] allLines = File.ReadAllLines(localPathIN);

                    File.WriteAllLines(localPathIN, allLines, Encoding.GetEncoding("TIS-620"));
                }


                localPathIN = ConfigurationManager.AppSettings["ftpPathLocal"].ToString() + ConfigurationManager.AppSettings["Filename2"].ToString() + "_" + formatdate + ".csv";  //@"D:/Webapp/RDM/MNF_CORP_BIS_RDM_CIR5144_20210930.csv";

                if (File.Exists(localPathIN))
                {
                    string[] allLines = File.ReadAllLines(localPathIN);

                    File.WriteAllLines(localPathIN, allLines, Encoding.GetEncoding("TIS-620"));
                }

                //MessageBox.Show("Finish");

                using (FileStream fs = File.Create("LOG.txt"))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("Success");
                    fs.Write(title, 0, title.Length);
                }

                Close();

            }
            catch(Exception ex)
            {
                using (FileStream fs = File.Create("LOG.txt"))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(ex.ToString());
                    fs.Write(title, 0, title.Length);
                }

                Close();
            }


        }
    }
}
