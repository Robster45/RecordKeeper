using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

/// <summary>
/// AUTHOR: Robley Evans
/// PURPOSE: To load in the usernames and 
/// passwords so that people can login. It
/// will also be able to save updated or new
/// users into the files
/// </summary>
namespace RecordKeeperProgram
{
    class CredentialLoader
    {
        // streams and readers
        private BinaryReader binaryReader;
        private BinaryWriter binaryWriter;
        private const string path = @"C:\Record Keeper";
        private FileStream stream;
        // private ASCIIEncoding encoder;

        // Dictionary for usernames and such
        private Dictionary<string, string> users;

        // PROPERTIES
        public Dictionary<string, string> Users
        {
            get { return users; }
            set { users = value; }
        }

        // CONSTRUCTOR
        public CredentialLoader()
        {
            users = new Dictionary<string, string>();
            users.Add("ADMIN", "YELBOR3120");
        }

        // METHODS
        public void Start()
        {
            try
            {
                string credentialsPath = path + "\\Misc Data\\Credentials.data";

                if (Directory.Exists(path))
                {
                    binaryReader = new BinaryReader(stream);
                    stream = File.Open(credentialsPath, FileMode.OpenOrCreate);

                    int numUsers = binaryReader.ReadInt32();

                    for (int i = 0; i < numUsers; i++)
                    {
                        users.Add(binaryReader.ReadString(), binaryReader.ReadString());
                    }
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di = Directory.CreateDirectory(path + "\\Main Data");
                    di = Directory.CreateDirectory(path + "\\Misc Data");
                    File.Create(path + "\\Misc Data\\Credentials.data");

                    stream = File.Open(credentialsPath, FileMode.Open);
                    binaryWriter = new BinaryWriter(stream);

                    binaryWriter.Write(1);
                    binaryWriter.Write("ADMIN");
                    binaryWriter.Write("YELBOR3120");
                    binaryWriter.Close();

                    MessageBox.Show("Directory created!");
                }

                

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


    }
}
