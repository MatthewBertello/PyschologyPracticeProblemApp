using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbOneSampleTTest : IProblem {
    public string Name => "One Sample T-Test";

    public int Id { get; set; }

    public DataSet GenData()
    {
        int count = (int)StatsUtil.GenRandomValue(7, 10);
        return new DataSet(
            StatsUtil.GenRandomDataForceMean(count, (int)StatsUtil.GenRandomValue(5, 10), 10),
            null,
            StatsUtil.GenRandomValue(5, 10)
            );
    }

    public double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcOneSampleTTest(dataSet.DataA, dataSet.ValueA);
    }
}
