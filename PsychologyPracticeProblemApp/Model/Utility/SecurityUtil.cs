using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.Model.Utility; 

public static class SecurityUtil {
    /// <summary>
    /// Hash the password using BCrypt
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verify if the entered password matches the hashed password using BCrypt
    /// </summary>
    /// <param name="enteredPassword"></param>
    /// <param name="hashedPassword"></param>
    ///<param name="salt"></param>
    /// <returns></returns>
    public static bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

}
