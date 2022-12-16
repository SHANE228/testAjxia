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
    public class tableController : Controller
    {

        //ViewModel db放在INDEX裡面
        public ActionResult Index(ViewModel db)
        {
            DeleteData();
            CheckBellTime();
            using (SqlConnection cn = new SqlConnection())
            {
                string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\txtAjaxyy\txtAjaxyy\App_Data\databaseStaff.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
                cn.ConnectionString = cnstr;
                cn.Open();

                // 把A=B 判斷先放前面，IF判斷放SQL的AND加上getJobID傳回值
                string searchID = "A.jobID = B.jobID";

                if (!string.IsNullOrEmpty(db.getJobID))
                {
                    searchID += " AND A.jobID ='" + db.getJobID + "'";
                }
                //合併兩個Table，注意jobID別重複，會導致Index值重複
                string sqlSearch = "SELECT A.name, B.* FROM staffs A,bellTable B WHERE " + searchID;

                //此方法可以直接在網頁顯示SQL查詢有沒有錯誤
                //Response.Write(sqlSearch);

                SqlCommand cmd = new SqlCommand(sqlSearch, cn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList alst = new ArrayList();
                while (reader.Read())
                {
                    int drAcount = reader.FieldCount;

                    Hashtable ht = new Hashtable();
                    for (int a = 0; a < drAcount; a++)
                    {
                        if (true)
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
                    }

                    alst.Add(ht);

                }
            //放在迴圈外
            db.ayList = alst;
            }
            return View(db);
        }

        public void CheckBellTime()
        {
            //讀取所有檔名 依照大小排列
            string[] files = Directory.GetFiles(@"C:\ftp", "*.txt");

            //讀取最大與最小檔名
            string first = files[0];
            string last = files[files.Length - 1];
            //字串年
            string year = first.Substring(9, 4);
            //字串首月末月
            string lastMonth = last.Substring(13, 2);
            string firstMonth = first.Substring(13, 2);
            //字串首日末日
            string lastDay = last.Substring(15, 2);
            string firstDay = first.Substring(15, 2);
            //字串轉int32
            int _year = Int32.Parse(year);
            int _lastMonth = Int32.Parse(lastMonth);
            int _firstMonth = Int32.Parse(firstMonth);
            int _lastDay = Int32.Parse(lastDay);
            int _firstDay = Int32.Parse(firstDay);

            DateTime start = new DateTime(_year, _firstMonth, _firstDay);
            DateTime end = new DateTime(_year, _lastMonth, _lastDay);
            //for迴圈 DateTime dt第三段要重新指dt = dt.AddDays(1)，不然會0601無窮迴圈
            for (DateTime dt = start; dt < end; dt = dt.AddDays(1))
            {
                string ddt = dt.ToString("yyyyMMdd");
                string emp = "EU" + (ddt);
                string path = "C:\\ftp\\" + emp + ".txt";

                //判斷檔案是否存在
                bool fileExist = System.IO.File.Exists(path);
                //如果存在TXT開始寫入資料庫
                if (fileExist)
                {
                    using (StreamReader sr = new StreamReader(path))
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
                                        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\txtAjaxyy\txtAjaxyy\App_Data\databaseStaff.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
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
                                Console.WriteLine(ex.Message);
                            }
                        }

                    }
                }
                //找不到該檔案執行continue
                else
                {
                    continue;
                }
            }
        }
        public void DeleteData()
        {
            using(SqlConnection cn =new SqlConnection())
            {
                string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\txtAjaxyy\txtAjaxyy\App_Data\databaseStaff.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
                cn.ConnectionString = cnstr;
                cn.Open();
                string sqlDel = "DELETE FROM bellTable";
                SqlCommand cmdDel = new SqlCommand(sqlDel, cn);
                cmdDel.ExecuteNonQuery();
                cn.Close();
            }
        }

    }
    
}