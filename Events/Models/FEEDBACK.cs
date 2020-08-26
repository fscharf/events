using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class FEEDBACK
    {
        public int COD_FEEDBACK { get; set; }
        public string COMENTARIO { get; set; }
        public string NOTA { get; set; }
        public Nullable<int> COD_INSCRICAO { get; set; }
        public Nullable<int> COD_EVENTO { get; set; }

        public virtual EVENTO EVENTO { get; set; }
        public virtual INSCRICAO INSCRICAO { get; set; }
    }
}