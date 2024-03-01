using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using web.Models;

namespace web.Services;

public class UserService
{
    private IEnumerable<MyUser> users { get; set; }
    public IEnumerable<MyUser> Users
    {
        get => users;
        set
        {
            var rawUsers = JsonSerializer.Serialize(value);
            File.WriteAllText(storageFile, rawUsers);
            users = value;
        }
    }

    private static string storageFile = "users.json";

    public UserService()
    {
        users = [];

        // if (File.Exists(storageFile))
        // {
        //     var rawUsers = File.ReadAllText(storageFile);
        //     users = JsonSerializer.Deserialize<IEnumerable<MyUser>>(rawUsers) ?? throw new Exception("deserialized users are null");
        // }
    }

    public async Task StoreFile(string userName, IBrowserFile file)
    {
        try
        {
            var trustedFileName = Path.GetRandomFileName();
            var path = Path.Combine("wwwroot/uploads", file.Name);

            await using FileStream fs = new(path, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);


            Console.WriteLine($"file saved: {file.Name}");
            addFileToUser(userName, file);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error - File: {file.Name} Error: {ex.Message}");
        }
    }

    private void addFileToUser(string userName, IBrowserFile file)
    {
        // var currentUser = Users.FirstOrDefault(u => u.Name == userName);
        // if (currentUser == null)
        // {
        //     var newUser = new User
        //     {
        //         Name = userName,
        //         // OwnedFiles = [file.Name]
        //     };

        //     Users = Users.Append(newUser);
        // }
        // else
        // {
        //     var myusers = Users;
        //     var updatedUser = currentUser with
        //     {
        //         // OwnedFiles = currentUser.OwnedFiles.Append(file.Name)
        //     };

        //     Users = Users.Select(u =>
        //         u.Name == updatedUser.Name
        //             ? updatedUser
        //             : u
        //     ).ToArray();
        // }
    }
}