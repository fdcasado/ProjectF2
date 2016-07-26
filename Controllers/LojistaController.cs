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

        public ActionResult HistoricoRespostas()
        {
            string userId = User.Identity.GetUserId();

            var q = from p in db.Pedidos
                    join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                    join rp in db.RespostasPedidos on p.PedidoId equals rp.PedidoId
                    join lj in db.Lojistas on rp.LojistaId equals lj.LojistaId
                    where p.Status != "Cancelado"
                        && lj.UserId == userId
                        && (rp.StatusResposta == StatusResposta.Responder || rp.StatusResposta == StatusResposta.NaoReponder)
                    orderby p.DataHora descending
                    select new ViewPedido
                    {
                        PedidoId = p.PedidoId,
                        Data = p.DataHora,
                        NomeModelo = mod.NomeModelo,
                        DescricaoPedido = p.DescricaoPedido,
                        RespostaId = rp.RespostaPedidosId
                    };

            IList<ViewPedido> pedidos = q.ToList();

            return View(new ListaPedidos
            {
                ListagemPedidos = pedidos
            });
        }


        // GET: Lojista
        //[ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var q = from p in db.Pedidos
                    join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                    join rp in db.RespostasPedidos on p.PedidoId equals rp.PedidoId
                    join lj in db.Lojistas on rp.LojistaId equals lj.LojistaId
                    where p.Status != "Cancelado" 
                        && lj.UserId == userId
                        && (rp.StatusResposta == StatusResposta.Pendente || rp.StatusResposta == StatusResposta.SolicitarMaisInfo)
                    orderby p.DataHora descending
                    select new ViewPedido
                    {
                        PedidoId = p.PedidoId,
                        Data = p.DataHora,
                        NomeModelo = mod.NomeModelo,
                        DescricaoPedido = p.DescricaoPedido,
                        RespostaId = rp.RespostaPedidosId
                    };

            IList<ViewPedido> pedidos = q.ToList();

            return View(new ListaPedidos
            {
                ListagemPedidos = pedidos
            });
        }

        public ActionResult DetalheResposta(int respostaId)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();

                ViewRespostaPedidos viewResposta = (from p in db.Pedidos
                                                    join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                                                    join mar in db.Marcas on mod.MarcaId equals mar.MarcaId
                                                    join tp in db.TiposPecas on p.TipoPecaId equals tp.TipoPecaId
                                                    join rp in db.RespostasPedidos on p.PedidoId equals rp.PedidoId
                                                    join lj in db.Lojistas on rp.LojistaId equals lj.LojistaId
                                                    where rp.RespostaPedidosId == respostaId
                                                        && lj.UserId == userId
                                                    //&& (rp.StatusResposta == StatusResposta.Pendente || rp.StatusResposta == StatusResposta.SolicitarMaisInfo)
                                                    select new ViewRespostaPedidos
                                                    {
                                                        RespostaPedidosId = rp.RespostaPedidosId,
                                                        PedidoId = p.PedidoId,
                                                        NomeModelo = mod.NomeModelo,
                                                        DescricaoPedido = p.DescricaoPedido,
                                                        AnoModelo = p.AnoModelo,
                                                        NomeMarca = mar.NomeMarca,
                                                        NomeTipoPeca = tp.NomeTipoPeca,
                                                        StatusResposta = rp.StatusResposta,
                                                        MotivoNegarResposta = rp.MotivoNegarResposta,
                                                        DescSolicitarMaisInfo = rp.DescSolicitarMaisInfo,
                                                        Resposta = rp.Resposta,
                                                        CondicoesPagamento = rp.CondicoesPagamento,
                                                        CondicoesEntrega = rp.CondicoesEntrega,
                                                    }).SingleOrDefault();

                if (viewResposta == null)
                {
                    return HttpNotFound();
                }

                return View(viewResposta);
            }


            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResponderPedido(ViewRespostaPedidos dados)
        {
            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {                
                RespostaPedidos rp = (from p in db.RespostasPedidos
                                      join l in db.Lojistas on p.LojistaId equals l.LojistaId
                                 where p.RespostaPedidosId == dados.RespostaPedidosId && l.UserId == userId
                                 select p).SingleOrDefault();

                rp.RespostaPedidosId = dados.RespostaPedidosId;
                rp.CondicoesEntrega = dados.CondicoesEntrega;
                rp.CondicoesPagamento = dados.CondicoesPagamento;
                rp.DataHoraResposta = DateTime.Now;
                rp.DescSolicitarMaisInfo = dados.DescSolicitarMaisInfo;
                rp.MotivoNegarResposta = dados.MotivoNegarResposta;
                rp.Resposta = dados.Resposta;
                rp.StatusResposta = dados.StatusResposta;

                db.Entry(rp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("/Index");
            }

            ViewRespostaPedidos viewResposta = (from p in db.Pedidos
                                                join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                                                join mar in db.Marcas on mod.MarcaId equals mar.MarcaId
                                                join tp in db.TiposPecas on p.TipoPecaId equals tp.TipoPecaId
                                                join rp in db.RespostasPedidos on p.PedidoId equals rp.PedidoId
                                                join lj in db.Lojistas on rp.LojistaId equals lj.LojistaId
                                                where rp.RespostaPedidosId == dados.RespostaPedidosId
                                                    && lj.UserId == userId
                                                    && (rp.StatusResposta == StatusResposta.Pendente || rp.StatusResposta == StatusResposta.SolicitarMaisInfo)
                                                select new ViewRespostaPedidos
                                                {
                                                    RespostaPedidosId = rp.RespostaPedidosId,
                                                    PedidoId = p.PedidoId,
                                                    NomeModelo = mod.NomeModelo,
                                                    DescricaoPedido = p.DescricaoPedido,
                                                    AnoModelo = p.AnoModelo,
                                                    NomeMarca = mar.NomeMarca,
                                                    NomeTipoPeca = tp.NomeTipoPeca,
                                                    StatusResposta = StatusResposta.Responder
                                                }).SingleOrDefault();

            if (viewResposta == null)
            {
                return HttpNotFound();
            }

            return View(viewResposta);
        }
            
        public ActionResult ResponderPedido(int respostaId)
        {
            if (ModelState.IsValid)
            {
                /*
                var listAcaoResposta = new SelectList(new[]
                {
                    new { ID = "1", Name = "Responder" },
                    new { ID = "2", Name = "Não Responder" },
                    new { ID = "3", Name = "Solicitar mais Info" },
                },
                    "ID", "Name", 1);
            
                var listMotivoNaoResposta = new SelectList(new[]
                {
                    new { ID = "1", Name = "Motivo 1" },
                    new { ID = "2", Name = "Motivo 2" },
                    new { ID = "3", Name = "Motivo 3" },
                }, "ID", "Name");

                ViewData["listMotivoNaoResposta"] = listMotivoNaoResposta;
                ViewData["listaAcaoResposta"] = listAcaoResposta;
                */

                string userId = User.Identity.GetUserId();

                ViewRespostaPedidos viewResposta = (from p in db.Pedidos
                                join mod in db.Modelos on p.ModeloId equals mod.ModeloId
                                join mar in db.Marcas on mod.MarcaId equals mar.MarcaId
                                join tp in db.TiposPecas on p.TipoPecaId equals tp.TipoPecaId
                                join rp in db.RespostasPedidos on p.PedidoId equals rp.PedidoId
                                join lj in db.Lojistas on rp.LojistaId equals lj.LojistaId
                                where rp.RespostaPedidosId == respostaId 
                                    && lj.UserId == userId
                                    && (rp.StatusResposta == StatusResposta.Pendente || rp.StatusResposta == StatusResposta.SolicitarMaisInfo)
                                select new ViewRespostaPedidos
                                {
                                    RespostaPedidosId = rp.RespostaPedidosId,
                                    PedidoId = p.PedidoId,
                                    NomeModelo = mod.NomeModelo,
                                    DescricaoPedido = p.DescricaoPedido,
                                    AnoModelo = p.AnoModelo,
                                    NomeMarca = mar.NomeMarca,
                                    NomeTipoPeca = tp.NomeTipoPeca,
                                    StatusResposta = StatusResposta.Responder
                                }).SingleOrDefault();

                if (viewResposta == null)
                {
                    return HttpNotFound();
                }

                return View(viewResposta);
            }


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