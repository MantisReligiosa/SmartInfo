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
        private readonly List<UIElement> _blocks = new List<UIElement>();
        private SortedList<int, MetablockFrame> _sortedFrames = new SortedList<int, MetablockFrame>();
        private int _currentFrameIndex = int.MinValue;

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

            var timer = new Timer
            {
                AutoReset = true,
                Interval = 0,
                Enabled = true
            };

            timer.Elapsed += (o, args) =>
            {
                var frameToShow = GetAllowedFrames().Where(f => f.Index > _currentFrameIndex).FirstOrDefault();
                if (frameToShow == null)
                {
                    _currentFrameIndex = int.MinValue;
                    frameToShow = GetAllowedFrames().Where(f => f.Index > _currentFrameIndex).FirstOrDefault();
                }
                if (frameToShow == null)
                {
                    timer.Interval = 1000;
                    HideAllFrames();
                }
                else
                {
                    SetFrameVisibility(frameToShow.Id);
                    _currentFrameIndex = frameToShow.Index;
                    timer.Interval = frameToShow.Duration * 1000;
                }

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

        private IEnumerable<MetablockFrame> GetAllowedFrames()
        {
            var dateTime = DateTime.Now;
            var dayOfWeek = dateTime.DayOfWeek;
            return _sortedFrames.Values.Where(f =>
                (f.UseInTimeInerval && f.UseFromTime.HasValue && f.UseToTime.HasValue && (f.UseFromTime.Value.TimeOfDay <= dateTime.TimeOfDay) && (dateTime.TimeOfDay <= f.UseToTime.Value.TimeOfDay))
                || (f.UseInDate && f.DateToUse.HasValue && f.DateToUse.Value.Date.Equals(dateTime.Date))
                || (f.UseInDayOfWeek &&
                    ((f.UseInMon && dayOfWeek.Equals(DayOfWeek.Monday))
                    || (f.UseInTue && dayOfWeek.Equals(DayOfWeek.Tuesday))
                    || (f.UseInWed && dayOfWeek.Equals(DayOfWeek.Wednesday))
                    || (f.UseInThu && dayOfWeek.Equals(DayOfWeek.Thursday))
                    || (f.UseInFri && dayOfWeek.Equals(DayOfWeek.Friday))
                    || (f.UseInSat && dayOfWeek.Equals(DayOfWeek.Saturday))
                    || (f.UseInSun && dayOfWeek.Equals(DayOfWeek.Sunday)))));
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
