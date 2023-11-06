using PsychologyPracticeProblemApp.Model.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel;
public class StatsViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    public LinkedList<HistoryLog>[] Logs = new LinkedList<HistoryLog>[5];
    public int[] Correct = new int[5];
    public int[] Total = new int[5];
    public string TotalText => GetTotalText();

    public String TextStandard => GetText(0);
    public String TextOneSamp => GetText(1);
    public String TextDepend => GetText(2);
    public String TextIndep => GetText(3);
    public String TextZScore => GetText(4);
    public String TextStandardName => GetTextName(0);
    public String TextOneSampName => GetTextName(1);
    public String TextDependName => GetTextName(2);
    public String TextIndepName => GetTextName(3);
    public String TextZScoreName => GetTextName(4);

    private int GTotal = 0;
    private int GCorrect = 0;
    public StatsViewModel()
    {
        // load all histories
        for(int i = 0; i < 5; i++)
        {
            Logs[i] = Database.GetHistory(User.Current.Id, i + 1);
            Total[i] = 0;
            Correct[i] = 0;
            Debug.WriteLine(Logs[i].Count);
            foreach(HistoryLog hl in Logs[i])
            {
                Total[i]++;
                GTotal++;
                Correct[i] += hl.IsCorrect ? 1 : 0;
                GCorrect += hl.IsCorrect ? 1 : 0;
            }
        }
    }
    private String GetText(int i)
    {
        if(Total[i] == 0) return "No attempts on record!";
        return String.Format("{0}/{1} - {2}%",
            Correct[i],
            Total[i],
            (int)Math.Floor(Correct[i] / (float)Total[i] * 100)
            );
    }
    private String GetTextName(int i)
    {
        return String.Format("{0,-20}", IProblem.Problem[i + 1].Name);
    }
    private String GetTotalText()
    {

        return String.Format("Total: {0}/{1} - {2}%",
            GCorrect,
            GTotal,
            (int)Math.Floor(GCorrect / (float)GTotal * 100)
            );
    }


}

