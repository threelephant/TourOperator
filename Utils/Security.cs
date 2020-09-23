using System;
using System.Security;

namespace TourOperator
{
    public class Security
    {
        public SecureString GetPassword()
        {
            Console.WriteLine("Enter the password:");

            var pwd = new SecureString();

            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.RemoveAt(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000')
                {
                  pwd.AppendChar(i.KeyChar);
                  Console.Write("*");
                }
            }
            Console.WriteLine('\n');

            return pwd;
        }
    }
}