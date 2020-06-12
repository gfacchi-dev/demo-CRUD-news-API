namespace DemoGestioneNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            News = new HashSet<News>();
        }

        [Key]
        [Display(Name = "Autore")]
        public int PKAuthor { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome Autore")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [Display(Name = "Data di Nascita")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
