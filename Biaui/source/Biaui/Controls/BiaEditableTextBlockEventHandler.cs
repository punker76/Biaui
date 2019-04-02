﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Biaui.Internals;

namespace Biaui.Controls
{
    public partial class BiaEditableTextBlockEventHandler
    {
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox) sender;

            textBox.CaptureMouse();
            textBox.Focus();
        }

        private string _startText;

        private void TextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var parent = ((TextBlock)sender).GetParent<BiaEditableTextBlock>();

            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                parent.IsEditing = true;
                _startText = parent.Text;
            }

            var listItem = parent.GetParent<ListBoxItem>();
            if (listItem != null)
                listItem.IsSelected = true;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox) sender;
            var parent = textBox.GetParent<BiaEditableTextBlock>();

            FinishEditing(parent, textBox);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox) sender;
            var parent = textBox.GetParent<BiaEditableTextBlock>();

            switch (e.Key)
            {
                case Key.Tab:
                    parent.Text = textBox.Text;
                    FinishEditing(parent, textBox);
                    break;

                case Key.Return:
                    parent.Text = textBox.Text;
                    FinishEditing(parent, textBox);
                    break;

                case Key.Escape:
                    parent.Text = _startText;
                    FinishEditing(parent, textBox);
                    break;
            }
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = (TextBox) sender;

            // 自コントロール上であれば、終了させない
            var pos = e.GetPosition(textBox);
            var rect = textBox.RoundLayoutActualRectangle(false);
            if (rect.Contains(pos))
                return;

            var parent = textBox.GetParent<BiaEditableTextBlock>();
            FinishEditing(parent, textBox);
        }

        private static void FinishEditing(BiaEditableTextBlock parent, TextBox textBox)
        {
            textBox.ReleaseMouseCapture();

            parent.IsEditing = false;
        }
    }
}