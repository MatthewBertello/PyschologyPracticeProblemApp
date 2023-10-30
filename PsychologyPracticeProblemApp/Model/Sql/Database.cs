using Npgsql;
using System.Collections.Generic;

namespace PsychologyPracticeProblemApp.Model
{
    public class Database {
        private string connString = GetConnectionString();


        public Database()
        {
            CreateProblemsTable(connString);
            CreateAttemptsTable(connString);
        }

        /// <summary>
        /// Creates a connection string to the database
        /// </summary>
        /// <returns></returns>
        static String GetConnectionString()
        {
            var connStringBuilder = new NpgsqlConnectionStringBuilder();
            connStringBuilder.Host = "wool-toucan-13031.5xj.cockroachlabs.cloud";
            connStringBuilder.Port = 26257;
            connStringBuilder.SslMode = SslMode.VerifyFull;
            connStringBuilder.Username = "matthew";
            connStringBuilder.Password = "azAnuXp8KjRLZ4SOesftYA";
            connStringBuilder.Database = "defaultdb";
            connStringBuilder.ApplicationName = "PsychologyPracticeProblemApp";
            connStringBuilder.IncludeErrorDetail = true;
            return connStringBuilder.ConnectionString;
        }

        /// <summary>
        /// Creates the problems table if it does not already exist
        /// </summary>
        /// <param name="connString"></param>
        static void CreateProblemsTable(string connString)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            new NpgsqlCommand("CREATE TABLE IF NOT EXISTS problems (id INT PRIMARY KEY, problemType INT, xValues STRING, yValues STRING, zValues STRING)", conn).ExecuteNonQuery();
        }

        /// <summary>
        /// Creates the attempts table if it does not already exist
        /// </summary>
        /// <param name="connString"></param>
        static void CreateAttemptsTable(string connString)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            new NpgsqlCommand("CREATE TABLE IF NOT EXISTS attempts (id INT PRIMARY KEY, userId INT, problemId INT, answer DECIMAL)", conn).ExecuteNonQuery();
        }

        /// <summary>
        /// Gets a problem from the database by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Problem GetProblem(int id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM problems WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            using var reader = cmd.ExecuteReader();
            Problem newProblem = null;
            while (reader.Read())
            {
                newProblem = new Problem(reader.GetInt32(0), (ProblemType)reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            }
            return newProblem;
        }   
    }
}
