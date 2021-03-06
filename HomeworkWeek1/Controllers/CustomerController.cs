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
	public class CustomerController : Controller
	{
		//private 客戶資料Entities db = new 客戶資料Entities();
		private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

		public ActionResult Index(string customerName, string uniNumber,string tel,string fax,string address,string email)
		{
			var result = repo.All();
			//var result = db.客戶資料.Where(c => c.是否已刪除 == false);
			if (!string.IsNullOrEmpty(customerName))
				result = result.Where(c => c.客戶名稱 == customerName);
			if (!string.IsNullOrEmpty(uniNumber))
				result = result.Where(c => c.統一編號 == uniNumber);
			if (!string.IsNullOrEmpty(tel))
				result = result.Where(c => c.電話 == tel);
			if (!string.IsNullOrEmpty(fax))
				result = result.Where(c => c.傳真 == fax);
			if (!string.IsNullOrEmpty(address))
				result = result.Where(c => c.地址 == address);
			if (!string.IsNullOrEmpty(email))
				result = result.Where(c => c.Email == email);
			return View(result.ToList());
		}

		// GET: Customer/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶資料 客戶資料 = repo.Find(id.Value);//db.客戶資料.Find(id);
			if (客戶資料 == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料);
		}

		// GET: Customer/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Customer/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
		{
			if (ModelState.IsValid)
			{
				var db = (客戶資料Entities) repo.UnitOfWork.Context;
				客戶資料.是否已刪除 = false;
				db.客戶資料.Add(客戶資料);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(客戶資料);
		}

		// GET: Customer/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶資料 客戶資料 = repo.Find(id.Value);//db.客戶資料.Find(id);
			if (客戶資料 == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料);
		}

		// POST: Customer/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
		public ActionResult Edit(int id, FormCollection form)
		{
			客戶資料 customer = repo.Find(id);

			if (TryUpdateModel<客戶資料>(customer, new String[]
			{
				"Id,客戶名稱","統一編號","電話","傳真","地址","Email"
			}))
			{
				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}
			return View(customer);
			#region
			//if (ModelState.IsValid)
			//{
			//	var db = (客戶資料Entities)repo.UnitOfWork.Context;
			//	客戶資料.是否已刪除 = false;
			//	db.Entry(客戶資料).State = EntityState.Modified;
			//	db.SaveChanges();
			//	return RedirectToAction("Index");
			//}
			//return View(客戶資料);
			#endregion
		}

		// GET: Customer/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶資料 客戶資料 = repo.Find(id.Value); 
				//db.客戶資料.Find(id);
			if (客戶資料 == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料);
		}

		// POST: Customer/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			客戶資料 customer = repo.Find(id);
			customer.是否已刪除 = true;
			repo.UnitOfWork.Commit();

			//客戶資料 客戶資料 = db.客戶資料.Find(id);
			//客戶資料.是否已刪除 = true;
			//db.Entry(客戶資料).State = EntityState.Modified;
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
