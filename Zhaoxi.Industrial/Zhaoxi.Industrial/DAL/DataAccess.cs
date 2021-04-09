using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Industrial.DAL
{
    public class DataAccess
    {
        private string strDBConfig = ConfigurationManager.ConnectionStrings["db_config"].ToString();
        SqlConnection conn = null;
        SqlCommand comm = null;
        SqlDataAdapter adapter = null;
        SqlTransaction trans = null;

        private static DataAccess instance = null;
        private DataAccess() { }
        public static DataAccess GetInstance()
        {
            lock ("da")
            {
                if (instance == null)
                    instance = new DataAccess();
                return instance;
            }
        }

        private void Dispose()
        {
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (trans != null)
            {
                trans.Dispose();
                trans = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        private bool DBConnection()
        {
            if (conn == null)
                conn = new SqlConnection(strDBConfig);
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region 错误信息写入日志
        /// <summary>
        /// 将错误信息写入日志文件
        /// </summary>
        /// <param name="msg"></param>
        public void WriteLog(string msg)
        {
            string file = Path.Combine(Environment.CurrentDirectory, "log-" + DateTime.Now.ToString("yyyyMMdd") + ".log");
            FileStream fs = new FileStream(file, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("[{0}]  错误信息：{1}", DateTime.Now.ToString(), msg);
            sw.Close();
            fs.Close();
        }
        #endregion


        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable GetDatas(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (DBConnection())
                {
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }

            return dt;
        }


        public DataTable GetStorageArea()
        {
            string strSql = "select * from storage_area";
            return GetDatas(strSql);
        }

        public DataTable GetDevices()
        {
            string strSql = "select * from devices";
            return GetDatas(strSql);
        }

        public DataTable GetMonitorValues()
        {
            string strSql = "select * from monitor_values order by d_id,address";
            return GetDatas(strSql);
        }
    }
}
