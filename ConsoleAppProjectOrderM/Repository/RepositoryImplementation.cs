using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppProjectOrderM.Model;
using SqlServerConnectionLibrary;

namespace ConsoleAppProjectOrderM.Repository
{
    public class RepositoryImplementation:IRepository
    {
        string winConnString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        public async Task AddData(Order order)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO OrderList (OrderID,CustomerName,ProductCode,Quantity,TotalPrice)" + "VALUES(@Id,@CName,@Pcode,@PQuantity,@PPrice)";
                    using (SqlCommand Command = new SqlCommand(query, conn))
                    {

                        Command.Parameters.AddWithValue("@Id", order.Details[0]);
                        Command.Parameters.AddWithValue("@CName", order.Details[1]);
                        Command.Parameters.AddWithValue("@PCode", order.Details[2]);
                        Command.Parameters.AddWithValue("@PQuantity", order.Details[3]);
                        Command.Parameters.AddWithValue("@PPrice", order.Details[4]);
                        await Command.ExecuteNonQueryAsync();
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine("Error Message : id already exist  " + ex.Message);
                }
                conn.Close();
            }
        }
        public async Task<Order> Search1(string code)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM OrderList  WHERE OrderID = @Code";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {

                        command.Parameters.AddWithValue("@Code", code);
                        // using sql data reader

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                Order order = new Order();
                                order.Details[0] = reader[order.DetailHeader[0]].ToString();
                                order.Details[1] = reader[order.DetailHeader[1]].ToString();
                                order.Details[2] = reader[order.DetailHeader[2]].ToString();
                                order.Details[3] = reader[order.DetailHeader[3]].ToString();
                                order.Details[4] = reader[order.DetailHeader[4]].ToString();
                                return order;
                            }
                            else
                            {
                                throw new InvalidOperationException("No  data found with that id.");
                             
                                
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;


                }
                conn.Close();

            }
        }
            public async Task<Order> Search2(string CustomerName)
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
                {    try
                {
                    conn.Open();
                    string query = "SELECT * FROM OrderList  WHERE CustomerName = @Name";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {

                        command.Parameters.AddWithValue("@Name", CustomerName);
                        // using sql data reader
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {


                                Order order = new Order();
                                order.Details[0] = reader[order.DetailHeader[0]].ToString();
                                order.Details[1] = reader[order.DetailHeader[1]].ToString();
                                order.Details[2] = reader[order.DetailHeader[2]].ToString();
                                order.Details[3] = reader[order.DetailHeader[3]].ToString();
                                order.Details[4] = reader[order.DetailHeader[4]].ToString();
                                return order;

                            }
                            else
                            {
                                throw new InvalidOperationException("No  data found with that id.");

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;


               }
               
                conn.Close ();
                }
            }
        

        public async Task EditData1(string code, string quantity)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE OrderList SET Quantity = @Qty WHERE OrderID = @Code";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {

                        command.Parameters.AddWithValue("@Qty", quantity);
                        command.Parameters.AddWithValue("@Code", code);
                        // using sql data reader
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            // Output the result
                            Console.WriteLine($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            throw new Exception("no data updated");
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                }
               conn.Close();
            }

        }
        public async Task DeleteData(string code)
        {

            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    string query = "DELETE FROM OrderList WHERE OrderID = @Code";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@Code", code);

                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were deleted
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Record deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No record found with the specified ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occurred
                    Console.WriteLine("An error occurred: " + ex.Message);
                } // using sql data reader

            }

        }
        public async Task  EditData2(string code, string price)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE OrderList SET TotalPrice = @Prc WHERE OrderID = @Code";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Prc", price);
                        command.Parameters.AddWithValue("@Code", code);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Output the result
                            Console.WriteLine($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            throw new Exception("updation error");
                        }
                        // using sql data reader

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                conn.Close();

            }
        }
    }
}
