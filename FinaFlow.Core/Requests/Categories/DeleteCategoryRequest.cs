using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Core.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        [Required(ErrorMessage = "Informe um ID")]
        public long Id { get; set; }
    }
}
