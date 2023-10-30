/*
 * Matthew Bertello
 * Date: 10/18/2023
*/
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
    }
    private void OnProblem_Summation(object sender, EventArgs e)
    {

        Navigation.PushAsync(new ProblemPage(new ProblemSummation()));
    }
    private void OnProblem_Mean(object sender, EventArgs e)
    {

        Navigation.PushAsync(new ProblemPage(new ProblemMean()));
    }

}
