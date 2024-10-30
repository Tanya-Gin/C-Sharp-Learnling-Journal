using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MineSweeper1.DAL.SQLHelper
{
    public class SQLHelper
    {
        public static string connString = "Data Source=.;Initial Catalog=MineSweeper; Integrated Security = true";
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <exception cref="Exception"></exception>
        public static void Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("public static void Update(string sql)方法出现异常：" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 返回多个数据库查询
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(" public static SqlDataReader GetReader(string sql)方法出现异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 返回单个数据库查询
        /// </summary>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("public static object GetSingleResult(string sql)方法出现异常：" + ex.Message);
            }
            finally { conn.Close(); }
        }
    }
}
