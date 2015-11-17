using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using HackathonWebApi.Membership;
using System.Globalization;
using System.Web.Security;
using System.Net.Http.Headers;

namespace HackathonWebApi.Controllers
{
    public class MembershipController : ApiController
    {
        // GET: api/Membership
        public IEnumerable<string> Get()
        {
            return new string[] { "value33", "value2" };
        }

        // GET: api/Membership/5
        //Returns if user is logged on
        public bool Get(int id)
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public class Person
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // POST: api/Membership
        public bool Post([FromBody]Person user)
        {
            var membership = new CustomMembershipProvider();
            if (membership.ValidateUser(user.Username, user.Password))//if (System.Web.Security.Membership.ValidateUser(user.Username, user.Password))//WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                SetupFormsAuthTicket(user.Username, true);
                //return RedirectToLocal(returnUrl);
                return true;
            }
            return false;
        }

        private HackathonWebApi.Membership.User SetupFormsAuthTicket(string userName, bool persistanceFlag)
        {
            var usersContext = new UsersContext();
            HackathonWebApi.Membership.User user = usersContext.GetUser(userName);
            var userId = user.UserId;
            var userData = userId.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                userName, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration
                                persistanceFlag, //Persistent
                                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var response = Request.CreateResponse(HttpStatusCode.Created);


            //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            //var cookieHeaderValue = new CookieHeaderValue("loginCookie",);

            var cookie = new CookieHeaderValue("session-id", "12345");
            cookie.Cookies.Add(new CookieState(FormsAuthentication.FormsCookieName, encTicket));
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            response.Headers.AddCookies(new List<CookieHeaderValue> { cookie });
            return user;
        }

        //public void Post([FromBody]Person2 value)
        //{

        //}

        //public void Login([FromBody]string value)
        //{
        //}

        // PUT: api/Membership/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Membership/5
        public void Delete(int id)
        {
        }
    }
}
