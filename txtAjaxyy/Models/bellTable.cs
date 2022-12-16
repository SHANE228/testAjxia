namespace txtAjaxyy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bellTable")]
    public partial class bellTable
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int work { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string jobID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime workTime { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string punchBell { get; set; }
    }
}
