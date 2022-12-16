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


        [Required(ErrorMessage = "����")]
        [Display(Name = "�u��")]
        [Remote("CheckJobID", "staffs", ErrorMessage = "�u������")]
        public string jobID { get; set; }
        [Display(Name = "����")]
        public int department { get; set; }
        [Display(Name = "���AID")]
        public int state { get; set; }
        [Display(Name = "¾��")]
        public int position { get; set; }

        [Required]
        [Display(Name = "�m�W")]
        public string name { get; set; }

        [Required]
        [Display(Name = "�q�l�l��")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Display(Name = "¾��")]
        public int level { get; set; }
        [Display(Name = "�ʧO")]
        public int sex { get; set; }

        [Required]
        [Display(Name = "�إߤH��")]
        public string CrePerson { get; set; }

        [Required]
        [Display(Name = "�ק�H��")]
        public string revPerson { get; set; }
        [Display(Name = "��¾��")]
        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }
        [Display(Name = "��¾��")]
        [DataType(DataType.Date)]
        public DateTime? quitDate { get; set; }
        [Display(Name = "�t�νs��")]
        public int SID { get; set; }

        [Required]
        [Display(Name = "�q��")]
        [RegularExpression(@"\d{2}.\d{7}.\d{3}", ErrorMessage = "�ݬ�07-xxxxxxx#xxx�榡")]
        public string phone { get; set; }
    }
}
