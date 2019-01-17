using DentalApplication.DAL;
using SampleApplication.Models;
using SampleApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleApplication.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private DentalAppContext db = new DentalAppContext();
        private AuthRepository authRepository = new AuthRepository();

        public AccountController() { }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IHttpActionResult Register(User user)
        {
            if (user == null)
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
                    user = authRepository.RegisterUser(user);
                    if (user == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return CreatedAtRoute("", new { id = user.ID }, user);
                    }
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoginAsync(User userModel)
        {
            if (!string.IsNullOrEmpty(userModel.UserName) && !string.IsNullOrEmpty(userModel.Password))
            {
                var user = authRepository.FindUser(userModel.UserName, userModel.Password);
                if (user != null)
                {
                    // Invoke the "token" OWIN service to perform the login (POST /api/token)
                    using (var client = new HttpClient())
                    {
                        var requestParams = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", userModel.UserName),
                            new KeyValuePair<string, string>("password", userModel.Password)
                        };
                        var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                        string absoluteUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                        var tokenServiceResponse = await client.PostAsync(absoluteUrl + "/Token", requestParamsFormUrlEncoded);

                        return this.ResponseMessage(tokenServiceResponse);
                    }
                }
                else
                {
                    return Content(HttpStatusCode.Unauthorized, "Invalid username/password");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Missing username/password");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}