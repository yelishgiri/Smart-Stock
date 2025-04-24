using SmartStock.Project.Views;

namespace SmartStock
{
    public partial class App : Application
    {
    public static DatabaseHelper DbHelper;
    public static User CurrentUser;

        public App()
        {
            InitializeComponent();
               string dbPath = Path.Combine(FileSystem.AppDataDirectory, "smartstock.db3");
                DbHelper = new DatabaseHelper(dbPath);
                MainPage = new NavigationPage(new LoginPage());

            MainPage = new AppShell();
        }
    }
}
