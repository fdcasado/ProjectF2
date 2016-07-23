using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectF2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectF2.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ProjectF2DBContext db = new ProjectF2DBContext();

        public UsuarioController()
        {
        }

        public UsuarioController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/RegistroSucesso
        public ActionResult RegistroSucesso()
        {
            return View();
        }



        // GET: /Usuario/Registro
        [AllowAnonymous]
        public ActionResult Registro()
        {
            return View();
        }


        // POST: /Usuario/Registro
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(Usuario usuario)
        {
            var user = new ApplicationUser { UserName = usuario.Email, Email = usuario.Email };
            var result = await UserManager.CreateAsync(user, usuario.Senha);
            if (result.Succeeded)
            {
                var currentUser = UserManager.FindByName(user.UserName);

                var roleresult = UserManager.AddToRole(currentUser.Id, "Usuario");

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                //return RedirectToAction("Index", "Home");
            }
            AddErrors(result);

            usuario.UserId = UserManager.FindByName(usuario.Email).Id;
            db.Usuarios.Add(usuario);
            db.SaveChanges();

            //TODO: Enviar e-mail com a chave de confirmação
            return RedirectToAction("RegistroSucesso");
        }

        // GET: Movies/Edit/5
        //[ValidateAntiForgeryToken]
        public ActionResult Editar()
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                Usuario usuario = (from u in db.Usuarios
                                   where u.UserId == userId
                                   select u).Single();
                if (usuario == null)
                {
                    return HttpNotFound();
                }

                return View(usuario);
            }
            return View();
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "UsuarioId,UserId,NomeCompleto,Email,Telefone,TelefoneVisivel,Senha,ConfirmacaoSenha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(usuario.UserId);
                user.UserName = usuario.Email;
                user.Email = usuario.Email;
                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("/Index");
                }
                AddErrors(result);
            }
            return View(usuario);
        }

        public ActionResult NovoPedido()
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
    }
}