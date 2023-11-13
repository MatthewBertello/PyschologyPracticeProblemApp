/*
 * Matthew Bertello
 * Date: 10/18/2023
*/
using PsychologyPracticeProblemApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public partial class ProblemPage : ContentPage {
    public ProblemViewModel VM { get; set; }
    public ProblemPage(IProblem problem)
    {
        VM = new(this, problem.Name, problem);

        InitializeComponent();

        BindingContext = VM;
    }
    public void OnRegenerateClicked(object sender, EventArgs e)
    {
        VM.RegenerateProblem();
        AnswerEntry.Text = "";
    }
    public void OnSolveClicked(object sender, EventArgs e)
    {
        VM.ApplySolution(AnswerEntry.Text);
    }

    private void OnSeeExampleClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Example());
    }

}

