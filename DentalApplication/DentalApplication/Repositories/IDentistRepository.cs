using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.Repositories
{
    public interface IDentistRepository
    {
        /// <summary>
        /// Add new dentist in the database
        /// </summary>
        /// <param name="dentist"></param>
        Dentist AddDentist(Dentist dentist);
        /// <summary>
        /// Get dentist by id from database
        /// </summary>
        /// <param name="dentistId"></param>
        /// <returns></returns>
        Dentist GetDentist(int dentistId);

        /// <summary>
        /// Get all dentists from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Dentist> GetAllDentists();

        /// <summary>
        /// Deletes a dentist from database
        /// </summary>
        /// <param name="id"></param>
        void DeleteDentist(int id);
        /// <summary>
        /// Modify dentist in database
        /// </summary>
        /// <param name="dentist"></param>
        void ModifyDentist(Dentist dentist);
    }
}
