using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : Request
    {
        [Required(ErrorMessage = "Informe um ID")]
        public long Id { get; set; }
    }
}
