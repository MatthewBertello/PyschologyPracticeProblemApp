using Npgsql;
using System.Collections.Generic;
using System.Diagnostics;

namespace PsychologyPracticeProblemApp;
public static class Database {
    private static Guid ADMIN_USER = Guid.Empty;

    private static NpgsqlConnection connection = null;

    private static Dictionary<String, String> SQL = new() {
        { "CreateProblemsTable", "CREATE TABLE IF NOT EXISTS problem (id UUID DEFAULT gen_random_uuid(), problemType INT, xValues STRING, yValues STRING, inputA DECIMAL, inputB DECIMAL, PRIMARY KEY(id));" },
        { "CreateAttemptsTable", "CREATE TABLE IF NOT EXISTS attempt (id UUID DEFAULT gen_random_uuid(), userId UUID, problemId UUID, answer DECIMAL, PRIMARY KEY(id));" },
        { "DropProblemsTable", "DROP TABLE IF EXISTS problem" },
        { "DropAttemptsTable", "DROP TABLE IF EXISTS attempt" },
        { "InsertProblem", "INSERT INTO problem (problemType, xValues, yValues, inputA, inputB) VALUES ($1, $2, $3, $4, $5) RETURNING id" },
        { "InsertAttempt", "INSERT INTO attempt (userId, problemId, answer) VALUES ($1, $2, $3) RETURNING id" },
    };

    /// <summary>
    /// Creates a connection string to the database
    /// </summary>
    /// <returns></returns>
    private static String GetConnectionString()
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

    public static NpgsqlCommand CallSQL(string sql, bool callCommand=true)
    {
        Verify();
        var cmd = new NpgsqlCommand(sql, connection);
        if(callCommand) cmd.ExecuteNonQuery();
        return cmd;
    }
    /// <summary>
    /// Check if Database has been initialized. If not, initialize it
    /// </summary>
    public static void Verify()
    {
        if(connection == null)
        {
            connection = new NpgsqlConnection(GetConnectionString());
            connection.Open();
            new NpgsqlCommand(SQL["DropProblemsTable"], connection).ExecuteNonQuery();
            new NpgsqlCommand(SQL["DropAttemptsTable"], connection).ExecuteNonQuery();
            new NpgsqlCommand(SQL["CreateProblemsTable"], connection).ExecuteNonQuery();
            new NpgsqlCommand(SQL["CreateAttemptsTable"], connection).ExecuteNonQuery();
        }
    }
    public static async void SaveAnswerAttempt(IProblem problem, DataSet dataSet, double? yourAnswer, Guid? userID = null)
    {
        
        Verify();
        try
        {
            Guid probID;
            {
                await using var cmd = new NpgsqlCommand(SQL["InsertProblem"], connection) {
                    Parameters = {
                        new() { Value = problem.Id },
                        new() { Value = DataToString(dataSet.DataA) },
                        new() { Value = DataToString(dataSet.DataB) },
                        new() { Value = dataSet.ValueA ?? double.NaN },
                        new() { Value = dataSet.ValueB ?? double.NaN }
                    }
                };
                probID = (Guid) await cmd.ExecuteScalarAsync();
            }
            {
                await using var cmd = new NpgsqlCommand(SQL["InsertAttempt"], connection) {
                    Parameters = {
                        new() { Value = userID ?? ADMIN_USER },
                        new() { Value = probID },
                        new() { Value = yourAnswer ?? double.NaN }
                    }
                };
                await cmd.ExecuteNonQueryAsync();
            }
        } catch(PostgresException e)
        {
            Debug.WriteLine("{0}\n\t{1}\n\t{2}\n\t{3}\n\t{4}", e.StackTrace, e.Where, e.Line, e.ConstraintName, e.Detail);
        }
    }
    private static String DataToString(double[] data)
    {
        String dataStr = "";
        for(int i = 0; i < data.Length; i++) dataStr += (i == 0 ? "" : ",") + data[i].ToString();
        return dataStr;
    }
    private static double[] StringToData(string dataStr)
    {
        string[] parts = dataStr.Split(",");
        double[] data = new double[parts.Length];
        for(int i=0; i<parts.Length; i++) data[i] = Double.Parse(parts[i]);
        return data;
    }
}

