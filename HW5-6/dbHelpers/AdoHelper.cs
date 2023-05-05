using HW5_6.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_6.dbHelpers
{
    class AdoHelper : AbstractDbHelper
    {
        SqlConnection connection;

        public AdoHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        public async Task<List<OrderView>> YearOrdersWithReaderAsync()
        {
            var command = connection.CreateCommand();
            command.CommandText = @$"SELECT ord_id, an_name, ord_datetime
                                     FROM Orders 
                                     INNER JOIN Analysis on an_id = ord_an
                                     WHERE YEAR(ord_datetime) = {DateTime.Now.Year}
                                     ORDER BY ord_datetime";

            connection.Open();
            var reader = await command.ExecuteReaderAsync();
            List<OrderView> orders = new List<OrderView>();
            while (reader.Read())
            {
                orders.Add(new OrderView()
                {
                    OrdId = reader.GetInt32(0),
                    AnName = reader.GetString(1),
                    OrdDatetime = reader.GetDateTime(2),
                });
            }
            connection.Close();

            return orders;
        }

        public async Task<List<OrderView>> YearOrdersWithAdapterAsync()
        {
            string commandText = @$"SELECT ord_id, an_name, ord_datetime
                                    FROM Orders
                                    INNER JOIN Analysis on an_id = ord_an
                                    WHERE YEAR(ord_datetime) = {DateTime.Now.Year}
                                    ORDER BY ord_datetime";

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, connection);
            DataSet dataSet = new DataSet();
            await Task.Run(() => adapter.Fill(dataSet));
            connection.Close();

            DataTable orderTable = dataSet.Tables[0];
            List<OrderView> orders = new List<OrderView>();

            foreach (DataRow row in orderTable.Rows)
            {

                orders.Add(new OrderView()
                {
                    OrdId = (int)row[0],
                    AnName = (string)row[1],
                    OrdDatetime = (DateTime)row[2],
                });
            }
            return orders;
        }

        public async Task<bool> CreateAsync(DateTime datetime, int analysisId)
        {
            var command = connection.CreateCommand();
            command.CommandText = @$"INSERT INTO Orders (ord_datetime, ord_an)
                                     VALUES (@date, {analysisId})";
            command.Parameters.Add(new SqlParameter("@date", datetime));

            connection.Open();
            try
            {
                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> UpdateAsync(int id, DateTime datetime, int analysisId)
        {
            var command = connection.CreateCommand();
            command.CommandText = @$"UPDATE Orders SET
                                      ord_datetime = @date,
                                      ord_an = {analysisId}
                                     WHERE ord_id = {id}";
            command.Parameters.Add(new SqlParameter("@date", datetime));

            connection.Open();
            try
            {
                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var command = connection.CreateCommand();
            command.CommandText = @$"DELETE
                                     FROM Orders
                                     WHERE ord_id = {id}";

            connection.Open();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return Convert.ToBoolean(rowsAffected);
        }
    }
}
