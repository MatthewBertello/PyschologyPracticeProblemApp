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

public partial class MHistoryPage : ContentPage {

    public MHistoryViewModel VM;
    public MHistoryPage()
    {
        VM = new();
        InitializeComponent();
        BindingContext = VM;
    }

    private void OnCheck(object sender, EventArgs e)
    {
        int inclusion = 0;
        inclusion |= ChStandard.IsChecked ? MHistoryViewModel.STANDARD : 0;
        inclusion |= ChOneSample.IsChecked ? MHistoryViewModel.ONE_SAMPLE : 0;
        inclusion |= ChDependent.IsChecked ? MHistoryViewModel.DEPENDENT : 0;
        inclusion |= ChIndendent.IsChecked ? MHistoryViewModel.INDEPENDENT : 0;
        inclusion |= ChZScore.IsChecked ? MHistoryViewModel.Z_SCORE : 0;
        VM.UpdateHistory(inclusion);
        //RadioButton rb = sender as RadioButton;
        //choiceTextBlock.Text = "You chose: " + rb.GroupName + ": " + rb.Name;
    }
    private void OnCheckAll(object sender, EventArgs e)
    {
        bool toSet = true;
        if(ChStandard.IsChecked && ChOneSample.IsChecked && ChDependent.IsChecked
            && ChIndendent.IsChecked && ChZScore.IsChecked)
            toSet = false;
        ChStandard.IsChecked = toSet;
        ChOneSample.IsChecked = toSet;
        ChDependent.IsChecked = toSet;
        ChIndendent.IsChecked = toSet;
        ChZScore.IsChecked = toSet;
    }

    private void OnProblems(object sender, EventArgs e)
    {
        MovePage(new MProblemSelectPage(), -1);
    }
    private void OnStats(object sender, EventArgs e)
    {
        MovePage(new MStatsPage(), -1);
    }
    private void OnHistory(object sender, EventArgs e)
    {
        MovePage(new MHistoryPage(), 0);
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
