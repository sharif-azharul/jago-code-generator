
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
////using Oracle.DataAccess.Client;
//using System.Data.SqlClient;
////using Oracle.DataAccess.Types;
//using System.Xml;
//using System.IO;
//using System.Text;
//using System.Runtime.InteropServices;

//namespace AmarDBBase
//{
//    public class AmarDBBase// : AmarBase
//    {

//        #region "  Class Specific  "

//        private string pstr_ClientRegKey = "DEFAULT";
//        private AmarConnection pcon_AmarConn = null;
//        private bool disposed = false;

//        private bool pbln_CallerSentConnection = false;
//        #region "  Public Properties  "

//        public string ClientRegCode
//        {
//            get { return pcon_AmarConn.ClientRegistryCode; }
//            set { pcon_AmarConn.ClientRegistryCode = value; }
//        }

//        public bool UsingMaskUser
//        {
//            get { return pcon_AmarConn.UsingMaskedDBUser; }
//            set { pcon_AmarConn.UsingMaskedDBUser = value; }
//        }

//        public virtual string UserName
//        {
//            get { return pcon_AmarConn.DBUser; }
//            set { pcon_AmarConn.DBUser = value; }
//        }

//        public virtual string Password
//        {
//            get { return pcon_AmarConn.DBPassword; }
//            set { pcon_AmarConn.DBPassword = value; }
//        }

//        public virtual string DataSource
//        {
//            get { return pcon_AmarConn.DBDsn; }
//            set { pcon_AmarConn.DBDsn = value; }
//        }

//        //Note: This property returns object so that derived
//        //      classes can override it as a Client Specific Connection Class 
//        public virtual object Connection
//        {
//            get { return pcon_AmarConn; }
//        }

//        //this property to be used when non destructive object model is used on multiple dbs (see HHStayB)
//        public System.Data.ConnectionState ConnectionState
//        {
//            get
//            {
//                if (pcon_AmarConn.MyOraConn == null)
//                {
//                    return System.Data.ConnectionState.Closed;
//                }
//                else
//                {
//                    return pcon_AmarConn.MyOraConn.State;
//                }
//            }
//        }

//        public virtual SqlTransaction Transaction
//        {
//            get { return pcon_AmarConn.MyOraTrans; }
//        }

//        public virtual bool InTransaction
//        {
//            get { return pcon_AmarConn.MyOraTrans != null; }
//        }

//        public virtual Int32 QueryTimeOut
//        {
//            get { return pcon_AmarConn.QueryTimeout; }
//            set { pcon_AmarConn.QueryTimeout = value; }
//        }

//        public virtual bool PreserveXMLWhiteSpace
//        {
//            get { return pcon_AmarConn.PreserveWhiteSpace; }
//            set { pcon_AmarConn.PreserveWhiteSpace = value; }
//        }

//        #endregion

//        #region "  Constructors\Destructors  "

//        //Constructor 1

//        public AmarDBBase(ref object rcon_Conn)
//        {
//            pcon_AmarConn = (AmarConnection)rcon_Conn;
//            pbln_CallerSentConnection = true;
//            var _with1 = pcon_AmarConn;
//            //Set the user, pwd , and DSN
//            this.UserName = _with1.DBUser;
//            this.Password = _with1.DBPassword;
//            this.DataSource = _with1.DBDsn;
//            pstr_ClientRegKey = _with1.ClientRegistryCode;

//        }

//        public AmarDBBase(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pcon_AmarConn = new AmarConnection(vstr_User, vstr_Password, vstr_DSN, vstr_Clientcode, robj_PWD);
//        }


//        public AmarDBBase(string vstr_User, string vstr_Password, string vstr_DSN, bool vb_ForceConn, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pcon_AmarConn = new AmarConnection(vstr_User, vstr_Password, vstr_DSN, vb_ForceConn, vstr_Clientcode, robj_PWD);
//        }

//        //Constructor 3
//        public AmarDBBase(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_Enlist, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pcon_AmarConn = new AmarConnection(vstr_User, vstr_Password, vstr_DSN, vstr_Enlist, vstr_Clientcode, robj_PWD);
//        }

//        public AmarDBBase(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_Enlist, bool vb_ForceConn, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pcon_AmarConn = new AmarConnection(vstr_User, vstr_Password, vstr_DSN, vstr_Enlist, vb_ForceConn, vstr_Clientcode, robj_PWD);
//        }

//        public void Dispose()
//        {
//            pDispose(true);
//            //base.Dispose();
//        }

//        //Destructor
//        private void pDispose(bool disposing)
//        {
//            //Dispose resources.
//            try
//            {
//                if (!this.disposed)
//                {
//                    if (disposing)
//                    {
//                        //Dispose resources.
//                        try
//                        {
//                            if (pbln_CallerSentConnection == false)
//                            {
//                                pcon_AmarConn.Dispose();
//                            }
//                        }
//                        catch (System.Exception excp)
//                        {
//                            // Not much we can do here
//                        }
//                        //base.Dispose();
//                    }
//                }
//                disposed = true;
//            }
//            catch (Exception ex)
//            {
//            }
//        }

//        #endregion

//        #endregion

//        #region "  Protected  "

//        //Note: This method is here so that you still have access to the base Oracle Connection when/if
//        //      you decide to override the Connection property in a derived class 
//        protected SqlConnection GetCurrentOracleConnection()
//        {

//            return pcon_AmarConn.MyOraConn;

//        }

//        //This method is used by derived classes to allow "In Trans" fucntionality
//        protected void SetOracleTransaction(ref SqlTransaction robj_Trans)
//        {
//            pcon_AmarConn.SetTransaction(ref robj_Trans);
//        }

//        protected virtual SqlCommand prepareCommand(string vstr_CommandText, string[] varr_Parameters)
//        {

//            SqlCommand lobj_Cmd = null;
//            string[] larr_Split = null;
//            int i = 0;

//            //Check array parameters match to variables in string
//            if (getVarCount(vstr_CommandText) != varr_Parameters.Length)
//            {
//                throw new Exception("Variables and parameters must be equal for prepared statements");
//            }

//            lobj_Cmd = pcon_AmarConn.MyOraConn.CreateCommand();
//            lobj_Cmd.CommandType = CommandType.Text;
//            lobj_Cmd.CommandText = vstr_CommandText;
//            //lobj_Cmd.BindByName = true;

//            for (i = 0; i <= varr_Parameters.GetUpperBound(0); i++)
//            {
//                larr_Split = varr_Parameters[i].Split('|');

//                if (larr_Split[1].Length != 0)
//                {
//                    lobj_Cmd.Parameters.Add(larr_Split[0], (DbType)larr_Split[2]).Value = larr_Split[1];
//                }
//                else
//                {
//                    //Try to insert a DBNULL 
//                    lobj_Cmd.Parameters.Add(larr_Split(0), (OracleDbType)larr_Split(2));
//                }

