using DomainObjects;
using System;

namespace Repository.QueuedTasks
{
    public class ItemTask : IQueuedTask
    {
        private readonly Func<Identity, Identity> _action;
        private readonly Identity _identity;

        public ItemTask(Func<Identity, Identity> action, Identity identity)
        {
            _action = action;
            _identity = identity;
        }

        public void Execute()
        {
            _action(_identity);
        }
    }
}
