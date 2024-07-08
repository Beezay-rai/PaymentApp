using PaymentApp.Areas.Admin.Models;
using PaymentApp.Model;

namespace PaymentApp.Areas.Admin.Interfaces
{
    public interface ITransaction
    {

        Task<ResponseModel> WithdrawAmount(decimal Amount, string Username);
        Task<ResponseModel> DeposiAmount(TransactionModel model);
        Task<ResponseModel> SetTransactionPending(TransactionModel model);
        Task<ResponseModel> SetTransactionFailure(TransactionModel model);
        Task<ResponseModel> CheckBalance(string Username);
        Task<ResponseModel> CreateNewBalance(BalanceModel model);
        Task<ResponseModel> GetAllTransaction();
        Task<ResponseModel> CheckStatus(string trackingId);
    }
}
