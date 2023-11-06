using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbDependentSampleTTest : IProblem {

    public override DataSet GenData()
    {
        int count = (int) StatsUtil.GenRandomValue(7, 10);
        return new DataSet(
            StatsUtil.GenRandomData(count, 5, 20),
            StatsUtil.GenRandomData(count, 5, 20)
            );
    }

    public override double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcDependentSampleTTest(dataSet.DataA, dataSet.DataB);
    }
}
