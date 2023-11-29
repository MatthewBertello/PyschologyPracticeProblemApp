/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

using PsychologyPracticeProblemApp.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp
{
    public partial class LoginPage : ContentPage
    {
        int count = 0;

        protected override void OnDisappearing()
        {
            // Clear the text fields when leaving the page
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;

            base.OnDisappearing();
        }

        public LoginPage()
        {
            InitializeComponent();
        }
        public async void OnGuest(object sender, EventArgs e)
        {
            User.LoginGuest();
            await Navigation.PushAsync(new HomePage());

        }


        public async void OnLogin(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Hash the entered password for comparison
            string hashedPassword = SecurityUtil.HashPassword(password);

            // Retrieve user data from the database
            User user = User.Login(username, hashedPassword);

            if (user != null)
            {
                // Successful login
                Database.CurrentUserId = user.Id;
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Failed login
                await DisplayAlert("Login Failed", "Invalid username or password. Please try again.", "OK");
            }
        }

        public async void OnCreateAccount(object sender, EventArgs e)
        {
            // Navigate to the CreateAccountPage
            await Navigation.PushAsync(new CreateAccount());
        }

    }
}
