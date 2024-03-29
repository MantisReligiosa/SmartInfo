﻿using SmartInfo.Blocks.Builders;
using DomainObjects.Blocks;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SmartInfo.Blocks
{
    public class BlockBuilder
    {
        private Dictionary<Type, AbstractBuilder> _builders = new Dictionary<Type, AbstractBuilder>
        {
            {typeof(TextBlock), new TextBlockBuilder() },
            {typeof(TableBlock), new TableBlockBuilder() },
            {typeof(PictureBlock), new PictureBlockBuilder() },
            {typeof(DateTimeBlock), new DateTimeBlockBuilder() },
            {typeof(Scenario), new ScenarioBuilder() }
        };

        public UIElement BuildElement(DisplayBlock displayBlock)
        {
            if (_builders.TryGetValue(displayBlock.GetType(), out AbstractBuilder builder))
            {
                try
                {
                    var element =  builder.BuildElement(displayBlock);
                    return element;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
