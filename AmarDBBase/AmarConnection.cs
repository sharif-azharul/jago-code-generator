using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmarDBBase
{
    public class AmarPWD
    {

    }
    //public class AmarConnection
    //{
    //    private SqlConnection pcon_Conn;
    //    private string pstr_User = "";
    //    private string pstr_Password = "";
    //    private string pstr_Database = "";
    //    private SqlTransaction pobj_Transaction;
    //    private int pint_QueryTimeout = 0;
    //    private Boolean dispose = false;
    //    // private Boolean InTransaction = false;

    //    public AmarConnection(string ConnectionString)
    //    {
    //        pcon_Conn = new SqlConnection(ConnectionString);
    //        pcon_Conn.Open();
    //    }
    //    public AmarConnection(SqlConnection pConnection)
    //    {
    //        pcon_Conn = pConnection;
    //    }

    //    public void BeginTransaction()
    //    {
    //        if (InTransaction == false)
    //        {
    //            if (pobj_Transaction == null)
    //            {
    //                pobj_Transaction = pcon_Conn.BeginTransaction();
    //            }
    //        }
    //    }

    //    public SqlConnection MySqlConnection { get { return pcon_Conn; } }

    //    public SqlTransaction MyTransaction { get { return pobj_Transaction; } }
    //    public void CommitTransaction()
    //    {
    //        if (InTransaction == true)
    //        {
    //            if (pobj_Transaction != null)
    //            {
    //                pobj_Transaction.Commit();
    //                pobj_Transaction.Dispose();
    //                pobj_Transaction = null;
    //            }
    //        }
    //    }
    //    public void RollbackTransaction()
    //    {
    //        if (InTransaction == true)
    //        {
    //            if (pobj_Transaction != null)
    //            {
    //                pobj_Transaction.Rollback();
    //                pobj_Transaction.Dispose();
    //                pobj_Transaction = null;
    //            }
    //        }
    //    }

    //    public void CloseConnection()
    //    {
    //        if (pcon_Conn != null)
    //        { pcon_Conn.Close(); }
    //    }

    //    public Boolean InTransaction { get { return pobj_Transaction != null; } }
    //    public void SetTransaction(SqlTransaction pTransaction)
    //    {
    //        pobj_Transaction = pTransaction;
    //    }

    //    public void OpenDBConection(SqlConnection pConnection)
    //    {
    //        pcon_Conn = pConnection;

    //    }
    //    public void OpenDBConection(string User, string password, string DBName)
    //    {
    //        pcon_Conn = new SqlConnection();
    //        pcon_Conn.ConnectionString = BuildConnectionString(User, password, DBName);

    //        try
    //        {
    //            pcon_Conn.Open();
    //            pint_QueryTimeout = GetQueryTimeout();
    //        }
    //        catch { }

    //    }

    //    private string BuildConnectionString(string User, string password, string DBName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private int GetQueryTimeout()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
