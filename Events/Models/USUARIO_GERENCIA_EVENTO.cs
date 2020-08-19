using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class USUARIO_GERENCIA_EVENTO
    {
        [Key]
        public int COD_GERENCIA { get; set; }
        public Nullable<int> COD_USUARIO { get; set; }
        public Nullable<int> COD_EVENTO { get; set; }

        public virtual EVENTO EVENTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}