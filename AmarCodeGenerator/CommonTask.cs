using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    public static class CommonTask
    {
        public static void LogError(Exception ex)
        {

            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sPathName = @"ErrorLog\";
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string sErrorTime = sYear + sMonth + sDay;
            if (!Directory.Exists(sPathName))
            {
                Directory.CreateDirectory(sPathName);
            }
            StreamWriter sw = new StreamWriter(sPathName + "ErrorLog" + sErrorTime + ".txt", true);
            sw.WriteLine(sLogFormat + ex.Message);
            sw.Flush();
            sw.Close();
            MessageBox.Show(ex.Message);
        }
        //........... Connection with database .................
        public static bool ConnectToDatabase(string SQL_CONN_STRING)
        {
            bool isConnected = false;
            try
            {
                SessionUtility.connection = new SqlConnection(SQL_CONN_STRING);
                SessionUtility.connection.Open();

                if (SessionUtility.connection != null)
                {
                    isConnected = true;
                }

            }
            catch
            {
                isConnected = false;
                MessageBox.Show("Login failed");
            }

            return isConnected;
        }

        public static Boolean CreateDirectory(string pDirectory)
        {
            if (!Directory.Exists(pDirectory))
                Directory.CreateDirectory(pDirectory);
            return true;
        }

        public static void WriteDefaultConstructor(StreamWriter sw, string ClassName)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\n\t\t#region Constructor");
                sb.Append("\r\n\t\tpublic ");
                sb.Append(ClassName);
                sb.Append("()\r\n\t\t{}");
                sb.Append("\r\n\t\t#endregion");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string PrepareInParameter(ColumnModel column, string pObjectName)
        {
            string strParameter = string.Empty;
            const string dq = @"""";
            const string consTemp = @"@P_";
            string temp = dq + consTemp + column.DBName + dq;
            switch (column.SYSType.Trim())
            {
                case "String":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(string.IsNullOrEmpty(" + pObjectName + "." + column.SYSName + "))";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";

                    }
                    break;
                case "DateTime":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + " != DateTime.MinValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";

                    }
                    break;
                case "Int16?":
                case "Int32?":
                case "Int64?":
                case "Int?":
                case "DateTime?":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
                        strParameter = strParameter.Replace('?', ' ');
                    }
                    break;
                case "Guid?":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
                        strParameter = strParameter.Replace('?', ' ');
                    }
                    break;

                case "Guid":
                    {
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
                    }
                    break;

                case "decimal?":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal," + pObjectName + "." + column.SYSName + ");";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal" + ", DBNull.Value );";

                    }
                    break;

                case "decimal":
                    {
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal" + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
                    }
                    break;

                default:
                    { strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");"; }
                    break;
            }
            return strParameter;

        }

        public static string PrepareInParameterForAmarHelper(int i, ColumnModel column, string pObjectName)
        {
            string strParameter = string.Empty;
            const string dq = @"""";
            const string consTemp = @"@P_";
            string temp = dq + consTemp + column.DBName + dq;

            strParameter = string.Format("\r\n\t\t\t\tParams[{0}] = db.CreateParameter({1}, {2}.{3}.ToString(), DbType.{4});", i, temp, pObjectName, column.SYSName, column.SYSType);
            strParameter = strParameter.Replace('?', ' ').Replace(".string", ".String");
            //switch (column.SYSType.Trim())
            //{
            //    case "String":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t if(string.IsNullOrEmpty(" + pObjectName + "." + column.SYSName + "))";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
            //            strParameter = strParameter + "\r\n\t\t\telse";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";

            //        }
            //        break;
            //    case "DateTime":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + " != DateTime.MinValue)";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";
            //            strParameter = strParameter + "\r\n\t\t\telse";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";

            //        }
            //        break;
            //    case "Int16?":
            //    case "Int32?":
            //    case "Int64?":
            //    case "Int?":
            //    case "DateTime?":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";
            //            strParameter = strParameter + "\r\n\t\t\telse";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
            //            strParameter = strParameter.Replace('?', ' ');
            //        }
            //        break;
            //    case "Guid?":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
            //            strParameter = strParameter + "\r\n\t\t\telse";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
            //            strParameter = strParameter.Replace('?', ' ');
            //        }
            //        break;

            //    case "Guid":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
            //        }
            //        break;

            //    case "decimal?":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal," + pObjectName + "." + column.SYSName + ");";
            //            strParameter = strParameter + "\r\n\t\t\telse";
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal" + ", DBNull.Value );";

            //        }
            //        break;

            //    case "decimal":
            //        {
            //            strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal" + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
            //        }
            //        break;

            //    default:
            //        { strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");"; }
            //        break;
            //}
            return strParameter;

        }
        public static string PrepareValueFromDB(string pType, string pValueName)
        {
            string strModelAttributes = string.Empty;
            switch (pType)
            {
                case "DateTime?":
                    {
                        strModelAttributes = "Convert.ToDateTime(" + pValueName + ");";
                    }
                    break;

                case "Int16?":
                case "short?":
                    {
                        strModelAttributes = " Convert.ToInt16(" + pValueName + ");";
                    }
                    break;
                case "int?":
                case "Int32?":
                    {
                        strModelAttributes = " Convert.ToInt32(" + pValueName + ");";
                    }
                    break;
                case "decimal?":
                case "decimal":
                    {
                        strModelAttributes = " Convert.ToDecimal(" + pValueName + ");";
                    }
                    break;
                case "Int64?":
                case "long?":
                    {
                        strModelAttributes = " Convert.ToInt64(" + pValueName + ");";
                    }
                    break;
                default:
                    { strModelAttributes = pValueName; }
                    break;
            }


            return strModelAttributes;
        }

        public static string PrepareModelAttributeFromDB(ColumnModel column, string p)
        {
            string strModelAttributes = string.Empty;
            switch (column.SYSType.Trim())
            {
                case "DateTime":
                case "DateTime?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Boolean":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Bool":
                case "bool":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Int16?":
                case "short?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? (short)0 : Convert.ToInt16(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;

                case "Int32?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "decimal?":
                case "decimal":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "int?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Int64?":
                case "long?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                default:
                    { strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? string.Empty : dr[""" + column.DBName + @"""].ToString();"; }
                    break;
            }


            return strModelAttributes;
        }

        public static string PrepareMailContent(dynamic master, string pTemplateName)
        {
            string dirName = AppDomain.CurrentDomain.BaseDirectory; // Starting Dir
            FileInfo fileInfo = new FileInfo(dirName);
            DirectoryInfo parentDir = fileInfo.Directory.Parent;
            string parentDirName = parentDir.FullName; // Parent of Starting Dir

            FileInfo fileInfo2 = new FileInfo(parentDirName);
            DirectoryInfo parentDir2 = fileInfo2.Directory;
            string parentDirName2 = parentDir2.FullName; // Parent of Starting Dir


            StringBuilder sbContent = new StringBuilder();
            string TemplatePath = Path.GetFullPath(parentDirName2 + "\\Templates\\" + pTemplateName);
            var template = System.IO.File.ReadAllText(TemplatePath);
            try
            {
                //sbContent.Append(RazorEngine.Razor.Parse(template, master));
                var templateService = new TemplateService();
                sbContent.Append(templateService.Parse(template, master, null, null));
            }
            catch (Exception ex) { }
            return sbContent.ToString();
        }

        public static string PrepareMailContentWithTemplatePath(dynamic master, string pTemplateName,string path)
        {
            string dirName = AppDomain.CurrentDomain.BaseDirectory; // Starting Dir
            FileInfo fileInfo = new FileInfo(dirName);
            DirectoryInfo parentDir = fileInfo.Directory.Parent;
            string parentDirName = parentDir.FullName; // Parent of Starting Dir

            FileInfo fileInfo2 = new FileInfo(parentDirName);
            DirectoryInfo parentDir2 = fileInfo2.Directory;
            string parentDirName2 = parentDir2.FullName; // Parent of Starting Dir


            StringBuilder sbContent = new StringBuilder();
            string TemplatePath = Path.GetFullPath(parentDirName2 + "\\Templates\\"+ path + "\\" + pTemplateName);
            var template = System.IO.File.ReadAllText(TemplatePath);
            try
            {
                //sbContent.Append(RazorEngine.Razor.Parse(template, master));
                var templateService = new TemplateService();
                sbContent.Append(templateService.Parse(template, master, null, null));
            }
            catch (Exception ex) { }
            return sbContent.ToString();
        }
        public static String RemoveUnderscoreAndTitleString(String s)
        {
            if (s == null) return s;

            String[] words = s.Split('_');
            //words.RemoveAt(0);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 0)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join("", words, 0, words.Length);
        }

        public static String RemoveUnderscoreAddSpaceAndUpperFirst(String s)
        {
            if (s == null) return s;

            String[] words = s.Split('_');
            //words.RemoveAt(0);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 0)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words, 0, words.Length);
        }
        //public static DbType GetDbType(object SystemType)
        //{
        //    var   typeMap = new Dictionary<Type, DbType>();
        //    typeMap[typeof(byte)] = DbType.Byte;
        //    typeMap[typeof(sbyte)] = DbType.SByte;
        //    typeMap[typeof(short)] = DbType.Int16;
        //    typeMap[typeof(ushort)] = DbType.UInt16;
        //    typeMap[typeof(int)] = DbType.Int32;
        //    typeMap[typeof(uint)] = DbType.UInt32;
        //    typeMap[typeof(long)] = DbType.Int64;
        //    typeMap[typeof(ulong)] = DbType.UInt64;
        //    typeMap[typeof(float)] = DbType.Single;
        //    typeMap[typeof(double)] = DbType.Double;
        //    typeMap[typeof(decimal)] = DbType.Decimal;
        //    typeMap[typeof(bool)] = DbType.Boolean;
        //    typeMap[typeof(string)] = DbType.String;
        //    typeMap[typeof(char)] = DbType.StringFixedLength;
        //    typeMap[typeof(Guid)] = DbType.Guid;
        //    typeMap[typeof(DateTime)] = DbType.DateTime;
        //    typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
        //    typeMap[typeof(byte[])] = DbType.Binary;
        //    typeMap[typeof(byte?)] = DbType.Byte;
        //    typeMap[typeof(sbyte?)] = DbType.SByte;
        //    typeMap[typeof(short?)] = DbType.Int16;
        //    typeMap[typeof(ushort?)] = DbType.UInt16;
        //    typeMap[typeof(int?)] = DbType.Int32;
        //    typeMap[typeof(uint?)] = DbType.UInt32;
        //    typeMap[typeof(long?)] = DbType.Int64;
        //    typeMap[typeof(ulong?)] = DbType.UInt64;
        //    typeMap[typeof(float?)] = DbType.Single;
        //    typeMap[typeof(double?)] = DbType.Double;
        //    typeMap[typeof(decimal?)] = DbType.Decimal;
        //    typeMap[typeof(bool?)] = DbType.Boolean;
        //    typeMap[typeof(char?)] = DbType.StringFixedLength;
        //    typeMap[typeof(Guid?)] = DbType.Guid;
        //    typeMap[typeof(DateTime?)] = DbType.DateTime;
        //    typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
        //    //typeMap[typeof(System.Data.Linq.Binary)] = DbType.Binary;
        //   return typeMap[typeof(SystemType)];
        //}
    }
  
    
}
