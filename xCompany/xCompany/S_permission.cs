//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xCompany
{
    using System;
    using System.Collections.Generic;
    
    public partial class S_permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public S_permission()
        {
            this.Sup_Quantity = new HashSet<Sup_Quantity>();
        }
    
        public int Per_id_S { get; set; }
        public string WH_Name { get; set; }
        public int P_code { get; set; }
        public Nullable<System.DateTime> S_Per_date { get; set; }
        public Nullable<int> S_id { get; set; }
    
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sup_Quantity> Sup_Quantity { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}