using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.INSCRICAOs = new HashSet<INSCRICAO>();
            this.USUARIO_GERENCIA_EVENTO = new HashSet<USUARIO_GERENCIA_EVENTO>();
        }

        [Key]
        public int COD_USUARIO { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
        public string CELULAR { get; set; }
        public Nullable<int> COD_PERFIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSCRICAO> INSCRICAOs { get; set; }
        public virtual PERFIL PERFIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_GERENCIA_EVENTO> USUARIO_GERENCIA_EVENTO { get; set; }
    }
}