//                larr_Split = null;
//            }

//            lobj_Cmd.Prepare();

//            return lobj_Cmd;

//        }


//        protected virtual void pExecuteNonQuery(string vstr_SQL, ref Int32 vint_NumRowsAffected,  ref SqlCommand robj_Command)
//        {
//            SqlCommand lobj_Cmd = null;
//            bool lbln_CallerSentCommand = false;

//            //Set the context user for DB triggers
//            //setUserContext();

//            if (robj_Command == null)
//            {
//                lobj_Cmd = pcon_AmarConn.MyOraConn.CreateCommand();
//                lobj_Cmd.CommandType = CommandType.Text;
//                lobj_Cmd.CommandText = vstr_SQL;
//            }
//            else
//            {
//                lobj_Cmd = robj_Command;
//                lbln_CallerSentCommand = true;
//            }

//            if (pcon_AmarConn.QueryTimeout > 0)
//                lobj_Cmd.CommandTimeout = pcon_AmarConn.QueryTimeout;

//            vint_NumRowsAffected = lobj_Cmd.ExecuteNonQuery();

//            if (lbln_CallerSentCommand == false)
//                lobj_Cmd.Dispose();
//            lobj_Cmd = null;

//        }

//        protected virtual void alterSession(string vstr_parameter, string vstr_paramValue)
//        {
//            SqlCommand lobj_Cmd = null;
//            pExecuteNonQuery("Alter Session Set " + vstr_parameter + " = " + vstr_paramValue, 0,  lobj_Cmd );
//        }
//        #endregion

//        #region "  Public Methods  "
//        public void openSoftConnection()
//        {
//            pcon_AmarConn.softConnect();
//        }

//        public void closeSoftConnection()
//        {
//            pcon_AmarConn.softDisconnect();
//        }

//        public void SetPWDObject(ref AmarPWD robj_PWD)
//        {
//            pcon_AmarConn.SetPWDObject(ref robj_PWD);
//        }

//        public object ReturnOracleCommand()
//        {
//            SqlCommand lobj_Cmd = this.GetCurrentOracleConnection().CreateCommand();
//            lobj_Cmd.CommandTimeout = this.QueryTimeOut;
//            return lobj_Cmd;
//        }

//        public object ReturnOracleConnection()
//        {
//            return this.GetCurrentOracleConnection();
//        }

//        public void setSessionCommitParameter(string vstr_newValue)
//        {
//            alterSession("COMMIT_WRITE", vstr_newValue);
//        }

//        public virtual void changeDBConnection(string vstr_User, string vstr_Password, string vstr_DSN)
//        {
//            pcon_AmarConn.changeDBConnection(vstr_User, vstr_Password, vstr_DSN);

//        }

//        public virtual void closeConnection()
//        {
//            if ((pcon_AmarConn.MyOraConn != null) && pcon_AmarConn.MyOraConn.State != System.Data.ConnectionState.Closed)
//            {
//                pcon_AmarConn.MyOraConn.Close();
//            }
//        }

//        public virtual void beginTransaction()
//        {
//            if (pbln_CallerSentConnection == false)
//            {
//                pcon_AmarConn.beginTransaction();
//            }
//        }

//        public virtual void commitTransaction()
//        {
//            if (pbln_CallerSentConnection == false)
//            {
//                pcon_AmarConn.commitTransaction();
//            }
//        }

//        public virtual void rollbackTransaction()
//        {
//            if (pbln_CallerSentConnection == false)
//            {
//                pcon_AmarConn.rollbackTransaction();
//            }
//        }


//        #region "  SQL to Data Reader  "
//        public IDataReader SQLtoDataReader(string vstr_SQL)
//        {
//            return pExecuteDataReader(vstr_SQL);
//        }

//        public IDataReader prepSQLtoDataReader(string vstr_CommandText, string[] varr_Parameters)
//        {
//            using (SqlCommand vcmd_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                return pExecuteDataReader("", ref vcmd_Command);
//            }
//        }
//        #endregion

//        #region "  SQLtoDataSet  "
//        //Overload 1
//        public DataSet SQLtoDataSet(string vstr_SQL)
//        {
//            return pExecuteDataSet(vstr_SQL, "", "");
//        }

//        //Overload 2
//        public DataSet SQLtoDataSet(string vstr_SQL, string vstr_DataSetName)
//        {
//            return pExecuteDataSet(vstr_SQL, vstr_DataSetName, "");
//        }

//        //Overload 3
//        public DataSet SQLtoDataSet(string vstr_SQL, string vstr_DataSetName, string vstr_DataBaseTableName)
//        {
//            return pExecuteDataSet(vstr_SQL, vstr_DataSetName, vstr_DataBaseTableName);
//        }

//        //Overload 1
//        public DataSet prepSQLtoDataSet(string vstr_CommandText, string[] varr_Parameters)
//        {

//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                return pExecuteDataSet("", "", "", ref lobj_Command);
//            }
//        }

//        //Overload 2
//        public DataSet prepSQLtoDataSet(string vstr_CommandText, string[] varr_Parameters, string vstr_DataSetName)
//        {

//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                return pExecuteDataSet("", vstr_DataSetName, "", ref lobj_Command);
//            }
//        }

//        //Overload 3
//        public DataSet prepSQLtoDataSet(string vstr_CommandText, string[] varr_Parameters, string vstr_DataSetName, string vstr_DataBaseTableName)
//        {

//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                return pExecuteDataSet("", vstr_DataSetName, vstr_DataBaseTableName, ref lobj_Command);
//            }
//        }
//        #endregion

//        #region "  SQLto1Value  "
//        public string SQLto1Value(string vstr_SQL)
//        {
//            return pExecuteScalar(vstr_SQL);
//        }

//        public string prepSQLto1Value(string vstr_CommandText, string[] varr_Parameters)
//        {
//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                return pExecuteScalar("", ref lobj_Command);
//            }
//        }
//        #endregion

//        #region "  SQLtoXML  "
//        //Overload 1
//        public string SQLtoXML(string vstr_SQL)
//        {
//            return pExecuteXMLQuery(vstr_SQL, "", "");
//        }

//        //Overload 2
//        public string SQLtoXML(string vstr_SQL, string vstr_SubNodeName)
//        {
//            return pExecuteXMLQuery(vstr_SQL, "", vstr_SubNodeName);
//        }

//        //Overload 3
//        public string SQLtoXML(string vstr_SQL, string vstr_RootNodeName, string vstr_SubNodeName)
//        {
//            return pExecuteXMLQuery(vstr_SQL, vstr_RootNodeName, vstr_SubNodeName);
//        }

