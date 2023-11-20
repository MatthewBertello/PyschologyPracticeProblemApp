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

public partial class MStatsPage : ContentPage {

    
    public MStatsPage()
    {
        InitializeComponent();
    }

    private void HandleCheck(object sender, EventArgs e)
    {
        RadioButton rb = sender as RadioButton;
        //choiceTextBlock.Text = "You chose: " + rb.GroupName + ": " + rb.Name;
    }


    private void OnProblems(object sender, EventArgs e)
    {
        MovePage(new MProblemSelectPage(), -1);
    }
    private void OnStats(object sender, EventArgs e)
    {
        MovePage(new MStatsPage(), 0);
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
            Navigation.RemovePage(this);
            await Navigation.PushAsync(page);
        }
    }
}
