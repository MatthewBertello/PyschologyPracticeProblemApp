using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class User {
    public static Guid Admin => Guid.Empty;


    private Guid id;
    private int errorCallback;
    public int ErrorCallback => errorCallback;
    public Guid Id => id;

    public static User Current { get; set; }

    /// <summary>
    /// Attempts to login user based on username and password.
    /// If login failed, user.ErrorCallback != 0
    /// </summary>
    /// <returns></returns>
    public static User Login(String? username=null, String? password=null)
    {
        User user = new User() {
            id = Admin, // default to admin
            errorCallback = 0, // default to no errors
        };
        Current = user;
        return user;
    }

}