//        //Overload 1
//        public string prepSQLtoXML(string vstr_CommandText, string[] varr_Parameters)
//        {
//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                DataSet lobj_DS = pExecuteDataSet("", "", "", ref lobj_Command);
//                return pGetXMLFromDataSet(ref lobj_DS, "", "");
//            }
//        }

//        //Overload 2
//        public string prepSQLtoXML(string vstr_CommandText, string[] varr_Parameters, string vstr_SubNodeName)
//        {
//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                DataSet lobj_DS = pExecuteDataSet("", "", "", ref lobj_Command);
//                return pGetXMLFromDataSet(ref lobj_DS, "", vstr_SubNodeName);
//            }
//        }

//        //Overload 3
//        public string prepSQLtoXML(string vstr_CommandText, string[] varr_Parameters, string vstr_RootNodeName, string vstr_SubNodeName)
//        {
//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                DataSet lobj_DS = pExecuteDataSet("", "", "", ref lobj_Command);
//                return pGetXMLFromDataSet(ref lobj_DS, vstr_RootNodeName, vstr_SubNodeName);
//            }
//        }

//        #endregion

//        #region "  SQLWrite  "
//        //Overload 1
//        public virtual void SQLWrite(string vstr_SQL)
//        {
//            Int32 lint_Rows = 0;
//            this.pExecuteNonQuery(vstr_SQL, lint_Rows);
//        }

//        //Overload 2
//        public virtual void SQLWrite(string vstr_SQL, ref Int32 vint_NumRowsAffected)
//        {
//            this.pExecuteNonQuery(vstr_SQL, vint_NumRowsAffected);
//        }

//        //Overload 1
//        public virtual void prepSQLWrite(string vstr_CommandText, string[] varr_Parameters)
//        {
//            Int32 lint_Rows = 0;
//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                this.pExecuteNonQuery("", ref lint_Rows, ref lobj_Command);
//            }
//        }

//        //Overload 2
//        public virtual void prepSQLWrite(string vstr_CommandText, string[] varr_Parameters, ref Int32 vint_NumRowsAffected)
//        {
//            using (SqlCommand lobj_Command = prepareCommand(vstr_CommandText, varr_Parameters))
//            {
//                this.pExecuteNonQuery("", ref vint_NumRowsAffected, ref lobj_Command);
//            }
//        }

//        #endregion

//        #region "  DatasetToXML  "
//        //Overload 1
//        public string getXMLFromDataSet(DataSet vobj_DataSet)
//        {
//            return pGetXMLFromDataSet(ref vobj_DataSet, "", "");
//        }

//        //Overload 2
//        public string getXMLFromDataSet(DataSet vobj_DataSet, string vstr_RootNodeName)
//        {
//            return pGetXMLFromDataSet(ref vobj_DataSet, vstr_RootNodeName, "");
//        }

//        //Overload 3
//        public string getXMLFromDataSet(DataSet vobj_DataSet, string vstr_RootNodeName, string vstr_SubNodeName)
//        {
//            return pGetXMLFromDataSet(ref vobj_DataSet, vstr_RootNodeName, vstr_SubNodeName);
//        }
//        #endregion

//        #region "  Sequencer Specific  "

//        public string getNextSequenceValue(string vstr_SequenceName)
//        {

//            string lstr_SQL = "SELECT " + vstr_SequenceName + ".nextval FROM dual";
//            return pExecuteScalar(lstr_SQL);

//        }

//        public string getCurrentSequenceValue(string vstr_SequenceName)
//        {

//            string lstr_SQL = "SELECT " + vstr_SequenceName + ".currval FROM dual";

//            //This command only works if the connection is inTrans
//            if ((pcon_AmarConn.MyOraTrans != null))
//            {
//                return pExecuteScalar(lstr_SQL);
//            }
//            else
//            {
//                return "";
//            }

//        }

//        #endregion

//        #region "  Command Specific  "

//        public string prepParameterString(string vstr_Name, string vstr_Value, SqlDbType vtyp_Type)
//        {

//            return vstr_Name + "|" + vstr_Value + "|" + vtyp_Type;

//        }

//        #endregion

//        #endregion

//        #region "  Private Methods  "

//        //PJC - Commented out becauase we are now retiving PWD inof from the AmarPWD object
//        //Private Function ProcessMaskPassword(ByVal vstr_UserID As String, _
//        //		  ByVal vstr_DSN As String) As String

//        //	'NOTE: This user should have access to the security tables only for decryption purposes
//        //	Dim lstr_MasterUser As String = ""
//        //	Dim lstr_MasterPassword As String = ""
//        //	Dim lcon_Conn As OracleConnection = Nothing
//        //	Dim lobj_Command As OracleCommand = Nothing
//        //	Dim lstr_Return As String = ""

//        //	Call pobj_PWD.getMasterCredentials(lstr_MasterUser, lstr_MasterPassword)

//        //	'Uppercase the user & dsn
//        //	vstr_UserID = vstr_UserID.ToUpper()
//        //	vstr_DSN = vstr_DSN.ToUpper()

//        //	Try
//        //		'Get a DB connection
//        //		lcon_Conn = New OracleConnection
//        //		lcon_Conn.ConnectionString = "User ID=" & lstr_MasterUser & " ;Password=" & lstr_MasterPassword & ";Data Source=" & vstr_DSN
//        //		lcon_Conn.Open()

//        //		'Use this command object with the master user to run your intial queries
//        //		lobj_Command = lcon_Conn.CreateCommand

//        //		If Me.validateUser(vstr_UserID, lobj_Command) = False Then
//        //			Throw New AmarSystemException("Error retrieving Mask User password from DB")
//        //		Else
//        //			lstr_Return = Me.GetPassword(vstr_UserID, lobj_Command)
//        //		End If

//        //	Catch ex As Exception
//        //		Throw New AmarSystemException("Error in Master User Connection:" & vbCrLf & ex.Message)
//        //	Finally
//        //		Try
//        //			If lobj_Command IsNot Nothing Then
//        //				lobj_Command.Dispose()
//        //				lobj_Command = Nothing
//        //			End If
//        //			If lcon_Conn IsNot Nothing Then
//        //				lcon_Conn.Close()
//        //				lcon_Conn.Dispose()
//        //				lcon_Conn = Nothing
//        //			End If
//        //		Catch ex As Exception
//        //			'Nothing to do here
//        //		End Try
//        //	End Try

//        //	Return lstr_Return

//        //End Function

//        //Private Function validateUser(ByVal vstr_User As String, _
//        //		 ByRef robj_Command As OracleCommand) As Boolean
//        //	Dim lsb_SQL As New System.Text.StringBuilder

