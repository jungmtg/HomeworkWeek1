using System.Linq;

namespace HomeworkWeek1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
			客戶資料Entities db = new 客戶資料Entities();
			if (this.Id == 0)
		    {
				//create
				if (db.客戶聯絡人.Any(cc => cc.Email == this.Email ))
				//.Any(cc => cc.Email == this.Email && cc.客戶Id = this.客戶Id))
				{
					yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複!!");

				}
			}
		    else
		    {
				//update
				//this.客戶Id
				if (db.客戶聯絡人.Any(cc=>cc.Email == this.Email && cc.客戶Id == this.客戶Id))
					//.Any(cc => cc.Email == this.Email && cc.客戶Id = this.客戶Id))
			    {
					yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複!!");
				}
		    }
	    }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }


        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
		[RegularExpression(@"\d{4}-\d{6}", ErrorMessage = " 手機輸入格式錯誤,範例 0911-111111.")]
		public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
