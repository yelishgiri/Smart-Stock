namespace SmartStock.Project.Views;

public partial class DashBoardPage : ContentPage
{
	public DashBoardPage()
	{
		InitializeComponent();
	}

    private void DashBoardbtn_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(DashBoardPage));
    }

    private void Productstbtn_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ProductsPage));
    }

    private void LogOutbtn_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(LoginPage));
    }
}