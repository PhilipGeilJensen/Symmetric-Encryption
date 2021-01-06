using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Symmetric_Encryption
{
    class Program
    {
        static string text = "";
        static bool path = false;
        static byte[] key;
        static byte[] iv;

        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Text/Path = {0}", text);
                Console.WriteLine("Key = {0}", key);
                Console.WriteLine("IV = {0}", iv);
                Menu();
                HandleOption(Console.ReadLine());
            }
        }
        /// <summary>
        /// Menu for representing the options
        /// </summary>
        static void Menu()
        {
            List<string> options = new List<string>() { "Enter text to encrypt", "Enter path to a file/folder", "Generate key", "Generate IV", "Encrypt with DES", "Encrypt with Triple DES", "Encrypt with AES", "Decrypt with DES", "Decrypt with Triple DES", "Decrypt with AES" };
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i+1, options[i]);
            }
        }

        /// <summary>
        /// Handles the option selected by the user - and initiates the chosen action
        /// </summary>
        /// <param name="input"></param>
        static void HandleOption(string input)
        {
            Console.Clear();
            int i;
            try
            {
                i = int.Parse(input);
            }
            catch (Exception)
            {
                throw;
            }
            switch (i)
            {
                case 1:
                    Console.WriteLine("Enter the text to encrypt");
                    text = Console.ReadLine();
                    path = false;
                    break;
                case 2:
                    Console.WriteLine("Enter the path");
                    text = Console.ReadLine();
                    path = true;
                    break;
                case 3:
                    Console.WriteLine("Choose key length");
                    key = Encrypt.GenerateKey(int.Parse(Console.ReadLine()));
                    break;
                case 4:
                    Console.WriteLine("Choose key length");
                    iv = Encrypt.GenerateKey(int.Parse(Console.ReadLine()));
                    break;
                case 5:
                    HandleEncryptOption(new DesEncryption());
                    break;
                case 6:
                    HandleEncryptOption(new TripleDesEncryption());
                    break;
                case 7:
                    HandleEncryptOption(new AesEncryption());
                    break;
                case 8:
                    HandleDecryptOption(new DesEncryption());
                    break;
                case 9:
                    HandleDecryptOption(new TripleDesEncryption());
                    break;
                case 10:
                    HandleDecryptOption(new AesEncryption());
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Handles the representation of the encryption
        /// </summary>
        /// <param name="e"></param>
        static void HandleEncryptOption(Encrypt e)
        {
            Stopwatch s = new Stopwatch();
            string t;
            if (path)
            {
                t = System.IO.File.ReadAllText(@text);
            }
            else
            {
                t = text;
            }
            s.Start();
            byte[] encrypted = e.Encryption(Encoding.UTF8.GetBytes(t), key, iv);
            s.Stop();
            if (path)
            {
                System.IO.File.WriteAllText(@text, Convert.ToBase64String(encrypted));
            }
            else
            {
                text = t;
            }
            Console.WriteLine();
            Console.WriteLine("Encrypted {0} bytes", Encoding.UTF8.GetBytes(t).Length);
            if (Encoding.UTF8.GetBytes(t).Length <= 200)
            {
                Console.WriteLine("This is the encrypted: {0}", Convert.ToBase64String(encrypted));
            }
            Console.WriteLine("Timespan: {0}", s.Elapsed);
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        /// <summary>
        /// Handles the representation of the decryption
        /// </summary>
        /// <param name="e"></param>
        static void HandleDecryptOption(Encrypt e)
        {
            Stopwatch s = new Stopwatch();
            string t;
            if (path)
            {
                t = System.IO.File.ReadAllText(@text);
            }
            else
            {
                t = text;
            }
            s.Start();
            byte[] decrypted = e.Decryption(Convert.FromBase64String(t), key, iv);
            s.Stop();
            if (path)
            {
                System.IO.File.WriteAllText(@text, Encoding.UTF8.GetString(decrypted));
            }
            else
            {
                text = t;
            }
            Console.WriteLine();
            Console.WriteLine("Decrypted {0} bytes", Encoding.UTF8.GetBytes(t).Length);
            if (Encoding.UTF8.GetBytes(t).Length <= 200)
            {
                Console.WriteLine("This is the decrypted: {0}", Encoding.UTF8.GetString(decrypted));
            }
            Console.WriteLine("Timespan: {0}", s.Elapsed);
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}
