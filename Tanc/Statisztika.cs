using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Tanc
{
    class Statisztika
    {
        List<Course> courses = new List<Course>();

        public void Run() 
        {
            Console.WriteLine();
            BeolvasasAdatbazisbol();
            Console.WriteLine();
            GroupKurzusSzama();
            Console.WriteLine();
            LeghosszabbKurzus();
            Console.WriteLine();
            KurzusokKereseseNevSzerint();
            Console.ReadLine();
        }

        public void BeolvasasAdatbazisbol()
        {
            var result = new List<Course>();
            String connectionString =
                "server=localhost;port=3306;database=dancestudio;" +
                "user id=root; password=;";
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = "SELECT *\n" +
                "FROM courses\n" +
                "ORDER BY id;";

            var cmd = new MySqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                var id = reader.GetInt32("id");
                var name = reader.GetString("name");
                var type = reader.GetString("type");
                var length = reader.GetInt32("length");
                var instructor = reader.GetString("instructor");

                result.Add(new Course(id, name, type, length, instructor));
            }

            courses.Clear();
            courses.AddRange(result);
        }

        public void GroupKurzusSzama()
        {
            var count = courses.Count(c => c.type.Equals("group"));
            Console.WriteLine($"Csoportos kurzusok száma: {count}");
        }

        public void LeghosszabbKurzus()
        {
            var longest = courses.OrderBy(c => c.lenght).FirstOrDefault();
            if (longest is null)
            {
                Console.WriteLine("Nincs kurzus az adatbázisban");
                return;
            }
            else
            {
                Console.WriteLine("Leghosszabb kurzus adatai:");
                Console.WriteLine(longest.ToString());
            }
        }

        public void KurzusokKereseseNevSzerint()
        {
            Console.Write("Adjon meg egy kurzus nevet:");
            var input = Console.ReadLine()?.Trim() ?? "";
            if (input.Length == 0)
            {
                Console.WriteLine("Nincs ilyen kurzus");
                return;
            }

            var exist = courses.Any(c => c.name.Equals(input));
            var course = courses.FirstOrDefault(c => c.name.Equals(input));

            Console.WriteLine(exist ? $"A megadott kurzus oktatója: " +
                $"{course.instructor}" : "Nincs ilyen kurzus");
        }

        public List<Course> KurzusokVisszaadasa()
        {
            var result = new List<Course>();
            String connectionString =
                "server=localhost;port=3306;database=dancestudio;" +
                "user id=root; password=;";
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = "SELECT *\n" +
                "FROM courses\n" +
                "ORDER BY id;";

            var cmd = new MySqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var id = reader.GetInt32("id");
                var name = reader.GetString("name");
                var type = reader.GetString("type");
                var length = reader.GetInt32("length");
                var instructor = reader.GetString("instructor");

                result.Add(new Course(id, name, type, length, instructor));
            }
            return result;
        }
    }
}
