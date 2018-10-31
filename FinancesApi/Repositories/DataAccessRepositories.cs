using System.Collections.Generic;
using System.Linq;
using FinancesApi.Models;

namespace FinancesApi.Repositories
{
    public interface IDataAccess<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> GetIncomes();
        TEntity GetIncome(U id);
        int AddIncome(TEntity b);
        int UpdateIncome(U id, TEntity b);
        int DeleteIncome(U id);
    }

    public class DataAccessRepository : IDataAccess<Income, int>
    {
        ApplicationContext ctx;
        public DataAccessRepository(ApplicationContext c)
        {
            ctx = c;
        }
        public int AddIncome(Income b)
        {
            ctx.Incomes.Add(b);
            int res = ctx.SaveChanges();
            return res;
        }

        public int DeleteIncome(int id)
        {
            int res = 0;
            var income = ctx.Incomes.FirstOrDefault(b => b.Id == id);
            if (income != null)
            {
                ctx.Incomes.Remove(income);
                res = ctx.SaveChanges();
            }
            return res;
        }

        public Income GetIncome(int id)
        {
            var income = ctx.Incomes.FirstOrDefault(b => b.Id == id);
            return income;
        }

        public IEnumerable<Income> GetIncomes()
        {
            var incomes = ctx.Incomes.ToList();
            return incomes;
        }

        public int UpdateIncome(int id, Income b)
        {
            int res = 0;
            var income = ctx.Incomes.Find(id);
            if (income != null)
            {
                income.IncomeTitle = b.IncomeTitle;
                income.IncomeInDate = b.IncomeInDate;
                income.Amount = b.Amount;
                res = ctx.SaveChanges();
            }
            return res;
        }
    }
}