using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace PaperonDePaperoni.QuiQuoQua.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IQuiQuoQua : IActor
    {
        /// <summary>
        /// Aggiorna l'ammontare dei soldi
        /// </summary>
        /// <param name="money"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateMoneyAsync(decimal money, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interfaccia specifica per l'attore Qui
    /// </summary>
    public interface IQui : IQuiQuoQua { }

    /// <summary>
    /// Interfaccia specifica per l'attore Quo
    /// </summary>
    public interface IQuo : IQuiQuoQua { }

    /// <summary>
    /// Interfaccia specifica per l'attore Qua
    /// </summary>
    public interface IQua : IQuiQuoQua { }
}
