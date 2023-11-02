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
    }
    private void OnProblem1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(new ProbStandardDeviation()));
    }
    private void OnProblem2(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(new ProbOneSampleTTest()));
    }
    private void OnProblem3(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(new ProbDependentSampleTTest()));
    }
    private void OnProblem4(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(new ProbIndependentSampleTTest()));
    }
    private void OnProblem5(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProblemPage(new ProbZScore()));
    }

}
