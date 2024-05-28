namespace FinaFlow.Core.Requests.Transactions
{
    public class GetTransactionsByPerioadRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
