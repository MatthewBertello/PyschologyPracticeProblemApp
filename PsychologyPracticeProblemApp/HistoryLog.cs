using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;

public class HistoryLog : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    public String Type { get; set; }
    public DateTime Date { get; set; }
    public int Answer { get; set; }
}