//        //	lsb_SQL.Append("SELECT COUNT(*) AS RecordCount")
//        //	lsb_SQL.Append(" FROM T_SYS_USER")
//        //	lsb_SQL.Append(" WHERE USER_ID = '" & vstr_User & "'")
//        //	lsb_SQL.Append(" AND STATUS_CD = 'A'")

//        //	Return CType(pExecuteScalar(lsb_SQL.ToString, robj_Command), Boolean)

//        //End Function

//        //Private Function GetPassword(ByVal vstr_User As String, _
//        //		ByRef robj_Command As OracleCommand) As String

//        //	Dim lsb_SQL As New System.Text.StringBuilder

//        //	lsb_SQL.Append("SELECT GET_PASSWORD(APPLICATION_PASSWORD) as password")
//        //	lsb_SQL.Append(" FROM SYSTEM_USER")
//        //	lsb_SQL.Append(" WHERE USER_ID = '" & vstr_User & "'")
//        //	lsb_SQL.Append(" AND STATUS_CD = 'A'")

//        //	Return pExecuteScalar(lsb_SQL.ToString, robj_Command).ToString

//        //End Function

//        private int getVarCount(string vstr_CommandText)
//        {

//            int i = 0;
//            int lint_Count = 0;

//            do
//            {
//                i = vstr_CommandText.IndexOf(":", i);

//                if (i >= 0)
//                {
//                    lint_Count += 1;
//                    i += 1;
//                }
//            } while (i > 0);

//            return lint_Count;

//        }

//        private string pGetXMLFromDataSet(ref DataSet robj_DataSet, string vstr_RootNodeName, string vstr_SubNodeName)
//        {
//            string lstr_XML = "";
//            StringWriter xmlStringWriter = new StringWriter();
//            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(xmlStringWriter);
//            Int32 x = 0;
//            Int32 y = 0;
//            Int32 z = 0;
//            Type stringType = Type.GetType("System.String");


//            try
//            {
//                if (robj_DataSet.Tables[0].Rows.Count > 0)
//                {
//                    xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
//                    xmlTextWriter.Indentation = 5;

//                    if (vstr_RootNodeName != null && vstr_RootNodeName.Length > 0)
//                    {
//                        robj_DataSet.DataSetName = vstr_RootNodeName;
//                    }

//                    if (vstr_SubNodeName != null && vstr_SubNodeName.Length > 0)
//                    {
//                        robj_DataSet.Tables[0].TableName = vstr_SubNodeName;
//                    }

//                    //Trim the Char fields
//                    if (pcon_AmarConn.PreserveWhiteSpace == false)
//                    {
//                        for (x = 0; x <= robj_DataSet.Tables.Count - 1; x++)
//                        {
//                            for (y = 0; y <= robj_DataSet.Tables[x].Rows.Count - 1; y++)
//                            {
//                                for (z = 0; z <= robj_DataSet.Tables[x].Columns.Count - 1; z++)
//                                {
//                                    if (!Information.IsDBNull(robj_DataSet.Tables[x].Rows[y].ItemArray[z]) && (object.ReferenceEquals(robj_DataSet.Tables[x].Columns[z].DataType, stringType)))
//                                    {
//                                        robj_DataSet.Tables[x].Rows[y].ItemArray[z] = Convert.ToString(robj_DataSet.Tables[x].Rows[y].ItemArray[z]).Trim();
//                                    }
//                                }
//                            }
//                        }
//                    }

//                    robj_DataSet.WriteXml(xmlTextWriter);

//                    lstr_XML = xmlStringWriter.ToString();
//                }
//                else
//                {
//                    lstr_XML = "";
//                }

//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//            finally
//            {
//                xmlStringWriter.Close();
//                xmlStringWriter = null;
//                xmlTextWriter.Close();
//                xmlTextWriter = null;

//            }

//            //:: Return XML String
//            return lstr_XML;

//        }

//        private string pExecuteXMLQuery(string vstr_SQL, string vstr_ROOTTAG, string vstr_ROWTAG)
//        {

//            SqlCommand lobj_Cmd = null;
//            XmlReader lobj_Reader = null;
//            XmlDocument lobj_Doc = null;

//            lobj_Cmd = pcon_AmarConn.MyOraConn.CreateCommand();

//            lobj_Cmd.XmlCommandType = OracleXmlCommandType.Query;
//            lobj_Cmd.CommandText = vstr_SQL;

//            if (pcon_AmarConn.QueryTimeout > 0)
//                lobj_Cmd.CommandTimeout = pcon_AmarConn.QueryTimeout;

//            lobj_Cmd.XmlQueryProperties.MaxRows = -1;

//            if (vstr_ROOTTAG.Length == 0)
//                vstr_ROOTTAG = "ROWSET";
//            lobj_Cmd.XmlQueryProperties.RootTag = vstr_ROOTTAG;
//            if (vstr_ROWTAG.Length == 0)
//                vstr_ROWTAG = "ROW";
//            lobj_Cmd.XmlQueryProperties.RowTag = vstr_ROWTAG;

//            lobj_Reader = lobj_Cmd.ExecuteXmlReader();

//            lobj_Doc = new XmlDocument();
//            lobj_Doc.PreserveWhitespace = true;

//            lobj_Doc.Load(lobj_Reader);

//            lobj_Reader.Close();
//            lobj_Cmd.Dispose();

//            return lobj_Doc.OuterXml;

//        }

//        private string pExecuteScalar(string vstr_SQL, [Optional] ref SqlCommand robj_Command)
//        {

//            SqlCommand lobj_Cmd = null;
//            object lobj_Ret = null;
//            bool lbln_CallerSentCommand = false;

//            //Initial setup
//            if (robj_Command == null)
//            {
//                lobj_Cmd = pcon_AmarConn.MyOraConn.CreateCommand();
//                lobj_Cmd.CommandType = CommandType.Text;
//                lobj_Cmd.CommandText = vstr_SQL;
//            }
//            else
//            {
//                lobj_Cmd = robj_Command;
//                lbln_CallerSentCommand = true;
//            }

//            //Set the timeout for the transaction
//            if (pcon_AmarConn.QueryTimeout > 0)
//                lobj_Cmd.CommandTimeout = pcon_AmarConn.QueryTimeout;

//            //Execute DB Operation
//            lobj_Ret = lobj_Cmd.ExecuteScalar();

//            //Clean up
//            if (lbln_CallerSentCommand == false)
//                lobj_Cmd.Dispose();
//            lobj_Cmd = null;

//            //Return
//            if (lobj_Ret != null)//&& !Information.IsDBNull(lobj_Ret))
//            {
//                return lobj_Ret.ToString();
//            }
//            else
//            {
//                return "";
//            }

//        }

//        private DataSet pExecuteDataSet(string vstr_SQL, string vstr_DATASETNAME, string vstr_DATASETTABLENAME,[Optional] ref SqlCommand robj_Cmd)
//        {

