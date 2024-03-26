using Dapper;
using PaymentApp.Areas.Admin.Interfaces;
using PaymentApp.Areas.Admin.Models;
using PaymentApp.DataModel;

namespace PaymentApp.Areas.Admin.Repositories
{
    public class MyDapperRepository : IMyDapper
    {
        private readonly DatabaseUtilities _con;

        public MyDapperRepository(DatabaseUtilities con)
        {
            _con = con;
        }

        public void ExecutestoredProcedure()
        {

            using (var con = _con.GetConnection())
            {
                _con.OpenConnection();

                using (var tran = con.BeginTransaction())
                {
                    var query = con.Query<Balance>("Select * from Balance", transaction: tran);
                    tran.Commit();


                    //var query = con.Query<ProcedureResponse>("",transaction:tran);
                }
                _con.CloseConnection();

            }
        }
    }
}
