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
    
    public partial class USUARIO_GERENCIA_EVENTO
    {
        public int COD_GERENCIA { get; set; }
        public Nullable<int> COD_USUARIO { get; set; }
        public Nullable<int> COD_EVENTO { get; set; }
    
        public virtual EVENTO EVENTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}