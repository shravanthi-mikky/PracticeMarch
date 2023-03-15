using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminModel
    {
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminMobile { get; set; }
        public string Role { get; set; }
    }

    public class AdminLoginModel
    {
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }
}
