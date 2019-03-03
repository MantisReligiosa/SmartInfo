using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DisplayBlock = DomainObjects.Blocks;
using Media = System.Windows.Media;


namespace Display_control.Blocks.Builders
{
    public class TableBlockBuilder : AbstractBuilder
    {
        public override UIElement BuildElement(DisplayBlock.DisplayBlock displayBlock)
        {
            var tableBlock = displayBlock as DisplayBlock.TableBlock;
            var rows = tableBlock.Details.Cells.GroupBy(c => c.Row).OrderBy(r => r.Key);

            var headerStyle = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));
            foreach (var style in GetRowStyleSetters(tableBlock.Details.HeaderDetails, tableBlock.Details.FontSize, tableBlock.Details.FontName))
            {
                headerStyle.Setters.Add(style);
            }

            var rowStyle = new Style(typeof(DataGridRow));
            var oddTrigger = new Trigger()
            {
                Property = DataGridRow.AlternationIndexProperty,
                Value = 1
            };
            var evenTrigger = new Trigger()
            {
                Property = DataGridRow.AlternationIndexProperty,
                Value = 0
            };
            rowStyle.Triggers.Add(oddTrigger);
            rowStyle.Triggers.Add(evenTrigger);

            var rowHeight = (tableBlock.Height - tableBlock.Details.FontSize * tableBlock.Details.FontIndex) / (rows.Count() - 1);

            foreach (var style in GetRowStyleSetters(tableBlock.Details.OddRowDetails, tableBlock.Details.FontSize, tableBlock.Details.FontName))
            {
                oddTrigger.Setters.Add(style);
            }
            oddTrigger.Setters.Add(new Setter(FrameworkElement.HeightProperty, (double)rowHeight));

            foreach (var style in GetRowStyleSetters(tableBlock.Details.EvenRowDetails, tableBlock.Details.FontSize, tableBlock.Details.FontName))
            {
                evenTrigger.Setters.Add(style);
            }
            evenTrigger.Setters.Add(new Setter(FrameworkElement.HeightProperty, (double)rowHeight));

            var dataGrid = new DataGrid
            {
                HeadersVisibility = DataGridHeadersVisibility.Column,
                Height = tableBlock.Height,
                Width = tableBlock.Width,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderThickness = new Thickness(0),
                RowHeight = 50,
                GridLinesVisibility = DataGridGridLinesVisibility.None,
                AlternationCount = 2,
                ColumnHeaderStyle = headerStyle,
                RowStyle = rowStyle
            };
            var columnCount = tableBlock.Details.Cells.Max(c => c.Column) + 1;
            var header = rows.FirstOrDefault().OrderBy(c => c.Column).ToArray();
            for (int i = 0; i < columnCount; i++)
            {
                var name = $"Column{i}";
                dataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = header[i].Value,
                    Binding = new Binding(name),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                });
            }
            foreach (var row in rows.Skip(1))
            {
                dynamic tableRow = new ExpandoObject();
                var i = 0;
                foreach (var cell in row.OrderBy(c => c.Column))
                {
                    var name = $"Column{i}";
                    ((IDictionary<string, object>)tableRow)[name] = cell.Value;
                    i++;
                }
                dataGrid.Items.Add(tableRow);
            }

            Canvas.SetTop(dataGrid, tableBlock.Top);
            Canvas.SetLeft(dataGrid, tableBlock.Left);
            Panel.SetZIndex(dataGrid, tableBlock.ZIndex);

            return dataGrid;
        }

        private IEnumerable<Setter> GetRowStyleSetters(DisplayBlock.Details.TableBlockRowDetails rowDetails, double fontSize, string fontName)
        {
            var bc = new Media.BrushConverter();
            if (ColorConverter.TryToParseRGB(rowDetails.TextColor, out string colorHex))
            {
                yield return new Setter(Control.ForegroundProperty, (Media.Brush)bc.ConvertFrom(colorHex));
            }
            if (ColorConverter.TryToParseRGB(rowDetails.BackColor, out colorHex))
            {
                yield return new Setter(Control.BackgroundProperty, (Media.Brush)bc.ConvertFrom(colorHex));
            }
            yield return new Setter(Control.FontWeightProperty, rowDetails.Bold ? FontWeights.Bold : FontWeights.Normal);
            yield return new Setter(Control.FontStyleProperty, rowDetails.Italic ? FontStyles.Italic : FontStyles.Normal);
            yield return new Setter(Control.HorizontalContentAlignmentProperty,
                    rowDetails.Align == DisplayBlock.Align.Left ? HorizontalAlignment.Left :
                    rowDetails.Align == DisplayBlock.Align.Center ? HorizontalAlignment.Center :
                    rowDetails.Align == DisplayBlock.Align.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left);
            yield return new Setter(Control.FontSizeProperty, fontSize);
            yield return new Setter(Control.FontFamilyProperty, new Media.FontFamily(fontName));
        }
    }
}
