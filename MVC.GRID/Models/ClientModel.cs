using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.GRID.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}