namespace SmartStock.Project.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage() => InitializeComponent();

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both username and password", "OK");
            return;
        }

        var user = await App.DbHelper.GetUserAsync(username, password);
        if (user != null)
        {
            App.CurrentUser = user;
            await Shell.Current.GoToAsync(nameof(DashBoardPage));
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid credentials", "Try Again");
        }
    }
}