//            DataSet lobj_DataSet = null;
//            SqlCommand lobj_Command = null;
//            SqlDataAdapter lobj_Adapter = null;
//            bool lbln_CallerSentCommand = false;

//            if (robj_Cmd == null)
//            {
//                lobj_Command = pcon_AmarConn.MyOraConn.CreateCommand();
//                lobj_Command.CommandType = CommandType.Text;
//                lobj_Command.CommandText = vstr_SQL;
//            }
//            else
//            {
//                lobj_Command = robj_Cmd;
//                lbln_CallerSentCommand = true;
//            }

//            if (pcon_AmarConn.QueryTimeout > 0)
//                lobj_Command.CommandTimeout = pcon_AmarConn.QueryTimeout;

//            lobj_Adapter = new SqlDataAdapter();
//            lobj_Adapter.SelectCommand = lobj_Command;

//            if (vstr_DATASETNAME.Length == 0)
//            {
//                lobj_DataSet = new DataSet();
//            }
//            else
//            {
//                lobj_DataSet = new DataSet(vstr_DATASETNAME);
//            }

//            if (vstr_DATASETTABLENAME.Length == 0)
//            {
//                lobj_Adapter.Fill(lobj_DataSet);
//            }
//            else
//            {
//                lobj_Adapter.Fill(lobj_DataSet, vstr_DATASETTABLENAME);
//            }

//            if (lbln_CallerSentCommand == false)
//                lobj_Command.Dispose();
//            lobj_Command = null;
//            lobj_Adapter.Dispose();
//            lobj_Adapter = null;

//            return lobj_DataSet;
//        }

//        private IDataReader pExecuteDataReader(string vstr_SQL, ref SqlCommand robj_Command)
//        {

//            SqlCommand lobj_Cmd = null;
//            IDataReader lobj_DR = null;
//            bool lbln_CallerSentCommand = false;

//            if (robj_Command == null)
//            {
//                lobj_Cmd = pcon_AmarConn.MyOraConn.CreateCommand();
//                lobj_Cmd.CommandText = vstr_SQL;
//            }
//            else
//            {
//                lobj_Cmd = robj_Command;
//                lbln_CallerSentCommand = true;
//            }

//            if (pcon_AmarConn.QueryTimeout > 0)
//                lobj_Cmd.CommandTimeout = pcon_AmarConn.QueryTimeout;

//            lobj_DR = lobj_Cmd.ExecuteReader();

//            if (lbln_CallerSentCommand == false)
//                lobj_Cmd.Dispose();
//            lobj_Cmd = null;

//            return lobj_DR;
//        }



//        //private void setUserContext()
//        //{
//        //    if (pcon_AmarConn.UsingMaskedDBUser)
//        //    {
//        //        if (this.Connection.PoolingEnabled || (pcon_AmarConn.LastContextDBUser.Length == 0 || pcon_AmarConn.LastContextDBUser != pcon_AmarConn.DBUser))
//        //        {
//        //            //reset the context for this user
//        //            OracleCommand lobj_Command = pcon_AmarConn.MyOraConn.CreateCommand();
//        //            //---[ Build the query ]---
//        //            var _with2 = lobj_Command;
//        //            _with2.CommandType = CommandType.StoredProcedure;
//        //            _with2.CommandText = "SET_MIDTIER_CTX";
//        //            _with2.Parameters.Add("pAppUser", OracleDbType.Varchar2).Value = pcon_AmarConn.DBUser;

//        //            _with2.ExecuteNonQuery();

//        //            pcon_AmarConn.LastContextDBUser = pcon_AmarConn.DBUser;
//        //        }
//        //    }

//        //}

//        #endregion

//    }

//    public class AmarConnection
//    {

//        private SqlConnection pcon_Conn;
//        private string pstr_User = "";
//        private string pstr_Password = "";
//        private string pstr_Database = "";
//        private bool pbln_UsingMaskedDBUser = false;
//        private string pstr_LastContextDBUser = "";
//        private SqlTransaction pobj_Transaction;
//        private Int32 pint_QueryTimeout = 0;
//        private bool pbln_PreserveWhiteSpace = false;
//        private string pstr_ClientRegistryCode = "DEFAULT";
//        private AmarPWD pepwd_Pwd = null;
//        private const string REGISTRY_PATH = "Software\\AmarCom\\Loyalty";
//        private bool disposed = false;
//        //Private pbln_CallerSentConnection As Boolean = False

//        private Int32 pint_SoftConnectReferenceCount = 0;


//        #region "  Constructors\Destructors  "
//        public AmarConnection(ref SqlConnection rcon_Conn)
//        {
//            pcon_Conn = rcon_Conn;
//        }
//        public AmarConnection(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pstr_ClientRegistryCode = vstr_Clientcode;
//            pepwd_Pwd = robj_PWD;
//            //OpenDBConnection(vstr_User, vstr_Password, vstr_DSN)
//            CreateConnection(vstr_User, vstr_Password, vstr_DSN);
//        }
//        public AmarConnection(string vstr_User, string vstr_Password, string vstr_DSN, bool vb_ForceConn, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pstr_ClientRegistryCode = vstr_Clientcode;
//            pepwd_Pwd = robj_PWD;
//            //OpenDBConnection(vstr_User, vstr_Password, vstr_DSN, vb_ForceConn)
//            CreateConnection(vstr_User, vstr_Password, vstr_DSN);

//        }

//        //Constructor 3
//        public AmarConnection(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_Enlist, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pstr_ClientRegistryCode = vstr_Clientcode;
//            pepwd_Pwd = robj_PWD;
//            //OpenDBConnection(vstr_User, vstr_Password, vstr_DSN, vstr_Enlist)
//            CreateConnection(vstr_User, vstr_Password, vstr_DSN);

//        }

//        public AmarConnection(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_Enlist, bool vb_ForceConn, string vstr_Clientcode, ref AmarPWD robj_PWD)
//        {
//            pstr_ClientRegistryCode = vstr_Clientcode;
//            pepwd_Pwd = robj_PWD;
//            //OpenDBConnection(vstr_User, vstr_Password, vstr_DSN, vstr_Enlist, vb_ForceConn)
//            CreateConnection(vstr_User, vstr_Password, vstr_DSN);

//        }

//        public void Dispose()
//        {
//            pDispose(true);
//        }
//        //Destructor
//        private void pDispose(bool disposing)
//        {
//            //Dispose resources.
//            try
//            {
//                if (!this.disposed)
//                {
//                    if (disposing)
//                    {
//                        //Dispose resources.
//                        try
//                        {
//                            pobj_Transaction = null;
//                            clearConnection();

