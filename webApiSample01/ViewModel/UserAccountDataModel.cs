using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApiSample01.ViewModel
{
    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public string token { get; set; }
    }

    public class UpdateViewModel
    {
        public int userid { get; set; }
        public int userrole { get; set; }
        public string userfullname { get; set; }
    }

}
