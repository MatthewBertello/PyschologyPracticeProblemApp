using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProblemSummation : IProblem {
    public string Name => "Summation";

    public DataSet GenData()
    {
        return new DataSet(StatsUtil.GenRandomData(5, 0, 10));
    }

    public double Solve(DataSet dataSet)
    {
        double sum = 0;
        for(int i = 0; i < dataSet.DataA.Length; i++) sum += dataSet.DataA[i];
        return sum;
    }
}
