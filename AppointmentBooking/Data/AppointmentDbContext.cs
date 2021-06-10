using AppointmentBooking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {
        }

        public DbSet<InterviewerModel> Interviewer { get; set; }
        //public DbSet<IntervieweeModel> Interviewee { get; set; }
        public DbSet<BookedAppointment> Appointment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IntervieweeModel>().ToTable("Interviewer");
            modelBuilder.Entity<InterviewerModel>().ToTable("Interviewer");
            modelBuilder.Entity<BookedAppointment>().ToTable("Appointment");
        }

    }
}
