using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcProjesi.Models.Entity;

namespace mvcProjesi.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {

                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()


                                             }).ToList();
            ViewBag.dgr = degerler;
                             
            return View();

        }
       
    

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p3)
        {
            
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p3.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
               p3.TBLKATEGORILER = ktg;
               db.TBLURUNLER.Add(p3);
               db.SaveChanges();
               return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {

            var urunSil = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urunSil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunGetir(int id)
        {

            var urun = db.TBLURUNLER.Find(id);
            

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {

                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()


                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);


        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {

            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNID = p1.URUNID;
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
           // urun.URUNKATEGORI = p1.URUNKATEGORI;
            urun.FIYAT = p1.FIYAT;
            urun.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}