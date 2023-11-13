using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class User {
    public static Guid Guest => Guid.Empty;
    public static User Current { get; set; }


    private Guid id;

    private int errorCallback;
    private String name;
    public int ErrorCallback => errorCallback;

    public String Name => name;
    public Guid Id => id;



    /// <summary>
    /// Attempts to login user based on username and password.
    /// If login failed, user.ErrorCallback != 0
    /// </summary>
    /// <returns></returns>
    public static User Login(String? username=null, String? password=null)
    {
        User user = new User() {
            id = Guest, // default to admin
            errorCallback = 0, // default to no errors
            name="Guest"
        };
        Current = user;
        return user;
    }
    public async static void Logout(ContentPage currentPage)
    {
        Current = null;
        await currentPage.Navigation.PopToRootAsync();
    }

}
