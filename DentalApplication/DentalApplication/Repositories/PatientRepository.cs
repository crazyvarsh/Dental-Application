using DentalApplication.DAL;
using SampleApplication.CustomExceptions;
using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public PatientRepository()
        {

        }
        public Patient AddPatient(Patient patient)
        {
            using (var context = new DentalAppContext())
            {
                context.Patients.Add(patient);
                context.SaveChanges();
                return patient;
            }
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            using (var context = new DentalAppContext())
            {
                var patientsList = context.Patients;
                if (patientsList != null && patientsList.Any())
                {
                    return patientsList.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public Patient GetPatient(int patientId)
        {
            using (var context = new DentalAppContext())
            {
                return (from d in context.Patients
                        where d.ID == patientId
                        select d).FirstOrDefault();
            }
        }

        public void DeletePatient(int patientId)
        {
            using (var context = new DentalAppContext())
            {
                Patient patient = (from d in context.Patients
                                   where d.ID == patientId
                                   select d).FirstOrDefault();
                if (patient == null)
                {
                    throw new PatientNotFoundException();
                }
                context.MarkAsDeleted(patient);
                context.SaveChanges();
            }
        }

        public void ModifyPatient(Patient patient)
        {
            using (var context = new DentalAppContext())
            {
                try
                {
                    context.MarkAsModified(patient);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }
        }
    }
}