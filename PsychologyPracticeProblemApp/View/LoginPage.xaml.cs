/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

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

            // Check if username and password are entered
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            // Retrieve user data from the database
            User user = Database.GetUser(username, password);

            if (user != null)
            {
                // Successful login
                await DisplayAlert("Login Successful", "Welcome, " + user.FirstName + "!", "OK");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Failed login
                await DisplayAlert("Login Failed", "Invalid username or password. Please try again.", "OK");
            }
        }
    }
}
