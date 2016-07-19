using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace TableOnBoardingV2
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_tableHeader);
            sb.Append('\t');
            sb.Append(_isSubject);
            sb.Append('\t');
            sb.Append(_schema);
            sb.Append('\t');
            sb.Append(_type);
            sb.Append('\t');
            sb.Append(_needIndex);
            sb.Append('\t');
            sb.Append(_regexForValue);
            sb.Append('\t');
            sb.Append(_nl);
            return sb.ToString();
        }
    }
    public partial class TableSchemaV2 : System.Web.UI.Page
    {
        private string tableSchema = TableOnBoardingV2._Default.TableHeader;
        private static List<SchemaItem> TableSchema = new List<SchemaItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (string ele in tableSchema.Split('\t'))
                {
                    string schemaCur = string.Format("{0}.{1}", TableOnBoardingV2._Default.SchemaPre, ele);
                    TableSchema.Add(new SchemaItem(ele, "True", schemaCur, "String", "True", "", ele));
                }
                BindGridView();
            }
        }

        private void BindGridView()
        {
            GridView1.DataSource = TableSchema;
            GridView1.DataBind();
            DropDownList ddl;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string curType = TableSchema[i].Type;
                if (curType == "String" || curType == "1")
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_Type");
                    ddl.SelectedIndex = 0;
                    ddl.SelectedItem.Text = "String";
                }
                else if (curType == "Bool" || curType == "2")
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_Type");
                    ddl.SelectedIndex = 1;
                    ddl.SelectedItem.Text = "Bool";
                }
                else
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_Type");
                    ddl.SelectedIndex = 2;
                    ddl.SelectedItem.Text = "Numeric";
                }

                String isSubject = TableSchema[i].IsSubject;
                if (isSubject.ToLower() == "true" || isSubject == "1")
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_IsSubject");
                    ddl.SelectedIndex = 0;
                    ddl.SelectedItem.Text = "True";
                }
                else
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_IsSubject");
                    ddl.SelectedIndex = 1;
                    ddl.SelectedItem.Text = "False";
                }

                string needIndex = TableSchema[i].NeedIndex;
                if (needIndex.ToLower() == "true" || isSubject == "1")
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_NeedIndex");
                    ddl.SelectedIndex = 0;
                    ddl.SelectedItem.Text = "True";
                }
                else
                {
                    ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList_NeedIndex");
                    ddl.SelectedIndex = 1;
                    ddl.SelectedItem.Text = "False";
                }
            }

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

            DropDownList ddl_isSubject = (DropDownList)GridView1.Rows[e.RowIndex].Cells[1].FindControl("DropDownList_IsSubject");
            string isSubject = ddl_isSubject.SelectedItem.Text;

            string schema = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            DropDownList ddl = (DropDownList)GridView1.Rows[e.RowIndex].Cells[3].FindControl("DropDownList_Type");
            string type = ddl.SelectedItem.Text;

            DropDownList ddl_needIndex = (DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("DropDownList_NeedIndex");
            string needIndex = ddl_isSubject.SelectedItem.Text;

            string regexForValue = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string nl = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            /*
            int idx = e.RowIndex;
            int innerIdx = GridView1.Rows[idx].RowIndex;

            string th = (string)GridView1.DataKeys[e.RowIndex]["TableHeader"];
            bool isSubject = (bool)GridView1.DataKeys[idx]["IsSubject"];
            string schema = (string)GridView1.DataKeys[idx]["Schema"];
            string type = (string)GridView1.DataKeys[e.RowIndex]["Type"];
            bool needIndex = (bool)GridView1.DataKeys[idx]["NeedIndex"];
            string regexForValue = (string)GridView1.DataKeys[idx]["RegexForValue"];
            string nl = (string)GridView1.DataKeys[idx]["NL"];
            */

            UpdateOrAddNewRecord(th, isSubject, schema, type, needIndex, regexForValue, nl, true, idx);
            GridView1.EditIndex = -1;
            BindGridView();
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
            /* string th = ((TextBox)GridView1.FooterRow.FindControl("TableHeader")).Text;
             bool isSubject = StringToBool(((TextBox)GridView1.FooterRow.FindControl("IsSubject")).Text, false);
             string schema = ((TextBox)GridView1.FooterRow.FindControl("Schema")).Text;
             string type = ((TextBox)GridView1.FooterRow.FindControl("Type")).Text;
             bool needIndex = StringToBool(((TextBox)GridView1.FooterRow.FindControl("NeedIndex")).Text, true);
             string regexForValue = ((TextBox)GridView1.FooterRow.FindControl("RegexForValue")).Text;
             string nl = ((TextBox)GridView1.FooterRow.FindControl("NL")).Text;
             UpdateOrAddNewRecord(th, isSubject, schema, type, needIndex, regexForValue, nl, false, TableSchema.Count-1);
             BindGridView();
             */
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            UpdateOrAddNewRecord(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, false, TableSchema.Count);
            BindGridView();
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
            string saveAsName = String.Format("{0}.Schema.tsv", TableOnBoardingV2._Default.FileNameUpload);
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

            SubmitResult.Text = "Submit Success!";
            
        }
    }
}