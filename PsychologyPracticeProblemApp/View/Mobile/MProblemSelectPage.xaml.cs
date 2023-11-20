/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;

public partial class MProblemSelectPage : ContentPage {

    
    public MProblemSelectPage()
    {
        InitializeComponent();
    }
    public async void OnStandard(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MProblemPage(IProblem.StandardDeviation));
    }
    public async void OnOneSample(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MProblemPage(IProblem.OneSampleTTest));
    }
    public async void OnDependent(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MProblemPage(IProblem.DependentSampleTTest));
    }
    public async void OnIndependent(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MProblemPage(IProblem.IndependentSampleTTest));
    }
    public async void OnZScore(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MProblemPage(IProblem.ZScore));
    }


    private void OnProblems(object sender, EventArgs e)
    {
        MovePage(new MProblemSelectPage(), 0);
    }
    private void OnStats(object sender, EventArgs e)
    {
        MovePage(new MStatsPage(), 1);
    }
    private async void OnHistory(object sender, EventArgs e)
    {

    }
    private async void OnSettings(object sender, EventArgs e)
    {

    }

    private async void MovePage(Page page, int dir)
    {
        if(dir < 0) // left
        {
            Navigation.InsertPageBefore(page, this);
            await Navigation.PopAsync();
        } else // right
        {
            await Navigation.PushAsync(page);
            Navigation.RemovePage(this);
        }
    }
}
