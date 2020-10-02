using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class INSCRICAO
    {
        public INSCRICAO()
        {
            this.FEEDBACK = new HashSet<FEEDBACK>();
        }

        public int COD_INSCRICAO { get; set; }
        public Nullable<int> COD_USUARIO { get; set; }
        public Nullable<int> COD_EVENTO { get; set; }
        public System.DateTime DATA_HORA_INSC { get; set; }
        public System.DateTime DATA_HORA_PARTICIPACAO { get; set; }
        public virtual EVENTO EVENTO { get; set; }
        public virtual ICollection<FEEDBACK> FEEDBACK { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}