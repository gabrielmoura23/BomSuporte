using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PagedList;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using ProjetoBomNegocio.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace ProjetoBomNegocio.Controllers
{
    public class SuporteController : Controller
    {

        private DB_BomSuporteContext db2 = new DB_BomSuporteContext();
        

        // GET: Suporte
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Solicitar()
        {
            return View();
        }



        // POST: Tab_Suporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Solicitar([Bind(Include = "descricao,sistema_operacional,problema_recorrente,prioridade,email,ddd_telefone,telefone,melhor_horario,status,flg_termo_aceito,data_abertura,idusuario_cadastro")] ProjetoBomNegocio.Models.Tab_Suporte tab_Suporte)
        {
            if (ModelState.IsValid)
            {
                if (!tab_Suporte.flg_termo_aceito)
                    ModelState.AddModelError(string.Empty, "É Necessário concordar com os termos de suporte remoto.");
                else
                {
                    try
                    {
                        db2.Suportes.Add(tab_Suporte);
                        db2.SaveChanges();
                    }catch
                    {
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro. Favor tentar novamente.");
                        return View(tab_Suporte);
                    }

                    return RedirectToAction("Acompanhar");
                }
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Ocorreu um erro.");
            }

            return View(tab_Suporte);
        }


        public ActionResult Detalhes()
        {
            return PartialView();
        }

        public ActionResult Detalhes2(int id = 0)
        {
            ProjetoBomNegocio.Models.Tab_Suporte model = db2.Suportes.FirstOrDefault(t => t.idsuporte == id);
            return PartialView(model);
        }

        public ActionResult Cancelar2(int id = 0)
        {
            ProjetoBomNegocio.Models.Tab_Suporte model = db2.Suportes.FirstOrDefault(t => t.idsuporte == id);

            model.status = "Cancelado";
            model.data_alteracao = DateTime.Now;
            model.data_fechamento = DateTime.Now;
            model.idusuario_alteracao = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db2.Entry(model).State = EntityState.Modified;
                db2.SaveChanges();
                //return RedirectToAction("Acompanhar");
            }
            return View("Acompanhar");
        }

        public ActionResult Acompanhar(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Código" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var tab = db2.Suportes.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                tab = tab.Where(s => s.descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Descrição":
                    tab = tab.OrderBy(s => s.descricao);
                    break;
                case "Slug":
                    tab = tab.OrderBy(s => s.descricao);
                    break;
                default:
                    tab = tab.OrderBy(s => s.descricao);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(tab.ToPagedList(pageNumber, pageSize));
        }




        [HttpPost]
        public JsonResult MinhaAction(FormCollection formCollection)
        {
            
            // funciona 1

            /*
            return Json(new
            {
                aaData = new[] 
            {
                new [] { "aaaaa", "a1", "a11" },
                new [] { "bbbbb", "b2", "b2222" },
                new [] { "cccc", "c3", "c3333"}
            }
            }, JsonRequestBehavior.AllowGet);
            */



            // funciona 2

            /*
            var aaData = new[] 
            {
                new [] { "aaaaa", "a1", "a11" },
                new [] { "bbbbb", "b2", "b2222" },
                new [] { "cccc", "c3", "c3333"}
            };

            

            return Json(new { aaData } , JsonRequestBehavior.AllowGet);
            
            */



            var aaData = db2.Suportes.ToList();
            return Json(new { aaData }, JsonRequestBehavior.AllowGet);

            /*
            
            List<Tab_Suporte> clientesDefaults = new List<Tab_Suporte>();
            clientesDefaults.Add(new Tab_Suporte() { idsuporte = 1, descricao = "Maria Ferreita", status = "Feminino" });

            JavaScriptSerializer jso = new JavaScriptSerializer();
            string res = jso.Serialize(clientesDefaults);

            return Json(res, JsonRequestBehavior.AllowGet);
            */
        }









        /*

        public ActionResult IPN()
        {

            var order = new Order(); // this is something I have defined in order to save the order in the database

            // Receive IPN request from PayPal and parse all the variables returned
            var formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-synch"); //notify-synch_notify-validate
            formVals.Add("at", "this is a long token found in Buyers account"); // this has to be adjusted
            formVals.Add("tx", Request["tx"]);

            // if you want to use the PayPal sandbox change this from false to true
            string response = GetPayPalResponse(formVals, false);

            if (response.Contains("SUCCESS"))
            {
                string transactionID = GetPDTValue(response, "txn_id"); // txn_id //d
                string sAmountPaid = GetPDTValue(response, "mc_gross"); // d
                string deviceID = GetPDTValue(response, "custom"); // d
                string payerEmail = GetPDTValue(response, "payer_email"); // d
                string Item = GetPDTValue(response, "item_name");

                //validate the order
                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out amountPaid);

                if (amountPaid == 9)  // you might want to have a bigger than or equal to sign here!
                {
                    if (orders.Count(d => d.PayPalOrderRef == transactionID) < 1)
                    {
                        //if the transactionID is not found in the database, add it
                        //then, add the additional features to the user account
                    }
                    else
                    {
                        //if we are here, the user must have already used the transaction ID for an account
                        //you might want to show the details of the order, but do not upgrade it!
                    }
                    // take the information returned and store this into a subscription table
                    // this is where you would update your database with the details of the tran

                    //return View();

                }
                else
                {
                    // let fail - this is the IPN so there is no viewer
                    // you may want to log something here
                    order.Comments = "User did not pay the right ammount.";

                    // since the user did not pay the right amount, we still want to log that for future reference.

                    _db.Orders.Add(order); // order is your new Order
                    _db.SaveChanges();
                }

            }
            else
            {
                //error
            }
            return View();
        }

        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
        string GetPDTValue(string pdt, string key)
        {

            string[] keys = pdt.Split('\n');
            string thisVal = "";
            string thisKey = "";
            foreach (string s in keys)
            {
                string[] bits = s.Split('=');
                if (bits.Length > 1)
                {
                    thisVal = bits[1];
                    thisKey = bits[0];
                    if (thisKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                        break;
                }
            }
            return thisVal;

        }
        */


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db2.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}