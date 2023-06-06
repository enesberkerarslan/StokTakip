using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class MagazaController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();

        // GET: Magaza
        public ActionResult MagazaIndex()
        {
            List<Magazalar> liste = s.Magazalar.ToList();
            ViewBag.magazaturleri = s.MagazaTurleri.ToList();
            ViewBag.adres = s.Adres.ToList();

            ViewBag.adres = s.Adres.ToList();
            return View(liste);
        }
        
        public ActionResult magazaEkle()
        {
            ViewBag.magazaturleri = s.MagazaTurleri.ToList();

            return View();
        }


        [HttpPost]
        public ActionResult magazaEkle(Magazalar u,Adres a)
        {
            Magazalar magaza = s.Magazalar.FirstOrDefault(x => x.MagazaID == u.MagazaID);

            if (magaza == null)
            {
                s.Magazalar.Add(u);
                s.Adres.Add(a);
            }
            else
            {
                magaza.MagazaAdi = u.MagazaAdi;
                magaza.MagazaTurleriID = u.MagazaTurleriID;
                magaza.Adres.Sehir = u.Adres.Sehir;
                magaza.Adres.İlce = u.Adres.İlce;
                magaza.Adres.Mahalle = u.Adres.Mahalle;
                magaza.Adres.BinaNumarası = u.Adres.BinaNumarası;
                magaza.Adres.Telefon = u.Adres.Telefon;
            }
            
            s.SaveChanges();
            return RedirectToAction("MagazaIndex");
        }

        [HttpPost]
        public String Sil(int id)
        {
            Magazalar magaza = s.Magazalar.FirstOrDefault(x => x.MagazaID == id);
            if (magaza != null)
            {
                s.Magazalar.Remove(magaza);

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

        public ActionResult Guncelle(int id)
        {
            Magazalar magaza = s.Magazalar.FirstOrDefault(x => x.MagazaID == id);
            ViewBag.magazaturleri = s.MagazaTurleri.ToList();


            return View("magazaEkle",magaza);
        }
    }
}