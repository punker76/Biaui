﻿using System.Windows;
using System.Windows.Media;
using Biaui.Internals;

namespace Biaui.Controls.Internals
{
    internal interface IHasTransform
    {
        ScaleTransform Scale { get; }

        TranslateTransform Translate { get; }
    }

    internal static class HasTransformExtensions
    {
        internal static Point TransformPos(this IHasTransform self, double x, double y)
        {
            var s = self.Scale.ScaleX;

            return new Point(
                (x - self.Translate.X) / s,
                (y - self.Translate.Y) / s);
        }

        internal static ImmutableRect TransformRect(this IHasTransform self, double w, double h)
        {
            var s = self.Scale.ScaleX;

            return new ImmutableRect(
                -self.Translate.X / s,
                -self.Translate.Y / s,
                w / s,
                h / s);
        }

        internal static ImmutableRect TransformRect(this IHasTransform self, in ImmutableRect rect)
        {
            var s = self.Scale.ScaleX;

            return new ImmutableRect(
                (rect.X - self.Translate.X) / s,
                (rect.Y - self.Translate.Y) / s,
                rect.Width / s,
                rect.Height / s);
        }

        internal static void SetTransform(this IHasTransform self, double scale, double centerX, double centerY)
        {
            var d0 = self.TransformPos(centerX, centerY);

            self.Scale.ScaleX = scale;
            self.Scale.ScaleY = scale;

            var d1 = self.TransformPos(centerX, centerY);

            var diff = d1 - d0;

            self.Translate.X += diff.X * scale;
            self.Translate.Y += diff.Y * scale;
        }
    }
}