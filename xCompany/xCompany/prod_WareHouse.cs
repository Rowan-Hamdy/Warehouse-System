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
    
    public partial class prod_WareHouse
    {
        public string WH_Name { get; set; }
        public int p_Code { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}