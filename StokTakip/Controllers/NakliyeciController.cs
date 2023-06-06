using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class NakliyeciController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();

        // GET: Nakliyeci
        public ActionResult NakliyeciIndex()
        {
            List<Nakliyeciler> liste = s.Nakliyeciler.ToList();
            return View(liste);
        }

        public ActionResult nakliyeciEkle()
        {
            Nakliyeciler n = new Nakliyeciler();

            return View(n);
        }

        [HttpPost]

        public ActionResult nakliyeciEkle(Nakliyeciler n)
        {
            Nakliyeciler nesne = s.Nakliyeciler.FirstOrDefault(x => x.NakliyeciID == n.NakliyeciID);

            if (nesne == null)
            {
                s.Nakliyeciler.Add(n);
            }
            else
            {
                nesne.FirmaAdi = n.FirmaAdi;
                nesne.FirmaBilgisi = n.FirmaBilgisi;
            }
            s.SaveChanges();

            return RedirectToAction("NakliyeciIndex");
        }


        public ActionResult Guncelle(int id)
        {
            Nakliyeciler nesne = s.Nakliyeciler.FirstOrDefault(x => x.NakliyeciID == id);

            return View("nakliyeciEkle", nesne);
        }

        [HttpPost]
        public String Sil(int id)
        {
            Nakliyeciler n = s.Nakliyeciler.FirstOrDefault(x => x.NakliyeciID == id);
            if (n != null)
            {
                s.Nakliyeciler.Remove(n);

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