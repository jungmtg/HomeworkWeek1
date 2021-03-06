﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeworkWeek1.Models;

namespace HomeworkWeek1.Controllers
{
    public class CustomerBankInformationController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
	    private 客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
		private 客戶資料Repository repo2 = RepositoryHelper.Get客戶資料Repository();


		public ActionResult Index(string bankname,string bankno,string branchno,string accountname,string accountno)
		{
			var result = repo.All();//db.客戶銀行資訊.Where(cb => cb.是否已刪除 == false);
			if(!string.IsNullOrEmpty(bankname))
				result = result.Where(c => c.銀行名稱 == bankname);
			if (!string.IsNullOrEmpty(bankno)) { 
				var banknoval = Int32.Parse(bankno);
				result = result.Where(c => c.銀行代碼 == banknoval);
			}
			if (!string.IsNullOrEmpty(branchno)) { 
				var branchnoval = Int32.Parse(branchno);
				result = result.Where(c => c.分行代碼 == branchnoval);
			}
			if (!string.IsNullOrEmpty(accountname))
				result = result.Where(c => c.帳戶名稱 == accountname);
			if (!string.IsNullOrEmpty(accountno))
				result = result.Where(c => c.帳戶號碼 == accountno);
			return View(result.ToList());
		}

        // GET: CustomerBankInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        客戶銀行資訊 客戶銀行資訊 = repo.Find(id.Value);//db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: CustomerBankInformation/Create
        public ActionResult Create()
        {
			IQueryable<客戶資料> 客戶資料 = repo2.All();
			ViewBag.客戶Id = new SelectList(客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerBankInformation/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
			var db = (客戶資料Entities)repo.UnitOfWork.Context;
			IQueryable<客戶資料> 客戶資料 = repo2.All();
			if (ModelState.IsValid)
            {
	            客戶銀行資訊.是否已刪除 = false;

				db.客戶銀行資訊.Add(客戶銀行資訊);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: CustomerBankInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        客戶銀行資訊 客戶銀行資訊 = repo.Find(id.Value);//db.客戶銀行資訊.Find(id);
			IQueryable<客戶資料> 客戶資料 = repo2.All();
			if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: CustomerBankInformation/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
		public ActionResult Edit(int id, FormCollection form)
		{
			客戶銀行資訊 custBankInfo = repo.Find(id);
			var db = (客戶資料Entities)repo.UnitOfWork.Context;

			if (TryUpdateModel<客戶銀行資訊>(custBankInfo, new String[]
			{
				"Id","客戶Id","銀行名稱","銀行代碼","分行代碼","帳戶名稱","帳戶號碼"
			}))
			{
				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}

			//if (ModelState.IsValid)
			//         {
			//	客戶銀行資訊.是否已刪除 = false;
			//	db.Entry(客戶銀行資訊).State = EntityState.Modified;
			//             db.SaveChanges();
			//             return RedirectToAction("Index");
			//         }
			ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", custBankInfo.客戶Id);
            return View(custBankInfo);
        }

        // GET: CustomerBankInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        客戶銀行資訊 客戶銀行資訊 = repo.Find(id.Value);//db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: CustomerBankInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

	        客戶銀行資訊 客戶銀行資訊 = repo.Find(id);//db.客戶銀行資訊.Find(id);
	        客戶銀行資訊.是否已刪除 = true;
			repo.UnitOfWork.Commit();
			//db.Entry(客戶銀行資訊).State = EntityState.Modified;
			//db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				var db = (客戶資料Entities)repo.UnitOfWork.Context;
				db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
