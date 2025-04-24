using SQLite;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public DateTime ExpiryDate { get; set; }
}
