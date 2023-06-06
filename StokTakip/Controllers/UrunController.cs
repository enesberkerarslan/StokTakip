using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;
using StokTakip.Security;

namespace StokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: urun
        StokTakipEntities s = new StokTakipEntities();

        [HttpGet]

        [MyAuthorization(Roles ="A")]

        public ActionResult UrunIndex()
        {
            List<Urunler> liste =s.Urunler.ToList();
                
            return View(liste);
        }
        [HttpPost]
        public String Sil(int id)
        {
            Urunler urun = s.Urunler.FirstOrDefault(x => x.UrunID == id);
            if (urun != null)
            {
                s.Urunler.Remove(urun);

                try
                {
                    s.SaveChanges();
                    return "başarılı";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }


            }
            return "başarısız";
        }
        public ActionResult urunEkle()
        {
            ViewBag.kategori = s.Kategori.ToList();



            return View();
        }


        [HttpPost]
        public ActionResult urunEkle(Urunler u)
        {

            Urunler urun = s.Urunler.FirstOrDefault(x => x.UrunID == u.UrunID);

            if (urun == null)
            {
                s.Urunler.Add(u);
            }
            else
            {
                urun.UrunAdi = u.UrunAdi;
                urun.StokMiktari = u.StokMiktari;
                urun.KategoriID = u.KategoriID;
                urun.Fiyat = u.Fiyat;
                urun.AnlıkkDurum = u.AnlıkkDurum;


            }
            s.SaveChanges();

            return RedirectToAction("UrunIndex");
        }


        public ActionResult guncelle(int id)
        {
            Urunler urun = s.Urunler.FirstOrDefault(x => x.UrunID == id);
            ViewBag.kategori = s.Kategori.ToList();
            
            return View("urunEkle",urun);

        }
    }
} 