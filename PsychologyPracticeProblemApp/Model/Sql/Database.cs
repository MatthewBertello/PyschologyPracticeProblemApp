﻿using Npgsql;
using PsychologyPracticeProblemApp.Model.Structs;
using System.Collections.Generic;
using System.Diagnostics;

namespace PsychologyPracticeProblemApp;
public static class Database {

    private static NpgsqlConnection connection = null;

    private static Dictionary<String, String> SQL = new() {
        { "CreateProblemsTable", "CREATE TABLE IF NOT EXISTS problem (id UUID DEFAULT gen_random_uuid(), problemType INT, xValues STRING, yValues STRING, inputA FLOAT, inputB FLOAT, PRIMARY KEY(id));" },
        { "CreateAttemptsTable", "CREATE TABLE IF NOT EXISTS attempt (id UUID DEFAULT gen_random_uuid(), date DATE, userId UUID, problemId UUID, answer FLOAT, PRIMARY KEY(id));" },
        { "DropProblemsTable", "DROP TABLE IF EXISTS problem" },
        { "DropAttemptsTable", "DROP TABLE IF EXISTS attempt" },
        { "InsertProblem", "INSERT INTO problem (problemType, xValues, yValues, inputA, inputB) VALUES ($1, $2, $3, $4, $5) RETURNING id" },
        { "InsertAttempt", "INSERT INTO attempt (userId, problemId, answer, date) VALUES ($1, $2, $3, $4) RETURNING id" },
        { "GetPastAttempts", "SELECT attempt.answer, problem.xValues, problem.yValues, problem.inputA, problem.inputB, attempt.date FROM attempt, problem WHERE attempt.problemId = problem.id and attempt.userId = $1 AND problem.problemType = $2" },
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
        if(connection == null)
        {
            connection = new NpgsqlConnection(GetConnectionString());
            connection.Open();
            //new NpgsqlCommand(SQL["DropProblemsTable"], connection).ExecuteNonQuery();
            //new NpgsqlCommand(SQL["DropAttemptsTable"], connection).ExecuteNonQuery();
            new NpgsqlCommand(SQL["CreateProblemsTable"], connection).ExecuteNonQuery();
            new NpgsqlCommand(SQL["CreateAttemptsTable"], connection).ExecuteNonQuery();
        }
    }
    /// <summary>
    /// Take any given attempt and save it to the database
    /// </summary>
    /// <param name="problem">the problem type to save</param>
    /// <param name="dataSet">the data set of inputs</param>
    /// <param name="yourAnswer">what the user answered</param>
    /// <param name="userID">user id</param>
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
                        new() { Value = userID ?? User.Admin },
                        new() { Value = probID },
                        new() { Value = yourAnswer ?? double.NaN },
                        new() { Value = DateTime.Now },
                    }
                };
                await cmd.ExecuteNonQueryAsync();
            }
        } catch(PostgresException e)
        {
            Debug.WriteLine("{0}\n\t{1}\n\t{2}\n\t{3}\n\t{4}", e.StackTrace, e.Where, e.Line, e.ConstraintName, e.Detail);
        }
    }
    /// <summary>
    /// Takes a user id and gets their attempt history from database (filtered by type)
    /// </summary>
    /// <param name="userID">user id</param>
    /// <param name="type">type of problem to retrieve</param>
    /// <returns>a list of attempts</returns>
    public static LinkedList<HistoryLog> GetHistory(Guid? userID, int type)
    {
        LinkedList<HistoryLog> log = new();

        using var cmd = new NpgsqlCommand(SQL["GetPastAttempts"], connection) {
            Parameters = {
                new() { Value = userID ?? User.Admin },
                new() { Value = type },
            }
        };
        NpgsqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            double answer = (double) reader.GetValue(0);
            double[] xValues = StringToData((string)reader.GetValue(1));
            double[] yValues = StringToData((string)reader.GetValue(2));
            double inputA = (double)reader.GetValue(3);
            double inputB = (double)reader.GetValue(4);
            DateTime date = (DateTime)reader.GetValue(5);
            log.AddLast(new HistoryLog(type, new DataSet(xValues, yValues, inputA, inputB), answer, date));
        }
        reader.Close();
        return log;
    }
    /// <summary>
    /// Converts array of data into a string
    /// </summary>
    /// <param name="data">raw data</param>
    /// <returns>data formatted as string</returns>
    private static String DataToString(double[] data)
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
    private static double[] StringToData(string dataStr)
    {
        if(dataStr == "") return new double[0];
        string[] parts = dataStr.Split(",");
        double[] data = new double[parts.Length];
        for(int i=0; i<parts.Length; i++) data[i] = Double.Parse(parts[i]);
        return data;
    }
}
