using Npgsql;
using PsychologyPracticeProblemApp.Model.Sql;
using PsychologyPracticeProblemApp.Model.Structs;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PsychologyPracticeProblemApp;
public static class Database {

    private static NpgsqlConnection connection = null;
    public static Guid CurrentUserId { get; set; } = Guid.Empty;
    private static bool hasStarted = false; // toggle for the first call to the database

    public static Dictionary<String, String> SQL = new() {
        { "CreateProblemsTable", "CREATE TABLE IF NOT EXISTS problem (id UUID DEFAULT gen_random_uuid(), problemType INT, xValues STRING, yValues STRING, inputA FLOAT, inputB FLOAT, PRIMARY KEY(id));" },
        { "CreateAttemptsTable", "CREATE TABLE IF NOT EXISTS attempt (id UUID DEFAULT gen_random_uuid(), date DATE, userId UUID, problemType FLOAT, xValues STRING, yValues STRING, inputA FLOAT, inputB FLOAT, answer FLOAT, PRIMARY KEY(id));" },
        { "CreateUsersTable", "CREATE TABLE IF NOT EXISTS account (id UUID DEFAULT gen_random_uuid(), username STRING, password STRING, email STRING, firstName STRING, lastName STRING, PRIMARY KEY(id));" },
        { "AddAdminUser", "INSERT INTO account (id, username, password, email, firstName, lastName) VALUES ($1, 'admin', '123', 'admin@admin.com', 'Admin', 'M')"},
        { "DropProblemsTable", "DROP TABLE IF EXISTS problem" },
        { "DropAttemptsTable", "DROP TABLE IF EXISTS attempt" },
        { "DropUsersTable", "DROP TABLE IF EXISTS account" },
        { "InsertProblem", "INSERT INTO problem (problemType, xValues, yValues, inputA, inputB) VALUES ($1, $2, $3, $4, $5) RETURNING id" },
        { "InsertAttempt", "INSERT INTO attempt (userId, problemType, xValues, yValues, inputA, inputB, answer, date) VALUES ($1, $2, $3, $4, $5, $6, $7, $8) RETURNING id" },
        { "InsertUser", "INSERT INTO account (username, password, firstName, lastName, email) VALUES ($1, $2, $3, $4, $5) RETURNING id" },
        { "GetPastAttempts", "SELECT attempt.answer, problem.xValues, problem.yValues, problem.inputA, problem.inputB, attempt.date FROM attempt, problem WHERE attempt.problemId = problem.id and attempt.userId = $1 AND problem.problemType = $2" },
        { "GetAllPastAttempts", "SELECT attempt.problemType, answer, xValues, yValues, inputA, inputB, date FROM attempt WHERE userId = $1" },
        { "GetUser", "SELECT id, firstName, lastName, email FROM account WHERE username = $1 AND password = $2" },
        { "GetUsernameExists", "SELECT id FROM account WHERE username = $1" },
        { "GetEmailExists", "SELECT id FROM account WHERE email = $1" },
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
    
    /// <summary>
    /// Calls any given sql command. can auto execute as well.
    /// </summary>
    /// <param name="sql">sql command</param>
    /// <param name="callCommand">if the command should be executed</param>
    /// <returns>the Npgsql command object associated with the sql</returns>
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
        if(connection != null) connection.Close();
        connection = new NpgsqlConnection(GetConnectionString());
        connection.Open();
        VerifyStartup();
    }
    // Runs on first call to database
    private static void VerifyStartup() {
        if(hasStarted) return;
        hasStarted = true;

        new NpgsqlCommand(SQL["CreateProblemsTable"], connection).ExecuteNonQuery();
        new NpgsqlCommand(SQL["CreateAttemptsTable"], connection).ExecuteNonQuery();
        new NpgsqlCommand(SQL["CreateUsersTable"], connection).ExecuteNonQuery();
    }
    public static void RebuildTables()
    {
        new NpgsqlCommand(SQL["DropProblemsTable"], connection).ExecuteNonQuery();
        new NpgsqlCommand(SQL["DropAttemptsTable"], connection).ExecuteNonQuery();
        new NpgsqlCommand(SQL["DropUsersTable"], connection).ExecuteNonQuery();

        new NpgsqlCommand(SQL["CreateProblemsTable"], connection).ExecuteNonQuery();
        new NpgsqlCommand(SQL["CreateAttemptsTable"], connection).ExecuteNonQuery();
        new NpgsqlCommand(SQL["CreateUsersTable"], connection).ExecuteNonQuery();

        try
        {
            using var cmd = new NpgsqlCommand(SQL["AddAdminUser"], connection) {
                Parameters = {
                new() { Value = User.Admin }
            }
            };
            cmd.ExecuteNonQuery();
        } catch { }
    }
    /// <summary>
    /// Take any given attempt and save it to the database
    /// </summary>
    /// <param name="problem">the problem type to save</param>
    /// <param name="dataSet">the data set of inputs</param>
    /// <param name="yourAnswer">what the user answered</param>
    /// <param name="userID">user id</param>
    public static void SaveAnswerAttempt(IProblem problem, DataSet dataSet, double? yourAnswer, string type, Guid? userID = null)
    {

        Verify();
        try
        {
            using var cmd = new NpgsqlCommand(SQL["InsertAttempt"], connection) {
                Parameters = {
                    new() { Value = userID ?? User.Guest },
                    new() { Value = (double)problem.Id },
                    new() { Value = DataToString(dataSet.DataA) },
                    new() { Value = DataToString(dataSet.DataB) },
                    new() { Value = dataSet.ValueA ?? double.NaN },
                    new() { Value = dataSet.ValueB ?? double.NaN },
                    new() { Value = yourAnswer ?? double.NaN },
                    new() { Value = DateTime.Now },
                }
            };
            cmd.ExecuteNonQuery();
        } catch { }
        DatabaseCache.SetDirty(userID ?? User.Guest);
    }
    /// <summary>
    /// Add user to database
    /// </summary>
    /// <param name="problem">the problem type to save</param>
    /// <param name="dataSet">the data set of inputs</param>
    /// <param name="yourAnswer">what the user answered</param>
    /// <param name="userID">user id</param>
    public static bool AddUser(string username, string password, string email, string firstName, string lastName)
    {

        if(CheckUsernameExists(username)) return false;
        if(CheckEmailExists(email)) return false;

        Verify();

        // Add user
        try
        {
            Guid probID;
            {
                using var cmd = new NpgsqlCommand(SQL["InsertUser"], connection) {
                    Parameters = {
                        new() { Value = username },
                        new() { Value = password },
                        new() { Value = firstName },
                        new() { Value = lastName },
                        new() { Value = email },
                    }
                };
                probID = (Guid) cmd.ExecuteScalar();
            }
        } catch { return false; }
        return true;
    }
    /// <summary>
    /// Takes a user id and gets their attempt history from database (filtered by type)
    /// </summary>
    /// <param name="userID">user id</param>
    /// <param name="type">type of problem to retrieve</param>
    /// <returns>a list of attempts</returns>
    public static LinkedList<HistoryLog> GetHistory(Guid? userID, int type)
    {
        Verify();
        LinkedList<HistoryLog> log = new();
        LinkedList<HistoryLog> copyLog = new();
        log = DatabaseCache.GetHistory(userID ?? User.Guest, connection);
        foreach(HistoryLog item in log)
        {
            if(item.Problem.Id == type) copyLog.AddLast(item);
        }
        return copyLog;
    }
    /// <summary>
    /// Takes a user id and gets their attempt history from database (filtered by type)
    /// </summary>
    /// <param name="userID">user id</param>
    /// <param name="type">type of problem to retrieve</param>
    /// <returns>a list of attempts</returns>
    public static LinkedList<HistoryLog> GetHistoryAll (Guid? userID=null)
    {
        Verify();
        return DatabaseCache.GetHistory(userID ?? User.Current.Id, connection);
    }


