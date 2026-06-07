using System;
using System.Collections.Generic;
using System.Text;

namespace sdev245_mod1_assign1
{
  internal class User(string id, string pass, string name)
  {
    public List<string> ListItems = [];
    private string _id = id.Replace("\"", "");
    private string _password = pass.Replace("\"", "");
    private string _name = name.Replace("\"", "");
    public string Id { get => _id; }
    public string Password { get => _password; }
    public string Name { get => _name; }

    public void ChangePassword(string password) {
      _password = password.Replace("\"","");
    }
  }
}
