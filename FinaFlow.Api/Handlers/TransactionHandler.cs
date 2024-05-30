using FinaFlow.Api.Data;
using FinaFlow.Core.Common;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: Core.Enums.ETransactionType.Withdraw, Amount: >= 0 })
            {
                request.Amount *= -1;
            }

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
            }
            catch (Exception)
            {
                return new Response<Transaction?>(null, 500, "Erro ao criar transação");
            }


        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação deletada com sucesso");
            }
            catch (Exception)
            {
                return new Response<Transaction?>(null, 500, "Erro ao deletar transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetAllAsync(GetTransactionsByPerioadRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();

            }
            catch (Exception)
            {
                return new PagedResponse<List<Transaction>?>(null,500,"Não foi possivel determinar a data da transação");
            }

            try
            {
                var query = context.Transactions
                    .AsNoTracking()
                    .Where(t => t.UserId == request.UserId && 
                           t.PaidOrReceivedAt >= request.StartDate &&
                           t.PaidOrReceivedAt <= request.EndDate)
                    .OrderBy(t => t.PaidOrReceivedAt);
                    
                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Erro ao buscar transações");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                    : new Response<Transaction?>(transaction, 200, "Transação encontrada");
            }
            catch (Exception)
            {

                return new Response<Transaction?>(null, 500, "Erro ao buscar transação");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: Core.Enums.ETransactionType.Withdraw, Amount: >= 0 })
            {
                request.Amount *= -1;
            }

            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                };

                transaction.UserId = request.UserId;
                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");

            }
            catch (Exception)
            {
                return new Response<Transaction?>(null, 500, "Erro ao atualizar transação");
            }
        }
    }
}
