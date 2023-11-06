/*
 * Michael Hulbert
 * Date: 10/18/2023
*/
using PsychologyPracticeProblemApp.ViewModel;

namespace PsychologyPracticeProblemApp;

public partial class StatsPage : ContentPage
{
    public StatsViewModel VM { get; set; }
    public StatsPage()
    {

        VM = new();
        InitializeComponent();
        BindingContext = VM;

    }
}