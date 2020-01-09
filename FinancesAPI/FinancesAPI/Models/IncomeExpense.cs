using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancesAPI.Models
{
    public class IncomeExpense
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Notes { get; set; }
    }
}
