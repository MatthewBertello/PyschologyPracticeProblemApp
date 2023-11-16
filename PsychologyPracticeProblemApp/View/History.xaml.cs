/*
 * Michael Meisenburg
 * Date: 10/18/2023
*/

using PsychologyPracticeProblemApp.Model.Structs;
using PsychologyPracticeProblemApp.ViewModel;
using System.Collections.ObjectModel;

namespace PsychologyPracticeProblemApp;

public partial class History : ContentPage
{
    public HistoryViewModel VM { get; set; }
    public History()
    {

        LinkedList<AttemptLog> attempts = Database.GetAttempts(Database.CurrentUserId);
        VM = new(attempts);


        InitializeComponent();

        BindingContext = VM;
    }

    void ViewProblemClicked(System.Object sender, System.EventArgs e)
    {
        var button = sender as Button;
        AttemptLog attempt = button?.BindingContext as AttemptLog;
        VM.problem = Database.GetProblem(attempt.ProblemId);
        
    }
}