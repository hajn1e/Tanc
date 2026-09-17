using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Tanc
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Any(arg => arg == "--stat")) 
            {
                try
                {
                    //helló
                    var stat = new Statisztika();
                    stat.Run();
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine("Hiba az adatbázis elérése közben");
                    Console.WriteLine(ex.Message);
                }
                
            }
            else
            {
                App.Main();
            }
        }

    }
}
