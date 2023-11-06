using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbOneSampleTTest : IProblem {

    public override DataSet GenData()
    {
        int count = (int)StatsUtil.GenRandomValue(7, 10);
        return new DataSet(
            StatsUtil.GenRandomDataForceMean(count, (int)StatsUtil.GenRandomValue(5, 10), 10),
            null,
            StatsUtil.GenRandomValue(5, 10)
            );
    }

    public override double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcOneSampleTTest(dataSet.DataA, dataSet.ValueA);
    }
}
