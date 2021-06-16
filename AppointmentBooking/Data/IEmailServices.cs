using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Data
{
    public interface IEmailServices
    {
        void SendMail(string from, string to, string subject, string html);
    }
}
