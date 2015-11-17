using System;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Web.Security;
using System.Web.Http;
using HackathonWebApi.Membership;
using HackathonWebApi.Models;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace HackathonWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Logon()
        {
            ViewBag.Title = "Logon Yo!";

            return View();
        }

        public ActionResult Cart()
        {
            ViewBag.Title = "My Cart";

            return View();
        }

        public ActionResult PurchaseHistory()
        {
            ViewBag.Title = "My Purchased Items";

            return View();
        }

        public ActionResult Checkout()
        {
            ViewBag.Title = "Cart Checkout";

            return View();
        }

        public bool IsUserAuthenticated()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

        public bool Login(Person user)
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

        public void EmptyCart()
        {
            var userId = GetUserId();

            if (userId != 0)
            {
                using (var entities = new AngularBootcampEntities())
                {
                    var remove = entities.UserCartItems.Where(x => x.UserId == userId);
                    entities.UserCartItems.RemoveRange(remove);

                    entities.SaveChanges();
                }
            }
        }
        


        public void UpdateCartItem(int productId, int count)
        {
            var userId = GetUserId();

            if (userId != 0)
            {
                using (var entities = new AngularBootcampEntities())
                {
                    var existingCartItem = entities.UserCartItems.FirstOrDefault(x => x.ProductId == productId);

                    if (existingCartItem != null)
                    {
                        if (count < 1)
                        {
                            entities.UserCartItems.Remove(existingCartItem);
                        }
                        else
                        {
                            existingCartItem.Count = count;
                        }
                    }

                    entities.SaveChanges();
                }
            }
        }

        public JsonResult PlaceOrder(OrderDetail customerOrder, List<Product> items)//List<UserPurchaseHistoryItem> items)
        {
            var success = true;
            List<UserPurchaseHistoryItem> purchaseItems = new List<UserPurchaseHistoryItem>();
            
            try
            {
                var userId = GetUserId();

                if (userId != 0)
                {
                    using (var entities = new AngularBootcampEntities())
                    {
                        customerOrder.UserId = userId;
                        var user = entities.Users.FirstOrDefault(x => x.Id == userId);

                        entities.OrderDetails.Add(customerOrder);
                        
                        UserPurchaseHistoryItem item;
                        items.ForEach(x =>
                        {
                            item = new UserPurchaseHistoryItem
                            {
                                Date = DateTime.Now,
                                UserId = userId,
                                OrderId = customerOrder.OrderId,
                                ProductId = x.ProductId,
                                Name = x.Name,
                                Price = x.Price,
                                OrderDetail = customerOrder,
                                User = user,
                            };
                            for(int i=0; i<x.Count; i++)
                            {
                                purchaseItems.Add(item);
                            }
                        });

                        entities.UserPurchaseHistoryItems.AddRange(purchaseItems);

                        entities.SaveChanges();
                    }
                }
            }
            catch
            {
                success = false;
            }

            return new JsonResult { Data = success };
        }

        public int AddToCart(int productId, string name, decimal price)
        {
            var userId = GetUserId();

            if (userId != 0)
            {
                using (var entities = new AngularBootcampEntities())
                {
                    var existingCartItem = entities.UserCartItems.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

                    if (existingCartItem == null)
                    {
                        UserCartItem cartItem = new UserCartItem
                        {
                            ProductId = productId,
                            UserId = userId,
                            ProductName = name,
                            Price = price,
                            Count = 1
                        };
                        entities.UserCartItems.Add(cartItem);
                    }
                    else
                    {
                        existingCartItem.Count++;
                    }

                    entities.SaveChanges();
                }
            }
            return userId;
        }

        public int RemoveFromCart(int productId)
        {
            var userId = GetUserId();

            if (userId != 0)
            {
                using (var entities = new AngularBootcampEntities())
                {
                    var existingCartItem = entities.UserCartItems.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

                    entities.UserCartItems.Remove(existingCartItem);

                    entities.SaveChanges();
                }
            }
            return userId;
        }

        private int GetUserId()
        {
            var userId = 0;
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if(authCookie != null)
            { 
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                userId = Convert.ToInt32(ticket.UserData);
            }
            return userId;
        }

        public JsonResult GetCartItems()
        {
            JsonResult result;
            List<Product> items;
            var userId = GetUserId();
            using (var model = new AngularBootcampEntities())
            {
                items = (from cart in model.UserCartItems
                            where cart.UserId == userId
                            select new Product
                            {
                                Id = cart.Id,
                                ProductId = cart.ProductId,
                                Name = cart.ProductName,
                                UserId = cart.UserId,
                                Price = cart.Price,
                                Count = cart.Count
                            }).ToList();
            }
            result = new JsonResult { Data = items };
            return result;
        }

        public JsonResult GetPurchaseHistoryItems()
        {
            JsonResult result;
            List<Product> items;
            var userId = GetUserId();
            using (var model = new AngularBootcampEntities())
            {
                items = (from purchases in model.UserPurchaseHistoryItems
                         where purchases.UserId == userId
                         select new Product
                         {
                             Id = purchases.Id,
                             ProductId = purchases.ProductId,
                             Name = purchases.Name,
                             UserId = purchases.UserId,
                             Price = purchases.Price,
                             Date = purchases.Date
                         }).ToList();

                items.ForEach(x => x.DateString = x.Date.ToString());
            }
            result = new JsonResult { Data = items };
            return result;
        }
        
        public class Person
        {
            public string Username { get; set; }
            public string Password { get; set; }
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
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return user;
        }
    }
}
