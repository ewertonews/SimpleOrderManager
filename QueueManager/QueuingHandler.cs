using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager
{
    public class QueuingHandler<T> : IQueuingHandler<T>
        where T : class, IComparable
    {
        public IDictionary<string, Queue<T>> QueuesRecord { get; } = new Dictionary<string, Queue<T>>();

        public async Task AddItemToQueue(string queueId, T message)
        {
            await Task.Run(() =>
            {
                if (!QueuesRecord.ContainsKey(queueId))
                {
                    QueuesRecord.Add(queueId, new Queue<T>());
                }

                QueuesRecord[queueId].Enqueue(message);
            });
        }

        public async Task<T> ProcessItemFromQueue(string queueId)
        {
            return await Task.Run(() =>
            {
                if (QueuesRecord.ContainsKey(queueId))
                {
                    return QueuesRecord[queueId].Dequeue();
                }
                return null;
            });
        }

        public async Task<Queue<T>> GetQueueById(string queueId)
        {
            return await Task.Run(() =>
            {
                var outQueue = new Queue<T>();
                QueuesRecord.TryGetValue(queueId, out outQueue);
                return outQueue;
            });
        }

        public async Task RemoveItemFromQueue(string queueId, T queueItemToRemove)
        {
            await Task.Run(() =>
            {
                var currentQueueItems = QueuesRecord[queueId].ToList<T>();
                var filteredQueueItems = new List<T>();

                foreach (var item in currentQueueItems)
                {
                    int compareResult = queueItemToRemove.CompareTo(item);
                    if (compareResult != 0)
                    {
                        filteredQueueItems.Add(item);
                    }
                }
                QueuesRecord[queueId] = new Queue<T>(filteredQueueItems);

            });
        }

    }
}
