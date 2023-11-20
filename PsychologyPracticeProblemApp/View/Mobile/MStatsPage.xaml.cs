/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

using PsychologyPracticeProblemApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;

public partial class MStatsPage : ContentPage {

    private MStatsViewModel VM;
    public MStatsPage()
    {
        VM = new();
        InitializeComponent();
        BindingContext = VM;
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
    private void OnHistory(object sender, EventArgs e)
    {
        MovePage(new MHistoryPage(), 1);
    }
    private void OnSettings(object sender, EventArgs e)
    {
        MovePage(new MSettingsPage(), 1);
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
