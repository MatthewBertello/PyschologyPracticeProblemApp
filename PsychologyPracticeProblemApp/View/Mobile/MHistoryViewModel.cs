using PsychologyPracticeProblemApp.Model.Structs;
using PsychologyPracticeProblemApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class MHistoryViewModel : CoreViewModel, INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    public static int
        STANDARD        = 1 << 0,
        ONE_SAMPLE      = 1 << 1,
        DEPENDENT       = 1 << 2,
        INDEPENDENT     = 1 << 3,
        Z_SCORE         = 1 << 4,
        ALL             = 0b11111;
    public int Size { get; set; }

    public ObservableCollection<HistoryLog> History { get; set; } = new();

    public MHistoryViewModel()
    {
        UpdateHistory(ALL);
    }
    private void UpdateHistory(int inclusionSet)
    {
        LinkedList<HistoryLog> logs = Database.GetHistoryAll();
        History.Clear();
        Size = logs.Count;
        foreach(HistoryLog log in logs)
        {
            if(log.Problem.Id == IProblem.StandardDeviation.Id && (inclusionSet & STANDARD) == 0) continue;
            if(log.Problem.Id == IProblem.OneSampleTTest.Id && (inclusionSet & ONE_SAMPLE) == 0) continue;
            if(log.Problem.Id == IProblem.DependentSampleTTest.Id && (inclusionSet & DEPENDENT) == 0) continue;
            if(log.Problem.Id == IProblem.IndependentSampleTTest.Id && (inclusionSet & INDEPENDENT) == 0) continue;
            if(log.Problem.Id == IProblem.ZScore.Id && (inclusionSet & Z_SCORE) == 0) continue;
            History.Add(log);
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("History"));
    }
}
