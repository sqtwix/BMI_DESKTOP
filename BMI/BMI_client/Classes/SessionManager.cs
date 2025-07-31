using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMI_client.Classes
{
    public static class SessionManager
    {
        public static string AccessToken { get; set; }
        public static string RefreshToken { get; set; }
        public static string UserName { get; set; }

    }
}