//                        }
//                        catch (System.Exception excp)
//                        {
//                            // Not much we can do here
//                        }
//                    }
//                }
//                disposed = true;
//            }
//            catch (Exception ex)
//            {
//            }
//        }
//        #endregion

//        #region "   Properties  "
//        private string DatabaseRegPath
//        {
//            get { return REGISTRY_PATH + "\\" + pstr_ClientRegistryCode + "\\Database"; }
//        }

//        private string PoolingRegPath
//        {
//            get { return string.Format(DatabaseRegPath + "\\Pooling\\Pool{0}", pstr_Database); }
//        }

//        public bool PoolingEnabled
//        {
//            get { return false; }
//            //get { return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "PoolingEnabled", "True", true); }
//        }

//        public SqlConnection MyOraConn
//        {

//            get { return pcon_Conn; }
//        }


//        public SqlTransaction MyOraTrans
//        {
//            get { return pobj_Transaction; }
//        }

//        public Int32 QueryTimeout
//        {
//            get { return pint_QueryTimeout; }
//            set { pint_QueryTimeout = value; }
//        }

//        public bool PreserveWhiteSpace
//        {
//            get { return pbln_PreserveWhiteSpace; }
//            set { pbln_PreserveWhiteSpace = value; }
//        }

//        public string DBUser
//        {
//            get { return pstr_User; }
//            set { pstr_User = value; }
//        }

//        public string DBPassword
//        {
//            get { return pstr_Password; }
//            set { pstr_Password = value; }
//        }

//        public string DBDsn
//        {
//            get { return pstr_Database; }
//            set { pstr_Database = value; }
//        }

//        public bool UsingMaskedDBUser
//        {
//            get { return pbln_UsingMaskedDBUser; }
//            set { pbln_UsingMaskedDBUser = value; }
//        }

//        public string LastContextDBUser
//        {
//            get { return pstr_LastContextDBUser; }
//            set { pstr_LastContextDBUser = value; }
//        }

//        public string ClientRegistryCode
//        {
//            get { return pstr_ClientRegistryCode; }
//            set { pstr_ClientRegistryCode = value; }
//        }
//        public AmarPWD ClientPwdObj
//        {
//            get { return pepwd_Pwd; }
//            set { pepwd_Pwd = value; }
//        }
//        public virtual bool InTransaction
//        {
//            get { return pobj_Transaction != null; }
//        }

//        #endregion

//        #region "  Public methods"
//        public void softConnect()
//        {
//            pint_SoftConnectReferenceCount = pint_SoftConnectReferenceCount + 1;
//            if (this.PoolingEnabled && (MyOraConn != null) && MyOraConn.State == ConnectionState.Closed)
//            {
//                MyOraConn.Open();
//            }
//        }

//        public void softDisconnect()
//        {
//            // Reference counter workaround for internal direct calls to EP methods (bad!)
//            pint_SoftConnectReferenceCount = pint_SoftConnectReferenceCount - 1;
//            if ((pint_SoftConnectReferenceCount < 1))
//            {
//                if (this.PoolingEnabled && (MyOraConn != null) && MyOraConn.State != ConnectionState.Closed)
//                {
//                    MyOraConn.Close();
//                }
//            }
//        }

//        public void SetTransaction(ref SqlTransaction robj_Trans)
//        {
//            pobj_Transaction = robj_Trans;
//        }


//        public void beginTransaction()
//        {
//            if (this.InTransaction == false)
//            {
//                if (pobj_Transaction == null)
//                {
//                    pobj_Transaction = pcon_Conn.BeginTransaction();
//                }
//            }
//        }

//        public void SetPWDObject(ref AmarPWD robj_PWD)
//        {
//            //pepwd_Pwd.Dispose();
//            pepwd_Pwd = null;
//            pepwd_Pwd = robj_PWD;
//        }


//        public virtual void commitTransaction()
//        {
//            if (this.InTransaction == true)
//            {
//                if (pobj_Transaction != null)
//                {
//                    pobj_Transaction.Commit();
//                    pobj_Transaction.Dispose();
//                    pobj_Transaction = null;
//                }
//            }
//        }


//        public virtual void rollbackTransaction()
//        {
//            if (this.InTransaction == true)
//            {
//                if (pobj_Transaction != null)
//                {
//                    pobj_Transaction.Rollback();
//                    pobj_Transaction.Dispose();
//                    pobj_Transaction = null;
//                }
//            }
//        }
//        public void closeConnection()
//        {
//            if ((MyOraConn != null) && MyOraConn.State != System.Data.ConnectionState.Closed)
//            {
//                MyOraConn.Close();
//            }
//        }
//        public void changeDBConnection(string vstr_User, string vstr_Password, string vstr_DSN)
//        {
//            closeConnection();
//            //OpenDBConnection(vstr_User, vstr_Password, vstr_DSN)
//            CreateConnection(vstr_User, vstr_Password, vstr_DSN);
//        }
//        #endregion

//        #region "  Private methods "

//        private void clearConnection()
//        {
//            if (pcon_Conn != null)
//            {
//                //Clear out the old connection
//                if (pcon_Conn.State != ConnectionState.Closed)
//                {
//                    pcon_Conn.Close();
//                }
//                pcon_Conn.Dispose();
//                pcon_Conn = null;
//            }
//        }
//        private Int32 getQueryTimeOut()
//        {
//            return 600;
//         //   return (Int32)RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "QueryTimeOut", "30", true);
//        }
//        private bool RegStringContainsName(string vstr_RegString, string vstr_Name)
//        {

//            //Note: we can NOT just search the string because JON will be found in a string of JONES
//            //      we must search each delimited entry
//            vstr_Name = "," + vstr_Name + ",";
//            vstr_RegString = "," + vstr_RegString + ",";

//            return vstr_RegString.Contains(vstr_Name);
//        }
//        #endregion

//        #region "  Protected Members  "

//        protected void CreateConnection(string vstr_User, string vstr_Password, string vstr_DSN)
//        {
//            // build the connection string. connect to the database if we're not pooling
//            pcon_Conn = new SqlConnection();
//            pcon_Conn.ConnectionString = BuildConnectionString(vstr_User, vstr_Password, vstr_DSN);

//            try
//            {
//                pint_QueryTimeout = this.getQueryTimeOut();

//                if (!this.PoolingEnabled)
//                {
//                    // Go ahead and open hard connection
//                    pcon_Conn.Open();
//                }

//            }
//            catch (SqlException oex)
//            {
//                throw oex;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }

//        //Protected Overloads Sub OpenDBConnection(ByRef rcon_conn As OracleConnection)
//        //    pcon_Conn = rcon_conn
//        //    If pint_QueryTimeout <= 0 Then
//        //        pint_QueryTimeout = Me.getQueryTimeOut()
//        //    End If
//        //    'pbln_CallerSentConnection = True
//        //End Sub

