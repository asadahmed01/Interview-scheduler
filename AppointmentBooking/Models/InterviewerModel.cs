using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Models
{
    public class InterviewerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption
        .Identity)]
        public int ID { get; set; }
        [DisplayName ("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Slots Remaining")]
        public int NumberOfSlots { get; set; }

        public virtual ICollection<BookedAppointment> AppointmentList { get; set; }
    }

    
}
