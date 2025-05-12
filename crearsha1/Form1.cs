using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace crearsha1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /// esta ruta debe estar en la base de datos o en un archivo de texto la cosa es que hay que buscar el exe del bingo
            var RutaPath = @"C:\Link_Tek_SAC";

            byte[] hash;
            using (Stream stream = File.OpenRead(RutaPath))
            {
                hash = SHA1.Create().ComputeHash(stream);
            }
            string base64Hash = Convert.ToBase64String(hash);
            textBox1.Text = base64Hash;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string someString = txtRuta.Text;
            string[] RutaPath = new string[] { someString };

            textBox1.Text = "";
            try
            {
                if (RutaPath.Length < 1)
                {
                    textBox1.Text = "No directory selected probando proeycto yomalui.";
                    return;
                }

                string directory = RutaPath[0];
                if (Directory.Exists(directory))
                {
                    // Create a DirectoryInfo object representing the specified directory.
                    var dir = new DirectoryInfo(directory);
                    // Get the FileInfo objects for every file in the directory.
                    FileInfo[] files = dir.GetFiles();
                    // Initialize a SHA256 hash object.
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        // Compute and print the hash values for each file in directory.
                        foreach (FileInfo fInfo in files)
                        {
                            using (FileStream fileStream = fInfo.Open(FileMode.Open))
                            {
                                try
                                {

                                    fileStream.Position = 0;
                                    byte[] hashValue = mySHA256.ComputeHash(fileStream);

                                    textBox1.Text = PrintByteArray(hashValue);
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine($"I/O Exception: {ex.Message}");
                                }
                                catch (UnauthorizedAccessException ex)
                                {
                                    Console.WriteLine($"Access Exception: {ex.Message}");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The directory specified could not be found.");
                }
            }
            catch (Exception ex)
            {

                textBox1.Text = $"Error: {ex.Message}";
            }
          
        }

        public static string PrintByteArray(byte[] array)
        {
            string sha = "";
            for (int i = 0; i < array.Length; i++)
            {
              
                sha = sha + $"{array[i]:X2}";
             
                
            }

            return sha;
        }

    }
}
