namespace SmartStock.Project.Views;

public partial class DashBoardPage : ContentPage
{
    public DashBoardPage()
    {
        InitializeComponent();
        LoadDashboardData();
    }

    private async void LoadDashboardData()
    {
        if (App.CurrentUser != null)
        {
            UsernameLabel.Text = App.CurrentUser.Username;
            CompanyLabel.Text = App.CurrentUser.CompanyName;
        }
 

        var products = await App.DbHelper.GetProductsAsync();
               foreach (var product in products)
{
    Console.WriteLine($"[DEBUG] Product: {product.Id} - {product.Name}");
}

        int totalProducts = products.Count;
        int totalQuantity = products.Sum(p => p.Quantity);
        decimal totalValue = products.Sum(p => p.Price * p.Quantity);

        TotalProductsLabel.Text = totalProducts.ToString();
        TotalQuantityLabel.Text = totalQuantity.ToString();
        TotalValueLabel.Text = "$" + totalValue.ToString("F2");

        ProductListView.ItemsSource = products;

        NewlyAddedList.ItemsSource = products.OrderByDescending(p => p.Id).Take(5).ToList();
    }

    private void Productstbtn_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ProductsPage));
    }

    private void LogOutbtn_Clicked(object sender, EventArgs e)
    {
        App.CurrentUser = default!;
        Shell.Current.GoToAsync(nameof(LoginPage));
    }
}