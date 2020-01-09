using System.Collections.Generic;

namespace FinancesAPI.Models
{
    public class Account
    {
        public Account()
        {
            Incomes = new List<IncomeExpense>();
            Expenses = new List<IncomeExpense>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public IList<IncomeExpense> Incomes{ get; set; }
        public IList<IncomeExpense> Expenses { get; set; }
    }
}
