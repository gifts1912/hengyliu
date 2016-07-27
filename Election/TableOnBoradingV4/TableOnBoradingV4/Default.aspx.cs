using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace TableOnBoardingV4
{

    public class SchemaItem
    {
        private string _tableHeader;
        private string _isSubject;
        private string _schema;
        private string _type;
        private string _needIndex;
        private string _regexForValue;
        private string _nl;

        public SchemaItem(string tableHeader, string isSubject, string schema, string type, string needIndex, string regexForValue, string nl)
        {
            _tableHeader = tableHeader;
            _isSubject = isSubject;
            _schema = schema;
            _type = type;
            _needIndex = needIndex;
            _regexForValue = regexForValue;
            _nl = nl;
        }
        public string TableHeader
        {
            get
            {
                return _tableHeader;
            }
            set
            {
                _tableHeader = value;
            }
        }

        public string IsSubject
        {
            get
            {
                return _isSubject;
            }
            set
            {
                _isSubject = value;
            }
        }

        public string Schema
        {
            get
            {
                return _schema;
            }
            set
            {
                _schema = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public string NeedIndex
        {
            get
            {
                return _needIndex;
            }
            set
            {
                _needIndex = value;
            }
        }

        public string RegexForValue
        {
            get
            {
                return _regexForValue;
            }
            set
            {
                _regexForValue = value;
            }
        }

        public string NL
        {
            get
            {
                return _nl;
            }
            set
            {
                _nl = value;
            }
        }
        
        public static List<DataColumn> GenDataColumn()
        {
            //SchemaItem(string tableHeader, string isSubject, string schema, string type, string needIndex, string regexForValue, string nl)
            List<DataColumn> dc = new List<DataColumn>();
            dc.Add(new DataColumn("TableHeader", typeof(string)));
            dc.Add(new DataColumn("IsSubject", typeof(string)));
            dc.Add(new DataColumn("Schema", typeof(string)));
            dc.Add(new DataColumn("Type", typeof(string)));
            dc.Add(new DataColumn("NeedIndex", typeof(string)));
            dc.Add(new DataColumn("RegexForValue", typeof(string)));
            dc.Add(new DataColumn("NL", typeof(string)));
            return dc;
        }

        public object[] GenDataRow()
        {
            object[] arr = new object[7];
            arr[0] = TableHeader;
            arr[1] = IsSubject;
            arr[2] = Schema;
            arr[3] = Type;
            arr[4] = NeedIndex;
            arr[5] = RegexForValue;
            arr[6] = NL;
            return arr;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_Default.FileName);
            sb.Append('\t');
            sb.Append(_tableHeader);
            sb.Append('\t');
            sb.Append(_schema);
            sb.Append('\t');
            sb.Append(_type);
            sb.Append('\t');
            /*
            sb.Append(_isSubject);
            sb.Append('\t');
            */
            sb.Append(_needIndex);
            sb.Append('\t');
            sb.Append(_regexForValue);
            /*
            sb.Append('\t');
            sb.Append(_nl);
            */
            return sb.ToString();
        }
    }


    public partial class _Default : Page
    {
        private static string tableHeader;
        private static string fileNameUpload;
        private static string schemaPre;
        public static string BotName;
        public static string FileName;
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

        protected void ButtonGenerateSchema_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileExtensio = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                string FileType = FileUpload1.PostedFile.ContentType;
                string UploadURL = Server.MapPath("~/App_Data/");
                BotName = TextBox1.Text;
                FileName = TextBox2.Text;
                SchemaPre = TextBox1.Text + '.' + TextBox2.Text;
                //  FileNameUpload = SchemaPre;
                FileNameUpload = FileName;
                if (FileType == "application/octet-stream" || FileType == "text/plain")
                {
                    try
                    {
                        if (!System.IO.Directory.Exists(UploadURL))
                        {
                            System.IO.Directory.CreateDirectory(UploadURL);
                        }
                        FileUpload1.PostedFile.SaveAs(UploadURL + FileNameUpload + ".txt");
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

            Page_Load_TableSchemaV2();

            Comment_Text.Text = "IsSubject: Denote the TableHeader pri is the primary key that can distinct the entity from others in the table.";
            SchemaSubmit.Visible = true;
        }

        private string tableSchema = TableHeader;
        private static List<SchemaItem> TableSchema = new List<SchemaItem>();
        private static bool Original_Load = true;

        public void Page_Load_TableSchemaV2()
        {
            TableSchema.Clear();
            Original_Load = false;
            foreach (string ele in TableHeader.Split('\t'))
            {
                string schemaNormalize = SchemaGenerate(ele);
                string schemaCur = string.Format("bot:{0}.{1}.{2}", BotName, FileName, schemaNormalize);
                TableSchema.Add(new SchemaItem(ele, "False", schemaCur, "String", "True", "", ele));
            }
            //BindGridView();
            /**** import method upper ****/
            ShowGrid();
        }

        public string SchemaGenerate(string feaName)
        {
            feaName = feaName.ToLower();
            feaName = Regex.Replace(feaName, @"\s+", "_");
            return feaName;
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["CurrentTime"] = Session["CurrentTime"];
        }

        private DataTable TableSchemaToDataTable(List<SchemaItem> tableSchema)
        {
            DataTable dt = new DataTable("HeaderSchema");
            List<DataColumn> dcs = SchemaItem.GenDataColumn();
            foreach(DataColumn dc in dcs)
            {
                dt.Columns.Add(dc);
            }            
            /*
            dt.Columns.Add("TableHeader", typeof(string));
            dt.Columns.Add("IsSubject", typeof(string));
            dt.Columns.Add("Schema", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("NeedIndex", typeof(string));
            dt.Columns.Add("RegexForValue", typeof(string));
            dt.Columns.Add("NL", typeof(string));
            */
            for (int i = 0; i < tableSchema.Count(); i++)
            {
                object [] array = tableSchema[i].GenDataRow();
                dt.Rows.Add(array);
            }
            return dt;
        }
        private void BindGridView()
        {
            DataTable dt = TableSchemaToDataTable(TableSchema) ;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void UpdateOrAddNewRecord(string tableHeader, string isSubject, string schema, string typeCur, string needIndex, string regexvalue, string nl, bool isUpdate, int posIdx)
        {
            if (isUpdate && posIdx < TableSchema.Count && posIdx >= 0)
            {
                SchemaItem item = TableSchema[posIdx];
                item.TableHeader = tableHeader;
                item.IsSubject = isSubject;
                item.Schema = schema;
                item.Type = typeCur;
                item.NeedIndex = needIndex;
                item.RegexForValue = regexvalue;
                item.NL = nl;
            }
            else
            {
                if (ViewState["CurrentTime"].ToString() != Session["CurrentTime"].ToString())
                    return;
                SchemaItem item = new SchemaItem(tableHeader, isSubject, schema, typeCur, needIndex, regexvalue, nl);
                TableSchema.Add(item);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            string th = (row.Cells[1]).Text;

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idx = e.RowIndex;
            string th = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text;

            //DropDownList ddl_isSubject = (DropDownList)GridView1.Rows[e.RowIndex].Cells[1].FindControl("DropDownList_IsSubject");
            //RadioButtonList ddl_isSubject = (RadioButtonList)GridView1.Rows[e.RowIndex].Cells[1].FindControl("RadioButtonList_IsSubject");
            CheckBox cb_IsSubject = (CheckBox)GridView1.Rows[e.RowIndex].Cells[1].FindControl("CheckBox_IsSubject");
            string isSubject = "False";
            if(cb_IsSubject.Checked)
            {
                isSubject = "True";
            }

            string schema = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            DropDownList ddl = (DropDownList)GridView1.Rows[e.RowIndex].Cells[3].FindControl("DropDownList_Type");
            string type = ddl.SelectedItem.Text;

            CheckBox cb = (CheckBox)GridView1.Rows[e.RowIndex].Cells[4].FindControl("CheckBox_NeedIndex");
            string needIndex = "True";
            if (!cb.Checked)
                needIndex = "False";

            string regexForValue = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string nl = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            UpdateOrAddNewRecord(th, isSubject, schema, type, needIndex, regexForValue, nl, true, idx);
            GridView1.EditIndex = -1;
            ShowGrid();
        }

        private bool StringToBool(string str, bool def)
        {
            bool result = def;
            if (!def && !string.IsNullOrEmpty(str) && (str.ToLower() == "yes" || str.ToLower() == "true"))
            {
                result = true;
            }

            if (def && !string.IsNullOrEmpty(str) && (str.ToLower() == "no" || str.ToLower() == "false"))
            {
                result = false;
            }
            return result;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idx = e.RowIndex;
            TableSchema.RemoveAt(idx);
            BindGridView();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }


        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlType = (DropDownList)e.Row.FindControl("DropDownList_Type");

                List<string> typeList = new List<string>(new string[3] { "String", "Bool", "Numeric" });
                ddlType.DataSource = typeList;
                ddlType.DataTextField = "type";
                ddlType.DataValueField = "type";
                ddlType.DataBind();
                ddlType.Items.FindByValue((e.Row.FindControl("DropDownList_Type") as Label).Text).Selected = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                DropDownList ddlCategories = e.Row.FindControl("DropDownList_Type") as DropDownList;
                if (ddlCategories != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    ddlCategories.SelectedValue = drv["Type"].ToString();
                }
            }
        }

        protected void DropDownList_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SchemaSubmit_Click(object sender, EventArgs e)
        {
            string UploadURL = Server.MapPath("~/App_Data/");
            string saveAsName = String.Format("{0}.Schema.txt", FileNameUpload);
            using (Stream writer = new FileStream(UploadURL + saveAsName, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(writer))
                {
                    foreach (SchemaItem si in TableSchema)
                    {
                        sw.WriteLine(si.ToString());
                    }
                    sw.Flush();
                }
            }

            SubmitResult.Text = "  Submit Success!";
            string cmdStr = "/in PhoneInfo:PhoneInfo.txt /def header.schema.txt";
            string[] cmdArgs = cmdStr.Split(' ');
            string botFile = string.Format("{0}:{1}.txt", BotName, FileName);
            GenTableGraphFromTSV.Run(botFile, saveAsName, UploadURL);
            //TwentySeg.QnA.GenTableGraphFromTSV.Run(cmdArgs, UploadURL); 
        }
        protected void EntityInforsExtract()
        {

        }

        protected void ButtonHaveNoUse_Click(object sender, EventArgs e)
        {
            return;
        }

        protected void DropDownList_IsSubject_DataBinding(object sender, EventArgs e)
        {
        }

        private void ShowGrid()
        {
            DataTable dt = TableSchemaToDataTable(TableSchema) ;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        static DataTable GenerateDataTable(string colName, string[] rows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(colName, typeof(string));
            for (int i = 0; i < rows.Count(); i++)
            {
                dt.Rows.Add(rows[i]);
            }
            return dt;
        }
    }

}