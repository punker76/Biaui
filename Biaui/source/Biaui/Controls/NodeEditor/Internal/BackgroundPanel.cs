﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Biaui.Controls.Internals;
using Biaui.Interfaces;
using Biaui.Internals;

namespace Biaui.Controls.NodeEditor.Internal
{
    internal class BackgroundPanel : Canvas
    {
        private readonly BiaNodeEditor _parent;

        static BackgroundPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BackgroundPanel),
                new FrameworkPropertyMetadata(typeof(BackgroundPanel)));
        }

        internal BackgroundPanel(BiaNodeEditor parent, MouseOperator mouseOperator)
        {
            _parent = parent;

            _parent.Translate.Changed += (_, __) => InvalidateVisual();
            _parent.Scale.Changed += (_, __) => InvalidateVisual();
            _parent.NodeItemMoved += (_, __) => InvalidateVisual();
            _parent.LinksSourceChanging += (_, __) => InvalidateVisual();
            _parent.LinkChanged += (_, __) => InvalidateVisual();

            mouseOperator.PreMouseLeftButtonUp += (_, __) => InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (ActualWidth <= 1 ||
                ActualHeight <= 1)
                return;

            DrawGrid(dc);

            DrawNodeLink(dc);
        }

        private readonly StreamGeometry _gridGeom = new StreamGeometry();

        private void DrawGrid(DrawingContext dc)
        {
            const double unit = 1024;

            var p = this.GetBorderPen(Color.FromRgb(0x37, 0x37, 0x40));

            var s = _parent.Scale.ScaleX;
            var tx = _parent.Translate.X;
            var ty = _parent.Translate.Y;

            var bx = FrameworkElementHelper.RoundLayoutValue(ActualWidth);
            var by = FrameworkElementHelper.RoundLayoutValue(ActualHeight);

            _gridGeom.Clear();

            var geomCtx = _gridGeom.Open();
            {
                for (var h = 0;; ++h)
                {
                    var x = (h * unit) * s + tx;

                    x = FrameworkElementHelper.RoundLayoutValue(x);

                    if (x < 0) continue;

                    if (x > ActualWidth) break;

                    geomCtx.BeginFigure(new Point(x, 0), false, false);
                    geomCtx.LineTo(new Point(x, by), true, false);
                }

                for (var h = 0;; --h)
                {
                    var x = (h * unit) * s + tx;

                    x = FrameworkElementHelper.RoundLayoutValue(x);

                    if (x > ActualWidth) continue;

                    if (x < 0) break;

                    geomCtx.BeginFigure(new Point(x, 0), false, false);
                    geomCtx.LineTo(new Point(x, by), true, false);
                }

                for (var v = 0;; ++v)
                {
                    var y = (v * unit) * s + ty;

                    y = FrameworkElementHelper.RoundLayoutValue(y);

                    if (y < 0) continue;

                    if (y > ActualHeight) break;

                    geomCtx.BeginFigure(new Point(0, y), false, false);
                    geomCtx.LineTo(new Point(bx, y), true, false);
                }

                for (var v = 0;; --v)
                {
                    var y = (v * unit) * s + ty;

                    y = FrameworkElementHelper.RoundLayoutValue(y);

                    if (y > ActualHeight) continue;

                    if (y < 0) break;

                    geomCtx.BeginFigure(new Point(0, y), false, false);
                    geomCtx.LineTo(new Point(bx, y), true, false);
                }
            }
            ((IDisposable) geomCtx).Dispose();
            dc.DrawGeometry(null, p, _gridGeom);
        }

        private static readonly
            Dictionary<(Color Color, BiaNodeLinkStyle Style), (StreamGeometry Geom, StreamGeometryContext Ctx)> _curves
                = new Dictionary<(Color, BiaNodeLinkStyle), (StreamGeometry, StreamGeometryContext)>();

        private void DrawNodeLink(DrawingContext dc)
        {
            if (_parent.LinksSource == null)
                return;

            var viewport = _parent.TransformRect(ActualWidth, ActualHeight);

            var backgroundColor = ((SolidColorBrush) _parent.Background).Color;

            var alpha = _parent.IsNodePortDragging
                ? 0.2
                : 1.0;

            Span<ImmutableVec2> work = stackalloc ImmutableVec2[10];

            foreach (var link in _parent.LinksSource)
            {
                if (link.InternalData().Port1 == null || link.InternalData().Port2 == null)
                    continue;

                var pos1 = link.ItemPort1.Item.MakePortPos(link.InternalData().Port1);
                var pos2 = link.ItemPort2.Item.MakePortPos(link.InternalData().Port2);
                var item1 = link.ItemPort1.Item;
                var item2 = link.ItemPort2.Item;

                // 大雑把にカリング
                //  --> 接続線が膨らんだ前提でバウンディングボックスを作りビューポートと判定を取る
                var (left, right) = (pos1.X, pos2.X).MinMax();
                var (top, bottom) = (pos1.Y, pos2.Y).MinMax();

                // 膨らませ量
                var w = (item1.Size.Width, item2.Size.Width).Max();
                var h = (item1.Size.Height, item2.Size.Height).Max();

                var inflateBox = new ImmutableRect(left - w, top - h, right - left + w * 2, bottom - top + h * 2);
                if (viewport.IntersectsWith(inflateBox) == false)
                    continue;

                var lines = ConnectionLineRenderer.MakeLines(ref pos1, ref pos2, item1, item2, link.InternalData(), work);

                // ラインのバウンディングボックスと判定
                var lineBox = new ImmutableRect(lines);
                if (viewport.IntersectsWith(lineBox) == false)
                    continue;

                // 
                var color = ColorHelper.Lerp(alpha, backgroundColor, link.Color);
                var key = (color, link.Style);

                if (_curves.TryGetValue(key, out var curve) == false)
                {
                    var geom = new StreamGeometry
                    {
                        FillRule = FillRule.Nonzero
                    };
                    var ctx = geom.Open();

                    curve = (geom, ctx);
                    _curves.Add(key, curve);
                }

                ConnectionLineRenderer.DrawLines(curve.Ctx, lines);

#if false
                // 矢印
                if ((link.Style & BiaNodeLinkStyle.Arrow) != 0)
                    DrawArrow(curve.Ctx, in pos1, in pos12, in pos21, in pos2);
#endif
            }

            dc.PushTransform(_parent.Translate);
            dc.PushTransform(_parent.Scale);
            {
                foreach (var c in _curves)
                {
                    var pen =
                        (c.Key.Style & BiaNodeLinkStyle.DashedLine) != 0
                            ? Caches.GetDashedPen(c.Key.Color, 3)
                            : Caches.GetPen(c.Key.Color, 3);

                    ((IDisposable) c.Value.Ctx).Dispose();
                    dc.DrawGeometry(Caches.GetSolidColorBrush(c.Key.Color), pen, c.Value.Geom);
                }
            }
            dc.Pop();
            dc.Pop();

            _curves.Clear();
        }

        private static void DrawArrow(
            StreamGeometryContext ctx,
            in Point pos1, in Point pos12, in Point pos21, in Point pos2)
        {
            var p1 = BiaNodeEditorHelper.InterpolationBezier(pos1, pos12, pos21, pos2, 0.50);
            var p2 = BiaNodeEditorHelper.InterpolationBezier(pos1, pos12, pos21, pos2, 0.45);

            const double size = 20;
            var pv = ImmutableVec2.SetSize(p1 - p2, size);
            var sv = new ImmutableVec2(-pv.Y / 1.732, pv.X / 1.732);

            var t1 = p1 + pv;
            var t2 = p1 + sv;
            var t3 = p1 - sv;

            ctx.DrawTriangle(
                Unsafe.As<ImmutableVec2, Point>(ref t1),
                Unsafe.As<ImmutableVec2, Point>(ref t2),
                Unsafe.As<ImmutableVec2, Point>(ref t3),
                false, false);
        }
    }
}