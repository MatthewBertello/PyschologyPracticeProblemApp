/*
 * Michael Meisenburg
 * Date: 10/18/2023
*/
using System.Collections.ObjectModel;

namespace PsychologyPracticeProblemApp;

public partial class Example : ContentPage
{
    private string[] problemTypes = { "standarddeviationex.png", "onesamplettestex.png", "dependentsameplettestex.png", "independentsamplettestex.png", "zscoreex.png" };

    public Example(int problemType)
	{
        InitializeComponent();
        ExampleImage.Source = problemTypes[problemType - 1];
    }

    private void OnCloseExampleClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}