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
public partial class MProblemPage : ContentPage {
    private string[] problemTypes = { "standarddeviationf.png", "onesamplettestf.png", "dependentsameplettestf.png", "independentsamplettestf.png", "zscoref.png" };
    public ProblemViewModel VM { get; set; }
    private IProblem problem;
    public MProblemPage(IProblem problem, DataSet? dataset=null)
    {
        this.problem = problem;
        VM = new(this, problem.Name, problem);
        VM.RegenerateProblem(dataset);

        InitializeComponent();

        BindingContext = VM;
        FormulaImage.Source = problemTypes[VM.problem.Id - 1];
        VM.RegenerateProblem();
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
        Navigation.PushAsync(new MExamplePage(problem));
    }
    public void OnBack(object sender, EventArgs e)
    {
        Navigation?.PopAsync();
    }

}

