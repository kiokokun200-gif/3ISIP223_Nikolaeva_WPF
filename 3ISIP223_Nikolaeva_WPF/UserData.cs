using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF
{
    public static class UserData
    {
        public static User CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

    }
}
