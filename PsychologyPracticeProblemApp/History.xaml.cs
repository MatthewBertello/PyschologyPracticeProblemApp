using System.Collections.ObjectModel;

namespace PsychologyPracticeProblemApp;

public partial class History : ContentPage {
	//private ObservableCollection<HistoryLog> history = new ObservableCollection<HistoryLog>();
    public ObservableCollection<HistoryLog> Hist { get; set; }
    public History()
	{
		InitializeComponent();
        Hist = new ObservableCollection<HistoryLog> {
            new HistoryLog() { Type = "Dependent Test", Date = DateTime.Now, Answer = 10525 },
            new HistoryLog() { Type = "Dependent Test", Date = DateTime.Now, Answer = 10525 }
        };
        BindingContext = this;
    }
}