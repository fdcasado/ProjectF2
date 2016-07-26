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
    [Authorize(Roles ="Usuario")]
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
            string userId = User.Identity.GetUserId();

            var q = from p in db.Pedidos
                    join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                    where p.UserId == userId && p.Status != "Cancelado"
                    orderby p.DataHora descending
                    select new ViewPedido
                    {
                        PedidoId = p.PedidoId,
                        Data = p.DataHora,
                        NomeModelo = mod.NomeModelo,
                        DescricaoPedido = p.DescricaoPedido,
                        QtRespostasPendentes = 0,
                        QtRespostasRecebidas = 0,
                        Status = p.Status
                    };

            IList<ViewPedido> pedidos = q.ToList();


            return View(new ListaPedidos
            {
                ListagemPedidos = pedidos
            });
        }

        // GET: Usuario/RegistroSucesso
        public ActionResult RegistroSucesso()
        {
            return View();
        }

        public ActionResult CancelarPedido(int pedidoId)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                Pedido pedido = (from p in db.Pedidos
                                 where p.PedidoId == pedidoId && p.UserId==userId                                 
                                 select p).SingleOrDefault();

                if (pedido == null)
                {
                    return HttpNotFound();
                }

                pedido.Status = "Cancelado";
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                
            }

            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult EditarPedido(int pedidoId)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();

                ViewPedido q = (from p in db.Pedidos
                        join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                        join mar in db.Marcas on mod.MarcaId equals mar.MarcaId
                        join tp in db.TiposPecas on p.TipoPecaId equals tp.TipoPecaId
                        where p.PedidoId == pedidoId && p.UserId == userId
                                select new ViewPedido
                        {
                            PedidoId = p.PedidoId,
                            Data = p.DataHora,
                            NomeModelo = mod.NomeModelo,
                            DescricaoPedido = p.DescricaoPedido,
                            QtRespostasPendentes = 0,
                            QtRespostasRecebidas = 0,
                            Status = p.Status,
                            AnoModelo = p.AnoModelo,
                            NomeMarca = mar.NomeMarca,
                            NomeTipoPeca = tp.NomeTipoPeca
                        }).SingleOrDefault();

                
                return View(q);

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPedido([Bind(Include = "PedidoId,MaisInfo")] ViewPedido viewPedido)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                Pedido pedido = (from p in db.Pedidos where p.PedidoId == viewPedido.PedidoId && p.UserId == userId select p).Single();
                pedido.DescricaoPedido += "<br>" + viewPedido.MaisInfo;
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerRespostas(int pedidoId)
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
                var currentUser = UserManager.FindByName(user.Email);

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
                                   select u).SingleOrDefault();
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

        //[ValidateAntiForgeryToken]
        public ActionResult NovoPedido()
        {
            ViewBag.ListaMarcas = db.Marcas; 
            ViewBag.TipoPecas =db.TiposPecas;
            GetYears();
            return View();
        }

        private void GetYears()
        {
            List<int> Years = new List<int>();
            int startYear = DateTime.Now.Year;

            for (int i = startYear; i >= startYear-20; i--)
            {
                Years.Add(i);
            }
            
            ViewBag.Years = Years;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult NovoPedido([Bind(Include = "ModeloId, AnoModelo, TipoPecaId, DescricaoPedido")] Pedido pedido)
        {
            //TODO: implementar transação

            pedido.UserId = User.Identity.GetUserId();
            pedido.DataHora = DateTime.Now;
            
            db.Pedidos.Add(pedido);
            db.SaveChanges();

            EnviarCotacaoLojistas(pedido);

            return RedirectToAction("PedidoEnviadoSucesso");
        }

        public ActionResult PedidoEnviadoSucesso()
        {
            return View();
        }

        private void EnviarCotacaoLojistas(Pedido pedido)
        {
            List<int> lojistas = SelecionarLojistas(pedido);

            foreach (int lojistaId in lojistas)
            {
                db.RespostasPedidos.Add(new RespostaPedidos
                {
                    PedidoId = pedido.PedidoId,
                    LojistaId = lojistaId,
                    DataHoraResposta = DateTime.Now,
                    StatusResposta = StatusResposta.Pendente,
                    MotivoNegarResposta = MotivoNegarResposta.Motivo_0
                });
                db.SaveChanges();
            }
        }

        private List<int> SelecionarLojistas(Pedido pedido)
        {
            //List<int> r = new List<int>();
            //r.Add(1);
            //r.Add(2);
            //r.Add(3);
            //return r;

            int marcaId = (from m in db.Modelos where m.ModeloId == pedido.ModeloId select m.MarcaId).SingleOrDefault();

            List<int> r = (from p in db.Assinaturas
                     where (p.Marca.Contains(marcaId + "#") || p.Marca == "*")
                         && (p.Modelo.Contains(pedido.ModeloId + "#") || p.Modelo == "*")
                         && (p.TipoPeca.Contains(pedido.TipoPecaId + "#") || p.TipoPeca == "*")
                     select p.LojistaId).ToList();


            return r;
        }

        public ActionResult FillModelos(int id)
        {
            var modelos = db.Modelos.Where(c => c.MarcaId == id);
            return Json(modelos, JsonRequestBehavior.AllowGet);
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