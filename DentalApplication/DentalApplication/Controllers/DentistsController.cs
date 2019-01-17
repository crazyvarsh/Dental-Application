using DentalApplication.DAL;
using SampleApplication.CustomExceptions;
using SampleApplication.Models;
using SampleApplication.Repositories;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SampleApplication.Controllers
{
    [Authorize]
    [RoutePrefix("api/dentists")]
    public class DentistsController : ApiController
    {
        private DentalAppContext db = new DentalAppContext();
        private IDentistRepository dentistRepo;
        public DentistsController()
        {

        }
        public DentistsController(IDentistRepository repository)
        {
            this.dentistRepo = repository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetDentists()
        {
            try
            {
                var dentists = dentistRepo.GetAllDentists();
                return Ok(dentists);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetDentist(int id)
        {
            try
            {
                Dentist result = dentistRepo.GetDentist(id);
                if (result == null)
                {
                    string message = string.Format("Dentist with id {0} not found", id);
                    return Content(HttpStatusCode.NotFound, message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Modify(int id, Dentist dentist)
        {
            if (dentist == null || dentist.ID <= 0)
                return BadRequest();
            if (id != dentist.ID)
                return BadRequest("ID mismatched");
            // Check for Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    dentistRepo.ModifyDentist(dentist);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DentistExists(id))
                    {
                        return Content(HttpStatusCode.NotFound, string.Format("Dentist with id {0} not found", dentist.ID));
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Post(Dentist dentist)
        {
            if (dentist == null)
                return BadRequest();

            // Check for Model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    dentist = dentistRepo.AddDentist(dentist);
                    if (dentist == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return CreatedAtRoute("", new { id = dentist.ID }, dentist);
                    }
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteDentist(int id)
        {
            try
            {
                dentistRepo.DeleteDentist(id);
                return Content(HttpStatusCode.OK, string.Format("Dentist with id {0} deleted successfully.", id));
            }
            catch (DentistNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id}/patients")]
        [HttpGet]
        public IHttpActionResult GetPatientsForDentist(int id)
        {
            try
            {
                if (!DentistExists(id))
                {
                    return Content(HttpStatusCode.NotFound, string.Format("Dentist with id {0} not found", id));
                }
                var result = (from d in db.Dentists
                              join p in db.Patients
                              on d.ID equals p.DentistID
                              where d.ID == id
                              select p);

                if (result == null || !result.Any())
                {
                    string message = string.Format("No patients under doctor with id {0}", id);
                    return Content(HttpStatusCode.NotFound, message);
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("Search")]
        [HttpGet]
        public IHttpActionResult SearchDentist([FromUri]Dentist dentistParams)
        {
            var dentists = db.Dentists.AsQueryable();
            if (dentistParams != null)
            {
                if (!string.IsNullOrWhiteSpace(dentistParams.FirstName))
                {
                    dentists = dentists.Where(p => p.FirstName.Equals(dentistParams.FirstName));
                }
                if (!string.IsNullOrWhiteSpace(dentistParams.LastName))
                {
                    dentists = dentists.Where(p => p.LastName.Equals(dentistParams.LastName));
                }
                if (!string.IsNullOrWhiteSpace(dentistParams.Email))
                {
                    dentists = dentists.Where(p => p.Email.Equals(dentistParams.Email));
                }
                if (dentistParams.ID > 0)
                {
                    dentists = dentists.Where(p => p.ID == dentistParams.ID);
                }
            }
            return Ok(dentists.ToList());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DentistExists(int id)
        {
            return db.Dentists.Any(e => e.ID == id);
        }
    }
}