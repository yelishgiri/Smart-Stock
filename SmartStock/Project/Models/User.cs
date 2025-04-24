using SQLite;
public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string CompanyName { get; set; }
    public bool IsAdmin { get; set; } = false;
}

