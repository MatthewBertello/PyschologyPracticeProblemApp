using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.Model.Sql; 
public class DatabaseCache {
    private static Dictionary<Guid, LinkedList<HistoryLog>> History { get; set; } = new();
    private static Dictionary<Guid, Boolean> Dirty { get; set; } = new();
    private static void Verify(Guid guid)
    {
        if(!History.ContainsKey(guid)) History[guid] = new();
        if(!Dirty.ContainsKey(guid)) Dirty[guid] = true;
    }
    public static LinkedList<HistoryLog> GetHistory(Guid userID, NpgsqlConnection connection)
    {
        Verify(userID);

        if(Dirty[userID]) { // data is not cached. Get it from database.
            LinkedList<HistoryLog> log = new();
            using var cmd = new NpgsqlCommand(Database.SQL["GetAllPastAttempts"], connection) {
                Parameters = { new() { Value = userID } }
            };
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                double type = (double)reader.GetValue(0);
                double answer = (double)reader.GetValue(1);
                double[] xValues = Database.StringToData((string)reader.GetValue(2));
                double[] yValues = Database.StringToData((string)reader.GetValue(3));
                double inputA = (double)reader.GetValue(4);
                double inputB = (double)reader.GetValue(5);
                DateTime date = (DateTime)reader.GetValue(6);
                log.AddLast(new HistoryLog((int)type, new DataSet(xValues, yValues, inputA, inputB), answer, date));
            }
            reader.Close();

            History[userID] = log;
            Dirty[userID] = false;
        }
        return History[userID];
    }
    public static void SetDirty(Guid userID)
    {
        Dirty[userID] = true;
    }
}
