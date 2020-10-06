using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EVENTO
    {
        public EVENTO()
        {
            this.FEEDBACK = new HashSet<FEEDBACK>();
            this.INSCRICAO = new HashSet<INSCRICAO>();
            this.USUARIO_GERENCIA_EVENTO = new HashSet<USUARIO_GERENCIA_EVENTO>();
        }

        public int COD_EVENTO { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public System.TimeSpan DURACAO { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DATA { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public System.TimeSpan HORARIO { get; set; }
        public string IMAGEM_URL { get; set; }
        public string SALA { get; set; }
        public int? ATIVO { get; set; }
        public int? VAGAS { get; set; }

        public virtual ICollection<FEEDBACK> FEEDBACK { get; set; }
        public virtual ICollection<INSCRICAO> INSCRICAO { get; set; }
        public virtual ICollection<USUARIO_GERENCIA_EVENTO> USUARIO_GERENCIA_EVENTO { get; set; }
    }
}