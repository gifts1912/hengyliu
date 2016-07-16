using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace TableOnBoardingV2
{
    public class SchemaItem
    {
        private string _tableHeader;
        private bool _isSubject;
        private string _schema;
        private string _type;
        private bool _needIndex;
        private string _regexForValue;
        private string _nl;

        public SchemaItem(string tableHeader, bool isSubject, string schema, string type, bool needIndex, string regexForValue, string nl)
        {
            _tableHeader = tableHeader;
            _isSubject = IsSubject;
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

        public bool IsSubject
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
        
        public bool NeedIndex
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
    }
    public partial class TableSchemaPerform : System.Web.UI.Page
    {
        private string tableSchema = TableOnBoardingV2._Default.TableHeader;
        private List<string> TypeList = new List<string>(new string[3] { "string", "numeric", "bool" });
        
        private static List<SchemaItem> TableSchema = new List<SchemaItem>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                foreach(string ele in tableSchema.Split('\t'))
                {
                    TableSchema.Add(new SchemaItem(ele, true, "MS", "string", true, "", ele));
                }
                SchemaContent.DataSource = TableSchema;
                SchemaContent.DataBind();
            }
        }

        protected void AddNewRow_Click(object sender, EventArgs e)
        {
            TableSchema.Add(new SchemaItem("", false, "", "string", true, "", ""));
            SchemaContent.DataSource = TableSchema;
            SchemaContent.EditIndex = TableSchema.Count - 1;
            SchemaContent.DataBind();
        }
        protected void SchemaContent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SchemaContent.DataSource = TableSchema;
            SchemaContent.EditIndex = e.NewEditIndex;
            SchemaContent.DataBind();
        }

        protected void SchemaContent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableSchema.RemoveAt(e.RowIndex);
            SchemaContent.DataSource = TableSchema;
            SchemaContent.EditIndex = -1;
            SchemaContent.DataBind();
        }
        protected void SchemaContent_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            var row = SchemaContent.Rows[index];
            /*
            var th = row.FindControl("").Text;
            var ssb = Convert.ToBoolean((row.Cells[2].Controls[0] as TextBox).Text);
            var sch= (row.Cells[3].Controls[0] as TextBox).Text;
            var ty = (row.Cells[4].Controls[0] as TextBox).Text;
            var ni = Convert.ToBoolean((row.Cells[5].Controls[0] as TextBox).Text);
            var rv = (row.Cells[6].Controls[0] as TextBox).Text;
            var nl = (row.Cells[7].Controls[0] as TextBox).Text;
            */
            var th = (row.Cells[1].Controls[0] as TextBox).Text;
            var ssb = Convert.ToBoolean((row.Cells[2].Controls[0] as TextBox).Text);
            var sch= (row.Cells[3].Controls[0] as TextBox).Text;
            var ty = (row.Cells[4].Controls[0] as TextBox).Text;
            var ni = Convert.ToBoolean((row.Cells[5].Controls[0] as TextBox).Text);
            var rv = (row.Cells[6].Controls[0] as TextBox).Text;
            var nl = (row.Cells[7].Controls[0] as TextBox).Text;

            var editItem = TableSchema[index];
            editItem.TableHeader = th;
            editItem.IsSubject = ssb;
            editItem.Schema = sch;
            editItem.Type = ty;
            editItem.NeedIndex = ni;
            editItem.RegexForValue = rv;
            editItem.NL = nl;

            SchemaContent.DataSource = TableSchema;
            SchemaContent.EditIndex = -1;
            SchemaContent.DataBind();
        }

        protected void SchemaContent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SchemaContent.DataSource = TableSchema;
            SchemaContent.EditIndex = -1;
            SchemaContent.DataBind();
        }

    }
}
