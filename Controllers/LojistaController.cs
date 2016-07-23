using Microsoft.AspNet.Identity.Owin;
using ProjectF2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace ProjectF2.Controllers
{
    [Authorize]
    public class LojistaController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ProjectF2DBContext db = new ProjectF2DBContext();

        public LojistaController()
        {
        }

        public LojistaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Lojista
        //[ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        // GET: Lojista/Registro
        [AllowAnonymous]
        public ActionResult Registro()
        {
            return View();
        }

        // POST: /Account/Registro
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(Lojista lojista)
        {
            var user = new ApplicationUser { UserName = lojista.Email, Email = lojista.Email };
            var result = await UserManager.CreateAsync(user, lojista.Senha);
            if (result.Succeeded)
            {
                var currentUser = UserManager.FindByName(user.UserName);

                var roleresult = UserManager.AddToRole(currentUser.Id, "Lojista");

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                //return RedirectToAction("Index", "Home");
            }
            AddErrors(result);

            lojista.UserId = UserManager.FindByName(lojista.Email).Id;
            db.Lojistas.Add(lojista);
            db.SaveChanges();

            //TODO: Enviar e-mail com a chave de confirmação
            return RedirectToAction("../Lojista/RegistroSucesso");
        }

        // GET: Lojista/RegistroSucesso
        //[ValidateAntiForgeryToken]
        public ActionResult RegistroSucesso()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
       

        // GET: Movies/Edit/5
        //[ValidateAntiForgeryToken]
        public ActionResult Editar()
        {
            if (ModelState.IsValid)
            { 
                string userId = User.Identity.GetUserId();
                Lojista lojista = (from l in db.Lojistas
                                  where l.UserId == userId
                                   select l).Single();           
                if (lojista == null)
                {
                    return HttpNotFound();
                }

                return View(lojista);
            }
            return View();
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "LojistaId,UserId,RazaoSocial,NomeFantasia,Email,Telefone,Senha,ConfirmacaoSenha,CNPJ")] Lojista lojista)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(lojista.UserId);
                user.UserName = lojista.Email;
                user.Email = lojista.Email;
                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    db.Entry(lojista).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("/Index");
                }
                AddErrors(result);
            }
            return View(lojista);
        }
 
    }
}