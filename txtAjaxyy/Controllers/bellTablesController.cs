using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using LinqKit;
using System.Net;
using System.Web;
using System.Web.Mvc;
using txtAjaxyy.Models;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace txtAjaxyy.Controllers
{
    public class bellTablesController : Controller
    {
        private belltables db = new belltables();
        private staff db2 = new staff();
        ViewModel dba = new ViewModel();

        // GET: bellTables
        public ActionResult Index(string searchString, int? id)
        {
            using(SqlConnection cn =new SqlConnection())
            {
                string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ym387\source\repos\txtAjaxyy\txtAjaxyy\App_Data\databaseStaff.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
                cn.ConnectionString = cnstr;
                cn.Open();
                string sqlStr = "SELECT A.*, B.* FROM staffs A,bellTables b WHERE A.jobID = B.jobID";
                SqlCommand cmd = new SqlCommand(sqlStr, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList alst = new ArrayList();
                while (reader.Read())
                {
                    int drAcount = reader.FieldCount;

                    Hashtable ht = new Hashtable();
                    for (int a = 0; a < drAcount; a++)
                    {
                        string nowValue = "";
                        try
                        {
                            nowValue = Convert.ToString(reader.GetValue(a));
                        }
                        catch
                        {
                            nowValue = Convert.ToString(reader.GetDouble(a));
                        }

                        //nowValue = EuString.DisplayNull(nowValue);
                        ht.Add(reader.GetName(a), nowValue.ToString());

                    }

                    alst.Add(ht);
                    dba.ayList = alst;
                }

            }

            //var punchBell = from m in db.bellTable
            //                join o in db2.staffs on m.jobID equals o.jobID
            //                select m;
                            //select new ViewModel
                            //{
                            //    work = m.work,
                            //    jobID=m.jobID,
                            //    workTime=m.workTime,
                            //    punchBell=m.punchBell,
                            //    name=o.name 
                            //};

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    punchBell = punchBell.Where(m => m.jobID.Contains(searchString));
            //}
            return View(dba);

        }
        public ActionResult CheckBellTime()
        {
            string[] filePaths = Directory.GetFiles("C:\\ftp");

            foreach (string filePath in filePaths)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            try
                            {
                                //逗點分割
                                string[] array = sr.ReadLine().Split(',');

                                String flag = array[0].ToString();
                                String empid = array[1].ToString();
                                String cord = array[2].ToString();
                                String deviceid = array[3].ToString();

                                //判斷工號前兩個英文
                                String decide = empid.Substring(0, 2);

                                if (decide == "SJ")
                                {
                                    using (SqlConnection cn = new SqlConnection())
                                    {
                                        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ym387\source\repos\txtAjaxyy\txtAjaxyy\App_Data\databaseStaff.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
                                        cn.ConnectionString = cnstr;
                                        cn.Open();




                                        string sqlStr = "INSERT INTO bellTable(work, jobID, workTime, punchBell)" + "VALUES(@work, @jobID, @workTime, @punchBell)";

                                        SqlCommand cmd = new SqlCommand(sqlStr, cn);
                                        cmd.Parameters.Add("@work", SqlDbType.Int).Value = int.Parse(array[0]);
                                        cmd.Parameters.Add("@jobID", SqlDbType.NVarChar).Value = array[1];
                                        cmd.Parameters.Add("@workTime", SqlDbType.DateTime).Value = DateTime.ParseExact(array[2], "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
                                        cmd.Parameters.Add("@punchBell", SqlDbType.NVarChar).Value = array[3];
                                        cmd.ExecuteNonQuery();
                                        cn.Close();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                }

            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
