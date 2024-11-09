using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloMusicPlayer
{
    internal class DatabaseHelper
    {
        private static DatabaseHelper _instance;
        private SqlConnection connection;

        private DatabaseHelper()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\csharp_projects\SoloMusicPlayer\SoloMusicPlayer\Database1.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }
        //singleton
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseHelper();
                }
                return _instance;
            }
        }
        //Veritabanı islemleri buraya yazılacak
        // Paths tablosundaki verileri okuma
        public List<string> GetAllPaths()
        {
            List<string> paths = new List<string>();

            try
            {
                connection.Open();
                string query = "SELECT path FROM Paths";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paths.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return paths;
        }

        public bool AddPath(string path)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO [Paths] (path) VALUES (@pathname)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@pathname", path);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
