using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Notes.Controller
{
    public class SqlDatabase
    {
        public SqlDatabase()
        {

        }

        #region Sql 連接
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        private SqlConnection GetSqlConnection()
        {
            try
            { return new SqlConnection(connectionString); }
            catch
            {
                throw new ArgumentNullException("The SqlServer Is Not Valid");
            }
        }

        #endregion

        #region  運行儲存過程

        /// <summary>
        ///  運行存儲過程 返回存儲過程返回值
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <returns>存儲過程返回值</returns>
        public int RunProcNonQueryReturn(string procedureName)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, null);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return (int)cmd.Parameters["ReturnValue"].Value;
            }
        }

        /// <summary>
        /// 運行存儲過程 返回存儲過程返回值
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <param name="parameter">存儲過程參數</param>
        /// <returns>存儲過程返回值</returns>
        public int RunProcNonQueryReturn(string procedureName, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, parameter);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return (int)cmd.Parameters["ReturnValue"].Value;
            }
        }

        public void RunProcNonQuery(string procedureName)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, null);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 運行存儲過程 獲取存儲過程輸出參數值
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <param name="parameter">存儲過程參數</param>
        public void RunProcNonQuery(string procedureName, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, parameter);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 運行存儲過程
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <returns>返回第一條記錄第一個</returns>
        public int RunProcScalar(string procedureName)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, null);
                int tmp;
                tmp = (int)cmd.ExecuteScalar();
                sqlConnection.Close();
                return tmp;
            }
        }


        /// <summary>
        /// 運行存儲過程
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <param name="parameter">存儲過程參數</param>
        /// <returns>返回第一條記錄第一個</returns>
        public int RunProcScalar(string procedureName, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, parameter);
                int tmp;
                tmp = (int)cmd.ExecuteScalar();
                sqlConnection.Close();
                return tmp;
            }
        }

        /// <summary>
        /// 運行存儲過程並返回 DataReader
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <returns>返回一個新的 SqlDataReader 物件</returns>
        public SqlDataReader RunProcReader(string procedureName)
        {
            //using ( SqlConnection sqlConnection = GetSqlConnection() )
            //{
            SqlConnection sqlConnection = GetSqlConnection();
            SqlCommand cmd = CreateCommand(sqlConnection, procedureName, null);
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //}
        }

        /// <summary>
        /// 運行存儲過程並返回 DataReader
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <param name="parameter">存儲過程參數</param>
        /// <returns>返回一個新的 SqlDataReader 物件</returns>
        public SqlDataReader RunProcReader(string procedureName, SqlParameter[] parameter)
        {
            //using ( SqlConnection sqlConnection = GetSqlConnection() )
            //{
            SqlConnection sqlConnection = GetSqlConnection();
            SqlCommand cmd = CreateCommand(sqlConnection, procedureName, parameter);
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //}
        }

        /// <summary>
        /// 運行存儲過程返回DataSet
        /// </summary>
        /// <param name="procedureName">存儲過程名</param>
        /// <returns>返回DataSet</returns>
        public DataSet RunProcDataSet(string procedureName)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, null);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                da.Fill(dataSet);
                sqlConnection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 運行存儲過程返回DataSet
        /// </summary>
        /// <param name="procedureName">存儲過程名稱</param>
        /// <param name="parameter">存儲過程參數</param>
        /// <returns>返回DataSet</returns>
        public DataSet RunProcDataSet(string procedureName, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateCommand(sqlConnection, procedureName, parameter);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                da.Fill(dataSet);
                sqlConnection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 創建一個SqlCommand物件以此來執行存儲過程
        /// </summary>
        /// <param name="sqlConnection">sql連結</param>
        /// <param name="procedureName">存儲過程名稱</param>
        /// <param name="parameter">存儲過程參數</param>
        /// <returns>返回SqlCommand對象</returns>

        private SqlCommand CreateCommand(SqlConnection sqlConnection, string procedureName, SqlParameter[] parameter)
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
            SqlCommand command = new SqlCommand(procedureName, sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameter != null)
            {
                foreach (SqlParameter param in parameter)
                {
                    command.Parameters.Add(param);
                }
            }

            /// 加入返回参数
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null));

            return command;
        }

        #endregion

        #region  生成儲存過程參數
        ///  示例
        ///  SqlDataProvider myData = new SqlDataProvider();
        ///  SqlParameter[] prams = { myData.CreateInParam("@ID",SqlDbType.Int,4,1),
        ///         myData.CreateOutParam("@OutParam",SqlDbType.Int,4)
        ///        }


        /// <summary>
        /// 生成存儲過程參數
        /// </summary>
        /// <param name="parameterName">存儲過程名稱</param>
        /// <param name="dataType">參數類型</param>
        /// <param name="size">參數大小</param>
        /// <param name="parameterDirection">參數方向</param>
        /// <param name="parameterValue">參數值</param>
        /// <returns>新的 parameter 物件</returns>

        public SqlParameter CreateParam(string parameterName, SqlDbType dataType, Int32 size, ParameterDirection parameterDirection, object parameterValue)
        {
            SqlParameter parameter;

            if (size > 0)
                parameter = new SqlParameter(parameterName, dataType, size);
            else
                parameter = new SqlParameter(parameterName, dataType);

            parameter.Direction = parameterDirection;
            if (!(parameterDirection == ParameterDirection.Output && parameterValue == null))
                parameter.Value = parameterValue;

            return parameter;
        }

        /// <summary>
        /// 傳入輸入參數
        /// </summary>
        /// <param name="parameterName">存儲過程名稱</param>
        /// <param name="dataType">參數類型</param>
        /// <param name="size">參數大小</param>
        /// <param name="parameterValue">參數值</param>
        /// <returns>新的 parameter 物件</returns>

        public SqlParameter CreateInParam(string parameterName, SqlDbType dataType, Int32 size, object parameterValue)
        {
            return CreateParam(parameterName, dataType, size, ParameterDirection.Input, parameterValue);
        }

        /// <summary>
        /// 傳入返回值參數
        /// </summary>
        /// <param name="parameterName">存儲過程名稱</param>
        /// <param name="dataType">參數類型</param>
        /// <param name="size">參數大小</param>
        /// <param name="parameterValue">參數值</param>
        /// <returns>新的 parameter 物件</returns>

        public SqlParameter CreateOutParam(string parameterName, SqlDbType dataType, Int32 size)
        {
            return CreateParam(parameterName, dataType, size, ParameterDirection.Output, null);
        }


        #endregion

        #region  運行 SQL 語句

        /// <summary>
        /// 運行 SQL 語句 無返回值
        /// </summary>
        /// <param name="strSql">SQL語句</param>

        public void RunSqlNonQuery(string strSql)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, null);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 運行 SQL 語句 無返回值
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <param name="parameter">構建SQL語句參數</param>

        public void RunSqlNonQuery(string strSql, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, parameter);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 運行 SQL 語句
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <returns>返回int</returns>

        public int RunSqlScalar(string strSql)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, null);
                int tmp = (int)cmd.ExecuteScalar();
                sqlConnection.Close();
                return tmp;
            }
        }

        /// <summary>
        ///  運行 SQL 語句
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <param name="parameter">構建SQL語句參數</param>
        /// <returns>返回int</returns>

        public int RunSqlScalar(string strSql, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, parameter);
                int tmp = (int)cmd.ExecuteScalar();
                sqlConnection.Close();
                return tmp;
            }
        }

        /// <summary>
        /// 運行 SQL 語句 返回DataReader
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <returns>返回一個SqlDataReader 物件</returns>

        public SqlDataReader RunSqlReader(string strSql)
        {
            //using ( SqlConnection sqlConnection = GetSqlConnection() )
            //{
            SqlConnection sqlConnection = GetSqlConnection();
            SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, null);
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //}
        }

        /// <summary>
        /// 運行 SQL 語句 返回DataReader
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <param name="parameter">構建SQL語句參數</param>
        /// <returns>返回一個SqlDataReader 物件</returns>

        public SqlDataReader RunSqlReader(string strSql, SqlParameter[] parameter)
        {
            //using ( SqlConnection sqlConnection = GetSqlConnection() )
            //{
            SqlConnection sqlConnection = GetSqlConnection();
            SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, parameter);
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //}
        }

        /// <summary>
        /// 運行 SQL 語句 返回DataSet
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <returns>返回DataSet</returns>

        public DataSet RunSqlDataSet(string strSql)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, null);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                da.Fill(dataSet);
                sqlConnection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 運行 SQL 語句 返回DataSet
        /// </summary>
        /// <param name="strSql">SQL語句</param>
        /// <param name="parameter">構建SQL語句參數</param>
        /// <returns>返回DataSet</returns>

        public DataSet RunSqlDataSet(string strSql, SqlParameter[] parameter)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = CreateSqlCommand(sqlConnection, strSql, parameter);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                da.Fill(dataSet);
                sqlConnection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 根據SQL語句生成 SqlCommand 物件
        /// </summary>
        /// <param name="sqlConnection">SqlConnection 連接</param>
        /// <param name="strSql">SQL語句</param>
        /// <param name="parameter">構造SQL語句參數</param>
        /// <returns>返回一個新的 SqlCommand 物件</returns>

        public SqlCommand CreateSqlCommand(SqlConnection sqlConnection, string strSql, SqlParameter[] parameter)
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
            SqlCommand command = new SqlCommand(strSql, sqlConnection);

            if (parameter != null)
            {
                foreach (SqlParameter param in parameter)
                {
                    command.Parameters.Add(param);
                }
            }

            return command;
        }
        #endregion

        #region  生成 建構SQL語句 參數

        /// <summary>
        /// 根據SQL語句生成SqlParameter物件
        /// </summary>
        /// <param name="parameterName">SqlParameter名</param>
        /// <param name="dbType">SqlParameter DbType</param>
        /// <param name="size">SqlParameter 大小</param>
        /// <param name="parameterValue">SqlParameter 值</param>
        /// <returns>返回SqlParameter對象</returns>

        public SqlParameter CreateSqlParam(string parameterName, SqlDbType dbType, Int32 size, object parameterValue)
        {
            SqlParameter parameter;
            parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = parameterValue;
            return parameter;
        }
        #endregion

        #region IDisposable 成員

        public void Dispose()
        {

        }

        #endregion
    }
}