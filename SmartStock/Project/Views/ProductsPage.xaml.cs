namespace SmartStock.Project.Views;
public partial class ProductsPage : ContentPage
{
    private List<Product> allProducts = new();
	public ProductsPage()
	{
		InitializeComponent();
        LoadProductsData();
        LoadLowStockData();

	}

    private async void LoadProductsData(){
        if (App.CurrentUser != null)
        {
            UsernameLabel.Text = App.CurrentUser.Username;
            CompanyLabel.Text = App.CurrentUser.CompanyName;
        }     

         var products = await App.DbHelper.GetProductsAsync();
        allProducts = products; 
        LatestProductListView.ItemsSource = products.OrderByDescending(p => p.Id).Take(5).ToList();
        LowStockProductView.ItemsSource = products;


    }

    private async void LoadLowStockData(){
        var product = await App.DbHelper.GetLowStockProductsAsync(10);
        LowStockProductView.ItemsSource = product;
    }


	 private void DashBoardtbtn_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(DashBoardPage));
    }

    private async void LogOutbtn_Clicked(object sender, EventArgs e)
    {
        App.CurrentUser = null;
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private async Task RefreshProductLists()
{
    var products = await App.DbHelper.GetProductsAsync();
    LatestProductListView.ItemsSource = products.OrderByDescending(p => p.Id).Take(5).ToList();

    var lowStock = await App.DbHelper.GetLowStockProductsAsync(10);
    LowStockProductView.ItemsSource = lowStock;
}


private void OnDecreaseQuantityClicked(object sender, EventArgs e)
{
    if (int.TryParse(QuantityEntry.Text, out int quantity))
    {
        if (quantity > 0)
        {
            quantity--;
            QuantityEntry.Text = quantity.ToString();
        }
    }
    else
    {
        QuantityEntry.Text = "0"; 
    }
}

    private void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        if (int.TryParse(QuantityEntry.Text, out int quantity))
    {
        quantity++;
        QuantityEntry.Text = quantity.ToString();
    }
        else
    {
        QuantityEntry.Text = "1";
    }
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
    try
    {
                AddProductButton.IsEnabled = false; 
        if (string.IsNullOrWhiteSpace(ProductNameEntry.Text) ||
            string.IsNullOrWhiteSpace(BarcodeEntry.Text) ||
            string.IsNullOrWhiteSpace(CategoryEntry.Text) ||
            string.IsNullOrWhiteSpace(QuantityEntry.Text) ||
            string.IsNullOrWhiteSpace(PriceEntry.Text))
        {
            await DisplayAlert("Error", "Please fill out all required fields.", "OK");
            return;
        }

        if (!int.TryParse(QuantityEntry.Text, out int quantity) || quantity <= 0)
        {
            await DisplayAlert("Error", "Quantity must be a positive number.", "OK");
            return;
        }

        if (!decimal.TryParse(PriceEntry.Text, out decimal price) || price <= 0)
        {
            await DisplayAlert("Error", "Price must be a valid amount.", "OK");
            return;
        }

        var newProduct = new Product
        {
            Name = ProductNameEntry.Text.Trim(),
            Barcode = BarcodeEntry.Text.Trim(),
            Category = CategoryEntry.Text.Trim(),
            Quantity = quantity,
            Price = price,
            Description = DescriptionEditor.Text?.Trim(),
            ExpiryDate = ExpiryDatePicker.Date
        };

        await App.DbHelper.AddProductAsync(newProduct);

        await DisplayAlert("Success", "Product added successfully!", "OK");

        ProductNameEntry.Text = "";
        BarcodeEntry.Text = "";
        CategoryEntry.Text = "";
        QuantityEntry.Text = "";
        PriceEntry.Text = "";
        DescriptionEditor.Text = "";
        ExpiryDatePicker.Date = DateTime.Now;

        await RefreshProductLists();
    }
    catch (Exception ex)
    {
        await DisplayAlert("Error", $"Failed to add product: {ex.Message}", "OK");
    }
     finally
    {
        AddProductButton.IsEnabled = true; 
    }
}

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
     var keyword = SearchEntry.Text?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            LatestProductListView.ItemsSource = allProducts;
        }
        else
        {
         var filteredProducts = allProducts
            .Where(p => (p.Name?.ToLower().Contains(keyword) ?? false) ||
                        (p.Category?.ToLower().Contains(keyword) ?? false) ||
                        (p.Barcode?.ToLower().Contains(keyword) ?? false))
            .ToList();

        LatestProductListView.ItemsSource = filteredProducts;
        }
    }
}
