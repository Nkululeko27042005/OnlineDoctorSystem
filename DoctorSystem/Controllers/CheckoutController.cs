using Stripe.BillingPortal;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using DoctorSystem.Models;


namespace WebApplication6.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout

        public ActionResult CheckOut()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            productList = new List<ProductEntity>
            {
                new ProductEntity
                {
                    product = "Appointment",
                    Price = 500,
                    quantity = 1
                },
            };

           var domain = "https://localhost:44309/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
               SuccessUrl = domain + $"Checkout/Success",
               CancelUrl = domain + $"Checkout/Failed",
               LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
               Mode = "payment",
            };

            foreach (var item in productList)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = item.Price * 100,
                        Currency = "zar",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.product,
                        },
                    },

                    Quantity = item.quantity,
                };

               
                options.LineItems.Add(sessionListItem);
            }

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);

            return new HttpStatusCodeResult(303);
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }
    }
}