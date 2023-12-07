using PsychologyPracticeProblemApp.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
/// <summary>
/// A set of inputs used for any given problem.
/// </summary>
public readonly struct DataSet {
    public double[] DataA { get; }
    public double[] DataB { get; }
    public double? ValueA { get; }
    public double? ValueB { get; }



    /// <summary>
    /// constructs a basic data set. all paramaters default to null
    /// </summary>
    /// <param name="dataA">inputs row 1</param>
    /// <param name="dataB">inputs row 2</param>
    /// <param name="valA">meta input 1</param>
    /// <param name="valB">meta input 2</param>
    public DataSet(double[] dataA=null, double[] dataB=null, double? valA=null, double? valB=null)
    {
        this.DataA = dataA ?? new double[0];
        this.DataB = dataB ?? new double[0];
        this.ValueA = valA == double.NaN ? null : valA;
        this.ValueB = valB == double.NaN ? null : valB;
    }

    /// <summary>
    /// converts inputs to easy to read string for debug purpouses.
    /// </summary>
    /// <returns>string of data</returns>
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

