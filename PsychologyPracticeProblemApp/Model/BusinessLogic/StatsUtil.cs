using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class StatsUtil {
    /// <summary>
    /// M = Sum / n
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double GetMean(double[] data)
    {
        double n = 0;
        for(int i = 0; i < data.Length; i++)
            n += data[i];
        return n / data.Length;
    }
    /// <summary>
    /// S = sqrt ( Sum((x-M)^2) / (n-1) )
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double GetStandardDeviation(double[] data, double? mean = null)
    {
        // get mean
        double m = (double)(mean == null ? GetMean(data) : mean); // if mean is null, generate it

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
    /// m(X') = Sample mean
    /// u = population mean
    /// s = sample standardDeviation
    /// n = sample size 
    /// </summary>
    /// <returns></returns>
    public static double GetOneSampleTTest(double[] data, double populationMean)
    {
        double m = GetMean(data);
        double u = populationMean;
        double s = GetStandardDeviation(data, m);
        double n = data.Length;

        double upper = m - u;
        double lower = s / Math.Sqrt(n);
        return upper / lower;

    }
    /// <summary>
    /// Dependent T Test
    /// D -> differance
    /// T = Mean(D) / Deviation(D)
    /// </summary>
    /// <param name="dataPre"></param>
    /// <param name="dataPost"></param>
    /// <returns></returns>
    public static double? GetDependentSampleTTest(double[] dataPre, double[] dataPost)
    {
        if(dataPre.Length != dataPost.Length) return null;

        // get differances
        double[] differances = new double[dataPre.Length];
        for(int i = 0; i < dataPre.Length; i++)
            differances[i] = dataPre[i] - dataPost[i];
        // mean of differance
        double diffMean = GetMean(differances);
        // standard dev of differance
        double diffStandardDev = GetStandardDeviation(differances, diffMean);
        // error of means
        double standardErrorOfMean = diffStandardDev / Math.Sqrt(differances.Length);

        return diffMean / standardErrorOfMean;


    }
    /// <summary>
    /// Calculate Independent T Test
    /// T = (M1 - M2) / sqrt((S1^2 / N1) + (S2^2 / N2))
    /// </summary>
    /// <param name="dataA"></param>
    /// <param name="dataB"></param>
    /// <returns></returns>
    public static double? GetIndependentSampleTTest(double[] dataA, double[] dataB)
    {

        double meanA = GetMean(dataA);
        double meanB = GetMean(dataB);

        double deviationA = GetStandardDeviation(dataA, meanA);
        double deviationB = GetStandardDeviation(dataB, meanB);

        double val = Math.Sqrt((deviationA * deviationA) / dataA.Length + (deviationB * deviationB) / dataA.Length);

        return (meanA - meanB) / val;
    }
    /// <summary>
    /// Z Score = how many deviations away from the mean
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double GetZScore(double[] data, double popMean, double popDeviation)
    {
        double mean = GetMean(data);
        double meanDiff = mean - popMean;

        return meanDiff / (popDeviation / Math.Sqrt(data.Length));
    }
    /// <summary>
    /// Generates a list of random points
    /// </summary>
    /// <param name="count">how many elements to make</param>
    /// <param name="minRange">smallest an element can be</param>
    /// <param name="maxRange">largest an element can be</param>
    /// <param name="decimals">how many decimal places to use</param>
    /// <returns></returns>
    public static double[] GenerateRandomData(int count, int minRange = 0, int maxRange = 20)
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
    /// <param name="maxRange">largest an element can be</param>
    /// <param name="decimals">how many decimal places to use</param>
    /// <returns></returns>
    public static double[] GenerateRandomDataForceMean(int count, int targetMean, int range = 10)
    {
        // Geneate a basic data set that is close to the correct mean (based on range)
        Random rand = new Random();
        double[] data = GenerateRandomData(count, targetMean - range, targetMean + range);

        // while mean is not correct, add or subtract 1 to random elements until the mean is correct
        double mean;
        while((mean = GetMean(data)) != targetMean)
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
    /// <param name="data"></param>
    public static String DataToString(double[] data)
    {
        String str = "{ ";
        for(int i = 0; i < data.Length; i++)
            str += String.Format((i == 0 ? "" : ", ") + "{0:0.##}", data[i]);
        str += " }";
        return str;
    }
}
