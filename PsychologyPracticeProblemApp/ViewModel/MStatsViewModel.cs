using PsychologyPracticeProblemApp.Model.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel; 
public class MStatsViewModel : CoreViewModel {
    public String RecordText => String.Format("Data from last {0} problems", recordCount);

    // Standard Deviation
    public Boolean StandardDev_Show => total[IProblem.StandardDeviation.Id] > 0;
    public String StandardDev_OutOf => String.Format("{0} / {1}", correct[IProblem.StandardDeviation.Id], total[IProblem.StandardDeviation.Id]);
    public String StandardDev_Perc  => String.Format("{0}%", (int)(correct[IProblem.StandardDeviation.Id] / (float)total[IProblem.StandardDeviation.Id] * 100));
    public String StandardDev_Date  => String.Format("Last Attempt: {0}", mostRecent[IProblem.StandardDeviation.Id]?.ToString(dateFormat) ?? "");
    // One Sample
    public Boolean OneSample_Show => total[IProblem.OneSampleTTest.Id] > 0;
    public String OneSample_OutOf => String.Format("{0} / {1}", correct[IProblem.OneSampleTTest.Id], total[IProblem.OneSampleTTest.Id]);
    public String OneSample_Perc => String.Format("{0}%", (int)(correct[IProblem.OneSampleTTest.Id] / (float)total[IProblem.OneSampleTTest.Id] * 100));
    public String OneSample_Date => String.Format("Last Attempt: {0}", mostRecent[IProblem.OneSampleTTest.Id]?.ToString(dateFormat) ?? "");

    public Boolean Dependent_Show => total[IProblem.DependentSampleTTest.Id] > 0;
    public String Dependent_OutOf => String.Format("{0} / {1}", correct[IProblem.DependentSampleTTest.Id], total[IProblem.DependentSampleTTest.Id]);
    public String Dependent_Perc => String.Format("{0}%", (int)(correct[IProblem.DependentSampleTTest.Id] / (float)total[IProblem.DependentSampleTTest.Id] * 100));
    public String Dependent_Date => String.Format("Last Attempt: {0}", mostRecent[IProblem.DependentSampleTTest.Id]?.ToString(dateFormat) ?? "");

    public Boolean Independent_Show => total[IProblem.IndependentSampleTTest.Id] > 0;
    public String Independent_OutOf => String.Format("{0} / {1}", correct[IProblem.IndependentSampleTTest.Id], total[IProblem.IndependentSampleTTest.Id]);
    public String Independent_Perc => String.Format("{0}%", (int)(correct[IProblem.IndependentSampleTTest.Id] / (float)total[IProblem.IndependentSampleTTest.Id] * 100));
    public String Independent_Date => String.Format("Last Attempt: {0}", mostRecent[IProblem.IndependentSampleTTest.Id]?.ToString(dateFormat) ?? "");

    public Boolean ZScore_Show => total[IProblem.ZScore.Id] > 0;
    public String ZScore_OutOf => String.Format("{0} / {1}", correct[IProblem.ZScore.Id], total[IProblem.ZScore.Id]);
    public String ZScore_Perc => String.Format("{0}%", (int)(correct[IProblem.ZScore.Id] / (float)total[IProblem.ZScore.Id] * 100));
    public String ZScore_Date => String.Format("Last Attempt: {0}", mostRecent[IProblem.ZScore.Id]?.ToString(dateFormat) ?? "");


    private int recordCount => PropertiesUtil.HistoryCount;
    private LinkedList<HistoryLog> historyLogs;

    private int[] correct = new int[6];
    private int[] total = new int[6];
    private DateTime?[] mostRecent = new DateTime?[6];

    private String dateFormat = "dddd, MMMM dd";


    public MStatsViewModel()
    {
        historyLogs = Database.GetHistoryAll(User.Current.Id);
        foreach(HistoryLog log in historyLogs)
        {
            if(total[log.Problem.Id] >= recordCount) continue;
            total[log.Problem.Id]++;
            correct[log.Problem.Id] += log.IsCorrect ? 1 : 0;

            DateTime? thisDate = mostRecent[log.Problem.Id];
            if(thisDate == null || thisDate < log.Date) mostRecent[log.Problem.Id] = log.Date;
        }
    }
}
