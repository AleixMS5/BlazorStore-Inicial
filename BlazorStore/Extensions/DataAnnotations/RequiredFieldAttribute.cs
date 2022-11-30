using System.ComponentModel.DataAnnotations;
using System.IO.Enumeration;

namespace BlazorStore.Extensions.DataAnnotations
{
    public class RequiredFieldAttribute : RequiredAttribute
    {
        public RequiredFieldAttribute()
        {
            ErrorMessage = "This field is required";
        }
    }
}
