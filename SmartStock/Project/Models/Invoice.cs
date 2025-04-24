using SQLite;
public class Invoice
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
}
