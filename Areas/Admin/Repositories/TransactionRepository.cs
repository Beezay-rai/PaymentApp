using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentApp.Areas.Admin.Interfaces;
using PaymentApp.Areas.Admin.Models;
using PaymentApp.DataModel;
using PaymentApp.Model;

namespace PaymentApp.Areas.Admin.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly PaymentContext _context;
        
        private readonly ILogger<TransactionRepository> _logger;
        private readonly string connectionstring;

        public TransactionRepository(IOptions<Appsetting>app,PaymentContext context, ILogger<TransactionRepository> logger)
        {
            _context = context;
            _logger = logger;
            connectionstring = app.Value.ConnectionStrings;
        }

        public async Task<ResponseModel> CheckBalance(string Username)
        {
            var response = new ResponseModel();
            try
            {
                var data = await _context.Balance.Where(x => x.Username == Username).FirstOrDefaultAsync();
                if (data != null)
                {
                    response.Data = data;
                    response.Status = true;
                    response.Message = "Successfully Generated Result !";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Provided username not found !";
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in fetching balance of {Username}, Error Message :{ex.Message},Date Time:{DateTime.UtcNow}");
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel> CreateNewBalance(BalanceModel model)
        {
            var response = new ResponseModel();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    using(var connection = new SqlConnection(connectionstring))
                    {
                        connection.Open();
                        var result = connection.Execute("Insert into Balance (Username,TotalAmount) Values (@Username,@TotalAmount)", model);
                        Console.WriteLine(result);

                    }
                   

                    var data = new Balance()
                    {
                        Username = model.Username,
                        TotalAmount = model.TotalAmount
                    };
                    await _context.Balance.AddAsync(data);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    response.Status = true;
                    response.Message = "Succefuly created user " + model.Username;
                    return response;

                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = "Error in creating balance !";
                    _logger.LogInformation($"Error in creating new balance of {model.Username}, Error Message :{ex.Message},Date Time:{DateTime.UtcNow}");
                    await transaction.RollbackAsync();
                    return response;

                }
            }
        }

        public async Task<ResponseModel> DeposiAmount(TransactionModel model)
        {
            var response = new ResponseModel();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = await _context.Balance.Where(x => x.Username == model.Username).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.TotalAmount += model.Amount;
                        _context.Entry(data).State = EntityState.Modified;

                        var balantransaction = new Transaction()
                        {
                            Username = model.Username,
                            Status = TransactionStatus.Success.ToString(),
                            Amount = model.Amount,
                            TrackingId = model.TrackingId,
                        };
                        await _context.Transaction.AddAsync(balantransaction);
                        response.Data = balantransaction;
                        response.Status = true;
                        response.Message = "Deposited Succfully";
                        await transaction.CommitAsync();
                        await _context.SaveChangesAsync();
                        return response;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "User not found";
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Error in Depositing balance of {model.Username}, Error Message :{ex.Message},Date Time:{DateTime.UtcNow}");
                    response.Status = false;
                    await transaction.RollbackAsync();

                    return response;
                }
            }
        }

        public async Task<ResponseModel> WithdrawAmount(decimal Amount, string Username)
        {
            var response = new ResponseModel();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = await _context.Balance.Where(x => x.Username == Username).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        if (data.TotalAmount < Amount)
                        {
                            response.Message = "Withdrawal Amount Greater than Remaining Balance";
                            return response;
                        }
                        else
                        {
                            data.TotalAmount -= Amount;
                            _context.Entry(data).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            response.Status = true;
                            response.Message = "Successfully withdrawn Amoun" + Amount;
                            return response;
                        }
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Provided username is not found !";
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = $"Error Occured while withdrawal of username :{Username}";
                    _logger.LogInformation($"Error in Withdrawal balance of {Username}, Error Message :{ex.Message},Date Time:{DateTime.UtcNow}");

                    return response;
                }
            }
        }

        public async Task<ResponseModel> SetTransactionPending(TransactionModel model)
        {
            var response = new ResponseModel();
            try
            {
                var data = new Transaction()
                {
                    Status = TransactionStatus.Pending.ToString(),
                    Amount = model.Amount,
                    Username = model.Username,
                    TrackingId = model.TrackingId,
                };
                await _context.Transaction.AddAsync(data);
                await _context.SaveChangesAsync();
                response.Message = "Transaction on pending !";
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public async Task<ResponseModel> SetTransactionFailure(TransactionModel model)
        {
            var response = new ResponseModel();
            try
            {
                var data = new Transaction()
                {
                    Status = TransactionStatus.Failed.ToString(),
                    Amount = model.Amount,
                    Username = model.Username,
                    TrackingId = model.TrackingId,

                };
                await _context.Transaction.AddAsync(data);
                await _context.SaveChangesAsync();
                response.Message = "System Failure ! ";
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel> GetAllTransaction()
        {
            var response = new ResponseModel();
            try
            {
                var data = await _context.Transaction.Select(x => new TransactionGETModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Status = x.Status.ToString(),
                    Username = x.Username,
                    TrackingId = x.TrackingId,
                }).ToListAsync();
                response.Data = data;
                response.Status = true;
                response.Message = "Successfully Genereated Available transaction !";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Failure in generating list , error msg:{ex.Message}";
                return response;

            }
        }

        public async Task<ResponseModel> CheckStatus(string trackingId)
        {
            var response = new ResponseModel();
            try
            {
                var data = await _context.Transaction.Where(x => x.TrackingId == trackingId).FirstOrDefaultAsync();
                if (data != null)
                {
                    response.Status = true;
                    response.Message = TransactionStatus.Success.ToString();
                }
                else
                {
                    response.Status = false;
                    response.Message = TransactionStatus.Failed.ToString();
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "System Failure !";
                response.Data = "System Failure !";
                _logger.LogInformation($"Error occured on Transaction Repo ,error msg:{ex.Message}, Datetime :{DateTime.UtcNow}");
                return response;
            }

        }
    }
}
