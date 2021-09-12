using System;
using System.Collections.Generic;
using System.Text;

namespace AxolonTask.Table
{
  public  class RegUserTable
    {
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
