using DentalApplication.DAL;
using SampleApplication.CustomExceptions;
using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.Repositories
{
    public class DentistRepository : IDentistRepository
    {
        public DentistRepository()
        {

        }
        public Dentist AddDentist(Dentist dentist)
        {
            using (var context = new DentalAppContext())
            {
                context.Dentists.Add(dentist);
                context.SaveChanges();
                return dentist;
            }
        }

        public IEnumerable<Dentist> GetAllDentists()
        {
            using (var context = new DentalAppContext())
            {
                var dentistList = context.Dentists;
                if (dentistList != null && dentistList.Any())
                {
                    return dentistList.ToList<Dentist>();
                }
                else
                {
                    return null;
                }
            }
        }

        public Dentist GetDentist(int dentistId)
        {
            using (var context = new DentalAppContext())
            {
                return (from d in context.Dentists
                        where d.ID == dentistId
                        select d).FirstOrDefault();
            }
        }

        public void DeleteDentist(int dentistId)
        {
            using (var context = new DentalAppContext())
            {
                Dentist dentist = (from d in context.Dentists
                                   where d.ID == dentistId
                                   select d).FirstOrDefault();
                if (dentist == null)
                {
                    throw new DentistNotFoundException();
                }
                context.MarkAsDeleted(dentist);
                context.SaveChanges();
            }
        }

        public void ModifyDentist(Dentist dentist)
        {
            using (var context = new DentalAppContext())
            {
                try
                {
                    context.MarkAsModified(dentist);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ////If exception thrown, check if the exception is thrown because of dentist non-existence
                    //Dentist originalDentist = (from d in context.Dentists
                    //                           where d.ID == dentist.ID
                    //                           select d).FirstOrDefault();
                    //if (originalDentist == null)
                    //{
                    //    throw new DentistNotFoundException();
                    //}
                    throw ex;
                }
            }
        }
    }
}