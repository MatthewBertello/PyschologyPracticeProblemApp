using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public readonly struct DataSet {
    public double[] DataA { get; }
    public double[] DataB { get; }
    public double? ValueA { get; }
    public double? ValueB { get; }




    public DataSet(double[] dataA=null, double[] dataB=null, double? valA=null, double? valB=null)
    {
        this.DataA = dataA ?? new double[0];
        this.DataB = dataB ?? new double[0];
        this.ValueA = valA;
        this.ValueB = valB;
    }

    public override string ToString()
    {
        return String.Format("A({0}) : B({1})\nData A: {2}\nData B: {3}\n",
                ValueA.ToString(),
                ValueB.ToString(),
                StatsUtil.DataToString(DataA),
                StatsUtil.DataToString(DataB)
            );
    }
}

