﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Biaui.Internals;

namespace Biaui.NodeEditor
{
    public interface INodeItem : INotifyPropertyChanged
    {
        string Name { get; }

        bool IsSelected { get; set; }
        bool IsPreSelected { get; set; }

        Color TitleColor { get; set; }
        Point Pos { get; set; }
        Size Size { get; set; }
    }

    internal static class NodeItemExtensions
    {
        internal static bool IntersectsWith(this INodeItem self, in ImmutableRect rect)
        {
            var pos = self.Pos;
            var size = self.Size;

            return rect.IntersectsWith(pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Height);
        }
    }
}