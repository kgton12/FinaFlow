using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Core.Requests.Transactions
{
    public class DeleteTransactionRequest : Request
    {
        [Required(ErrorMessage = "Informe um ID")]
        public long Id { get; set; }
    }
}
