using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EVENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENTO()
        {
            this.FEEDBACKs = new HashSet<FEEDBACK>();
            this.INSCRICAOs = new HashSet<INSCRICAO>();
            this.USUARIO_GERENCIA_EVENTO = new HashSet<USUARIO_GERENCIA_EVENTO>();
        }

        [Key]
        public int COD_EVENTO { get; set; }
        public System.TimeSpan DURACAO { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public System.DateTime DATA { get; set; }
        public System.TimeSpan HORARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FEEDBACK> FEEDBACKs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSCRICAO> INSCRICAOs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_GERENCIA_EVENTO> USUARIO_GERENCIA_EVENTO { get; set; }
    }
}