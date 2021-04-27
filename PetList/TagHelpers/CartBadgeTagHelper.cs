using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PetList.Models
{
    [HtmlTargetElement("span", Attributes = "my-cart-badge")]
    public class CartBadgeTagHelper : TagHelper
    {
        private IPetList cart;
        public CartBadgeTagHelper(IPetList c) => cart = c;

        public bool MyCartBadge { get; set; } // not used - keeps attribute from being output

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            output.Content.SetContent(cart.Count?.ToString());
        }
    }
}
