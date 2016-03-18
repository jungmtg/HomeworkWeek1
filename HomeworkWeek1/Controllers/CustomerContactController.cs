using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeworkWeek1.Models;

namespace HomeworkWeek1.Controllers
{
    public class CustomerContactController : Controller
    {
		//private 客戶資料Entities db = new 客戶資料Entities();
	    private 客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
	    private 客戶資料Repository repo2 = RepositoryHelper.Get客戶資料Repository();
		// GET: CustomerContact
		//public ActionResult Index()
		//{
		//    var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
		//    return View(客戶聯絡人.Where(c=>c.是否已刪除==false).ToList());
		//}
		public ActionResult Index(string jobdesc, string name,string email,string mobile,string telphone)
		{
			var result = repo.All();
			//var result = db.客戶聯絡人.Where(c => c.是否已刪除 == false);
			if (!string.IsNullOrEmpty(jobdesc))
				result = result.Where(c => c.職稱 == jobdesc);
			if (!string.IsNullOrEmpty(name))
				result = result.Where(c => c.姓名 == name);
			if (!string.IsNullOrEmpty(email))
				result = result.Where(c => c.Email == email);
			if (!string.IsNullOrEmpty(mobile))
				result = result.Where(c => c.手機 == mobile);
			if (!string.IsNullOrEmpty(telphone))
				result = result.Where(c => c.電話 == telphone);
			return View(result.ToList());
		}

        // GET: CustomerContact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
			//客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: CustomerContact/Create
        public ActionResult Create()
        {
			//var db = (客戶資料Entities)repo.UnitOfWork.Context;
	        IQueryable<客戶資料> 客戶資料 = repo2.All();
			ViewBag.客戶Id = new SelectList(客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerContact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
			//     if (db.客戶聯絡人.Any(cc => cc.Email == 客戶聯絡人.Email))
			//     {
			//throw new Exception("同一個客戶下的聯絡人，其 Email 不能重複!!");
			//     }
			var db = (客戶資料Entities)repo.UnitOfWork.Context;
			if (ModelState.IsValid)
            {
				
	            客戶聯絡人.是否已刪除 = false;
				db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: CustomerContact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);//db.客戶聯絡人.Find(id);
	        var cId = 客戶聯絡人.客戶Id;
	        IQueryable<客戶資料> 客戶資料 = repo2.Where(c => c.Id == cId);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
			var db = (客戶資料Entities)repo.UnitOfWork.Context;
			ViewBag.客戶Id = new SelectList(客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: CustomerContact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
		public ActionResult Edit(int id, FormCollection form)
        {
	        客戶聯絡人 customerContract = repo.Find(id);
			var db = (客戶資料Entities)repo.UnitOfWork.Context;
			if (TryUpdateModel<客戶聯絡人>(customerContract, new String[]
	        {
		        "Id", "客戶Id", "職稱", "姓名", "Email", "手機", "電話"
	        }))
	        {
				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}
			ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", customerContract.客戶Id);
			return View(customerContract);
			#region
			//        if (ModelState.IsValid)
			//        {
			//客戶聯絡人.是否已刪除 = false;
			//db.Entry(客戶聯絡人).State = EntityState.Modified;
			//            db.SaveChanges();
			//            return RedirectToAction("Index");
			//        }
			//        ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
			//return View(客戶聯絡人);
			#endregion
		}

        // GET: CustomerContact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
				//db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: CustomerContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
	        try
	        {
				客戶聯絡人 cc = repo.Find(id);
				cc.是否已刪除 = true;
				repo.UnitOfWork.Commit();
				
			}
	        catch (DbEntityValidationException ex)
	        {
				var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
				var getFullMessage = string.Join("; ", entityError);
				var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
				throw new Exception(ex.Message);
				
			}
			return RedirectToAction("Index");
			//客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			//客戶聯絡人.是否已刪除 = true;
			//db.Entry(客戶聯絡人).State = EntityState.Modified;
			//db.SaveChanges();

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
