/*
* Matthew Bertello
* Date: 10/18/2023
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public partial class MLoginPage : ContentPage
{

    protected override void OnDisappearing()
    {
        // Clear the text fields when leaving the page
        //UsernameEntry.Text = string.Empty;
        //PasswordEntry.Text = string.Empty;

        base.OnDisappearing();
    }

    public MLoginPage()
    {
        InitializeComponent();
    }
    public async void OnGuest(object sender, EventArgs e)
    {
    }


    public async void OnLogin(object sender, EventArgs e)
    {
    }

    public async void OnCreateAccount(object sender, EventArgs e)
    {
        // Navigate to the CreateAccountPage
        await Navigation.PushAsync(new CreateAccount());
    }

}