//        //Protected Overloads Sub OpenDBConnection(ByVal vstr_User As String, ByVal vstr_Password As String, ByVal vstr_DSN As String, Optional ByVal vb_ForceConn As Boolean = True)

//        //    'build the connection string and connect to the database
//        //    pcon_Conn = New OracleConnection
//        //    pcon_Conn.ConnectionString = BuildConnectionString(vstr_User, vstr_Password, vstr_DSN)

//        //    Try

//        //        pcon_Conn.Open()
//        //        pint_QueryTimeout = Me.getQueryTimeOut()

//        //    Catch oex As Oracle.DataAccess.Client.OracleException
//        //        'If we Then 're pooling and we have exceeded the maximum number of pooled connections,
//        //        'Simply create a new connection
//        //        If oex.Number = -1000 AndAlso pcon_Conn.ConnectionString.ToUpper().IndexOf("POOLING=TRUE") >= 0 AndAlso vb_ForceConn Then
//        //            pcon_Conn = Nothing
//        //            pcon_Conn = New OracleConnection
//        //            pcon_Conn.ConnectionString = BuildConnectionString(vstr_User, vstr_Password, vstr_DSN, True)

//        //            pcon_Conn.Open()
//        //            pint_QueryTimeout = Me.getQueryTimeOut()
//        //        Else
//        //            Throw
//        //        End If

//        //    Catch ex As Exception
//        //        Throw
//        //    End Try

//        //End Sub

//        //Protected Overloads Sub OpenDBConnection(ByVal vstr_User As String, ByVal vstr_Password As String, ByVal vstr_DSN As String, _
//        //    ByVal vstr_Enlist As String, Optional ByVal vb_ForceConn As Boolean = True)
//        //    'build the connection string and connect to the database
//        //    pcon_Conn = New OracleConnection
//        //    pcon_Conn.ConnectionString = BuildConnectionStringEnlist(vstr_User, vstr_Password, vstr_DSN, vstr_Enlist)

//        //    Try

//        //        pcon_Conn.Open()
//        //        pint_QueryTimeout = Me.getQueryTimeOut()

//        //    Catch oex As Oracle.DataAccess.Client.OracleException
//        //        'If we're pooling and we have exceeded the maximum number of pooled connections,
//        //        'Simply create a new connection
//        //        If oex.Number = -1000 AndAlso pcon_Conn.ConnectionString.ToUpper().IndexOf("POOLING=TRUE") >= 0 AndAlso vb_ForceConn Then
//        //            pcon_Conn = Nothing
//        //            pcon_Conn = New OracleConnection
//        //            pcon_Conn.ConnectionString = BuildConnectionStringEnlist(vstr_User, vstr_Password, vstr_DSN, vstr_Enlist, True)

//        //            pcon_Conn.Open()
//        //            pint_QueryTimeout = Me.getQueryTimeOut()
//        //        Else
//        //            Throw
//        //        End If

//        //    Catch ex As Exception
//        //        Throw
//        //    End Try

//        //End Sub
//        protected virtual string BuildConnectionString(string vstr_User, string vstr_Password, string vstr_DSN, bool vbln_DisableConnPool = false)
//        {

//            SqlConnectionStringBuilder ocsb = new SqlConnectionStringBuilder();
//            bool pbln_PoolUser = true;

//            string lstr_appUser = "";
//            string lstr_appPass = "";

//            //set the connection parameters
//            pstr_User = vstr_User.ToUpper();
//            pstr_Password = vstr_Password;
//            pstr_Database = vstr_DSN;

//            //set the connection user to the inbound user name/pwd
//            lstr_appUser = vstr_User.ToUpper();
//            lstr_appPass = vstr_Password;

//            // If there is no masking exception for this user then use the masking user credentials
//            if (RegStringContainsName(getNoMaskUsers(), pstr_User) == false)
//            {
//                pepwd_Pwd.getMaskCredentialsByDB(pstr_Database, lstr_appUser, lstr_appPass);
//                pbln_UsingMaskedDBUser = true;
//            }

//            string lstr_poolUsers = "";

//            if (RegStringContainsName(getNoPoolUsers(), pstr_User))
//            {
//                pbln_PoolUser = false;
//            }

//            //get the connection pooling settings from the registry for users
//            lstr_poolUsers = getCnPoolUsers();

//            ocsb.UserID = lstr_appUser;
//            ocsb.Password = lstr_appPass;
//            ocsb.DataSource = pstr_Database;

//            //We can consider this to implement - Panna
//            //If vbln_DisableConnPool = False AndAlso (lstr_poolUsers.ToUpper = "ALL" OrElse RegStringContainsName(lstr_poolUsers, pstr_User) = True) Then
//            if (this.PoolingEnabled)
//            {
//                ocsb.Pooling = true;
//                ocsb.MaxPoolSize = getPoolMax();
//                ocsb.MinPoolSize = getPoolMin();
//                ocsb.IncrPoolSize = getPoolIncrement();
//                ocsb.DecrPoolSize = getPoolDecrement();
//                if (!string.IsNullOrEmpty(getPoolLifetime()))
//                {
//                    ocsb.ConnectionLifeTime = getPoolLifetime();
//                }

//                if (IsRACEnabled() == "TRUE")
//                {
//                    ocsb.LoadBalancing = true;
//                    ocsb.HAEvents = true;
//                }
//            }
//            else
//            {
//                ocsb.Pooling = false;
//            }
//            ocsb.PromotableTransaction = "local";
//            ocsb.Enlist = "false";

//            return ocsb.ConnectionString;
//        }

//        //Protected Overridable Function BuildConnectionString(ByVal vstr_User As String, _
//        //      ByVal vstr_Password As String, _
//        //      ByVal vstr_DSN As String, _
//        //      Optional ByVal vbln_DisableConnPool As Boolean = False) As String

//        //    Dim lstr_cnPool As String = ""
//        //    Dim lstr_poolUsers As String = ""
//        //    Dim lstr_appUser As String = ""
//        //    Dim lstr_appPass As String = ""

//        //    'Set the connection parameters
//        //    pstr_User = vstr_User.ToUpper()
//        //    pstr_Password = vstr_Password
//        //    pstr_Database = vstr_DSN

//        //    'start by setting the conneciton user to the inbound user name/pwd
//        //    lstr_appUser = vstr_User.ToUpper
//        //    lstr_appPass = vstr_Password

//        //    ''check for application user to log in the DB
//        //    'PJC - 02112010 - Removed mask user from registry - now implemented in new PWD object
//        //    If RegStringContainsName(getNoMaskUsers(), pstr_User) = False Then
//        //        'Grab the mask user credentials
//        //        Call pepwd_Pwd.getMaskCredentialsByDB(pstr_Database, lstr_appUser, lstr_appPass)
//        //        pbln_UsingMaskedDBUser = True
//        //    End If

