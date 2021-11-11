using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineStore.DATA.EF;//Added for access to domain models (Book)
using System.ComponentModel.DataAnnotations;//added for display and validation

namespace OnlineStore.UI.MVC.Models
{
    //Added this ViewModel (not tied to data) to combine Domain models (Book) with other info (Quantity) - Therefore, this is a ViewModel
    public class CartItemViewModel
    {
        //fields

        //props
        [Range(1, int.MaxValue)]//ensures the values are greater than 1 when an item is added to the cart
        public int Qty { get; set; }

        public Product Product { get; set; }
        //ctors
        public CartItemViewModel(int qty, Product product)
        {
            //Property = param;
            Qty = qty;
            Product = product;
        }
        //methods
    }
}