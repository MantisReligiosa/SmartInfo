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
            var scenarioScheduler = new ScenarioScheduler();
            var scenario = displayBlock as Scenario;
            var canvas = new Canvas
            {
                Height = scenario.Height,
                Width = scenario.Width
            };

            var blockBuilder = new BlockBuilder();

            foreach (var block in scenario.Details.Scenes.SelectMany(f => f?.Blocks ?? new List<DisplayBlock>()))
            {
                var element = blockBuilder.BuildElement(block);
                if (element != null)
                {
                    element.Uid = block.SceneId.ToString();
                    blocks.Add(element);
                    canvas.Children.Add(element);
                }
            }

            scenarioScheduler.Scenes = scenario.Details.Scenes.OrderByDescending(f => f.Index).Reverse().ToList();

            var timer = new Timer
            {
                AutoReset = true,
                Interval = 1,
                Enabled = true
            };


            timer.Elapsed += (o, args) =>
            {
                timer.Stop();
                var currentDateTime = DateTime.Now;
                var frameToShow = scenarioScheduler.GetNextScene(currentDateTime, currentFrameIndex);
                if (frameToShow == null)
                {
                    currentFrameIndex = int.MinValue;
                    timer.Interval = 1000;
                    HideAllFrames(blocks);
                }
                else
                {
                    if (currentFrameIndex != frameToShow.Index)
                    {
                        HideAllFrames(blocks);
                        SetFrameVisibility(frameToShow.Id, blocks);
                        currentFrameIndex = frameToShow.Index;
                    }
                    timer.Interval = frameToShow.Duration * 1000;
                }
                timer.Start();
            };

            Canvas.SetTop(canvas, scenario.Top);
            Canvas.SetLeft(canvas, scenario.Left);
            Panel.SetZIndex(canvas, scenario.ZIndex);
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
