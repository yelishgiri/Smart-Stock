namespace SmartStock.Project.Views;
public partial class ProductsPage : ContentPage
{
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

        LatestProductListView.ItemsSource = products;
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

    private void LogOutbtn_Clicked(object sender, EventArgs e)
    {
        App.CurrentUser = default!;
        Shell.Current.GoToAsync(nameof(LoginPage));
    }

}
