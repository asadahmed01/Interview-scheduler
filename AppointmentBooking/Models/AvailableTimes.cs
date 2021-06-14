using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Models
{
    public class AvailableTimes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption
        .Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public int InterviewerID { get; set; }
        public virtual InterviewerModel Interviewer { get; set; }
       
    }
}
