using DomainObjects.Blocks;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace SmartInfo.Blocks.Builders
{
    public class ScenarioBuilder : AbstractBuilder
    {
        public override UIElement BuildElement(DisplayBlock displayBlock)
        {
            var blocks = new List<UIElement>();
            var currentFrameIndex = int.MinValue;
            var metablockScheduler = new ScenarioScheduler();
            var metablock = displayBlock as Scenario;
            var canvas = new Canvas
            {
                Height = metablock.Height,
                Width = metablock.Width
            };

            var blockBuilder = new BlockBuilder();

            foreach (var block in metablock.Details.Scenes.SelectMany(f => f?.Blocks ?? new List<DisplayBlock>()))
            {
                var element = blockBuilder.BuildElement(block);
                if (element != null)
                {
                    element.Uid = block.SceneId.ToString();
                    blocks.Add(element);
                    canvas.Children.Add(element);
                }
            }

            metablockScheduler.Scenes = metablock.Details.Scenes.OrderByDescending(f => f.Index).Reverse().ToList();

            var timer = new Timer
            {
                AutoReset = true,
                Interval = 1,
                Enabled = true
            };


            timer.Elapsed += (o, args) =>
            {
                var currentDateTime = DateTime.Now;
                var frameToShow = metablockScheduler.GetNextScene(currentDateTime, currentFrameIndex);
                if (frameToShow == null)
                {
                    currentFrameIndex = int.MinValue;
                    timer.Interval = 1000;
                    HideAllFrames(blocks);
                }
                else
                {
                    HideAllFrames(blocks);
                    SetFrameVisibility(frameToShow.Id, blocks);
                    currentFrameIndex = frameToShow.Index;
                    timer.Interval = frameToShow.Duration * 1000;
                }
            };

            Canvas.SetTop(canvas, metablock.Top);
            Canvas.SetLeft(canvas, metablock.Left);
            Panel.SetZIndex(canvas, metablock.ZIndex);
            return canvas;
        }

        private void HideAllFrames(List<UIElement> elements)
        {
            _dispatcher.Invoke(() =>
            {
                foreach (var block in elements)
                {
                    block.Visibility = Visibility.Collapsed;
                }
            });
        }

        private void SetFrameVisibility(int frameId, List<UIElement> elements)
        {
            _dispatcher.Invoke(() =>
            {
                foreach (var block in elements)
                {
                    var isVisible = block.Uid.Equals(frameId.ToString());
                    block.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                }
            });
        }
    }
}
