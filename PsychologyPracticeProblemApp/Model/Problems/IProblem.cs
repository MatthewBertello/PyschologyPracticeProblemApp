using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class IProblem {
    public static ProbStandardDeviation StandardDeviation { get => (ProbStandardDeviation)problems[1]; }
    public static ProbOneSampleTTest OneSampleTTest { get => (ProbOneSampleTTest)problems[2]; }
    public static ProbDependentSampleTTest DependentSampleTTest { get => (ProbDependentSampleTTest)problems[3]; }
    public static ProbIndependentSampleTTest IndependentSampleTTest { get => (ProbIndependentSampleTTest)problems[4]; }
    public static ProbZScore ZScore { get => (ProbZScore)problems[5]; }

    private static IProblem[] problems = new IProblem[] {
        null,
        new ProbStandardDeviation() { Id=1, Name="Standard Deviation"},
        new ProbOneSampleTTest() { Id=2, Name="One Sample T-Test" },
        new ProbDependentSampleTTest() { Id=3, Name="Dependent Sample T-Test" },
        new ProbIndependentSampleTTest() { Id=4, Name="Independent Sample T-Test" },
        new ProbZScore() { Id=5, Name="Z-Score" },
    };
    public static IProblem[] Problem => problems;

    public int Id { get; set; }
    /// <summary>
    /// The name of the problem to display
    /// </summary>
    public string Name { get; set; }

    public DataSet Dataset { get; set; }


    /// <summary>
    /// Solve a generic problem.
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public virtual double Solve(DataSet dataSet) { return 0; }

    /// <summary>
    /// Generate a set of data related to this problem using some fixed size
    /// </summary>
    /// <param name="dataLength">Number of data points being used</param>
    /// <param name="lowerBounds">Smallest any value should be</param>
    /// <param name="upperBounds">Largest any value should be</param>
    /// <returns>DataSet relating to this problem type</returns>
    public virtual DataSet GenData() { return new DataSet(); }
    
    public static double? ToPrecise(double? x, int precision=2)
    {
        if(x == null) return null;
        precision = (int)Math.Pow(10, precision);
        return Math.Floor((double)x * precision) / precision;
    }
}
