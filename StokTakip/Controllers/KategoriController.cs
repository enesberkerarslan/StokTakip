using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models;

namespace StokTakip.Controllers
{

    public class KategoriController : Controller
    {
        StokTakipEntities s = new StokTakipEntities();
        // GET: Kategori
        public ActionResult KategoriIndex()
        {
            
            return View();
        }
        public ActionResult Kategoriler(int id)
        {
            Kategori kat = s.Kategori.FirstOrDefault(x => x.KategoriID == id);
            ViewBag.isim = kat.Kategori1;
            ViewBag.urun = kat.KategoriID;
            List<Urunler> liste = s.Urunler.ToList();
            return View("KategoriIndex",liste);
        }
     

    }
       
}