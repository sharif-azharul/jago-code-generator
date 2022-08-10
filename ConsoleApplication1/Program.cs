using AmarDBHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            #region commented
            //var fromAddress = new MailAddress("sharif.azharul@gmail.com", "azhar");
            //var toAddress = new MailAddress("lahor_ice@yahoo.com ", "Ashfia");
            //const string fromPassword = "qxyotolntgxjfjoe";
            //const string subject = "Test mail from application";
            //const string body = "If u get the email plzz reply";

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
            //    Timeout = 20000
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}

            //DC obj = new DC();

            //obj.GetTaskAll();
            //TestTemplate t = new TestTemplate();

            #endregion


            //CalcAreaPointer cp = new CalcAreaPointer(
            //    delegate (int r)
            //    {
            //        return 3.14 * r * r;

            //    }
            //    );

            Func<int, double> cp1 = r => 3.14 * r * r;
            double area = cp1(5);
            Console.WriteLine("Area: " + area);
            Action<string> MyAction = str => Console.WriteLine(str);
            MyAction("Hello Rumman");

            Predicate<string> pr = r => r.Length > 5;
            Console.WriteLine(pr("Rumman"));
            Console.WriteLine("success");
            Console.ReadLine();
        }





    }

    public class DC : BaseDC
    {
        AdoHelper DB = null;
        //public DC() {
        //    DB = new AdoHelper(base.cnstr);
        //}
        public void Insert()
        {

            string[] Params = new string[1];
            string qry = @"INSERT INTO [T_Nationality]
                           ([NationalityName])
                             VALUES
                           (@NationalityName)";



            using (AdoHelper db = new AdoHelper(base.cnstr))
            {
                try
                {
                    db.BeginTransaction();
                    Params[0] = db.CreateParameter("@NationalityName", "Bangladeshi", DbType.String);
                    int result = db.ExecNonQuery(qry, Params);

                    Params = new string[1];
                    qry = @"INSERT INTO [T_Religion]
                       ([Religion])
                        VALUES
                       (@Religion)";


                    Params[0] = db.CreateParameter("@Religion", "Islam", DbType.String);
                    result = db.ExecNonQuery(qry, Params);
                    db.CloseTransaction();
                }

                catch (Exception ex)
                {
                    db.Rollback();
                    throw ex;
                }
            }

        }

        public void GetTaskAll()
        {
            string[] Params = new string[2];
            string qry = "SELECT *  FROM [T_Task]";
            List<TaskViewModel> tasks = new List<TaskViewModel>();
            using (AdoHelper db = new AdoHelper(base.cnstr))
            {
                tasks = db.SqlToList<TaskViewModel>(qry);
            }
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
                            prop.SetValue(obj, dr[prop.Name], null);
                            break;
                        }

                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static T DataReaderMapToModel<T>(SqlDataReader dr)
        {
            DataTable dt = dr.GetSchemaTable();
            T obj = default(T);
            obj = Activator.CreateInstance<T>();

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    string col = dRow.ItemArray[0].ToString();
                    if (col == prop.Name)
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                        break;
                    }

                }
            }
            return obj;
        }

        public void TestAdoHelper()
        {
            string[] Params = new string[2];
            string qry = "SELECT *  FROM [User] where email= @email and  UserName= @name ";

            using (AdoHelper db = new AdoHelper(base.cnstr))
            {
                Params[0] = db.CreateParameter("@email", "s@c.com", DbType.String.ToString());
                Params[1] = db.CreateParameter("@name", "s@c.com", DbType.String.ToString());

                using (SqlDataReader rdr = db.ExecDataReader(qry, Params))
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr["UserId"]);
                    }
                }
            }
        }
    }

    public class BaseDC
    {
        string connectionName = "myConnection";
        public string cnstr = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
    }

    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan TSWorkingHour
        {
            get
            {
                TimeSpan diff = EndTime - StartTime;
                return diff;
            }
            set { }
        }
        public string WorkingHour
        {
            get
            {
                TimeSpan diff = EndTime - StartTime;
                return diff.ToString("h'h 'm'm 's's'");
                //double hours = diff.TotalHours;
                //return hours;
            }
            set { }
        }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }

        public Boolean IsSelectedReport { get; set; }
        public string trColor { get; set; }
        public string UpdateDate { get; set; }
    }
}
