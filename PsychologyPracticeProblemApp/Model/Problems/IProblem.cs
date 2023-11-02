using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public interface IProblem {
    private static IProblem[] problemIds = new IProblem[] {
        null,
        new ProbStandardDeviation() { Id=1 },
        new ProbOneSampleTTest() { Id=2 },
        new ProbDependentSampleTTest() { Id=3 },
        new ProbIndependentSampleTTest() { Id=4 },
        new ProbZScore() { Id=5 },
    };
    public static IProblem[] ProblemIds { get => problemIds; }
    public abstract int Id { get; set; }

    /// <summary>
    /// The name of the problem to display
    /// </summary>
    public abstract string Name { get; }


    /// <summary>
    /// Solve a generic problem.
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public abstract double Solve(DataSet dataSet);

    /// <summary>
    /// Generate a set of data related to this problem using some fixed size
    /// </summary>
    /// <param name="dataLength">Number of data points being used</param>
    /// <param name="lowerBounds">Smallest any value should be</param>
    /// <param name="upperBounds">Largest any value should be</param>
    /// <returns>DataSet relating to this problem type</returns>
    public abstract DataSet GenData();

}
