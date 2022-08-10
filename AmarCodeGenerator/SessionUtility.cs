using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public static class SessionUtility
    {
        public static string DB_USER { get; set; }
        public static string DB_PASSWORD { get; set; }
        public static string DB_NAME { get; set; }
        public static string DB_SERVER_NAME { get; set; }
        public static string SQL_CONN_STRING { get; set; }
        public static SqlConnection connection { get; set; }

        public static string RootFolderName = ConfigurationManager.AppSettings["ROOTFOLDERNAME"].ToString();


        public static string NameSpaceFirstPart = ConfigurationManager.AppSettings["NAMESPACEFIRSTPART"].ToString();
        public static string ModuleName = ConfigurationManager.AppSettings["MODULENAME"].ToString();
        public static string DBConnectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        public static string ParentChildTables = ConfigurationManager.AppSettings["ParentChildTables"].ToString();
        public static Boolean IsModelInterfaceRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["ISMODELINTERFACEREQUIRED"].ToString());
        public static Boolean IsBLLInterfaceRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["ISBLLINTERFACEREQUIRED"].ToString());
        public static string ColumnsToSkip = ConfigurationManager.AppSettings["COLUMNSTOSKIP"].ToString();
        public static string DeleteColumn = ConfigurationManager.AppSettings["DELETECOLUMN"].ToString();
        public static string DeleteSupportingColumns = ConfigurationManager.AppSettings["DELETESUPPORTINGCOLUMNS"].ToString();
        public static string InsertSupportingColumns = ConfigurationManager.AppSettings["INSERTSUPPORTINGCOLUMNS"].ToString();
        public static string UpdateSupportingColumns = ConfigurationManager.AppSettings["UPDATESUPPORTINGCOLUMNS"].ToString();
        public static Boolean IsTableHasUnderline = Convert.ToBoolean(ConfigurationManager.AppSettings["IsTableHasUnderline"].ToString());
        public static int SkippingTableName = Convert.ToInt32(ConfigurationManager.AppSettings["SkippingTableName"].ToString());

        public static string SPFolderName = SessionUtility.RootFolderName + ConfigurationManager.AppSettings["SPFOLDERNAME"].ToString() + @"\";

        public static string ModelFolder = SessionUtility.RootFolderName + ConfigurationManager.AppSettings["MODEL"].ToString() + @"\";

        public static string ModelInterfaceFolder = RootFolderName + ConfigurationManager.AppSettings["IMODEL"].ToString() + @"\";

        public static string BLLFolder = RootFolderName + ConfigurationManager.AppSettings["BLL"].ToString() + @"\";

        public static string IBLLFolder = RootFolderName + ConfigurationManager.AppSettings["IBLL"].ToString() + @"\";

        public static string DataContextFolder = RootFolderName + ConfigurationManager.AppSettings["DATACONTEXT"].ToString() + @"\";

        public static string ViewsFolder = RootFolderName + "Views" + @"\";

        public static string ControllerFolder = RootFolderName + "Controller" + @"\";

        public static string RepsitoryFolder = RootFolderName + ConfigurationManager.AppSettings["REPOSITORY"].ToString() + @"\";

        public static string RepsitoryInterfaceFolder = RootFolderName + ConfigurationManager.AppSettings["REPOSITORYINTERFACE"].ToString() + @"\";


    }
}
