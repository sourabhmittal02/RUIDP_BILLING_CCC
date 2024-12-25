using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using Microsoft.Ajax.Utilities;
namespace ComplaintTracker.Controllers
{
    public class NewConnecionRUIDPController : Controller
    {
        // GET: NewConnecionRUIDP
        public ActionResult Index()
        {
            return View();
        }

        // GET: NewConnecionRUIDP/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewConnecionRUIDP/Create
        public ActionResult Create()
        {
            ModelNewConnectionRUIDP modelNewConnectionRUIDP = new ModelNewConnectionRUIDP();
            modelNewConnectionRUIDP.Commission_date = DateTime.Now;

            return View();
        }

        // POST: NewConnecionRUIDP/Create
        [HttpPost]
        public ActionResult Create(ModelNewConnectionRUIDP  modelNewConnectionRUIDP)
        {
            try
            {
                    if(ModelState.IsValid) 
                    {
                        ModelStatus modelStatus  = Repository.SaveNewConnnectionRUIDP(modelNewConnectionRUIDP);
                        if (Convert.ToInt16(modelStatus.StatusId) > 0)
                        {

                            TempData["AlertMessage"] = "New Connection  created Successfully...!";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            TempData["AlertMessage"] = "Error in New Connection Generating...!";
                            return View();
                        }
                    }
                else
                {
                    return View();

                }
       
            }
            catch
            {
                return View();
            }
        }

        // GET: NewConnecionRUIDP/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewConnecionRUIDP/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewConnecionRUIDP/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewConnecionRUIDP/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
