using System;

namespace Repository.QueuedTasks
{
    public class GuidTask : IQueuedTask
    {
        private readonly Action<Guid> _action;
        private readonly Guid _id;

        public GuidTask(Action<Guid> action, Guid id)
        {
            _action = action;
            _id = id;
        }

        public void Execute()
        {
            _action(_id);
        }
    }
}
