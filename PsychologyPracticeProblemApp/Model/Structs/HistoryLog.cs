using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
/// <summary>
/// Stores a single attempt at a problem.
/// </summary>
public class HistoryLog {
    private DataSet inputs;     // inputs 1
    private double answer;
    private bool isCorrect;
    private IProblem problem;
    private DateTime date;
    public DataSet Inputs => inputs;
    public double Answer => answer;
    public bool IsCorrect => isCorrect;
    public bool IsNotCorrect => !isCorrect;
    public IProblem Problem => problem;
    public DateTime Date => date;
    public Command RetryCommand => new Command(OnRetry);
    public String LogDate           => Date.ToString("MMMM dd");
    public String LogProblemName    => problem.Name;
    public String LogYourAnswer     => String.Format("{0}", answer);
    public String LogRealAnswer     => String.Format("{0}", IProblem.ToPrecise(problem?.Solve(inputs)));

    /// <summary> 
    /// Basic History attempt
    /// </summary>
    /// <param name="type">type of problem</param>
    /// <param name="inputs">problem inputs</param>
    /// <param name="answer">provided answer</param>
    /// <param name="date">date attempted</param>
    public HistoryLog(int type, DataSet inputs, double answer, DateTime date)
    {
        problem = IProblem.Problem[type];
        isCorrect = IProblem.ToPrecise(problem?.Solve(inputs)) == answer;
        this.inputs = inputs;
        this.answer = answer;
        this.date = date;
    }

    private void OnRetry()
    {
        if(App.IsWindows)
            App.CurrentApp.MainPage.Navigation.PushAsync(new ProblemPage(Problem, Inputs));
        else
            App.CurrentApp.MainPage.Navigation.PushAsync(new MProblemPage(Problem, Inputs));
    }
}
