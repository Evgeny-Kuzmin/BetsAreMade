using System;
using System.Collections.Generic;

namespace BetsAreMade.DataContracts.Dto.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BetDto> Bets { get; set; } = new List<BetDto>();

        public void AddBets(BetDto bet)
        {
            if (Bets == null) 
            {
                Bets = new List<BetDto>();
            }

            if (bet != null) 
            {
                Bets.Add(bet);
            }
        }

        internal void RemoveBet(BetDto bet)
        {
            if (Bets == null)
            {
                Bets = new List<BetDto>();
            }

            if (bet != null)
            {
                Bets.Remove(bet);
            }
        }
    }
}
