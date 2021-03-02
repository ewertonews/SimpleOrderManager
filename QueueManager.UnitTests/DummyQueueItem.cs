using System;

namespace OrderManager.UnitTests
{
    internal class DummyQueueItem : IComparable
    {
        public int Id { get; set; }

        public int CompareTo(object otherObject)
        {
            if (Id < ((DummyQueueItem)otherObject).Id) return -1;
            if (Id == ((DummyQueueItem)otherObject).Id) return 0;
            return 1;
        }
    }
}