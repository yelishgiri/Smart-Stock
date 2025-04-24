namespace SmartStock.Project.Views;
public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(DashBoardPage));
    }
}