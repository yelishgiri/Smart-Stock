namespace SmartStock.Project.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage() => InitializeComponent();

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(CompanyNameEntry.Text))
        {
            await DisplayAlert("Error", "Please fill out all fields.", "OK");
            return;
        }

        var newUser = new User
        {
            Username = UsernameEntry.Text,
            Password = PasswordEntry.Text,
            Email = EmailEntry.Text,
            CompanyName = CompanyNameEntry.Text
        };

        await App.DbHelper.AddUserAsync(newUser);
        await DisplayAlert("Success", "Registered successfully!", "OK");
        await Navigation.PopAsync();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
