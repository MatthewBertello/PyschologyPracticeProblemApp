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
using PsychologyPracticeProblemApp.Model.Utility;

namespace PsychologyPracticeProblemApp;

public partial class MCreateAccountPage : ContentPage, INotifyPropertyChanged {
    private static int MIN_PASSWORD_LENGTH = 5;
    private static int MIN_USERNAME_LENGTH = 5;

    public event PropertyChangedEventHandler PropertyChanged;
    public String ErrorMessage { get; set; } = String.Empty;
    public MCreateAccountPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    private void OnCreateAccount(object sender, EventArgs e)
    {
        String username = EntryUsername.Text;
        String fname = EntryFirstName.Text;
        String lname = EntryLastName.Text;
        String email = EntryEmail.Text;
        String password = EntryPassword.Text;
        String password2 = EntryPassword2.Text;

        // verify credentials
        ErrorMessage = VerifyInputs(fname, lname, username, password, password2, email);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
        if(ErrorMessage != String.Empty) return;

        // is valid, so create and login
        Database.AddUser(username, password, email, fname, lname);

        User user = User.Login(username, password);
        if(user != null)
            Navigation.PushAsync(new MProblemSelectPage());
    }
    private String VerifyInputs(String fname, String lname, String username, String password, String password2, String email)
    {
        if(string.IsNullOrEmpty(fname)) return "First Name can't be empty";
        if(string.IsNullOrEmpty(lname)) return "Last Name can't be empty";
        if(string.IsNullOrEmpty(email)) return "Email can't be empty";
        if(string.IsNullOrEmpty(username)) return "Please enter username";
        if(string.IsNullOrEmpty(password)) return "Please enter password";
        if(string.IsNullOrEmpty(password2)) return "Please enter password";
        if(!password.Equals(password2)) return "Passwords do not match";

        if(Database.CheckUsernameExists(username)) return "Username already in use";
        if(Database.CheckEmailExists(email)) return "Email already in use";

        if(password.Length < MIN_PASSWORD_LENGTH) return "Password must be at least " + MIN_PASSWORD_LENGTH + " characters";
        if(username.Length < MIN_USERNAME_LENGTH) return "Password must be at least " + MIN_USERNAME_LENGTH + " characters";

        return String.Empty;
    }
    private void OnCancel(object sender, EventArgs e)
    {
        Task _ = Navigation.PopToRootAsync();
    }

}
