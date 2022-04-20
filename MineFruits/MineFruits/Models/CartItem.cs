using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MineFruits.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}