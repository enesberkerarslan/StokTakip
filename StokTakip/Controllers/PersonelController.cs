using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;
namespace StokTakip.Controllers
{
    public class PersonelController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();

        // GET: Personel
        public ActionResult PersonelIndex()
        {
            List<Personeller> liste = s.Personeller.ToList();
            return View(liste);
        }

        public ActionResult personelEkle()
        {
            ViewBag.magazalar = s.Magazalar.ToList();
            Personeller p = new Personeller();
            return View(p);
        }

        [HttpPost]
        public ActionResult personelEkle(Personeller p)
        {
           
            Personeller personel = s.Personeller.FirstOrDefault(x => x.PersonelID == p.PersonelID);

            if (personel == null)
            {
                s.Personeller.Add(p);
            }
            else
            {
                personel.PersonalAdi = p.PersonalAdi;
                personel.PersonelSoyadi = p.PersonelSoyadi;
                personel.PersonalUnvan = p.PersonalUnvan;
                personel.IseBaslamaTarihi = p.IseBaslamaTarihi;
                personel.MagazaID = p.MagazaID;
            }



            s.SaveChanges();

            return RedirectToAction("PersonelIndex");
        }
        public ActionResult Guncelle(int id)
        {
            ViewBag.magazalar = s.Magazalar.ToList();
            Personeller pers = s.Personeller.FirstOrDefault(x => x.PersonelID == id);

            return View("personelEkle", pers);
        }


        [HttpPost]
        public String Sil(int id)
        {
            Personeller pers = s.Personeller.FirstOrDefault(x => x.PersonelID == id);
            if (pers != null)
            {
                s.Personeller.Remove(pers);

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
    }
    
}