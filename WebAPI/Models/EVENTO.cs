//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EVENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENTO()
        {
            this.FEEDBACK = new HashSet<FEEDBACK>();
            this.INSCRICAO = new HashSet<INSCRICAO>();
            this.USUARIO_GERENCIA_EVENTO = new HashSet<USUARIO_GERENCIA_EVENTO>();
        }
    
        public int COD_EVENTO { get; set; }
        public System.TimeSpan DURACAO { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public System.DateTime DATA { get; set; }
        public System.TimeSpan HORARIO { get; set; }
        public string IMAGEM_URL { get; set; }
        public string SALA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FEEDBACK> FEEDBACK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSCRICAO> INSCRICAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_GERENCIA_EVENTO> USUARIO_GERENCIA_EVENTO { get; set; }
    }
}
