using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbZScore : IProblem {
    public string Name => "Z-Test";

    public int Id { get; set; }

    public DataSet GenData()
    {
        int v = (int)StatsUtil.GenRandomValue(7, 10);
        return new DataSet(
            StatsUtil.GenRandomData(v, 0, 10),
            null,
            StatsUtil.GenRandomValue(5,15),
            StatsUtil.GenRandomValue(5,15)
            );
    }

    public double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcZScore(dataSet.DataA, dataSet.ValueA, dataSet.ValueB);
    }
}
