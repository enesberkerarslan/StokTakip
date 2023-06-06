using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;
using System.Web.Security;
using StokTakip.Security;

namespace StokTakip.Controllers
{

    public class AnaSayfaController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();

        // GET: AnaSayfa

        [MyAuthorization(Roles = "A,Y,P")]
        public ActionResult AnaSayfaIndex()
        {
            var user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            Siparis siparis = s.Siparis.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
            ViewBag.tip = user.Tip;
            
                ViewBag.siparisler = s.Siparis.ToList();
                ViewBag.KullaniciID = user.KullaniciID;
                ViewBag.sıralama = 1;
            


            ViewBag.magaza = s.Magazalar.ToList();
            ViewBag.kullanici = s.Personeller.ToList();
            ViewBag.user = user.PersonelID;
            return View();
        }

        public ActionResult MenuKategori()
        {
            List<Kategori> liste = s.Kategori.ToList();

            return PartialView("MenuKategori", liste);
        }
        [AllowAnonymous]

        public ActionResult Login()
        {

            return View();       
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Kullanıcı k)
        {
            StokTakipEntities s = new StokTakipEntities();
            Kullanıcı user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == k.KullaniciAdi && x.KullaniciSifre == k.KullaniciSifre);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.KullaniciAdi, false);
                return RedirectToAction("AnasayfaIndex", "Anasayfa");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Şifre hatalı";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            string name = FormsAuthentication.FormsCookieName;
            HttpCookie authcookie = HttpContext.Request.Cookies[name];
            FormsAuthenticationTicket t = FormsAuthentication.Decrypt(authcookie.Value);
            string tname = t.Name;

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
        public ActionResult hata()
        {
            return View();
        }

        [MyAuthorization(Roles = "A,Y,P")]

        public ActionResult SiparisOnayla(int id)
        {
            var sepet = s.Siparis.FirstOrDefault(x => x.SiparisID == id);
            sepet.Onay = "Siparişiniz Hazırlanıyor";
            s.SaveChanges();
            return RedirectToAction("AnasayfaIndex");
        }
        public ActionResult KargoOnayla(int id)
        {
            var sepet = s.Siparis.FirstOrDefault(x => x.SiparisID == id);
            sepet.Onay = "Kargoya Verildi";
            s.SaveChanges();
            return RedirectToAction("AnasayfaIndex");
        }
    }
}