﻿
// ReSharper disable All
// <auto-generated />

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Biaui.Internals
{
    public readonly struct ImmutableRect_float : IEquatable<ImmutableRect_float>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;

        public bool HasArea => Width > (float)0 && Height > (float)0;

        public float Left => X;
        public float Top => Y;
        public float Right => X + Width;
        public float Bottom => Y + Height;

        public ImmutableVec2_float TopLeft => new ImmutableVec2_float(Left, Top);
        public ImmutableVec2_float TopRight => new ImmutableVec2_float(Right, Top);
        public ImmutableVec2_float BottomLeft => new ImmutableVec2_float(Left, Bottom);
        public ImmutableVec2_float BottomRight => new ImmutableVec2_float(Right, Bottom);

        public ImmutableRect_float(float x, float y, float width, float height)
            => (X, Y, Width, Height) = (x, y, width, height);

        public ImmutableRect_float(Point pos, Size size)
            => (X, Y, Width, Height) = ((float)pos.X, (float)pos.Y, (float)size.Width, (float)size.Height);

        public ImmutableRect_float(Point pos0, Point pos1)
        {
            var (minX, maxX) = (pos0.X, pos1.X).MinMax();
            var (minY, maxY) = (pos0.Y, pos1.Y).MinMax();

            (X, Y, Width, Height) = (
                (float)minX,
                (float)minY,
                (float)(maxX - minX),
                (float)(maxY - minY)
            );
        }

        public ImmutableRect_float(in ImmutableVec2_float pos0, in ImmutableVec2_float pos1)
        {
            var (minX, maxX) = (pos0.X, pos1.X).MinMax();
            var (minY, maxY) = (pos0.Y, pos1.Y).MinMax();

            (X, Y, Width, Height) = (
                (float)minX,
                (float)minY,
                (float)(maxX - minX),
                (float)(maxY - minY)
            );
        }

        public ImmutableRect_float(Span<Point> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = ((float)0, (float)0, (float)0, (float)0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                (float)minX,
                (float)minY,
                (float)(maxX - minX),
                (float)(maxY - minY)
            );
        }

        public ImmutableRect_float(ReadOnlySpan<Point> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = (0, 0, 0, 0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                (float)minX,
                (float)minY,
                (float)(maxX - minX),
                (float)(maxY - minY)
            );
        }

        public ImmutableRect_float(IEnumerable<Point> points)
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            var isAny = false;

            foreach (var point in points)
            {
                isAny = true;

                minX = (minX, point.X).Min();
                maxX = (maxX, point.X).Max();

                minY = (minY, point.Y).Min();
                maxY = (maxY, point.Y).Max();
            }

            if (isAny)
                (X, Y, Width, Height) = (
                    (float)minX,
                    (float)minY,
                    (float)(maxX - minX),
                    (float)(maxY - minY)
                );
            else
                (X, Y, Width, Height) = ((float)0, (float)0, (float)0, (float)0);
        }

        public ImmutableRect_float(Span<ImmutableVec2_float> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = ((float)0, (float)0, (float)0, (float)0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        public ImmutableRect_float(ReadOnlySpan<ImmutableVec2_float> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = ((float)0, (float)0, (float)0, (float)0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        public ImmutableRect_float(IEnumerable<ImmutableVec2_float> points)
        {
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;

            var isAny = false;

            foreach (var point in points)
            {
                isAny = true;

                minX = (minX, point.X).Min();
                maxX = (maxX, point.X).Max();

                minY = (minY, point.Y).Min();
                maxY = (maxY, point.Y).Max();
            }

            if (isAny)
                (X, Y, Width, Height) = (
                    minX,
                    minY,
                    maxX - minX,
                    maxY - minY
                );
            else
                (X, Y, Width, Height) = ((float)0, (float)0, (float)0, (float)0);
        }

        public enum CtorPoint4
        {
        };

        public ImmutableRect_float(ReadOnlySpan<Point> points, CtorPoint4 _)
        {
            var minX = (points[0].X, points[1].X, points[2].X, points[3].X).Min();
            var maxX = (points[0].X, points[1].X, points[2].X, points[3].X).Max();
            var minY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Min();
            var maxY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Max();

            (X, Y, Width, Height) = (
                (float)minX,
                (float)minY,
                (float)(maxX - minX),
                (float)(maxY - minY)
            );
        }

        public ImmutableRect_float(ReadOnlySpan<ImmutableVec2_float> points, CtorPoint4 _)
        {
            var minX = (points[0].X, points[1].X, points[2].X, points[3].X).Min();
            var maxX = (points[0].X, points[1].X, points[2].X, points[3].X).Max();
            var minY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Min();
            var maxY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Max();

            (X, Y, Width, Height) = (
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectsWith(in ImmutableRect_float target)
        {
            var right = X + Width;
            var bottom = Y + Height;
            var targetRight = target.X + target.Width;
            var targetBottom = target.Y + target.Height;

            return target.X <= right && targetRight >= X && target.Y <= bottom && targetBottom >= Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectsWith(in ImmutableVec2_float target)
        {
            var right = X + Width;
            var bottom = Y + Height;

            return target.X >= X &&
                   target.X < right &&
                   target.Y >= bottom &&
                   target.Y < bottom;
        }

        public bool IntersectsWith(in ImmutableCircle_float target)
        {
            var right = X + Width;
            var bottom = Y + Height;

            if (target.CenterX > X &&
                target.CenterX < right &&
                target.CenterY > Y - target.Radius &&
                target.CenterY < bottom + target.Radius)
                return true;

            if (target.CenterX > X - target.Radius &&
                target.CenterX < right + target.Radius &&
                target.CenterY > Y &&
                target.CenterY < bottom)
                return true;

            var rr = target.Radius * target.Radius;

            var xx1 = (X - target.CenterX) * (X - target.CenterX);
            var yy1 = (Y - target.CenterY) * (Y - target.CenterY);

            var xx2 = (right - target.CenterX) * (right - target.CenterX);
            var yy2 = (bottom - target.CenterY) * (bottom - target.CenterY);

            if (xx1 + yy1 < rr)
                return true;

            if (xx2 + yy1 < rr)
                return true;

            if (xx1 + yy2 < rr)
                return true;

            if (xx2 + yy2 < rr)
                return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point p)
            => p.X >= X &&
               p.X < X + Width &&
               p.Y >= Y &&
               p.Y < Y + Height;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in ImmutableVec2_float p)
            => p.X >= X &&
               p.X < X + Width &&
               p.Y >= Y &&
               p.Y < Y + Height;

        public static bool operator ==(in ImmutableRect_float source1, in ImmutableRect_float source2)
            => source1.X == source2.X &&
               source1.Y == source2.Y &&
               source1.Width == source2.Width &&
               source1.Height == source2.Height;

        public static bool operator !=(in ImmutableRect_float source1, in ImmutableRect_float source2)
            => !(source1 == source2);

        public bool Equals(ImmutableRect_float other)
            => this == other;

        public override bool Equals(object? obj)
        {
            if (obj is ImmutableRect_float other)
                return this == other;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => HashCodeMaker.Make(X, Y, Width, Height);
    }
    public readonly struct ImmutableRect_double : IEquatable<ImmutableRect_double>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Width;
        public readonly double Height;

        public bool HasArea => Width > (double)0 && Height > (double)0;

        public double Left => X;
        public double Top => Y;
        public double Right => X + Width;
        public double Bottom => Y + Height;

        public ImmutableVec2_double TopLeft => new ImmutableVec2_double(Left, Top);
        public ImmutableVec2_double TopRight => new ImmutableVec2_double(Right, Top);
        public ImmutableVec2_double BottomLeft => new ImmutableVec2_double(Left, Bottom);
        public ImmutableVec2_double BottomRight => new ImmutableVec2_double(Right, Bottom);

        public ImmutableRect_double(double x, double y, double width, double height)
            => (X, Y, Width, Height) = (x, y, width, height);

        public ImmutableRect_double(Point pos, Size size)
            => (X, Y, Width, Height) = ((double)pos.X, (double)pos.Y, (double)size.Width, (double)size.Height);

        public ImmutableRect_double(Point pos0, Point pos1)
        {
            var (minX, maxX) = (pos0.X, pos1.X).MinMax();
            var (minY, maxY) = (pos0.Y, pos1.Y).MinMax();

            (X, Y, Width, Height) = (
                (double)minX,
                (double)minY,
                (double)(maxX - minX),
                (double)(maxY - minY)
            );
        }

        public ImmutableRect_double(in ImmutableVec2_double pos0, in ImmutableVec2_double pos1)
        {
            var (minX, maxX) = (pos0.X, pos1.X).MinMax();
            var (minY, maxY) = (pos0.Y, pos1.Y).MinMax();

            (X, Y, Width, Height) = (
                (double)minX,
                (double)minY,
                (double)(maxX - minX),
                (double)(maxY - minY)
            );
        }

        public ImmutableRect_double(Span<Point> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = ((double)0, (double)0, (double)0, (double)0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                (double)minX,
                (double)minY,
                (double)(maxX - minX),
                (double)(maxY - minY)
            );
        }

        public ImmutableRect_double(ReadOnlySpan<Point> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = (0, 0, 0, 0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                (double)minX,
                (double)minY,
                (double)(maxX - minX),
                (double)(maxY - minY)
            );
        }

        public ImmutableRect_double(IEnumerable<Point> points)
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            var isAny = false;

            foreach (var point in points)
            {
                isAny = true;

                minX = (minX, point.X).Min();
                maxX = (maxX, point.X).Max();

                minY = (minY, point.Y).Min();
                maxY = (maxY, point.Y).Max();
            }

            if (isAny)
                (X, Y, Width, Height) = (
                    (double)minX,
                    (double)minY,
                    (double)(maxX - minX),
                    (double)(maxY - minY)
                );
            else
                (X, Y, Width, Height) = ((double)0, (double)0, (double)0, (double)0);
        }

        public ImmutableRect_double(Span<ImmutableVec2_double> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = ((double)0, (double)0, (double)0, (double)0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        public ImmutableRect_double(ReadOnlySpan<ImmutableVec2_double> points)
        {
            if (points.Length == 0)
            {
                (X, Y, Width, Height) = ((double)0, (double)0, (double)0, (double)0);
                return;
            }

            var minX = points[0].X;
            var minY = points[0].Y;
            var maxX = points[0].X;
            var maxY = points[0].Y;

            for (var i = 1; i < points.Length; ++i)
            {
                minX = (minX, points[i].X).Min();
                maxX = (maxX, points[i].X).Max();

                minY = (minY, points[i].Y).Min();
                maxY = (maxY, points[i].Y).Max();
            }

            (X, Y, Width, Height) = (
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        public ImmutableRect_double(IEnumerable<ImmutableVec2_double> points)
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            var isAny = false;

            foreach (var point in points)
            {
                isAny = true;

                minX = (minX, point.X).Min();
                maxX = (maxX, point.X).Max();

                minY = (minY, point.Y).Min();
                maxY = (maxY, point.Y).Max();
            }

            if (isAny)
                (X, Y, Width, Height) = (
                    minX,
                    minY,
                    maxX - minX,
                    maxY - minY
                );
            else
                (X, Y, Width, Height) = ((double)0, (double)0, (double)0, (double)0);
        }

        public enum CtorPoint4
        {
        };

        public ImmutableRect_double(ReadOnlySpan<Point> points, CtorPoint4 _)
        {
            var minX = (points[0].X, points[1].X, points[2].X, points[3].X).Min();
            var maxX = (points[0].X, points[1].X, points[2].X, points[3].X).Max();
            var minY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Min();
            var maxY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Max();

            (X, Y, Width, Height) = (
                (double)minX,
                (double)minY,
                (double)(maxX - minX),
                (double)(maxY - minY)
            );
        }

        public ImmutableRect_double(ReadOnlySpan<ImmutableVec2_double> points, CtorPoint4 _)
        {
            var minX = (points[0].X, points[1].X, points[2].X, points[3].X).Min();
            var maxX = (points[0].X, points[1].X, points[2].X, points[3].X).Max();
            var minY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Min();
            var maxY = (points[0].Y, points[1].Y, points[2].Y, points[3].Y).Max();

            (X, Y, Width, Height) = (
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectsWith(in ImmutableRect_double target)
        {
            var right = X + Width;
            var bottom = Y + Height;
            var targetRight = target.X + target.Width;
            var targetBottom = target.Y + target.Height;

            return target.X <= right && targetRight >= X && target.Y <= bottom && targetBottom >= Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectsWith(in ImmutableVec2_double target)
        {
            var right = X + Width;
            var bottom = Y + Height;

            return target.X >= X &&
                   target.X < right &&
                   target.Y >= bottom &&
                   target.Y < bottom;
        }

        public bool IntersectsWith(in ImmutableCircle_double target)
        {
            var right = X + Width;
            var bottom = Y + Height;

            if (target.CenterX > X &&
                target.CenterX < right &&
                target.CenterY > Y - target.Radius &&
                target.CenterY < bottom + target.Radius)
                return true;

            if (target.CenterX > X - target.Radius &&
                target.CenterX < right + target.Radius &&
                target.CenterY > Y &&
                target.CenterY < bottom)
                return true;

            var rr = target.Radius * target.Radius;

            var xx1 = (X - target.CenterX) * (X - target.CenterX);
            var yy1 = (Y - target.CenterY) * (Y - target.CenterY);

            var xx2 = (right - target.CenterX) * (right - target.CenterX);
            var yy2 = (bottom - target.CenterY) * (bottom - target.CenterY);

            if (xx1 + yy1 < rr)
                return true;

            if (xx2 + yy1 < rr)
                return true;

            if (xx1 + yy2 < rr)
                return true;

            if (xx2 + yy2 < rr)
                return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point p)
            => p.X >= X &&
               p.X < X + Width &&
               p.Y >= Y &&
               p.Y < Y + Height;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in ImmutableVec2_double p)
            => p.X >= X &&
               p.X < X + Width &&
               p.Y >= Y &&
               p.Y < Y + Height;

        public static bool operator ==(in ImmutableRect_double source1, in ImmutableRect_double source2)
            => source1.X == source2.X &&
               source1.Y == source2.Y &&
               source1.Width == source2.Width &&
               source1.Height == source2.Height;

        public static bool operator !=(in ImmutableRect_double source1, in ImmutableRect_double source2)
            => !(source1 == source2);

        public bool Equals(ImmutableRect_double other)
            => this == other;

        public override bool Equals(object? obj)
        {
            if (obj is ImmutableRect_double other)
                return this == other;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => HashCodeMaker.Make(X, Y, Width, Height);
    }

}

