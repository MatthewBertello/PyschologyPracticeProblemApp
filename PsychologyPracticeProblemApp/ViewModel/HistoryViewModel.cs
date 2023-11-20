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
    public class HistoryViewModel : OverlayViewModel, INotifyPropertyChanged
    {
        public LinkedList<HistoryLog> Attempts { get; set; }
        public IProblem Problem { get; set; }

        public HistoryViewModel(LinkedList<HistoryLog> attempts, ContentPage parent) : base(parent, "History Page")
        {
            Attempts = attempts;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