//        //    '''check for application user to log in the DB
//        //    'Dim lstr_maskUser As String = getDBMaskUser()
//        //    'If lstr_maskUser.Length > 0 AndAlso RegStringContainsName(getNoMaskUsers(), pstr_User) = False Then
//        //    '	'this user is not on the "Do not mask list," so get the pwd
//        //    '	lstr_appUser = lstr_maskUser
//        //    '	lstr_appPass = ProcessMaskPassword(lstr_maskUser, pstr_Database)
//        //    '	pbln_usingMaskedDBUser = True
//        //    'End If

//        //    'get the connection pooling settings from the registry for users
//        //    lstr_poolUsers = getCnPoolUsers()
//        //    If IsRACEnabled() = "TRUE" Then
//        //        lstr_cnPool = GetRacConnectionString()
//        //    Else
//        //        If vbln_DisableConnPool = False AndAlso (lstr_poolUsers.ToUpper = "ALL" OrElse RegStringContainsName(lstr_poolUsers, pstr_User) = True) Then
//        //            'get the setting from the DB for pooling, min/max/incr/decr settings
//        //            lstr_cnPool = getCnPoolString()
//        //        Else
//        //            'it's not supposed to be on for this user, override the registry setting
//        //            lstr_cnPool = ";Pooling=false"
//        //        End If
//        //    End If


//        //    Return "User ID=" & lstr_appUser & " ;Password=" & lstr_appPass & ";Data Source=" & pstr_Database & lstr_cnPool

//        //End Function

//        //protected virtual string BuildConnectionStringEnlist(string vstr_User, string vstr_Password, string vstr_DSN, string vstr_EnlistFlag, bool vbln_DisableConnPool = false)
//        //{

//        //    string lstr_cnPool = "";
//        //    string lstr_poolUsers = "";
//        //    string lstr_appUser = "";
//        //    string lstr_appPass = "";

//        //    //Set the connection parameters
//        //    pstr_User = vstr_User.ToUpper();
//        //    pstr_Password = vstr_Password;
//        //    pstr_Database = vstr_DSN;

//        //    //start by setting the conneciton user to the inbound user name/pwd
//        //    lstr_appUser = vstr_User;
//        //    lstr_appPass = vstr_Password;

//        //    //'check for application user to log in the DB
//        //    //PJC - 02112010 - Removed mask user from registry - now implemented in new PWD object
//        //    if (RegStringContainsName(getNoMaskUsers(), pstr_User) == false)
//        //    {
//        //        //Grab the mask user credentials
//        //        pepwd_Pwd.getMaskCredentialsByDB(pstr_Database, lstr_appUser, lstr_appPass);
//        //        pbln_UsingMaskedDBUser = true;
//        //    }

//        //    ///check for application user to log in the DB
//        //    //Dim lstr_maskUser As String = getDBMaskUser()
//        //    //If lstr_maskUser.Length > 0 AndAlso RegStringContainsName(getNoMaskUsers(), pstr_User) = False Then
//        //    //	'this user is not on the "Do not mask list," so get the pwd
//        //    //	lstr_appUser = lstr_maskUser
//        //    //	lstr_appPass = ProcessMaskPassword(lstr_maskUser, pstr_Database)
//        //    //	pbln_usingMaskedDBUser = True
//        //    //End If

//        //    //get the connection pooling settings from the registry for users
//        //    lstr_poolUsers = getCnPoolUsers();
//        //    if (IsRACEnabled() == "TRUE")
//        //    {
//        //        lstr_cnPool = GetRacConnectionString();
//        //    }
//        //    else
//        //    {
//        //        if (vbln_DisableConnPool == false && (lstr_poolUsers.ToUpper == "ALL" || RegStringContainsName(lstr_poolUsers, pstr_User) == true))
//        //        {
//        //            //get the setting from the DB for pooling, min/max/incr/decr settings
//        //            lstr_cnPool = getCnPoolString();
//        //        }
//        //        else
//        //        {
//        //            //it's not supposed to be on for this user, override the registry setting
//        //            lstr_cnPool = ";Pooling=false";
//        //        }
//        //    }
//        //    return "User ID=" + lstr_appUser + " ;Password=" + lstr_appPass + ";Data Source=" + pstr_Database + ";Enlist=" + vstr_EnlistFlag + lstr_cnPool;

//        //}
//        //protected string GetRacConnectionString()
//        //{
//        //    //NOTES:  For Oracle RAC Implementations - You must have Pooling = True to enable Load Balancing =True and HA Events = True
//        //    return ";Pooling=true;Min Pool Size=" + getMinPoolString() + ";Max Pool Size=" + getMaxPoolString() + "; Connection Lifetime=120; Connection Timeout=5;Incr Pool Size=1;Decr Pool Size=1;Load Balancing=true;HA Events=true";
//        //}

//        //protected string getMinPoolString()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "MinPool" + pstr_Database, 1, true).ToString.Trim;
//        //}
//        //protected string getMaxPoolString()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "MaxPool" + pstr_Database, 2, true).ToString.Trim;
//        //}

//        //protected string getCnPoolString()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "Pool" + pstr_Database, ";Pooling=false;Min Pool Size=0;Max Pool Size=1;Connection Timeout=2;Validate Connection=true", true).ToString.Trim;
//        //}

//        //protected string getDBMaskUser()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "AppMaskUser" + pstr_Database, "", true).ToString.Trim;
//        //}

//        //protected string getNoMaskUsers()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "DoNotMask" + pstr_Database, "", true).ToString.Trim;
//        //}

//        //protected string getCnPoolUsers()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "Users" + pstr_Database, "NONE", true).ToString.Trim;
//        //}
//        //protected string IsRACEnabled()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.DatabaseRegPath, "RAC_ENABLED" + pstr_Database, "FALSE", true).ToString().Trim();
//        //}

//        //protected string getPoolMax()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "PoolMax", "16", true).ToString.Trim;
//        //}
//        //protected string getPoolMin()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "PoolMin", "0", true).ToString.Trim;
//        //}
//        //protected string getPoolLifetime()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "PoolLifetime", "30", true).ToString.Trim;
//        //}
//        //protected string getPoolIncrement()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "PoolInc", "1", true).ToString.Trim;
//        //}
//        //protected string getPoolDecrement()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "PoolDec", "1", true).ToString.Trim;
//        //}

//        //protected string getNoPoolUsers()
//        //{
//        //    return RegHelper.R_RegistryRead(RegHelper.RegKEYS.LOCALMACHINE, this.PoolingRegPath, "NoPoolUsers", "", true).ToString.Trim;
//        //}
//        #endregion




//    }

//}