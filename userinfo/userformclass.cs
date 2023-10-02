using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace project
{
    class userformclass
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }

        private static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(myconnstrng))
                {
                    string sql = "SELECT * FROM userdetails";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dt;
        }

        public bool Insert(userformclass userdata)
        {
            bool isSuccess = false;

            string connectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO userdetails (Name, Email, Gender, Mobile) VALUES (@Name, @Email, @Gender, @Mobile)", connection))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@Name", userdata.Name);
                    cmd.Parameters.AddWithValue("@Email", userdata.Email);
                    cmd.Parameters.AddWithValue("@Gender", userdata.Gender);
                    cmd.Parameters.AddWithValue("@Mobile", userdata.Mobile);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return isSuccess;
        }

        public bool Update(userformclass user)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(myconnstrng))
                {
                    string query = "UPDATE userdetails SET Name=@Name, Email=@Email, Gender=@Gender, Mobile=@Mobile WHERE UserID=@UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Gender", user.Gender);
                        cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        isSuccess = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            return isSuccess;
        }

        public bool Delete(string userID)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(myconnstrng))
                {
                    string query = "DELETE FROM userdetails WHERE UserID=@UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        isSuccess = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return isSuccess;
        }
    }
}
