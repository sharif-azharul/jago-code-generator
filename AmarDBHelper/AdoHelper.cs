using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmarDBHelper
{
    public class AdoHelper : IDisposable
    {
        // Internal members
        protected string _connString = null;
        protected SqlConnection _conn = null;
        protected SqlTransaction _trans = null;
        protected bool _disposed = false;

        /// <summary>
        /// Sets or returns the connection string use by all instances of this class.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Returns the current SqlTransaction object or null if no transaction
        /// is in effect.
        /// </summary>
        public SqlTransaction Transaction { get { return _trans; } }

        /// <summary>
        /// Constructor using global connection string.
        /// </summary>
        public AdoHelper()
        {
            _connString = ConnectionString;
            Connect();
        }

        /// <summary>
        /// Constructure using connection string override
        /// </summary>
        /// <param name="connString">Connection string for this instance</param>
        public AdoHelper(string connString)
        {
            _connString = connString;
            Connect();
        }

        // Creates a SqlConnection using the current connection string
        protected void Connect()
        {
            _conn = new SqlConnection(_connString);
            _conn.Open();
        }

        /// <summary>
        /// Constructs a SqlCommand with the given parameters. This method is normally called
        /// from the other methods and not called directly. But here it is if you need access
        /// to it.
        /// </summary>
        /// <param name="qry">SQL query or stored procedure name</param>
        /// <param name="type">Type of SQL command</param>
        /// <param name="args">Query arguments. Arguments should be in pairs where one is the
        /// name of the parameter and the second is the value. The very last argument can
        /// optionally be a SqlParameter object for specifying a custom argument type</param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string qry, CommandType type, params object[] args)
        {
            SqlCommand cmd = new SqlCommand(qry, _conn);

            // Associate with current transaction, if any
            if (_trans != null)
                cmd.Transaction = _trans;

            // Set command type
            cmd.CommandType = type;

            // Construct SQL parameters
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is string && i < (args.Length - 1))
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = (string)args[i];
                    parm.Value = args[++i];
                    cmd.Parameters.Add(parm);
                }
                else if (args[i] is SqlParameter)
                {
                    cmd.Parameters.Add((SqlParameter)args[i]);
                }
                else throw new ArgumentException("Invalid number or type of arguments supplied");
            }
            return cmd;
        }

        public SqlCommand CreateCommand(string qry, CommandType type)
        {
            SqlCommand cmd = new SqlCommand(qry, _conn);

            // Associate with current transaction, if any
            if (_trans != null)
                cmd.Transaction = _trans;
            // Set command type
            cmd.CommandType = type;
            return cmd;
        }

        public SqlCommand CreateCommand(string qry, CommandType type, string[] Params)
        {
            SqlCommand cmd = new SqlCommand(qry, _conn);

            // Associate with current transaction, if any
            if (_trans != null)
                cmd.Transaction = _trans;

            // Set command type
            cmd.CommandType = type;
            //
            if (type == CommandType.Text)
            {
                string[] parms = qry.Split('@');
                if ((parms.Length - 1) != Params.Length)
                {
                    throw new ArgumentException("Invalid number or type of arguments supplied");
                }
            }
            // Construct SQL parameters
            foreach (string param in Params)
            {
                string[] args = param.Split('|');

                if (args.Length > 1)
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = args[0];
                    parm.Value = args[1] == "" ? DBNull.Value : (object)args[1];
                    if (args.Length > 2)
                    {
                        parm.DbType = (DbType)Enum.Parse(typeof(DbType), args[2]);
                    }
                    if (args.Length > 3)
                    {
                        parm.Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), args[3]);
                    }
                    cmd.Parameters.Add(parm);
                }

                else throw new ArgumentException("Invalid number or type of arguments supplied");
            }
            return cmd;
        }


        public string CreateParameter(string ParaName, string ParamValue, string DBParamType = "DbType.String", string ParamDirection = "In")
        {
            return string.Format("{0}|{1}|{2}|{3}", ParaName, ParamValue, DBParamType, ParamDirection);
        }

        public string CreateParameter(string ParaName, dynamic ParamValue, DbType DBParamType = DbType.String, ParameterDirection ParamDirection = ParameterDirection.Input)
        {
            return string.Format("{0}|{1}|{2}|{3}", ParaName, ParamValue, DBParamType, ParamDirection);
        }
        #region Exec Members

        /// <summary>
        /// Executes a query that returns no results
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQuery(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public int PrepSqlToSave(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public int PrepSPToSave(string SPName, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure, Params))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public int ExecNonQuery(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public int PrepSPToNonQuery(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, Params))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public int PrepSqlToNonQuery(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Executes a stored procedure that returns no results
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQueryProc(string proc, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(proc, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalar(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteScalar();
            }
        }


        public string PrepSqlToOneValue(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                // return cmd.ExecuteScalar();

                var returValue = cmd.ExecuteScalar();

                if (returValue != null)
                {
                    return returValue.ToString();
                }
                else
                    return string.Empty;
            }
        }

        public string PrepSPToOneValue(string SPName, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure, Params))
            {
                // return cmd.ExecuteScalar();

                var returValue = cmd.ExecuteScalar();

                if (returValue != null)
                {
                    return returValue.ToString();
                }
                else
                    return string.Empty;
            }
        }
        public string SqlToOneValue(string qry)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text))
            {
                // return cmd.ExecuteScalar();

                var returValue = cmd.ExecuteScalar();

                if (returValue != null)
                {
                    return returValue.ToString();
                }
                else
                    return string.Empty;

            }
        }


        public string SPToOneValue(string SPName)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure))
            {
                // return cmd.ExecuteScalar();

                var returValue = cmd.ExecuteScalar();

                if (returValue != null)
                {
                    return returValue.ToString();
                }
                else
                    return string.Empty;

            }
        }


        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalarProc(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes a query and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public SqlDataReader ExecDataReader(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteReader();
            }
        }

        public SqlDataReader ExecDataReader(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return cmd.ExecuteReader();
            }
        }

        public SqlDataReader PrepSqlToDataReader(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return cmd.ExecuteReader();
            }
        }
        public SqlDataReader SqlToDataReader(string qry)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text))
            {
                return cmd.ExecuteReader();
            }
        }

        public T PrepSqlToModel<T>(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return DataReaderMapToModel<T>(cmd.ExecuteReader());
            }
        }
        public List<T> PrepSqlToList<T>(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                return DataReaderMapToList<T>(cmd.ExecuteReader());
            }
        }

        public T SqlToModel<T>(string qry)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text))
            {
                return DataReaderMapToModel<T>(cmd.ExecuteReader());
            }
        }

        public List<T> SqlToList<T>(string qry)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text))
            {
                return DataReaderMapToList<T>(cmd.ExecuteReader());
            }
        }

        public SqlDataReader PrepSPToDataReader(string SPName, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure, Params))
            {
                return cmd.ExecuteReader();
            }
        }

        public T PrepSPToModel<T>(string SPName, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure, Params))
            {
                return DataReaderMapToModel<T>(cmd.ExecuteReader());
            }
        }
        public List<T> PrepSPToList<T>(string SPName, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure, Params))
            {
                return DataReaderMapToList<T>(cmd.ExecuteReader());
            }
        }

        public SqlDataReader SPToDataReader(string SPName)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure))
            {
                return cmd.ExecuteReader();
            }
        }

        public T SPToModel<T>(string SPName)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure))
            {
                return DataReaderMapToModel<T>(cmd.ExecuteReader());
            }
        }

        public List<T> SPToList<T>(string SPName)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure))
            {
                return DataReaderMapToList<T>(cmd.ExecuteReader());
            }
        }

        public static T DataReaderMapToModel<T>(SqlDataReader dr)
        {
            DataTable dt = dr.GetSchemaTable();
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            while (dr.Read())
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    foreach (DataRow dRow in dt.Rows)
                    {
                        string col = dRow.ItemArray[0].ToString();
                        if (col == prop.Name)
                        {
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                //Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                //object safeValue = (dr[prop.Name] == null) ? null : Convert.ChangeType(dr[prop.Name], t);

                                //prop.SetValue(obj, safeValue, null);
                                Type t = prop.PropertyType;
                                if (prop.PropertyType.ToString().ToUpper().Contains("NULLABLE`1"))
                                {
                                    //input.Split('(', ')')[1];
                                    t = Type.GetType(prop.PropertyType.ToString().Split('[', ']')[1]);
                                    //if (prop.PropertyType.ToString().ToUpper().Contains("DATE"))
                                    //{ }
                                }

                                //prop.SetValue(obj, Convert.ChangeType(dr[prop.Name], prop.PropertyType), null);
                                prop.SetValue(obj, Convert.ChangeType(dr[prop.Name], t), null);
                                //prop.SetValue(obj, GetConverterMethod(prop.PropertyType), null);


                            }
                            break;
                        }

                    }
                }
            }
            dr.Close();
            return obj;
        }

        public static List<T> DataReaderMapToList<T>(SqlDataReader dr)
        {
            DataTable dt = dr.GetSchemaTable();
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    //if (!object.Equals(dr[prop.Name], DBNull.Value))
                    //{
                    //    prop.SetValue(obj, dr[prop.Name], null);
                    //}
                    foreach (DataRow dRow in dt.Rows)
                    {
                        string col = dRow.ItemArray[0].ToString();
                        if (col == prop.Name)
                        {
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                //prop.SetValue(obj, Convert.ChangeType(dr[prop.Name], prop.PropertyType), null);
                                Type t = prop.PropertyType;
                                if (prop.PropertyType.ToString().ToUpper().Contains("NULLABLE`1"))
                                {
                                    t = Type.GetType(prop.PropertyType.ToString().Split('[', ']')[1]);
                                }
                                prop.SetValue(obj, Convert.ChangeType(dr[prop.Name], t), null);
                            }
                            break;
                        }

                    }
                }
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// Executes a stored procedure and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public SqlDataReader ExecDataReaderProc(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes a query and returns the results as a DataSet
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSet(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        public DataSet ExecDataSet(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        public DataSet PrepSqlToDataSet(string qry, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, Params))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        public DataSet SqlToDataSet(string qry)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        public DataSet PrepSPToDataSet(string SPName, string[] Params)
        {
            using (SqlCommand cmd = CreateCommand(SPName, CommandType.StoredProcedure, Params))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        public DataSet SPToDataSet(string qry)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }


        /// <summary>
        /// Executes a stored procedure and returns the results as a Data Set
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSetProc(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        #endregion

        #region Transaction Members

        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <returns>The new SqlTransaction object</returns>
        public SqlTransaction BeginTransaction()
        {
            Rollback();
            _trans = _conn.BeginTransaction();
            return Transaction;
        }

        /// <summary>
        /// Commits any transaction in effect.
        /// </summary>
        public void CloseTransaction()
        {
            if (_trans != null)
            {
                _trans.Commit();
                _trans = null;
            }
        }

        /// <summary>
        /// Rolls back any transaction in effect.
        /// </summary>
        public void Rollback()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                _trans = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Need to dispose managed resources if being called manually
                if (disposing)
                {
                    if (_conn != null)
                    {
                        Rollback();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
                _disposed = true;
            }
        }

        #endregion

        private MethodInfo GetConverterMethod(Type type)
        {
            switch (type.Name.ToUpper())
            {
                case "INT16":
                    return CreateConverterMethodInfo("ToInt16");
                case "INT32":
                    return CreateConverterMethodInfo("ToInt32");
                case "INT64":
                    return CreateConverterMethodInfo("ToInt64");
                case "SINGLE":
                    return CreateConverterMethodInfo("ToSingle");
                case "BOOLEAN":
                    return CreateConverterMethodInfo("ToBoolean");
                case "STRING":
                    return CreateConverterMethodInfo("ToString");
                case "DATETIME":
                    return CreateConverterMethodInfo("ToDateTime");
                case "DECIMAL":
                    return CreateConverterMethodInfo("ToDecimal");
                case "DOUBLE":
                    return CreateConverterMethodInfo("ToDouble");
                case "GUID":
                    return CreateConverterMethodInfo("ToGuid");
                case "BYTE[]":
                    return CreateConverterMethodInfo("ToBytes");
                case "BYTE":
                    return CreateConverterMethodInfo("ToByte");
                case "NULLABLE`1":
                    {
                        if (type == typeof(DateTime?))
                        {
                            return CreateConverterMethodInfo("ToDateTimeNull");
                        }
                        else if (type == typeof(Int32?))
                        {
                            return CreateConverterMethodInfo("ToInt32Null");
                        }
                        else if (type == typeof(Boolean?))
                        {
                            return CreateConverterMethodInfo("ToBooleanNull");
                        }
                        break;
                    }
            }
            return null;
        }

        private MethodInfo CreateConverterMethodInfo(string method)
        {
            return typeof(Converter).GetMethod(method, new Type[] { typeof(object) });
        }
    }
}