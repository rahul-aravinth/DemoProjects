using EmployeeManagementDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeManagementDemo.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = CheckIfAdmin(user);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.InvalidUser = "Invalid UserName or Password";
                }
            }
            return View();
        }

        private bool CheckIfAdmin(UserModel user)
        {
            string commandText = "SELECT * FROM AdminCredentialsTable WHERE Admin_Name = @AdminName";
            SqlDataReader dr;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@AdminName", SqlDbType.VarChar);
                command.Parameters["@AdminName"].Value = user.UserName;

                try
                {
                    connection.Open();
                    dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string encryptedpassword = dr["Admin_Password"].ToString();
                            byte[] cipherBytes = Convert.FromBase64String(encryptedpassword);
                            try
                            {
                                using (AesManaged aes = new AesManaged())
                                {
                                    aes.Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["AESKey"].ToString());
                                    aes.IV = new byte[16];
                                    string decryptedPassword = Decrypt(cipherBytes, aes.Key, aes.IV);
                                    if(decryptedPassword == user.Password)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                            catch (Exception exp)
                            {
                                //Log
                            }

                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Log
                }
            }
            return false;
        }

        private static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = string.Empty;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            plaintext = reader.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModelRegister user)
        {
            if (ModelState.IsValid)
            {
                if (user.RootPassword == ConfigurationManager.AppSettings["RootPassword"].ToString())
                {
                    CreateAdmin(user);
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    TempData["AdminCreated"] = "Admin User Created, Please Login";
                    return RedirectToAction("Login", "Home", TempData["AdminCreated"]);
                }
            }
            return View();
        }

        private void CreateAdmin(UserModelRegister user)
        {
            string commandText = "INSERT INTO AdminCredentialsTable(Admin_Name, Admin_Password) VALUES(@AdminName, @AdminPassword)";
            byte[] encryptedPassword = null;
            string encryptedPasswordString = String.Empty;
            try
            { 
                using (AesManaged aes = new AesManaged())
                {
                    aes.Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["AESKey"].ToString());
                    aes.IV = new byte[16];
                    encryptedPassword = Encrypt(user.Password, aes.Key, aes.IV);
                    encryptedPasswordString = Convert.ToBase64String(encryptedPassword, 0, encryptedPassword.Length);
                }
            }
            catch (Exception exp)
            {
                //Log
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@AdminName", SqlDbType.VarChar);
                command.Parameters["@AdminName"].Value = user.UserName;

                command.Parameters.Add("@AdminPassword", SqlDbType.VarChar);
                command.Parameters["@AdminPassword"].Value = encryptedPasswordString;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //Log
                }
            }
        }

        private static byte[] Encrypt(object pass, byte[] Key, byte[] IV)
        {
            byte[] encrypted;    
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(pass);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
    }
}