﻿using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace Display_control.Blocks.Builders
{
    public class MetaBlockBuilder : AbstractBuilder
    {
        private readonly List<UIElement> _blocks = new List<UIElement>();
        private SortedList<int, MetablockFrame> _sortedFrames = new SortedList<int, MetablockFrame>();
        private int _currentFrameIndex = int.MinValue;
        private MetablockScheduler _metablockScheduler;

        public MetaBlockBuilder(MetablockScheduler metablockScheduler)
        {
            _metablockScheduler = metablockScheduler;
        }

        public override UIElement BuildElement(DisplayBlock displayBlock)
        {
            var metablock = displayBlock as MetaBlock;
            var canvas = new Canvas
            {
                Height = metablock.Height,
                Width = metablock.Width
            };

            var blockBuilder = new BlockBuilder();

            foreach (var frame in metablock.Details.Frames)
                _sortedFrames.Add(frame.Index, frame);

            foreach (var block in metablock.Details.Frames.SelectMany(f => f?.Blocks ?? new List<DisplayBlock>()))
            {
                var element = blockBuilder.BuildElement(block);
                if (element != null)
                {
                    element.Uid = block.MetablockFrameId.ToString();
                    _blocks.Add(element);
                    canvas.Children.Add(element);
                }
            }

            _metablockScheduler.Frames = _sortedFrames.Values.ToList();

            var timer = new Timer
            {
#if !DEBUG
                    AutoReset = true,
#endif
#if DEBUG
                    AutoReset = false,
#endif
                Interval = 1,
                Enabled = true
            };

#if DEBUG
            timer.Start();
#endif


            timer.Elapsed += (o, args) =>
            {
                var currentDateTime = DateTime.Now;
                var frameToShow = _metablockScheduler.GetNextFrame(currentDateTime, _currentFrameIndex);
                if (frameToShow == null)
                {
                    _currentFrameIndex = int.MinValue;
                    timer.Interval = 1000;
                    HideAllFrames();
                }
                else
                {
                    SetFrameVisibility(frameToShow.Id);
                    _currentFrameIndex = frameToShow.Index;
                    timer.Interval = frameToShow.Duration * 1000;
                }
#if DEBUG
                timer.Start();
#endif
            };

            Canvas.SetTop(canvas, metablock.Top);
            Canvas.SetLeft(canvas, metablock.Left);
            Panel.SetZIndex(canvas, metablock.ZIndex);
            return canvas;
        }

        private void HideAllFrames()
        {
            _dispatcher.Invoke(() =>
            {
                foreach (var block in _blocks)
                {
                    block.Visibility = Visibility.Collapsed;
                }
            });
        }

        private void SetFrameVisibility(Guid frameId)
        {
            _dispatcher.Invoke(() =>
            {
                foreach (var block in _blocks)
                {
                    var isVisible = block.Uid.Equals(frameId.ToString());
                    block.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                }
            });
        }
    }
}
