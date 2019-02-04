using Display_control.Blocks.Builders;
using DomainObjects.Blocks;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Display_control.Blocks
{
    public class BlockBuilder
    {
        private Dictionary<Type, AbstractBuilder> _builders = new Dictionary<Type, AbstractBuilder>
        {
            {typeof(TextBlock), new TextBlockBuilder() },
            {typeof(TableBlock), new TableBlockBuilder() },
            {typeof(PictureBlock), new PictureBlockBuilder() }
        };

        public UIElement BuildElement(DisplayBlock displayBlock)
        {
            if (_builders.TryGetValue(displayBlock.GetType(), out AbstractBuilder builder))
            {
                return builder.BuildElement(displayBlock);
            }
            return null;
        }
    }
}
