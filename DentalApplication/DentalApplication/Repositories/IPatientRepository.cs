using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.Repositories
{
    public interface IPatientRepository
    {
        /// <summary>
        /// Add new patient in the database
        /// </summary>
        /// <param name="patient"></param>
        Patient AddPatient(Patient patient);
        /// <summary>
        /// Get patient by id from database
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Patient GetPatient(int patientId);

        /// <summary>
        /// Get all Patients from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Patient> GetAllPatients();

        /// <summary>
        /// Deletes a Patient from database
        /// </summary>
        /// <param name="id"></param>
        void DeletePatient(int id);
        /// <summary>
        /// Modify Patient in database
        /// </summary>
        /// <param name="patient"></param>
        void ModifyPatient(Patient patient);
    }
}
