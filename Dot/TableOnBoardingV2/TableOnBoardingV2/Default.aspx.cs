using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace TableOnBoardingV2
{
    public partial class _Default : Page
    {
        private static string tableHeader;
        private static string fileNameUpload;
        private static string schemaPre;
        public static string SchemaPre
        {
            get { return schemaPre; }
            set { schemaPre = value; }
        }

        public static string TableHeader
        {
            get
            {
                return tableHeader;
            }
        }
        public static string FileNameUpload
        {
            set
            {
                fileNameUpload = value;
            }
            get
            {
                return fileNameUpload;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileExtensio = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                string FileType = FileUpload1.PostedFile.ContentType;
                string UploadURL = Server.MapPath("~/App_Data/");
                SchemaPre = TextBox1.Text + '.' + TextBox2.Text;
                FileNameUpload = SchemaPre;
                if (FileType == "application/octet-stream")
                {
                    try
                    {
                        if (!System.IO.Directory.Exists(UploadURL))
                        {
                            System.IO.Directory.CreateDirectory(UploadURL);
                        }
                        FileUpload1.PostedFile.SaveAs(UploadURL + FileNameUpload + ".tsv");
                    }
                    catch
                    {
                        Response.Write("Fail");
                    }
                }
                else
                {
                    Response.Write("Format error!");
                }
            }
            else
            {
                Response.Write("Please select file!");
            }
            System.IO.Stream uploadStream = FileUpload1.PostedFile.InputStream;
            System.IO.StreamReader sr = new System.IO.StreamReader(uploadStream);
            tableHeader = sr.ReadLine();
            sr.Close();

            Response.Redirect("TableSchemaV2.aspx");
            // Server.Transfer("TableSchemaPerform.aspx", true);
        }

    }
}