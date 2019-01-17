namespace SampleApplication.Migrations
{
    using SampleApplication.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DentalApplication.DAL.DentalAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DentalApplication.DAL.DentalAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Dentists.AddOrUpdate(v => v.ID, new[]
            {
            new Dentist{ FirstName = "Tim", LastName = "Davis", Email = "tim.davis@dentalcare.com" },
            new Dentist{ FirstName = "Sean", LastName = "Cayer", Email = "sean.cayer@dentalcare.com" },
            new Dentist{ FirstName = "Elizabeth", LastName = "Read", Email = "lizzyread@dentalcare.com" }
            });
            context.SaveChanges();

            context.Patients.AddOrUpdate(v => v.ID, new[]
            {
                new Patient {FirstName = "Monica", LastName = "Perry", Email = "monica_perry@gmail.com", EmergencyContactName = "Matthew Perry", EmergencyContactPhone = "9124213563", DentistID = 1},
                new Patient {FirstName = "Jennifer", LastName = "Becker", Email = "jbsunshine@hotmail.com", EmergencyContactName = "Sandra Becker", EmergencyContactPhone = "5421428125", DentistID = 1},
                new Patient {FirstName = "David", LastName = "Griggs", Email = "david.griggs@yahoo.com", EmergencyContactName = "Mary Griggs", EmergencyContactPhone = "8752310987", DentistID = 1},
                new Patient {FirstName = "Matt", LastName = "Blanc", Email = "mblanc87@gmail.com", EmergencyContactName = "Scarlett Willis", EmergencyContactPhone = "7651230986", DentistID = 1},
                new Patient {FirstName = "Joe", LastName = "Johnson", Email = "joebuddy@gmail.com", EmergencyContactName = "Ariana Johnson", EmergencyContactPhone = "5413459870", DentistID = 2},
                new Patient {FirstName = "Taylor", LastName = "Alderson", Email = "t.alderson@yahoo.com", EmergencyContactName = "Shawn Alderson", EmergencyContactPhone = "3126448743", DentistID = 2},
                new Patient {FirstName = "Sara", LastName = "Cooper", Email = "sara_cooper@yahoo.com", EmergencyContactName = "Mary Cooper", EmergencyContactPhone = "8907530987", DentistID = 2},
                new Patient {FirstName = "Chris", LastName = "Valencia", Email = "chr1s.v3@hotmail.com", EmergencyContactName = "Harry Valencia", EmergencyContactPhone = "6539861230", DentistID = 3},
                new Patient {FirstName = "Aaron", LastName = "Brookshire", Email = "gr8aaron@hotmail.com", EmergencyContactName = "Amy Brookshire", EmergencyContactPhone = "7412093127", DentistID = 3},
                new Patient {FirstName = "Penny", LastName = "Barker", Email = "pennybarker@yahoo.com", EmergencyContactName = "Ethen Barker", EmergencyContactPhone = "2538454613", DentistID = 3},
                new Patient {FirstName = "Thomas", LastName = "Goldner", Email = "tgoldner@gmail.com", EmergencyContactName = "Tracey Goldner", EmergencyContactPhone = "4590128639", DentistID = 3}
            });
            context.SaveChanges();
        }
    }
}
