//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Personeller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personeller()
        {
            this.Siparis = new HashSet<Siparis>();
            this.Kullanıcı = new HashSet<Kullanıcı>();
        }
    
        public int PersonelID { get; set; }
        public string PersonalAdi { get; set; }
        public string PersonelSoyadi { get; set; }
        public string PersonalUnvan { get; set; }
        public string IseBaslamaTarihi { get; set; }
        public Nullable<int> MagazaID { get; set; }
    
        public virtual Magazalar Magazalar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanıcı> Kullanıcı { get; set; }
    }
}