using PsychologyPracticeProblemApp.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        public LinkedList<AttemptLog> attempts { get; set; } = new();
        public IProblem problem { get; set; }

        public HistoryViewModel(LinkedList<AttemptLog> attempts)
        {
            this.attempts = attempts;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
