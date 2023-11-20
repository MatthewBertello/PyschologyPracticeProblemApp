using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel;
public class ProblemViewModel : OverlayViewModel, INotifyPropertyChanged {


    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<DataItem> DataSetA { get; set; } = new();
    public ObservableCollection<DataItem> DataSetB { get; set; } = new();
    public Double? Input1 { get; set; }
    public Double? Input2 { get; set; }

    public Double? CorrectAnswer { get; set; }
    public Double? YourAnswer { get; set; }


    public String ProblemName { get => problem.Name; }
    public String CorrectAnswerText { get => IsCorrect ? "" : String.Format("expected {0}", CorrectAnswer);  }
    public Boolean ShowSetB { get => data.DataB.Length > 0; }
    public Boolean ShowInput1 { get => Input1 != null; }
    public Boolean ShowInput2 { get => Input2 != null; }
    public Boolean ShowCorrectAnswer { get => YourAnswer != null; }
    public Boolean NotShowCorrectAnswer { get => !ShowCorrectAnswer; }
    public Boolean IsCorrect { get => YourAnswer == CorrectAnswer; }
    public Boolean IsIncorrect { get => !IsCorrect; }
    public int SetASpan => (!ShowSetB && !ShowInput1 && !ShowInput2) ? 2 : 1;

    public IProblem problem;
    private DataSet data;

    public ProblemViewModel(ContentPage parent, String pageName, IProblem problem) : base(parent, pageName)
    {
        this.problem = problem;

    }
    public void RegenerateProblem(DataSet? dataSet = null)
    {
        this.data = dataSet ?? problem.GenData();

        DataSetA.Clear();
        DataSetB.Clear();
        for(int i = 0; i < data.DataA.Length; i++) BindDataSetItem(DataSetA, data.DataA[i], i);
        for(int i = 0; i < data.DataB.Length; i++) BindDataSetItem(DataSetB, data.DataB[i], i);
        while(DataSetA.Count < 5) BindDataSetItem(DataSetA, null, DataSetA.Count);
        while(DataSetB.Count < 5) BindDataSetItem(DataSetB, null, DataSetA.Count);
        Input1 = data.ValueA;
        Input2 = data.ValueB;
        CorrectAnswer = IProblem.ToPrecise(problem.Solve(data));
        YourAnswer = null;

        ApplyPropertyChange(true, true);
    }

    private void BindDataSetItem(ObservableCollection<DataItem> set, double? value, int index)
    {
        bool isDesktop = DeviceInfo.Current.Idiom == DeviceIdiom.Desktop || DeviceInfo.Current.Idiom == DeviceIdiom.Tablet;
        // Every other element is readded with a next value, rather than as a whole new element.
        // This allows for double-row data
        if(index % 2 != 0 && value != null && !isDesktop)
        {
            double? last = set.Last().value;
            set.RemoveAt(set.Count - 1);
            set.Add(new DataItem(last, value, index-1));
        } else
        {
            set.Add(new DataItem(value, index));
        }
    }
    public void ApplySolution(String solution)
    {
        try
        {
            YourAnswer = double.Parse(solution);
            Database.SaveAnswerAttempt(problem, data, YourAnswer ?? 0, problem.Name, User.Current.Id);
            ApplyPropertyChange(false, true);
        } catch (Exception ex)
        {

        }
    }
    private void ApplyPropertyChange(bool includeData, bool includeAnswer)
    {
        if(includeData)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataSetA"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataSetB"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input1"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input2"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowSetB"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowInput1"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CorrectAnswer"));
        }
        if(includeAnswer)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("YourAnswer"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CorrectAnswerText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowCorrectAnswer"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NotShowCorrectAnswer"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCorrect"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsIncorrect"));
        }
    }
}
public class DataItem {
    public String Value => value?.ToString() ?? String.Empty;
    public String NextValue => next?.ToString() ?? String.Empty;
    public bool IsDark => index % 4 == 0;

    public int index;
    public double? value;
    public double? next;
    public DataItem(double? value, int index)
    {
        this.value = value;
        this.index = index;
    }
    public DataItem(double? value, double? next, int index)
    {
        this.value = value;
        this.index = index;
        this.next = next;
    }
}

