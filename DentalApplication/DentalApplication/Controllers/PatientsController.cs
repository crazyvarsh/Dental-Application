using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DentalApplication.DAL;
using SampleApplication.CustomExceptions;
using SampleApplication.Models;
using SampleApplication.Repositories;

namespace SampleApplication.Controllers
{
    [Authorize]
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private DentalAppContext db = new DentalAppContext();
        private IPatientRepository patientRepo;
        public PatientsController()
        {

        }
        public PatientsController(IPatientRepository repository)
        {
            this.patientRepo = repository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPatients()
        {
            try
            {
                var patients = patientRepo.GetAllPatients();
                return Ok(patients.ToList());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetPatient(int id)
        {
            try
            {
                Patient result = patientRepo.GetPatient(id);
                if (result == null)
                {
                    string message = string.Format("Patient with id {0} not found", id);
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
        public IHttpActionResult Modify(int id, [FromBody]Patient Patient)
        {
            if (Patient == null || Patient.ID <= 0)
                return BadRequest();
            if (id != Patient.ID)
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
                    patientRepo.ModifyPatient(Patient);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(id))
                    {
                        return Content(HttpStatusCode.NotFound, string.Format("Patient with id {0} not found", Patient.ID));
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
        public IHttpActionResult Post(Patient Patient)
        {
            if (Patient == null)
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
                    Patient = patientRepo.AddPatient(Patient);
                    if (Patient == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return CreatedAtRoute("", new { id = Patient.ID }, Patient);
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
        public IHttpActionResult DeletePatient(int id)
        {
            try
            {
                patientRepo.DeletePatient(id);
                return Content(HttpStatusCode.OK, string.Format("Patient with id {0} deleted successfully.", id));
            }
            catch (PatientNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id}/dentist")]
        [HttpGet]
        public IHttpActionResult GetDentistForPatient(int id)
        {
            try
            {
                if (!PatientExists(id))
                {
                    return Content(HttpStatusCode.NotFound, string.Format("Patient with id {0} not found", id));
                }
                var result = (from d in db.Dentists
                              join p in db.Patients
                              on d.ID equals p.DentistID
                              where p.ID == id
                              select d);

                if (result == null || !result.Any())
                {
                    string message = string.Format("No doctor is assigned to the patient with id {0}", id);
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
        [HttpPost]
        public IHttpActionResult SearchPatient([FromBody]Patient patientParams)
        {
            var patients = db.Patients.AsQueryable();
            if (patientParams != null)
            {
                if (!string.IsNullOrWhiteSpace(patientParams.FirstName))
                {
                    patients = patients.Where(p => p.FirstName.Equals(patientParams.FirstName));
                }
                if (!string.IsNullOrWhiteSpace(patientParams.LastName))
                {
                    patients = patients.Where(p => p.LastName.Equals(patientParams.LastName));
                }
                if (!string.IsNullOrWhiteSpace(patientParams.Email))
                {
                    patients = patients.Where(p => p.Email.Equals(patientParams.Email));
                }
                if (!string.IsNullOrWhiteSpace(patientParams.EmergencyContactName))
                {
                    patients = patients.Where(p => p.EmergencyContactName.Equals(patientParams.EmergencyContactName));
                }
                if (!string.IsNullOrWhiteSpace(patientParams.EmergencyContactPhone))
                {
                    patients = patients.Where(p => p.EmergencyContactPhone.Equals(patientParams.EmergencyContactPhone));
                }
                if (patientParams.ID > 0)
                {
                    patients = patients.Where(p => p.ID == patientParams.ID);
                }
            }
            return Ok(patients.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Any(e => e.ID == id);
        }

    }
}