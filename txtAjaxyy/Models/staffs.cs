namespace txtAjaxyy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class staffs
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "必填")]
        [Display(Name = "工號")]
        [Remote("CheckJobID", "staffs", ErrorMessage = "工號重複")]
        public string jobID { get; set; }
        [Display(Name = "部門")]
        public int department { get; set; }
        [Display(Name = "狀態ID")]
        public int state { get; set; }
        [Display(Name = "職稱")]
        public int position { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string name { get; set; }

        [Required]
        [Display(Name = "電子郵件")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Display(Name = "職等")]
        public int level { get; set; }
        [Display(Name = "性別")]
        public int sex { get; set; }

        [Required]
        [Display(Name = "建立人員")]
        public string CrePerson { get; set; }

        [Required]
        [Display(Name = "修改人員")]
        public string revPerson { get; set; }
        [Display(Name = "到職日")]
        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }
        [Display(Name = "離職日")]
        [DataType(DataType.Date)]
        public DateTime? quitDate { get; set; }
        [Display(Name = "系統編號")]
        public int SID { get; set; }

        [Required]
        [Display(Name = "電話")]
        [RegularExpression(@"\d{2}.\d{7}.\d{3}", ErrorMessage = "需為07-xxxxxxx#xxx格式")]
        public string phone { get; set; }
    }
}
