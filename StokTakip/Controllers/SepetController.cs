using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;
using System.Web.Security;
namespace StokTakip.Controllers
{
    public class SepetController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();

        //GET: Sepet
        public ActionResult SepetIndex()

        {
            var user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            Sepet sepet = s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
           
            if(sepet !=null)
            {
                ViewBag.nakliye = s.Nakliyeciler.ToList();
                ViewBag.sepeturunler = s.SepetUrunler.ToList();
                ViewBag.sepetid = sepet.SepetID;
            }
            return View(sepet);
        }



        public ActionResult sepeteEkle(int id)
        {
            var user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            if ((s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID)) != null)
            {
                Urunler urun = s.Urunler.FirstOrDefault(x => x.UrunID == id);
                var sepet = s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
                var sepeturunler = s.SepetUrunler.FirstOrDefault(x => x.SepetID == sepet.SepetID && x.UrunID==urun.UrunID);
                if (sepeturunler != null)
                {
                    sepeturunler.Adet += 1;
                    sepet.ToplamFiyat += urun.Fiyat;
                    sepeturunler.ToplamUrunFiyat += urun.Fiyat;
                    s.SaveChanges();
                }
                else
                {
                    SepetUrunler se = new SepetUrunler();
                    se.Adet = 1;
                    
                    se.SepetID = sepet.SepetID;
                    se.UrunID = urun.UrunID;
                    se.ToplamUrunFiyat += urun.Fiyat;
                    sepet.ToplamFiyat += urun.Fiyat;
                    s.SepetUrunler.Add(se);
                    s.SaveChanges();

                }
            }

            else 
            {               
            SepetUrunler sepeturunler = new SepetUrunler();
            Sepet sepet = new Sepet();
            sepet.KullaniciID = user.KullaniciID;
            sepeturunler.Adet = 1;
            Urunler urun = s.Urunler.FirstOrDefault(x => x.UrunID == id);
            sepet.ToplamFiyat += urun.Fiyat;
            s.Sepet.Add(sepet);
            s.SaveChanges();
            sepet.KullaniciID = user.KullaniciID;
            sepeturunler.SepetID = sepet.SepetID;
            sepeturunler.UrunID = urun.UrunID; 
            sepeturunler.ToplamUrunFiyat+= urun.Fiyat;
            s.SepetUrunler.Add(sepeturunler);
            s.SaveChanges();              
            }
            Sepet sepet2 = s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);

            ViewBag.sepeturunler = s.SepetUrunler.ToList();
            ViewBag.sepetid = sepet2.SepetID;
            return View("SepetIndex",sepet2);
        }

        [HttpPost]
        public ActionResult siparisOlustur()
        {
            var user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            Sepet sepet = s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
            SepetUrunler sepeturun = s.SepetUrunler.FirstOrDefault(x => x.SepetID == sepet.SepetID);
            Siparis siparis = new Siparis();
            siparis.SiparisTarihi = DateTime.Now;
            siparis.SepetID = sepet.SepetID;
            siparis.Onay = "Onaylanmadı";
            siparis.SevkiyatTarihi = 1;
            siparis.KullaniciID = sepet.KullaniciID;
            s.Siparis.Add(siparis);
            sepet.KullaniciID = null;           
            s.SaveChanges();
            return View("SepetIndex");
        }

        public ActionResult sepettenKaldir(int id)
        {
            var user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name);
            Sepet sepet = s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
            SepetUrunler sepeturun = s.SepetUrunler.FirstOrDefault(x => x.SepetID == sepet.SepetID && x.UrunID==id);
            sepet.ToplamFiyat -= sepeturun.ToplamUrunFiyat;
            s.SepetUrunler.Remove(sepeturun);
            if(sepet.ToplamFiyat==0)
            {
                s.Sepet.Remove(sepet);
            }
            s.SaveChanges();
            Sepet sepett = s.Sepet.FirstOrDefault(x => x.KullaniciID == user.KullaniciID);
            if (sepett != null)
            {
                ViewBag.nakliye = s.Nakliyeciler.ToList();
                ViewBag.sepeturunler = s.SepetUrunler.ToList();
                ViewBag.sepetid = sepet.SepetID;
            }
            return View("SepetIndex",sepett);
        }


    }
}