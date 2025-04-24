namespace SmartStock.Project.Views;
public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Shell.Current.GoToAsync(nameof(LoginPage));
    }
}