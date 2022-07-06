using System.Collections.Generic;

namespace BetsAreMade.DataContracts.Dbo.Users
{
    public class UserDbo : BaseDbo
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        public List<BetDbo> Bets { get; set; } = new List<BetDbo>();
    }
}
