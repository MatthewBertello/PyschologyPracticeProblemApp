using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProblemMean : IProblem {
    public string Name => "Mean";
    public int Id { get; set; }
    public DataSet GenData()
    {
        return new DataSet(StatsUtil.GenRandomData(5, 0, 10));
    }

    public double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcMean(dataSet.DataA);
    }
}
