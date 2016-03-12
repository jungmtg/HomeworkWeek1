namespace HomeworkWeek1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶名稱聯絡人數量銀行帳戶數量MetaData))]
    public partial class 客戶名稱聯絡人數量銀行帳戶數量
    {
    }
    
    public partial class 客戶名稱聯絡人數量銀行帳戶數量MetaData
    {
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
        public Nullable<int> 銀行帳戶數量 { get; set; }
    }
}
