using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeworkWeek1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
		public override IQueryable<客戶銀行資訊> All()
		{
			return base.All().Where(p => !p.是否已刪除);
		}

		public IQueryable<客戶銀行資訊> All(bool isAll)
		{
			if (isAll)
			{
				return this.All();
			}
			else
			{
				return base.All();
			}
		}

		public 客戶銀行資訊 Find(int id)
		{
			return this.All().FirstOrDefault(cb => cb.Id == id);
		}
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}