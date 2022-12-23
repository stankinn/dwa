// -- FILE ------------------------------------------------------------------
// created    : Jani Giannoudis - 2008.03.27
// copyright  : (c) 2008-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2019.GridViewRangeColumn
{
    public sealed class RangeColumn : LayoutColumn
    {
        public static readonly DependencyProperty MinWidthProperty =
            DependencyProperty.RegisterAttached(
                "MinWidth",
                typeof(double),
                typeof(RangeColumn));

        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.RegisterAttached(
                "MaxWidth",
                typeof(double),
                typeof(RangeColumn));

        public static readonly DependencyProperty IsFillColumnProperty =
            DependencyProperty.RegisterAttached(
                "IsFillColumn",
                typeof(bool),
                typeof(RangeColumn));

        public static double GetMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MinWidthProperty);
        } 

        public static void SetMinWidth(DependencyObject obj, double minWidth)
        {
            obj.SetValue(MinWidthProperty, minWidth);
        } 

        public static double GetMaxWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MaxWidthProperty);
        } 

        public static void SetMaxWidth(DependencyObject obj, double maxWidth)
        {
            obj.SetValue(MaxWidthProperty, maxWidth);
        } 

        public static bool IsRangeColumn(GridViewColumn column)
        {
            if (column == null)
            {
                return false;
            }
            return
                HasPropertyValue(column, MinWidthProperty) ||
                HasPropertyValue(column, MaxWidthProperty) ||
                HasPropertyValue(column, IsFillColumnProperty);
        } 

        public static double? GetRangeMinWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, MinWidthProperty);
        } 

        public static double? GetRangeMaxWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, MaxWidthProperty);
        } 

        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double minWidth,
            double width, double maxWidth)
        {
            return ApplyWidth(gridViewColumn, minWidth, width, maxWidth, false);
        } 
        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double minWidth,
            double width, double maxWidth, bool isFillColumn)
        {
            SetMinWidth(gridViewColumn, minWidth);
            gridViewColumn.Width = width;
            SetMaxWidth(gridViewColumn, maxWidth);
            return gridViewColumn;
        } 
    } 
}
