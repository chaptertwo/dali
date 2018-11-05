using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI
{
    public class DisplayAndFormats
    {
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine(@"  /$$$$$$  /$$      /$$  /$$$$$$         /$$$$$$                                        
 /$$__  $$| $$  /$ | $$ /$$__  $$       /$$__  $$                                       
| $$  \__/| $$ /$$$| $$| $$  \__/      | $$  \__/  /$$$$$$   /$$$$$$   /$$$$$$          
|  $$$$$$ | $$/$$ $$ $$| $$            | $$       /$$__  $$ /$$__  $$ /$$__  $$         
 \____  $$| $$$$_  $$$$| $$            | $$      | $$  \ $$| $$  \__/| $$  \ $$         
 /$$  \ $$| $$$/ \  $$$| $$    $$      | $$    $$| $$  | $$| $$      | $$  | $$         
|  $$$$$$/| $$/   \  $$|  $$$$$$/      |  $$$$$$/|  $$$$$$/| $$      | $$$$$$$//$$      
 \______/ |__/     \__/ \______/        \______/  \______/ |__/      | $$____/|__/      
                                                                     | $$               
                                                                     | $$               
                                                                     |__/               ");
            Console.WriteLine("1. Display Orders");
            Console.WriteLine("2. Add an Order");
            Console.WriteLine("3. Edit an Order");
            Console.WriteLine("4. Remove an Order");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("5. Quit");
            Console.ResetColor();
        }
    }
}
