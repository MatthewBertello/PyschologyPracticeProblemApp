/*
 * Matthew Bertello
 * Date: 10/18/2023
*/
using PsychologyPracticeProblemApp;
using PsychologyPracticeProblemApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public partial class HomePage : ContentPage
{
    public OverlayViewModel VM { get; set; }
    public HomePage()
    {
        OverlayViewModel VM = new(this, "Home");
        InitializeComponent();
        BindingContext = VM;


        Database.Verify();
    }
    private void OnStandard(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(IProblem.StandardDeviation));
    }
    private void OnOneSample(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(IProblem.OneSampleTTest));
    }
    private void OnDependent(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(IProblem.DependentSampleTTest));
    }
    private void OnIndependent(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(IProblem.IndependentSampleTTest));
    }
    private void OnZScore(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(IProblem.ZScore));
    }


    private void OnStatsPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StatsPage());
    }
    private void OnHistoryPage(object sender, EventArgs e)
    {
        
        Navigation.PushAsync(new History());
    }
    private void OnSettingsPage(object sender, EventArgs e)
    {
    }

}
