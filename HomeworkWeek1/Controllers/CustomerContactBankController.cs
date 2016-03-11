using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeworkWeek1.Models;

namespace HomeworkWeek1.Controllers
{
    public class CustomerContactBankController : Controller
    {
		private 客戶資料Entities db = new 客戶資料Entities();
		// GET: CustomerContactBank
		public ActionResult Index()
        {
            return View(db.客戶名稱聯絡人數量銀行帳戶數量.ToList());
        }
    }
}