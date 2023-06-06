using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Security;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class SiparislerController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();
        // GET: Siparisler
        [MyAuthorization(Roles = "A,Y,P")]
        public ActionResult SiparislerIndex()
        {
            var user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            Siparis siparis = s.Siparis.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
            if(siparis !=null)
            {
                ViewBag.siparisler = s.Siparis.ToList();
                ViewBag.KullaniciID = user.KullaniciID;
                ViewBag.sıralama = 1;
            }
            return View(siparis);
        }

        [MyAuthorization(Roles = "A,Y,P")]  
        public ActionResult SiparisDetay(int id)
        {
            var sepet = s.Siparis.FirstOrDefault(x => x.SepetID == id);
            ViewBag.sepetid = sepet.SepetID;
            ViewBag.sepettekiler = s.SepetUrunler.ToList();
            var topl = s.Sepet.FirstOrDefault(x => x.SepetID == id);
            ViewBag.ToplamFiyat = topl.ToplamFiyat;
            return View();
        }

        

    }
}