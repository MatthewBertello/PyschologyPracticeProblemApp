/*
 * Matthew Bertello
 * Date: 10/18/2023
*/
using PsychologyPracticeProblemApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public partial class HomePage : ContentPage
{

    public HomePage()
    {
        InitializeComponent();
        Database.Verify();

        User.Login();
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
    private void OnStatsTest(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StatsPage());
    }
    private void OnLogoutTest(object sender, EventArgs e)
    {
        User.Logout(this);
    }

}
