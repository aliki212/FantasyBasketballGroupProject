using FantasyEuroleague.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using Microsoft.Owin.Security;

namespace FantasyEuroleague.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext context;

        public UserController()
        {
            context = new ApplicationDbContext();
        }


        [Authorize]

        public ActionResult Edit(string Id)
        {

            var userId = User.Identity.GetUserId();
            var user = context.UserAccounts
                .Include(u => u.Wallet)
                .SingleOrDefault(u => u.Id == Id);

            var viewModel = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                IdentityPapersNumber = user.IdentityPapersNumber,
                PrimaryResidence = user.PrimaryResidence,
                Street = user.Street,
                Town = user.Town,
                PostalCode = user.PostalCode,
                Wallet = user.Wallet.Amount
            };

            return View("Details", viewModel);
        }

        //KOSTAS
        /// /// ///
         //POST Update
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(IndexViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = context.UserAccounts
                .Include(u => u.Wallet)
                .Single(u => u.Id == userId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.IdentityPapersNumber = model.IdentityPapersNumber;
            user.PrimaryResidence = model.PrimaryResidence;
            user.Street = model.Street;
            user.Town = model.Town;
            user.PostalCode = model.PostalCode;
            user.Wallet.Amount = model.Wallet;


            context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
        ////Temp


        //public ActionResult LogOut2()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Clear();
        //    Session.RemoveAll();
        //    Session.Abandon();
        //    return RedirectToAction("Index", "Home");
        //}


        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");

        //}
        ////Temp

        //public ActionResult Delete()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var user = context.UserAccounts
        //        .Include(u => u.Wallet)
        //        .SingleOrDefault(u => u.Id == userId);

        //    //var wallet = context.Wallets
        //    //    .SingleOrDefault(w => w.Id == user.WalletId);

        //    context.UserAccounts.Remove(user);
        //    //context.Wallets.Remove(wallet);
        //    context.SaveChanges();
        //    LogOff();


        //    return RedirectToAction("Index", "Home");
        //}


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}