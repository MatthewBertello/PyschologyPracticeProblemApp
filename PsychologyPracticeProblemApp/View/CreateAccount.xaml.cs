/*
 * Michael Meisenburg
 * Date: 10/18/2023
*/

namespace PsychologyPracticeProblemApp;

public partial class CreateAccount : ContentPage
{
	public CreateAccount()
	{
		InitializeComponent();
	}

    private async void OnCancel(object sender, EventArgs e)
    {
        //navigate back to the login page
        await Navigation.PopAsync();
    }
}
