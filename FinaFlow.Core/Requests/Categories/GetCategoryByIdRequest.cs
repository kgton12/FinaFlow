using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : Request
    {
        [Required(ErrorMessage = "Informe um ID")]
        public long Id { get; set; }
    }
}
