using AmarCodeGenerator.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    public partial class FrmAspnetZero : Form
    {
        public FrmAspnetZero()
        {
            InitializeComponent();
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {
            var spList = GetSPList();
            var o = spList.FirstOrDefault(x => x.Name == "HR_Attendance_Data_Process");
        }

        private List<StoredProcedureModel> GetSPList()
        {
            List<StoredProcedureModel> sPList = new List<StoredProcedureModel>();
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SparkExecutive;Data Source=DESKTOP-BPLNFDB";
            try
            {
                Database db = new SqlDatabase(connectionString);
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("SELECT ");
                sb.AppendLine("  ROUTINE_SCHEMA, ");
                sb.AppendLine("  ROUTINE_NAME ");
                sb.AppendLine("FROM INFORMATION_SCHEMA.ROUTINES ");
                sb.AppendLine("WHERE ROUTINE_TYPE = 'PROCEDURE' ");

                DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        while (dr.Read())
                        {
                            StoredProcedureModel obj = new StoredProcedureModel();
                            obj.Name = dr["ROUTINE_NAME"].ToString();
                            obj.SchemaName = dr["ROUTINE_SCHEMA"].ToString();
                            sPList.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (var item in sPList)
            {
                item.Parameters = GetSpParameterList(item, connectionString);
                item.Outputs = GetSpOutputList(item, connectionString);
            }
            return sPList;
        }

        private List<SpParameterModel> GetSpParameterList(StoredProcedureModel item, string connectionString)
        {
            List<SpParameterModel> objList = new List<SpParameterModel>();

            try
            {
                Database db = new SqlDatabase(connectionString);
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" SELECT ");

                sb.AppendLine("   'Parameter_name' = name,   ");
                sb.AppendLine("   'Type' = type_name(user_type_id),   ");
                sb.AppendLine("   'Length' = max_length,   ");
                sb.AppendLine("   'Prec' = case when type_name(system_type_id) = 'uniqueidentifier' ");
                sb.AppendLine("              then precision ");
                sb.AppendLine("              else OdbcPrec(system_type_id, max_length, precision) end,   ");
                sb.AppendLine("   'Scale' = OdbcScale(system_type_id, scale),   ");
                sb.AppendLine("   'Param_order' = parameter_id,   ");
                sb.AppendLine("   'Collation' = convert(sysname, ");
                sb.AppendLine("                   case when system_type_id in (35, 99, 167, 175, 231, 239) ");
                sb.AppendLine("                   then ServerProperty('collation') end) ");
                sb.AppendLine(" ,is_output ");
                sb.AppendLine($"  from sys.parameters where object_id = object_id('{item.Name}') ");

                DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        while (dr.Read())
                        {
                            SpParameterModel obj = new SpParameterModel();
                            obj.ParameterName = dr["Parameter_name"].ToString();
                            obj.DataType = dr["Type"].ToString();
                            obj.Length = dr["Length"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Length"].ToString());
                            obj.Prec = dr["Prec"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Prec"].ToString());
                            obj.Scale = dr["Scale"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Scale"].ToString());
                            obj.ParameterOrder = dr["Param_order"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Param_order"].ToString());
                            obj.IsOutput = (dr["is_output"] != DBNull.Value && dr["is_output"].ToString() == "True") ? true : false;
                            objList.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objList;
        }

        private List<SpOutputModel> GetSpOutputList(StoredProcedureModel item, string connectionString)
        {
            List<SpOutputModel> objList = new List<SpOutputModel>();

            try
            {
                Database db = new SqlDatabase(connectionString);
                StringBuilder sb = new StringBuilder();


                sb.AppendLine(" Begin                                               ");
                sb.AppendLine("                                                     ");
                sb.AppendLine(" IF OBJECT_ID('tempdb..#Temp') IS NOT NULL           ");
                sb.AppendLine("              DROP TABLE #Temp                       ");
                sb.AppendLine(" CREATE TABLE #Temp (                                ");
                sb.AppendLine("     is_hidden bit NOT NULL,                         ");
                sb.AppendLine("     column_ordinal int NOT NULL,                    ");
                sb.AppendLine("     name sysname NULL,                              ");
                sb.AppendLine("     is_nullable bit NOT NULL,                       ");
                sb.AppendLine("     system_type_id int NOT NULL,                    ");
                sb.AppendLine("     system_type_name nvarchar(256) NULL,            ");
                sb.AppendLine("     max_length smallint NOT NULL,                   ");
                sb.AppendLine("     precision tinyint NOT NULL,                     ");
                sb.AppendLine("     scale tinyint NOT NULL,                         ");
                sb.AppendLine("     collation_name sysname NULL,                    ");
                sb.AppendLine("     user_type_id int NULL,                          ");
                sb.AppendLine("     user_type_database sysname NULL,                ");
                sb.AppendLine("     user_type_schema sysname NULL,                  ");
                sb.AppendLine("     user_type_name sysname NULL,                    ");
                sb.AppendLine("     assembly_qualified_type_name nvarchar(4000),    ");
                sb.AppendLine("     xml_collection_id int NULL,                     ");
                sb.AppendLine("     xml_collection_database sysname NULL,           ");
                sb.AppendLine("     xml_collection_schema sysname NULL,             ");
                sb.AppendLine("     xml_collection_name sysname NULL,               ");
                sb.AppendLine("     is_xml_document bit NOT NULL,                   ");
                sb.AppendLine("     is_case_sensitive bit NOT NULL,                 ");
                sb.AppendLine("     is_fixed_length_clr_type bit NOT NULL,          ");
                sb.AppendLine("     source_server nvarchar(128),                    ");
                sb.AppendLine("     source_database nvarchar(128),                  ");
                sb.AppendLine("     source_schema nvarchar(128),                    ");
                sb.AppendLine("     source_table nvarchar(128),                     ");
                sb.AppendLine("     source_column nvarchar(128),                    ");
                sb.AppendLine("     is_identity_column bit NULL,                    ");
                sb.AppendLine("     is_part_of_unique_key bit NULL,                 ");
                sb.AppendLine("     is_updateable bit NULL,                         ");
                sb.AppendLine("     is_computed_column bit NULL,                    ");
                sb.AppendLine("     is_sparse_column_set bit NULL,                  ");
                sb.AppendLine("     ordinal_in_order_by_list smallint NULL,         ");
                sb.AppendLine("     order_by_list_length smallint NULL,             ");
                sb.AppendLine("     order_by_is_descending smallint NULL,           ");
                sb.AppendLine("     tds_type_id int NOT NULL,                       ");
                sb.AppendLine("     tds_length int NOT NULL,                        ");
                sb.AppendLine("     tds_collation_id int NULL,                      ");
                sb.AppendLine("     tds_collation_sort_id tinyint NULL              ");
                sb.AppendLine(" )                                                   ");
                sb.AppendLine("                                                     ");
                sb.AppendLine(" INSERT INTO #Temp                                   ");
                sb.AppendLine($" exec sp_describe_first_result_set N'{item.Name}'  ");
                sb.AppendLine("  ");
                sb.AppendLine(" select name, system_type_name from #Temp ");
                sb.AppendLine(" END ");

                DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        while (dr.Read())
                        {
                            SpOutputModel obj = new SpOutputModel();
                            obj.Name = dr["name"].ToString();
                            obj.DbTypeName = dr["system_type_name"].ToString();
                            objList.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return objList;
            }

            return objList;
        }
    }
}
