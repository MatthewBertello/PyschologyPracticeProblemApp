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
}
