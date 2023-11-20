using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel;
public class SettingsViewModel : CoreViewModel, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    public SettingsViewModel(ContentPage parent)
    {
    }

}
