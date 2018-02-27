﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.QuiQuoQua.Interfaces;

namespace PaperonDePaperoni.Qui
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Qui : Actor, IQui
    {
        private const string MoneyState = "Money";

        public Qui(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated. Persistence");
            return this.StateManager.TryAddStateAsync(MoneyState, 0);
        }

        protected override Task OnDeactivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor deactivated.");
            return base.OnDeactivateAsync();
        }

        public async Task UpdateMoneyAsync(decimal money, CancellationToken cancellationToken)
        {
            ActorEventSource.Current.ActorMessage(this, $"Id Attore Persistence: {this.Id}, imposto {money} money");
            await StateManager.SetStateAsync(MoneyState, money, cancellationToken);
        }

        public async Task<decimal> GetMoneyAsync()
        {
            return await StateManager.GetStateAsync<decimal>(MoneyState);
        }
    }
}
