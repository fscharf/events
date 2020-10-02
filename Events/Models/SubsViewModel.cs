using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class SubsViewModel
    {
        public IEnumerable<INSCRICAO> InscricaoVM { get; set; }
        public IEnumerable<EVENTO> EventoVM { get; set; }
    }
}