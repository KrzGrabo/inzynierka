//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aplikacja
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kategorie
    {
        public Kategorie()
        {
            this.Produkty = new HashSet<Produkty>();
        }
    
        public int ID { get; set; }
        public string Nazwa { get; set; }
    
        public virtual ICollection<Produkty> Produkty { get; set; }
    }
}
