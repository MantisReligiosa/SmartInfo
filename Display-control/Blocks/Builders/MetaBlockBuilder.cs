using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
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
        private List<UIElement> _blocks = new List<UIElement>();
        private SortedList<int, MetablockFrame> _sortedFrames = new SortedList<int, MetablockFrame>();
        private int _currentPosition = 0;

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
            var firstFrameKvp = _sortedFrames.First();
            var firstFrame = firstFrameKvp.Value;

            foreach (var block in metablock.Details.Frames.SelectMany(f => f.Blocks))
            {
                var element = blockBuilder.BuildElement(block);
                if (element != null)
                {
                    element.Uid = block.MetablockFrameId.ToString();
                    _blocks.Add(element);
                    canvas.Children.Add(element);
                }
            }

            SetFrameVisibility(firstFrame.Id);

            var timer = new Timer
            {
                AutoReset = true,
                Interval = firstFrame.Duration * 1000,
                Enabled = true
            };

            timer.Elapsed += (o, args) =>
            {
                _currentPosition++;
                if (_currentPosition == _sortedFrames.Count)
                {
                    _currentPosition = 0;
                }
                var frame = _sortedFrames.ElementAt(_currentPosition).Value;
                SetFrameVisibility(frame.Id);
                timer.Interval = frame.Duration * 1000;
            };

            Canvas.SetTop(canvas, metablock.Top);
            Canvas.SetLeft(canvas, metablock.Left);
            Panel.SetZIndex(canvas, metablock.ZIndex);
            return canvas;
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
