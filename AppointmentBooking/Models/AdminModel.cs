using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Models
{
    public class AdminModel
    {
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
