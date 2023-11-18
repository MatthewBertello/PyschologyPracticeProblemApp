using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbStandardDeviation : IProblem {

    public override DataSet GenData()
    {
        int count = (int) StatsUtil.GenRandomValue(10, 10);
        return new DataSet(StatsUtil.GenRandomData(count, 0, 10));
    }

    public override double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcStandardDeviation(dataSet.DataA);
    }
}
