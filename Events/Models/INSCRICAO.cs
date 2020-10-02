using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class INSCRICAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INSCRICAO()
        {
            this.FEEDBACK = new HashSet<FEEDBACK>();
        }

        public int COD_INSCRICAO { get; set; }
        public Nullable<int> COD_USUARIO { get; set; }
        public Nullable<int> COD_EVENTO { get; set; }
        public System.DateTime DATA_HORA_INSC { get; set; }
        public System.DateTime DATA_HORA_PARTICIPACAO { get; set; }

        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DATA { get; set; }
        public string IMAGEM_URL { get; set; }

        public virtual EVENTO EVENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FEEDBACK> FEEDBACK { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}