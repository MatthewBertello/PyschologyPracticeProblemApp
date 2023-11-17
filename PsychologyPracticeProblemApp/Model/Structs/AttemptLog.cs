using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.Model.Structs;
/// <summary>
/// Stores a single attempt at a problem.
/// </summary>
public class AttemptLog
{
    public Guid UserId { get; set; }
    public Guid ProblemId { get; set; }
    public DateTime Date { get; set; }
    public double Answer { get; set; }
    public string Type { get; set; }

    public AttemptLog(Guid userId, Guid problemId, DateTime date, double answer, string Type)
    {
        UserId = userId;
        ProblemId = problemId;
        Date = date;
        Answer = answer;
        this.Type = Type;
    }
}
