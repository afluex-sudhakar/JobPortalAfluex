using System;
using System.Net;

namespace Utility
{
    public static class Util
    {
        public static string GenerateOTP()
        {
            Random rnd = new Random();
            return (rnd.Next(100000, 999999)).ToString();
        }
        public static string GenerateAlphanumeric(int Size)
        {
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[Size];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            return new String(stringChars1);
        }

        public static string GetIPAddress()
        {
            string IPAddress = "";
            try
            {
                IPHostEntry Host = default(IPHostEntry);
                string Hostname = null;
                Hostname = System.Environment.MachineName;
                Host = Dns.GetHostEntry(Hostname);
                foreach (IPAddress IP in Host.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IPAddress = Convert.ToString(IP);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return IPAddress;
        }

        public static string GetDomainName()
        {
            try
            {
                return System.Environment.UserDomainName;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string GetOSVersion()
        {
            try
            {
                return System.Environment.OSVersion.ToString();
            }
            catch (Exception ex)
            {

                return "";
            }
        }

    }
}
