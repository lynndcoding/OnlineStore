using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DATA.EF//.Metadata
{
    class OnlineStoreMetadata
    {
        #region BeerStyle Metadata
        public class BeerStyleMetadata
        {
            [Required]
            [StringLength(50, ErrorMessage = "Beer Style cannot be more than 50 characters")]
            [Display(Name = "Beer Style")]
            public string BeerStyle1 { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Beer Description cannot be more than 100 characters")]
            [Display(Name = "Beer Style")]
            public string BeerDesc { get; set; }

        }
        [MetadataType(typeof(BeerStyleMetadata))]
        public partial class BeerStyle1
        {
        }
        #endregion



        #region Package Metadata

        public class PackageMetadata
        {
            [Required(ErrorMessage = "**Packaging is required**")]
            [Range(0, double.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.**")]
            [Display(Name = "Packaging - Quantity in Ounces")]
            public int OzsQty { get; set; }
            [Required(ErrorMessage = "**Price is required**")]
            [Range(0, double.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.**")]
            public decimal Price { get; set; }
            [Required(ErrorMessage = "**Units Sold is required**")]
            [Range(0, double.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.**")]
            [Display(Name = "Units Sold")]
            public int UnitsSold { get; set; }
        }

        [MetadataType(typeof(PackageMetadata))]
        public partial class Package
        {

        }
        #endregion

        #region Product Metadata

        public class ProductMetadata
        {
            [Required(ErrorMessage = "**Beer Style is required**")]
            [Range(0, int.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.**")]
            [Display(Name = "Beer Style")]
            public int BeerStyleID { get; set; }
            [DisplayFormat(NullDisplayText = "-N/A-")]
            [StringLength(50, ErrorMessage = "First Name cannot be more than 50 characters")]
            [Display(Name = "Beer Name")]
            public string BeerName { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Beer Description cannot be more than 100 characters")]
            [Display(Name = "Beer Style")]
            public string BeerDesc { get; set; }
            [Required(ErrorMessage = "**ABV is required**")]
            [Range(0, short.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.**")]
            [Display(Name = "Alcohol By Volume (ABV)")]
            public string ABV { get; set; }
            [StringLength(100, ErrorMessage = "Beer Label Image cannot be more than 100 characters")]
            [Display(Name = "Beer Label Image")]
            public string BeerImage { get; set; }
        }
        [MetadataType(typeof(ProductMetadata))]
        public partial class Product
        {

        }

        #endregion

        #region Status Metadata

        public class StatusMetadata
        {
            [Required(ErrorMessage = "**Product Status is required**")]
            [Display(Name = "Product Status")]
            public string Status { get; set; }

        }
        [MetadataType(typeof(StatusMetadata))]
        public partial class Status
        {

        }

        #endregion
    }
}
