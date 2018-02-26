using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.ZioPaperone.Interfaces;

namespace PaperonDePaperoni.ZioPaperone
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class ZioPaperone : Actor, IZioPaperone
    {
        public ZioPaperone(ActorService actorService, ActorId actorId) 
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
            throw new NotImplementedException();
        }

        public async Task MoreMoney(decimal money)
        {
            throw new NotImplementedException();
        }
    }
}
