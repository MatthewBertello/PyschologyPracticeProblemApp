using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbIndependentSampleTTest : IProblem {

    public override DataSet GenData()
    {
        int countA = (int)StatsUtil.GenRandomValue(7, 10);
        int countB = (int)StatsUtil.GenRandomValue(7, 10);
        return new DataSet(
            StatsUtil.GenRandomData(countA, 5, 20),
            StatsUtil.GenRandomData(countB, 5, 20)
            );
    }

    public override double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcIndependentSampleTTest(dataSet.DataA, dataSet.DataB);
    }
}
