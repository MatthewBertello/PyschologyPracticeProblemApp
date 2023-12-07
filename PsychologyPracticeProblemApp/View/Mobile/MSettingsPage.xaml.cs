/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

using PsychologyPracticeProblemApp.Model.Utility;
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
        SetTheme(PropertiesUtil.DarkMode);
    }

    private void OnLogout(object sender, EventArgs e)
    {
        User.Logout(this);
    }
    private void OnThemeChange(object sender, EventArgs e)
    {
        bool newTheme = !PropertiesUtil.DarkMode;
        SetTheme(newTheme);
        PropertiesUtil.Save();
    }
    private void SetTheme(bool isDark)
    {
        PropertiesUtil.DarkMode = isDark;
        PropertiesUtil.UpdateTheme();
        themeButton.Source = isDark ? "ico_dark.png" : "ico_light.png";
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

    private void dsCountUp(object sender, EventArgs e) { VM.ChangeDSCount(1); }
    private void dsCountDown(object sender, EventArgs e) { VM.ChangeDSCount(-1); }
    private void dsMinUp(object sender, EventArgs e) { VM.ChangeDSMin(1); }
    private void dsMinDown(object sender, EventArgs e) { VM.ChangeDSMin(-1); }
    private void dsMaxUp(object sender, EventArgs e) { VM.ChangeDSMax(1); }
    private void dsMaxDown(object sender, EventArgs e) { VM.ChangeDSMax(-1); }
    private void histUp(object sender, EventArgs e) { VM.ChangeHistoryCount(1); }
    private void histDown(object sender, EventArgs e) { VM.ChangeHistoryCount(-1); }
}
