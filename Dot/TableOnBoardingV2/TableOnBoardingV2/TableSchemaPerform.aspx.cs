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
        }

        public bool IsSubject
        {
            get
            {
                return _isSubject;
            }
        }

        public string Schema
        {
            get
            {
                return _schema;
            }
        }
        
        public string Type
        {
            get
            {
                return _type;
            }
        }
        
        public bool NeedIndex
        {
            get
            {
                return _needIndex;
            }
        }

        public string RegexForValue
        {
            get
            {
                return _regexForValue;
            }
        }

        public string NL
        {
            get
            {
                return _nl;
            }
        }
    }
    public partial class TableSchemaPerform : System.Web.UI.Page
    {
        private string tableSchema = TableOnBoardingV2._Default.TableHeader;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<SchemaItem> TableSchema = new List<SchemaItem>();
                foreach(string ele in tableSchema.Split('\t'))
                {
                    TableSchema.Add(new SchemaItem(ele, true, "MS", "string", true, "", ele));
                }
                SchemaContent.DataSource = TableSchema;
                SchemaContent.DataBind();
            }
        }
        /*
        protected void SchemaContent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeletedEventArgs e)
        {

        }
        */
    }
}
