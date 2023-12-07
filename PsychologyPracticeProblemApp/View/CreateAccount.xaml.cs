/*
 * Michael Meisenburg
 * Date: 10/18/2023
*/
using PsychologyPracticeProblemApp.Model.Utility;

namespace PsychologyPracticeProblemApp;

public partial class CreateAccount : ContentPage
{
	public CreateAccount()
	{
		InitializeComponent();
	}

    private async void OnCreateAccount(object sender, EventArgs e)
    {
        // Retrieve user input
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        // Hash the password before saving it to the database

        string email = EmailEntry.Text;
        string firstName = "";
        string lastName = "";

        // Validate user input
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Please fill out all the fields.", "OK");
            return;
        }

        // Check for duplicate username
        bool isUsernameExists = Database.CheckUsernameExists(username);
        if (isUsernameExists)
        {
            await DisplayAlert("Error", "Username already exists. Please choose a different username.", "OK");
            return;
        }

        // Check for duplicate email
        bool isEmailExists = Database.CheckEmailExists(email);
        if (isEmailExists)
        {
            await DisplayAlert("Error", "Email already exists. Please use a different email.", "OK");
            return;
        }

	
        bool accountCreated = Database.AddUser(username, password, email, firstName, lastName);


        if (accountCreated)
        {
            // account created successfully
            await DisplayAlert("Success", "Account created successfully!", "OK");
            await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            // account creation failed for some reason
            await DisplayAlert("Error", "Account creation failed. Please try again later.", "OK");
        }
    }




    private async void OnCancel(object sender, EventArgs e)
    {
        //navigate back to the login page
        await Navigation.PopAsync();
    }

}
