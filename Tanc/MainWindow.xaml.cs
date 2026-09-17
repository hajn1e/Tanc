using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Tanc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Course> courses = new List<Course>();
        public MainWindow()
        {

            InitializeComponent();

            try
            {
                LoadCourses();
            }
            catch (Exception ex) {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz. \n\n" + 
                    ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }
        }
        public void LoadCourses()
        {
            Statisztika statisztika = new Statisztika();
            courses = statisztika.KurzusokVisszaadasa();

            CoursesGrid.ItemsSource = null;
            CoursesGrid.ItemsSource = courses;

        }

        
        private void DltButton_Click(object sender, RoutedEventArgs e)
        {
            if(CoursesGrid.SelectedItem is not Course selected)
            {
                MessageBox.Show("Törléshez előbb válasszon ki kurzust!",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Biztos hogy törölni szeretné a kiválasztott kurzust?",
                "Megerősités",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if(confirm == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                var ok = DeleteCourseByID(selected.id);
                if (!ok)
                {
                    MessageBox.Show("Sikertelen a törlés", "Hiba",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                courses.Remove(selected);
                MessageBox.Show("Sikeres törlés");
                LoadCourses();
            } catch(Exception ex)
            {
                MessageBox.Show("Sikertelen törlés");
            }
        }

        public bool DeleteCourseByID(long id)
        {
            string connectionString =
                "server=localhost;port=3306;database=dancestudio;" +
                "user id=root; password=;";
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = "DELETE FROM courses WHERE id= @id";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);

            var affected = cmd.ExecuteNonQuery();

            return affected == 1;
        }
    }
}