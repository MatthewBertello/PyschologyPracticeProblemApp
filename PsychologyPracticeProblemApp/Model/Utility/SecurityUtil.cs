using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PsychologyPracticeProblemApp.Model.Utility;
public static class SecurityUtil {
   
    /// <summary>
    /// Encrypts the message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string Encrypt(string message)
    {
        //salt = new Guid().ToString(); // simple random salt
        // message to bytes
        byte[] data = Encoding.ASCII.GetBytes(message);
        // hash the bytes
        SHA384 sha = SHA384.Create();
        byte[] result = sha.ComputeHash(data);
        // bytes back to string
        return Encoding.UTF8.GetString(result);
    }
    
    /// <summary>
    /// Hash the password using BCrypt
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string HashPassword(string password)
    {
        return Encrypt(password);
        //return BCrypt.Net.BCrypt.HashPassword(password);
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
        return Encrypt(enteredPassword) == hashedPassword;
        //return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

}
