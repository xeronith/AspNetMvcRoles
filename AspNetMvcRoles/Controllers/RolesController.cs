using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AspNetMvcRoles.Models;

namespace AspNetMvcRoles.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationRoleManager roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                roleManager = value;
            }
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole role = RoleManager.FindById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Create,Read,Update,Delete")] ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                RoleManager.Create(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole role = RoleManager.FindById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Create,Read,Update,Delete")] ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                RoleManager.Update(RoleManager.FindById(role.Id).CopyFieldsFrom(role));
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole role = RoleManager.FindById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationRole role = RoleManager.FindById(id);
            RoleManager.Delete(role);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (roleManager != null)
                {
                    roleManager.Dispose();
                    roleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
