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
    private string[] problemTypes = { "standarddeviationf.png", "onesamplettestf.png", "dependentsameplettestf.png", "independentsamplettestf.png", "zscoref.png" };
    public ProblemViewModel VM { get; set; }

    public ProblemPage(IProblem problem, DataSet? dataset=null)
    {
        VM = new(this, problem.Name, problem);
        VM.RegenerateProblem(dataset);

        InitializeComponent();

        BindingContext = VM;
        FormulaImage.Source = problemTypes[VM.problem.Id - 1];
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
        Navigation.PushAsync(new Example(VM.problem.Id));
    }

}

