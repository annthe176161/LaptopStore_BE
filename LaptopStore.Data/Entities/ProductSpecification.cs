﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Entities
{
    public class ProductSpecification
    {
        public int ProductSpecificationID { get; set; }
        public int ProductID { get; set; }
        public int AttributeID { get; set; }
        public string AttributeValue { get; set; }

        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
    }
}
