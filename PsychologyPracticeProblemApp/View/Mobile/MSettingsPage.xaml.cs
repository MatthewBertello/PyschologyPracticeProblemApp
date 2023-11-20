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

public partial class MSettingsPage : ContentPage {

    public SettingsViewModel VM;
    public MSettingsPage()
    {
        VM = new(this);
        InitializeComponent();
        BindingContext = VM;
    }

    private void OnLogout(object sender, EventArgs e)
    {
        User.Logout(this);
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
        MovePage(new MHistoryPage(), -1);
    }
    private void OnSettings(object sender, EventArgs e)
    {
        MovePage(new MSettingsPage(), 0);
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
