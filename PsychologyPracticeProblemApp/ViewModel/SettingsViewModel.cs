using PsychologyPracticeProblemApp.Model.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel;
public class SettingsViewModel : CoreViewModel, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
    public String DSCount => PropertiesUtil.DatasetCount.ToString();
    public String DSMin => PropertiesUtil.DatasetMin.ToString();
    public String DSMax => PropertiesUtil.DatasetMax.ToString();
    public String HistCount => PropertiesUtil.HistoryCount.ToString();

    public SettingsViewModel(ContentPage parent)
    {
    }
    public void ChangeDSCount(int delta)
    {
        PropertiesUtil.DatasetCount = Math.Clamp(PropertiesUtil.DatasetCount + delta, 3, 20);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DSCount"));
    }
    public void ChangeDSMin(int delta)
    {
        PropertiesUtil.DatasetMin = Math.Clamp(PropertiesUtil.DatasetMin + delta, 0, PropertiesUtil.DatasetMax);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DSMin"));
    }
    public void ChangeDSMax(int delta)
    {
        PropertiesUtil.DatasetMax = Math.Clamp(PropertiesUtil.DatasetMax + delta, PropertiesUtil.DatasetMin, 100);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DSMax"));
    }
    public void ChangeHistoryCount(int delta)
    {
        PropertiesUtil.HistoryCount = Math.Clamp(PropertiesUtil.HistoryCount+delta, 5, 20);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HistCount"));
    }

}
