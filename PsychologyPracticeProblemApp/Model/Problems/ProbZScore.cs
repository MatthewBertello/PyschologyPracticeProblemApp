using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbZScore : IProblem {
    public override DataSet GenData()
    {
        int v = (int)StatsUtil.GenRandomValue(7, 10);
        return new DataSet(
            StatsUtil.GenRandomData(v, 0, 10),
            null,
            StatsUtil.GenRandomValue(5,15),
            StatsUtil.GenRandomValue(5,15)
            );
    }

    public override double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcZScore(dataSet.DataA, dataSet.ValueA, dataSet.ValueB);
    }
}
