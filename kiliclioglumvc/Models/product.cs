//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kiliclioglumvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public bool availability { get; set; }
        public Nullable<int> price { get; set; }
        public string imageSource { get; set; }
        public Nullable<int> indexOrder { get; set; }
    }
}
