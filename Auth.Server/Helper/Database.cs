using Auth.Server.Model;

namespace Auth.Server.Helper;

public class Database
{
    private const string JSON_FILE_PATH = "./Files/Users.json";
    private static readonly object myLockObject = new object();
    private static Database myInstance = null;
    private JsonFileManager myJsonFileManager;
    private Database()
    {
        myJsonFileManager = new JsonFileManager(JSON_FILE_PATH);
    }

    public static Database Instance
    {
        get
        {
            if (myInstance == null)
            {
                lock (myLockObject)
                {
                    if (myInstance == null)
                    {
                        myInstance = new Database();
                    }
                }
            }
            return myInstance;
        }
    }

    public bool Register(RegisterUser user)
    {
        bool status = false;
        List<DatabaseUser> jsonData = myJsonFileManager.ReadFromFile<List<DatabaseUser>>();
        if (!jsonData.Exists(data => data.User.username == user.username))
        {
            DatabaseUser databaseUser = new DatabaseUser();
            databaseUser.Id = Guid.NewGuid();
            databaseUser.User = user;
            databaseUser.User.password = BCrypt.Net.BCrypt.EnhancedHashPassword(databaseUser.User.password, 13);
            databaseUser.Role = "User";
            jsonData.Add(databaseUser);
            myJsonFileManager.WriteToFile(jsonData);
            status = true;
        }
        return status;
    }

    public bool Unregister(RegisterUser user)
    {
        bool status = false;
        List<DatabaseUser> jsonData = myJsonFileManager.ReadFromFile<List<DatabaseUser>>();
        int index = jsonData.FindIndex(data => data.User.username == user.username);
        if (index != -1)
        {
            jsonData.RemoveAt(index);
            myJsonFileManager.WriteToFile(jsonData);
            status = true;
        }
        return status;
    }

    public bool Authenticate(LoginUser user)
    {
        RegisterUser ifdata = myJsonFileManager.ReadFromFile<List<DatabaseUser>>().Find(data => data.User.username == user.username)?.User;
        if (ifdata != null)
        {
            return ifdata.username == user.username && BCrypt.Net.BCrypt.EnhancedVerify(user.password, ifdata.password);
        }
        return false;
    }
}
