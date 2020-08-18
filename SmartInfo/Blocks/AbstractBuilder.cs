using DomainObjects.Blocks;
using System.Windows;
using System.Windows.Threading;

namespace SmartInfo.Blocks
{
    public abstract class AbstractBuilder
    {
        internal readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        public abstract UIElement BuildElement(DisplayBlock displayBlock);
    }
}
