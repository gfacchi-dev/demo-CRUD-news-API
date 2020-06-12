namespace DemoGestioneNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int PKNews { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome Notizia")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data pubblicazione")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PublishDate { get; set; }

        
        public int FKAuthor { get; set; }
        
        public virtual Author Author { get; set; }
    }
}
