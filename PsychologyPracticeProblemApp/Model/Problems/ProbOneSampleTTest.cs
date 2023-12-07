using PsychologyPracticeProblemApp.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class ProbOneSampleTTest : IProblem {

    public override DataSet GenData()
    {
        return new DataSet(
            StatsUtil.GenRandomData(PropertiesUtil.DatasetCount, PropertiesUtil.DatasetMin, PropertiesUtil.DatasetMax),
            null,
            StatsUtil.GenRandomValue(PropertiesUtil.DatasetMin, PropertiesUtil.DatasetMax)
            );
    }

    public override double Solve(DataSet dataSet)
    {
        return StatsUtil.CalcOneSampleTTest(dataSet.DataA, dataSet.ValueA);
    }
}
