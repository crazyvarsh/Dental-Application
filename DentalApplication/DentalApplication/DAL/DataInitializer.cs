using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalApplication.DAL
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DentalAppContext>
    {
        protected override void Seed(DentalAppContext context)
        {
            var dentists = new List<Dentist>
            {
            new Dentist{ FirstName = "Tim", LastName = "Davis", Email = "tim.davis@dentalcare.com" },
            new Dentist{ FirstName = "Sean", LastName = "Cayer", Email = "sean.cayer@dentalcare.com" },
            new Dentist{ FirstName = "Elizabeth", LastName = "Read", Email = "tim.davis@dentalcare.com" }
            };

            dentists.ForEach(s => context.Dentists.Add(s));
            context.SaveChanges();

            var patients = new List<Patient>
            {
                new Patient {FirstName = "Monica", LastName = "Perry", Email = "monica_perry@gmail.com", EmergencyContactName = "Matthew Perry", EmergencyContactPhone = "9124213563", DentistID = 1},
                new Patient {FirstName = "Jennifer", LastName = "Becker", Email = "jbsunshine@hotmail.com", EmergencyContactName = "Sandra Becker", EmergencyContactPhone = "5421428125"},
                new Patient {FirstName = "David", LastName = "Griggs", Email = "david.griggs@yahoo.com", EmergencyContactName = "Mary Griggs", EmergencyContactPhone = "8752310987"},
                new Patient {FirstName = "Matt", LastName = "Blanc", Email = "mblanc87@gmail.com", EmergencyContactName = "Scarlett Willis", EmergencyContactPhone = "7651230986"},
                new Patient {FirstName = "Joe", LastName = "Johnson", Email = "joebuddy@gmail.com", EmergencyContactName = "Ariana Johnson", EmergencyContactPhone = "5413459870"},
                new Patient {FirstName = "Taylor", LastName = "Alderson", Email = "t.alderson@yahoo.com", EmergencyContactName = "Shawn Alderson", EmergencyContactPhone = "3126448743"},
                new Patient {FirstName = "Sara", LastName = "Cooper", Email = "sara_cooper@yahoo.com", EmergencyContactName = "Mary Cooper", EmergencyContactPhone = "8907530987"},
                new Patient {FirstName = "Chris", LastName = "Valencia", Email = "chr1s.v3@hotmail.com", EmergencyContactName = "Harry Valencia", EmergencyContactPhone = "6539861230"},
                new Patient {FirstName = "Aaron", LastName = "Brookshire", Email = "gr8aaron@hotmail.com", EmergencyContactName = "Amy Brookshire", EmergencyContactPhone = "7412093127"},
                new Patient {FirstName = "Penny", LastName = "Barker", Email = "pennybarker@yahoo.com", EmergencyContactName = "Ethen Barker", EmergencyContactPhone = "2538454613"},
                new Patient {FirstName = "Thomas", LastName = "Goldner", Email = "tgoldner@gmail.com", EmergencyContactName = "Tracey Goldner", EmergencyContactPhone = "4590128639"}
            };
            patients.ForEach(s => context.Patients.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}