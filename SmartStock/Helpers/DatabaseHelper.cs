using SQLite;

public class DatabaseHelper
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseHelper(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<User>().Wait();
        _db.CreateTableAsync<Product>().Wait();
        _db.CreateTableAsync<Invoice>().Wait();
        SeedMockData();
    }

    private async void SeedMockData()
    {
        var existingUser = await _db.Table<User>().FirstOrDefaultAsync();
        if (existingUser == null)
        {
            var admin = new User
            {
                Username = "admin",
                Password = "admin123",
                Email = "admin@smartstock.com",
                CompanyName = "SmartFuel Inc.",
                IsAdmin = true
            };
            await _db.InsertAsync(admin);

            var mockProducts = new List<Product>
            {
                new Product { Name = "Regular Gasoline", Category = "Fuel", Quantity = 1000, Price = 3.29m, Barcode = "100001", Description = "Standard fuel", ExpiryDate = DateTime.Now.AddYears(1) },
                new Product { Name = "Diesel", Category = "Fuel", Quantity = 800, Price = 3.59m, Barcode = "100002", Description = "Diesel fuel", ExpiryDate = DateTime.Now.AddYears(1) },
                new Product { Name = "Coca-Cola Can", Category = "Beverage", Quantity = 200, Price = 1.25m, Barcode = "100003", Description = "Chilled soda", ExpiryDate = DateTime.Now.AddMonths(6) },
                new Product { Name = "Snickers Bar", Category = "Snack", Quantity = 150, Price = 1.10m, Barcode = "100004", Description = "Chocolate snack", ExpiryDate = DateTime.Now.AddMonths(4) },
                new Product { Name = "Motor Oil 5W-30", Category = "Auto", Quantity = 75, Price = 5.99m, Barcode = "100005", Description = "Synthetic motor oil", ExpiryDate = DateTime.Now.AddYears(2) },
                new Product { Name = "Windshield Washer Fluid", Category = "Auto", Quantity = 50, Price = 3.49m, Barcode = "100006", Description = "All-season cleaner", ExpiryDate = DateTime.Now.AddYears(2) },
                new Product { Name = "Red Bull", Category = "Beverage", Quantity = 120, Price = 2.99m, Barcode = "100007", Description = "Energy drink", ExpiryDate = DateTime.Now.AddMonths(5) },
                new Product { Name = "Chips Lays", Category = "Snack", Quantity = 180, Price = 1.50m, Barcode = "100008", Description = "Potato chips", ExpiryDate = DateTime.Now.AddMonths(3) },
                new Product { Name = "Cigarettes", Category = "Tobacco", Quantity = 90, Price = 7.99m, Barcode = "100009", Description = "Pack of cigarettes", ExpiryDate = DateTime.Now.AddYears(1) },
                new Product { Name = "Car Freshener", Category = "Auto", Quantity = 60, Price = 2.50m, Barcode = "100010", Description = "Scented freshener", ExpiryDate = DateTime.Now.AddYears(1) },
                new Product { Name = "Flashlight Battery", Category = "Electronics", Quantity = 3, Price = 4.99m, Barcode = "100011", Description = "Low power battery", ExpiryDate = DateTime.Now.AddMonths(18) },
                new Product { Name = "Notebook", Category = "Stationery", Quantity = 5, Price = 2.50m, Barcode = "100012", Description = "200-page ruled", ExpiryDate = DateTime.Now.AddYears(2) },
                new Product { Name = "First Aid Kit", Category = "Health", Quantity = 2, Price = 15.00m, Barcode = "100013", Description = "Emergency use", ExpiryDate = DateTime.Now.AddYears(3) },
                new Product { Name = "USB Cable", Category = "Electronics", Quantity = 8, Price = 3.99m, Barcode = "100014", Description = "Type-C USB", ExpiryDate = DateTime.Now.AddYears(5) },
                new Product { Name = "Face Mask Pack", Category = "Health", Quantity = 1, Price = 6.00m, Barcode = "100015", Description = "Reusable masks", ExpiryDate = DateTime.Now.AddMonths(24) },
            };

            foreach (var product in mockProducts)
            {
                await _db.InsertAsync(product);
            }
        }
    }

    public Task<User> GetUserAsync(string username, string password) =>
        _db.Table<User>().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

    public Task<int> AddUserAsync(User user) => _db.InsertAsync(user);

    public Task<List<Product>> GetProductsAsync() => _db.Table<Product>().ToListAsync();

    public Task<int> AddProductAsync(Product product) => _db.InsertAsync(product);

    public Task<int> DeleteProductAsync(Product product) => _db.DeleteAsync(product);

    public Task<int> UpdateProductAsync(Product product) => _db.UpdateAsync(product);

    public Task<int> AddInvoiceAsync(Invoice invoice) => _db.InsertAsync(invoice);

    public Task<List<Invoice>> GetInvoicesByUserAsync(int userId) =>
        _db.Table<Invoice>().Where(i => i.UserId == userId).ToListAsync();

    public Task<List<Product>> GetLowStockProductsAsync(int threshold = 10){
    return _db.Table<Product>()
              .Where(p => p.Quantity < threshold)
              .Take(5)
              .ToListAsync();
}
}


