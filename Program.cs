using sdev245_mod1_assign1;
using System.Globalization;
internal class Program
{
  private static Dictionary<string, User> USERS = new() {
    { "user", new("user","user","User") },
    { "admin", new("admin","admin","Admin") }
  };

  private static void SetupDemoData()
  {
    USERS["user"].ListItems.AddRange(["Lorem Ipsum", "Do user things..."]);
    USERS["admin"].ListItems.AddRange(["Lorem Ipsum", "Do admin things..."]);
  }

  public static string CURRENT_USER_ID = "";
  public static bool USER_AUTHED = false;
  public static bool USER_IS_ADMIN = false;

  private static void ShowHelp()
  {
    Console.Write("Commands: help, exit, clear");
    if (USER_AUTHED) {
      Console.Write(", logout, list, add, remove, removeall");
      if (USER_IS_ADMIN) Console.Write(", adduser, removeuser, listuser");
    } else Console.Write(", login");
    Console.WriteLine();
  }
  private static void Login_Handler() { 
    if (!USER_AUTHED) {
      bool validUser = false;
      bool validPassword = false;
      string _user = "";
      string _password = "";
      
      Console.Write("username? ");
      _user = Console.ReadLine()??"";
      Console.Write("password? ");
      _password = Console.ReadLine() ?? "";

      validUser = _user != "" && USERS.ContainsKey( _user );
      validPassword = validUser && _password != "" && USERS[_user].Password == _password;

      if (!validUser || !validPassword) {
        Console.WriteLine("Invalid creds.");
        return;
      }

      CURRENT_USER_ID = _user;
      USER_AUTHED = true;
      USER_IS_ADMIN = CURRENT_USER_ID == "admin";
      
      Console.WriteLine($"Hello {USERS[CURRENT_USER_ID].Name}!");
    }
  }
  private static void Logout_Handler() {
    if (USER_AUTHED) {
      Console.WriteLine($"Goodbye {USERS[CURRENT_USER_ID].Name}!");
      CURRENT_USER_ID = "";
      USER_AUTHED = false;
      USER_IS_ADMIN = false;
    }
  }
  private static void List_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else {
      if (USERS[CURRENT_USER_ID].ListItems.Count < 1) Console.WriteLine("No items.");
      else {
        int i = 1;
        foreach (string s in USERS[CURRENT_USER_ID].ListItems) {
          Console.WriteLine($"({i}) {s}");
          i++;
        }
      }
    }
  }
  private static void RemoveAll_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else USERS[CURRENT_USER_ID].ListItems.Clear();
  }
  private static void Remove_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else {
      Console.Write($"item number? ");
      string s = Console.ReadLine() ?? "";
      if (s != "") {
        _ = int.TryParse(s, out int i);
        if ( i > 0 && i <= USERS[CURRENT_USER_ID].ListItems.Count) USERS[CURRENT_USER_ID].ListItems.RemoveAt(i-1);
      }
    }
  }
  private static void Add_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else {
      Console.Write($"{USERS[CURRENT_USER_ID].ListItems.Count+1}? ");
      string s = Console.ReadLine()??"";
      if (s != "") USERS[CURRENT_USER_ID].ListItems.Add(s.Replace("\"", ""));
    }
  }
  private static void AddUser_Handler() {
    if (!USER_AUTHED)
      Console.WriteLine("Please login.");
    else if (!USER_IS_ADMIN)
      Console.WriteLine("Only the admin can edit users.");
    else {
      Console.Write("ID? ");
      string new_id = Console.ReadLine() ?? "";
      if (new_id == "") {
        Console.WriteLine("A user cannot have a blank id.");
        return;
      } else if (USERS.ContainsKey(new_id)){
        Console.WriteLine("A user with that ID already exists.");
        return;
      }

      Console.Write("Password? ");
      string new_password = Console.ReadLine() ?? "";
      if (new_password == "") {
        Console.WriteLine("A user cannot have a blank password.");
        return;
      }

      Console.Write("Name? ");
      string new_name = Console.ReadLine() ?? "";
      if (new_name == "") {
        Console.WriteLine("A user must have a name");
        return;
      }

      USERS.Add(new_id, new User(new_id, new_password, new_name));
      Console.WriteLine("User added.");
    }
  }
  private static void RemoveUser_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else if (!USER_IS_ADMIN) Console.WriteLine("Only the admin can edit users.");
    else {
      Console.Write("ID? ");
      string id = Console.ReadLine() ?? "";
      if (id != "" || USERS.ContainsKey(id)) {
        if (id == "admin") Console.WriteLine("The admin account may not be removed.");
        else {
          USERS.Remove(id);
          Console.WriteLine("User removed.");
        }
      }
    }
  }
  private static void ListUser_Handler()
  {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else if (!USER_IS_ADMIN) Console.WriteLine("Only the admin can edit users.");
    else {
      foreach (string id in USERS.Keys) {
       Console.WriteLine($"Name: \"{USERS[id].Name}\" , id: \"{USERS[id].Id}\"");
      }
    }
  }

  private static void Passwd_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else {
      Console.Write("New Password? ");
      string new_password = Console.ReadLine() ?? "";
      if (new_password == "")
      {
        Console.WriteLine("A user cannot have a blank password.");
        return;
      } else {
        USERS[CURRENT_USER_ID].ChangePassword(new_password);
        Console.WriteLine("Password changed.");
      }
    }
  }
  private static void PasswdUser_Handler() {
    if (!USER_AUTHED) Console.WriteLine("Please login.");
    else if (!USER_IS_ADMIN) Console.WriteLine("Only the admin can edit users.");
    else
    {
      Console.Write("ID? ");
      string user_id = Console.ReadLine() ?? "";
      if (user_id == "" || !USERS.ContainsKey(user_id))
      {
        Console.WriteLine("Invalid user id.");
        return;
      }

      Console.Write("New Password? ");
      string new_password = Console.ReadLine() ?? "";
      if (new_password == "")
      {
        Console.WriteLine("A user cannot have a blank password.");
        return;
      }

      USERS[user_id].ChangePassword(new_password);

    }
  }
  private static void Main(string[] args)
  {
    Console.WriteLine("┌─────────────────────┐");
    Console.WriteLine("│ Simple List Tracker │");
    Console.WriteLine("└─────────────────────┘");
    Console.WriteLine();
    SetupDemoData();
    ShowHelp();

    string nextInput = "";

    while ( nextInput != "exit") {
      Console.Write($"{CURRENT_USER_ID}> ");
      nextInput = Console.ReadLine()??"";
      switch (nextInput) {
        case "help": ShowHelp(); break;
        case "clear": Console.Clear(); break;
        case "login": Login_Handler(); break;
        case "logout": Logout_Handler(); break;
        case "list": List_Handler(); break;
        case "removeall": RemoveAll_Handler(); break;
        case "remove": Remove_Handler(); break;
        case "add": Add_Handler(); break;
        case "adduser": AddUser_Handler(); break;
        case "removeuser": RemoveUser_Handler(); break;
        case "listuser": ListUser_Handler(); break;
        case "passwd": Passwd_Handler(); break;
        case "passwduser": PasswdUser_Handler(); break;
        default: break;
      }
    }
  }
}