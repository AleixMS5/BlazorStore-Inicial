using System.ComponentModel.DataAnnotations;

namespace BlazorStore.Extensions.DataAnnotations
{
    public class FieldLengthAttribute : MaxLengthAttribute
    {
        public FieldLengthAttribute(int maxLength) : base(maxLength)
        {
            ErrorMessage = "Up to {0} chars allowed";
        }
    }
}