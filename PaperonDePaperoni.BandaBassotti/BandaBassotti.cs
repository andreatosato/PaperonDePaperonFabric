using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.BandaBassotti.Interfaces;
using PaperonDePaperoni.Models;

namespace PaperonDePaperoni.BandaBassotti
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class BandaBassotti : Actor, IBandaBassotti
    {
        private const string CurrentMoney = nameof(CurrentMoney);
        private const string AccountBalance = nameof(AccountBalance);

        /// <summary>
        /// Initializes a new instance of BandaBassotti
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public BandaBassotti(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        public async Task LessMoney(decimal money)
        {
            if(!await StateManager.ContainsStateAsync(CurrentMoney))
            {
                AccountBalance accountBalance = new AccountBalance();
                await StateManager.AddStateAsync<AccountBalance>(AccountBalance, accountBalance);
                await StateManager.AddStateAsync<decimal>(CurrentMoney, money);
            }
            else
            {
                AccountBalance accountBalance = await StateManager.GetStateAsync<AccountBalance>(AccountBalance);
                await StateManager.SetStateAsync<decimal>(CurrentMoney, money);
            }
        }

        public Task MoreMoney(decimal money)
        {
            throw new NotImplementedException();
        }
    }
}