    /// <summary>
    /// Takes a user id and gets their attempt history from database (filtered by type)
    /// </summary>
    /// <param name="userID">user id</param>
    /// <param name="type">type of problem to retrieve</param>
    /// <returns>a list of attempts</returns>
    public static LinkedList<AttemptLog> GetAttempts(Guid? userID)
    {
        Verify();
        LinkedList<AttemptLog> log = new();
        //using var cmd = new NpgsqlCommand(SQL["GetPastAttempts"], connection) {
        using var cmd = new NpgsqlCommand("select id, date, userid, problemid, answer, type from attempt where userid=$1;", connection)
        {

            Parameters = {
                new() { Value = userID ?? User.Guest }
            }
        };
        NpgsqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Guid id = (Guid)reader.GetValue(0);
            DateTime date = (DateTime)reader.GetValue(1);
            Guid userId = (Guid)reader.GetValue(2);
            Guid problemId = (Guid)reader.GetValue(3);
            double answer = (double)reader.GetValue(4);
            string type = (string)reader.GetValue(5);
            log.AddLast(new AttemptLog(userId, problemId, date, answer, type));
        }
        reader.Close();
        return log;
    }

    public static IProblem GetProblem(Guid problemId)
    {
        IProblem problem = null;
        using var cmd = new NpgsqlCommand("select problemType, xvalues, yvalues, inputa, inputb from problem where id=$1;", connection)
        {
            Parameters =
            {
                new() { Value = problemId }
            }
        };
        NpgsqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            long problemType = (long)reader.GetValue(0);
            double[] xValues = StringToData((string)reader.GetValue(1));
            if(xValues.Length == 0)
            {
                xValues = null;
            }
            double[] yValues = StringToData((string)reader.GetValue(2));
            if (yValues.Length == 0)
            {
                yValues = null;
            }
            double? inputA = (double)reader.GetValue(3);
            //check if inputa is NaN
            if (double.IsNaN((double)inputA))
            {
                inputA = null;
            }
            double? inputB = (double)reader.GetValue(4);
            if(double.IsNaN((double)inputB))
            {
                inputB = null;
            }
            problem = IProblem.Problem[problemType];
            problem.Dataset = new DataSet(xValues, yValues, inputA, inputB);
        }
        reader.Close();
        return problem;
    }

    /// <summary>
    /// Takes a username and password and returns a user based on that. null if not exists
    /// </summary>
    /// <param name="username">username to look for</param>
    /// <param name="password">password to look for</param>
    /// <returns>User object. null if non-existant</returns>
    public static User GetUser(string username, string password)
    {
        Verify();
        User user = null;
        using var cmd = new NpgsqlCommand(SQL["GetUser"], connection) {
            Parameters = {
                new() { Value = username },
                new() { Value = password },
            }
        };
        NpgsqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            Guid id = (Guid)reader.GetValue(0);
            user = new User(
                    (Guid)reader.GetValue(0),
                    (string)reader.GetValue(1),
                    (string)reader.GetValue(2),
                    (string)reader.GetValue(3)
                );
        }
        reader.Close();
        return user;
    }
    /// <summary>
    /// Converts array of data into a string
    /// </summary>
    /// <param name="data">raw data</param>
    /// <returns>data formatted as string</returns>
    public static String DataToString(double[] data)
    {
        String dataStr = "";
        for(int i = 0; i < data.Length; i++) dataStr += (i == 0 ? "" : ",") + data[i].ToString();
        return dataStr;
    }
    /// <summary>
    /// converts a string of data into a raw data set
    /// </summary>
    /// <param name="dataStr">string of data</param>
    /// <returns>raw data</returns>
    public static double[] StringToData(string dataStr)
    {
        if(dataStr == "") return new double[0];
        string[] parts = dataStr.Split(",");
        double[] data = new double[parts.Length];
        for(int i=0; i<parts.Length; i++) data[i] = Double.Parse(parts[i]);
        return data;
    }
    /// <summary>
    /// Checks if a username already exists in the database.
    /// </summary>
    /// <param name="username">Username to check for existence.</param>
    /// <returns>True if the username exists; otherwise, false.</returns>
    public static bool CheckUsernameExists(string username)
    {
        Verify();
        using var cmd = new NpgsqlCommand(SQL["GetUsernameExists"], connection) { Parameters = { new NpgsqlParameter { Value = username } } };
        using var reader = cmd.ExecuteReader();
        return reader.Read();
    }

    /// <summary>
    /// Checks if an email already exists in the database.
    /// </summary>
    /// <param name="email">Email to check for existence.</param>
    /// <returns>True if the email exists; otherwise, false.</returns>
    public static bool CheckEmailExists(string email)
    {
        Verify();
        using var cmd = new NpgsqlCommand(SQL["GetEmailExists"], connection) { Parameters = { new NpgsqlParameter { Value = email } } };
        using var reader = cmd.ExecuteReader();
        return reader.Read();
    }
}

