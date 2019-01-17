using SampleApplication.Models;
using System.Data.Entity;

namespace DentalApplication.DAL
{
    public class DentalAppContext : DbContext
    {
        public DentalAppContext() : base("DentalAppContext")
        {

        }

        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }

        public void MarkAsModified(Dentist dentist)
        {
            Entry(dentist).State = EntityState.Modified;
        }
        public void MarkAsDeleted(Dentist dentist)
        {
            Entry(dentist).State = EntityState.Deleted;
        }
        public void MarkAsModified(Patient patient)
        {
            Entry(patient).State = EntityState.Modified;
        }
        public void MarkAsDeleted(Patient patient)
        {
            Entry(patient).State = EntityState.Deleted;
        }
    }
}