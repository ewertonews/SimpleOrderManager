using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManager
{
    public interface IQueuingHandler<T> where T : class, IComparable
    {
        IDictionary<string, Queue<T>> QueuesRecord { get; }
        Task AddItemToQueue(string queueId, T message);
        Task<Queue<T>> GetQueueById(string queueId);
        Task<T> ProcessItemFromQueue(string queueId);
        Task RemoveItemFromQueue(string queueId, T queueItem);
    }
}