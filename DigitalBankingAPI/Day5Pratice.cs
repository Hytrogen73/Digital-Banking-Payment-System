using DigitalBankingAPI.Models;
using System.Collections.Generic;
using System.Data;
namespace DigitalBankingAPI;

public class Day5Pratice
{
    public void Run()
    {
        List<User> users = new();

        users.Add(new User(1, "Nita"));
        users.Add(new User(2, "Linda"));
        users.Add(new User(3, "Vichet"));
        users.Add(new User(2, "Borin"));

        //User? foundUser = users.Find(users => users.Username == "Linda");
        //    if (foundUser == null)
        //    {
        //        Console.WriteLine("User Not Found!");
        //    }
        //    else
        //    {
        //        Console.WriteLine(foundUser.Username);
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}");
        }
    
    }
    

}
