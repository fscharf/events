using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class SubsViewModel
    {
        public IEnumerable<INSCRICAO> InscricaoVM { get; set; }
        public IEnumerable<EVENTO> EventoVM { get; set; }
        public IEnumerable<USUARIO> UsuarioVM { get; set; }
        public int? COD_INSCRICAO { get; set; }
        public string TITULO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DATA { get; set; }
        public Nullable<System.DateTime> DATA_HORA_PARTICIPACAO { get; set; }
        public string NOME { get; set; }
    }
}