using DomainObjects;
using System;
using System.Collections.Generic;

namespace Repository.QueuedTasks
{
    public class ItemsTask : IQueuedTask
    {
        private readonly Action<IEnumerable<Identity>> _action;
        private readonly IEnumerable<Identity> _list;

        public ItemsTask(Action<IEnumerable<Identity>> action, IEnumerable<Identity> list)
        {
            _action = action;
            _list = list;
        }

        public void Execute()
        {
            _action(_list);
        }
    }
}
