
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppProjectLibraryM.Model;
using SqlServerConnectionLibrary;

namespace ConsoleAppProjectLibraryM.Repository
{
    public class RepositoryImplementation : IRepository
    {
        string winConnString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        public async Task AddData(Book book)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO BookList (BookCode,Title,Author,Genre,Price)" + "VALUES(@Id,@CName,@Pcode,@PGenre,@PPrice)";
                    using (SqlCommand Command = new SqlCommand(query, conn))
                    {

                        Command.Parameters.AddWithValue("@Id", book.Details[0]);
                        Command.Parameters.AddWithValue("@CName", book.Details[1]);
                        Command.Parameters.AddWithValue("@PCode", book.Details[2]);
                        Command.Parameters.AddWithValue("@PGenre", book.Details[3]);
                        Command.Parameters.AddWithValue("@PPrice", book.Details[4]);
                        await Command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Message : id already exist  " + ex.Message);
                }
                conn.Close();
            }
        }
        public async Task<Book> Search1(string Title)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM BookList  WHERE Title = @title";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {

                        command.Parameters.AddWithValue("@title", Title);
                        // using sql data reader

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                Book book = new Book();
                                book.Details[0] = reader[book.DetailHeader[0]].ToString();
                                book.Details[1] = reader[book.DetailHeader[1]].ToString();
                                book.Details[2] = reader[book.DetailHeader[2]].ToString();
                                book.Details[3] = reader[book.DetailHeader[3]].ToString();
                                book.Details[4] = reader[book.DetailHeader[4]].ToString();
                                return book;
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
        public async Task<Book> Search2(string Author)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM BookList  WHERE Author = @auth";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {

                        command.Parameters.AddWithValue("@auth", Author);
                        // using sql data reader
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {


                                Book book = new Book();
                                book.Details[0] = reader[book.DetailHeader[0]].ToString();
                                book.Details[1] = reader[book.DetailHeader[1]].ToString();
                                book.Details[2] = reader[book.DetailHeader[2]].ToString();
                                book.Details[3] = reader[book.DetailHeader[3]].ToString();
                                book.Details[4] = reader[book.DetailHeader[4]].ToString();
                                return book;

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


        public async Task EditData1(string code, string Genre)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE BookList SET Genre = @Gnre WHERE Genre = @Genre";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {

                        command.Parameters.AddWithValue("@Gnre", Genre);
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
                    string query = "DELETE FROM BookList WHERE BookCode = @Code";
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
        public async Task EditData2(string code, string price)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE BookList SET Price = @Prc WHERE BookCode = @Code";
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

