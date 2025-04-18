using System.Runtime.InteropServices.Marshalling;
using AppFunctions.Helpers;

namespace AppFunctions
{
    public partial class App : Application
    {

        static SQLiteDatabaseHelpers _db;
        public static SQLiteDatabaseHelpers Db
        {

            get 
            {

                if (_db == null)
                {

                    string path = Path.Combine(
                            Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData),
                                "db_veterinario_especie.db3");

                    _db = new SQLiteDatabaseHelpers(path);

                }

                return _db;
            
            } 
                
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
