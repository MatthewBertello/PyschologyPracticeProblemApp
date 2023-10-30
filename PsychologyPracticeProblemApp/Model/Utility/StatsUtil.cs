using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
/// <summary>
/// A utility class specializing in statistical calculations.
/// </summary>
public static class StatsUtil { 
    /// <summary>
    /// Calculates the mean of a data set
    /// </summary>
    /// <param name="data">Data set</param>
    /// <returns></returns>
    public static double CalcMean(double[] data)
    {
        double n = 0;
        for(int i = 0; i < data.Length; i++)
            n += data[i];
        return n / data.Length;
    }
    /// <summary>
    /// Calculates standard deviation: 
    /// S = sqrt ( Sum((x-M)^2) / (n-1) )
    /// </summary>
    /// <param name="data">The data set</param>
    /// <param name="mean">The mean of the data set. Can be left blank</param>
    /// <returns>The Mean of the data set</returns>
    public static double CalcStandardDeviation(double[] data, double? mean = null)
    {
        // get mean
        double m = mean ?? CalcMean(data);

        // sum the differances
        double sumOfDifferances = 0;
        for(int i = 0; i < data.Length; i++)
            sumOfDifferances += (data[i] - m) * (data[i] - m); // square it

        // divide by n-1
        sumOfDifferances /= (data.Length - 1);
        // sqrt
        return Math.Sqrt(sumOfDifferances);
    }
    /// <summary>
    /// Calculates the one sample T-Test: 
    /// OT = (m - u) / (s / sqrt(n))
    /// </summary>
    /// <param name="data">The data set</param>
    /// <param name="populationMean">The population mean</param>
    /// <returns>The T score of the data set</returns>
    public static double CalcOneSampleTTest(double[] data, double populationMean)
    {
        double m = CalcMean(data);                      // sample mean
        double u = populationMean;                      // population mean
        double s = CalcStandardDeviation(data, m);      // sample standard deviation
        double n = data.Length;                         // sample size

        double upper = m - u;
        double lower = s / Math.Sqrt(n);
        return upper / lower;

    }
    /// <summary>
    /// Calculates a dependent T-Test score
    /// </summary>
    /// <param name="dataPre">The data pre-experiment</param>
    /// <param name="dataPost">The data post-experiment</param>
    /// <returns>The T score of the data set</returns>
    public static double? CalcDependentSampleTTest(double[] dataPre, double[] dataPost)
    {
        if(dataPre.Length != dataPost.Length) return null;

        // get differances
        double[] differances = new double[dataPre.Length];
        for(int i = 0; i < dataPre.Length; i++)
            differances[i] = dataPre[i] - dataPost[i];
        // mean of differance
        double diffMean = CalcMean(differances);
        // standard dev of differance
        double diffStandardDev = CalcStandardDeviation(differances, diffMean);
        // error of means
        double standardErrorOfMean = diffStandardDev / Math.Sqrt(differances.Length);

        return diffMean / standardErrorOfMean;


    }
    /// <summary>
    /// Calculate the independent T-Test score: 
    /// T = (M1 - M2) / sqrt((S1^2 / N1) + (S2^2 / N2))
    /// </summary>
    /// <param name="dataA">Data of group A</param>
    /// <param name="dataB">Data of group B</param>
    /// <returns>T-Score of both data sets</returns>
    public static double? CalcIndependentSampleTTest(double[] dataA, double[] dataB)
    {

        double meanA = CalcMean(dataA);
        double meanB = CalcMean(dataB);

        double deviationA = CalcStandardDeviation(dataA, meanA);
        double deviationB = CalcStandardDeviation(dataB, meanB);

        double val = Math.Sqrt((deviationA * deviationA) / dataA.Length + (deviationB * deviationB) / dataA.Length);

        return (meanA - meanB) / val;
    }
    /// <summary>
    /// Calculates Z-Score of a data set
    /// Z Score = how many deviations away from the mean
    /// </summary>
    /// <param name="data">The data set</param>
    /// <param name="popMean">Population Mean</param>
    /// <param name="popDeviation">Population Standard Deviation</param>
    /// <returns>Z-Score of the data set</returns>
    public static double CalcZScore(double[] data, double popMean, double popDeviation)
    {
        double mean = CalcMean(data);
        double meanDiff = mean - popMean;

        return meanDiff / (popDeviation / Math.Sqrt(data.Length));
    }
    /// <summary>
    /// Generates a list of random points
    /// </summary>
    /// <param name="count">how many elements to make</param>
    /// <param name="minRange">smallest an element can be</param>
    /// <param name="maxRange">largest an element can be</param>
    /// <returns>A randomized data set</returns>
    public static double[] GenRandomData(int count, int minRange = 0, int maxRange = 20)
    {
        Random rand = new Random();
        double[] data = new double[count];
        for(int i = 0; i < count; i++)
            data[i] = Math.Floor(rand.NextDouble() * (maxRange-minRange)) + minRange;
        return data;
    }
    /// <summary>
    /// Generates a list of random points, forcing a target mean
    /// </summary>
    /// <param name="count">how many elements to make</param>
    /// <param name="targetMean">the target mean</param>
    /// <param name="range">how spread out the data points should be</param>
    /// <returns></returns>
    public static double[] GenRandomDataForceMean(int count, int targetMean, int range = 10)
    {
        // Geneate a basic data set that is close to the correct mean (based on range)
        Random rand = new Random();
        double[] data = GenRandomData(count, targetMean - range, targetMean + range);

        // while mean is not correct, add or subtract 1 to random elements until the mean is correct
        double mean;
        while((mean = CalcMean(data)) != targetMean)
        {
            int delta = mean < targetMean ? 1 : -1;
            int index = rand.Next(count);
            data[index] += delta;
        }
        return data;
    }
    /// <summary>
    /// Turns data into a string format { a, b, c, d }
    /// </summary>
    /// <param name="data">Data converted into string format</param>
    public static String DataToString(double[] data)
    {
        String str = "{ ";
        for(int i = 0; i < data.Length; i++)
            str += String.Format((i == 0 ? "" : ", ") + "{0:0.##}", data[i]);
        str += " }";
        return str;
    }
}
