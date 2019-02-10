using DomainObjects.Blocks;
using System.Windows;

namespace Display_control.Blocks
{
    public abstract class AbstractBuilder
    {
        public abstract UIElement BuildElement(DisplayBlock displayBlock);
    }
}
