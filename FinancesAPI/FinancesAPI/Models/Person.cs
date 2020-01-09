using System.Collections.Generic;

namespace FinancesAPI.Models
{
    public class Person
    {
        public Person()
        {
            Accounts = new List<Account>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public IList<Account> Accounts { get; set; }
    }
}
