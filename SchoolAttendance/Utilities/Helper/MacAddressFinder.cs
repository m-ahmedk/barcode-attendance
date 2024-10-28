using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace SchoolAttendance.Utilities.Helper
{
    public static class MacAddressFinder
    {
        public static string GetAddress()
        {
            string? macAddress = null;

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                macAddress = networkInterfaces
                        .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
                        .Select(nic => nic.GetPhysicalAddress().ToString())
                        .FirstOrDefault();
            
            if (string.IsNullOrEmpty(macAddress))
            {
                throw new Exception("No active network interfaces found.");
            }

            return macAddress;
        }
    }
}