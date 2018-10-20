﻿using System.Collections;
using Xamarin.Forms;
using System;

namespace Forms9Patch
{
    /// <summary>
    /// Single picker.
    /// </summary>
    public class SinglePicker : Frame
    {
        #region Properties
        /// <summary>
        /// The item templates property.
        /// </summary>
        public static readonly BindableProperty ItemTemplatesProperty = BindableProperty.Create("ItemTemplates", typeof(DataTemplateSelector), typeof(SinglePicker), null);
        /// <summary>
        /// Gets the item templates.
        /// </summary>
        /// <value>The item templates.</value>
        public DataTemplateSelector ItemTemplates
        {
            get => (DataTemplateSelector)GetValue(ItemTemplatesProperty);
            private set => SetValue(ItemTemplatesProperty, value);
        }

        /// <summary>
        /// The items source property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList), typeof(SinglePicker), null);
        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The row height property.
        /// </summary>
        public static readonly BindableProperty RowHeightProperty = BindableProperty.Create("RowHeight", typeof(int), typeof(SinglePicker), 30);
        /// <summary>
        /// Gets or sets the height of the row.
        /// </summary>
        /// <value>The height of the row.</value>
        public int RowHeight
        {
            get => (int)GetValue(RowHeightProperty);
            set => SetValue(RowHeightProperty, value);
        }

        /// <summary>
        /// The index property.
        /// </summary>
        public static readonly BindableProperty IndexProperty = BindableProperty.Create("Index", typeof(int), typeof(SinglePicker), 0);
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get => (int)GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }

        /// <summary>
        /// The selected item property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(SinglePicker), null);
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        #endregion

        #region Fields
        readonly internal BasePicker _basePicker = new BasePicker
        {
            BackgroundColor = Color.Transparent
        };
        //readonly internal Xamarin.Forms.AbsoluteLayout _absLayout = new Xamarin.Forms.AbsoluteLayout();
        //readonly internal Xamarin.Forms.RelativeLayout _relLayout = new Xamarin.Forms.RelativeLayout();
        readonly internal ManualLayout _manLayout = new ManualLayout();

        readonly internal Color _overlayColor = Color.FromRgb(0.85, 0.85, 0.85);

        readonly internal ColorGradientBox _upperGradient = new ColorGradientBox
        {
            Orientation = StackOrientation.Vertical
        };

        readonly internal ColorGradientBox _lowerGradient = new ColorGradientBox
        {
            Orientation = StackOrientation.Vertical
        };

        readonly internal BoxView _upperEdge = new BoxView
        {
            BackgroundColor = Color.Gray
        };
        readonly internal BoxView _lowerEdge = new BoxView
        {
            BackgroundColor = Color.Gray
        };

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Forms9Patch.SinglePicker"/> class.
        /// </summary>
        public SinglePicker()
        {
            Padding = new Thickness(0, 0, 0, 0);

            _basePicker._listView.SelectedCellBackgroundColor = Color.Transparent;

            _upperGradient.StartColor = _overlayColor;
            _upperGradient.EndColor = _overlayColor.WithAlpha(0.5);
            _lowerGradient.StartColor = _overlayColor.WithAlpha(0.5);
            _lowerGradient.EndColor = _overlayColor;

            _manLayout.Children.Add(_upperEdge);
            _manLayout.Children.Add(_lowerEdge);
            _manLayout.Children.Add(_basePicker);
            _manLayout.Children.Add(_upperGradient);
            _manLayout.Children.Add(_lowerGradient);
            _manLayout.LayoutChildrenEvent += OnManualLayoutChildren;

            _basePicker.SelectBy = SelectBy.Position;

            _basePicker.RowHeight = RowHeight;

            VerticalOptions = LayoutOptions.FillAndExpand;

            Content = _manLayout;

            _basePicker.PropertyChanged += BasePickerPropertyChanged;
        }

        #region change management
        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (!P42.Utils.Environment.IsOnMainThread)
            {
                Device.BeginInvokeOnMainThread(() => OnPropertyChanged(propertyName));
                return;
            }

            base.OnPropertyChanged(propertyName);

            if (propertyName == RowHeightProperty.PropertyName)
                OnManualLayoutChildren(this, new ManualLayoutEventArgs(X, Y, Width, Height));
            else if (propertyName == ItemsSourceProperty.PropertyName)
                _basePicker.ItemsSource = ItemsSource;
            else if (propertyName == RowHeightProperty.PropertyName)
                _basePicker.RowHeight = RowHeight;
            else if (propertyName == IndexProperty.PropertyName)
                _basePicker.Index = Index;
            else if (propertyName == SelectedItemProperty.PropertyName)
                _basePicker.SelectedItem = SelectedItem;
        }

        void OnManualLayoutChildren(object sender, ManualLayoutEventArgs e)
        {
            if (Height > 0 && Width > 0)
            {
                //System.Diagnostics.Debug.WriteLine("SinglePicker.OnManualLayoutChildren");
                double overlayHeight = (Height - RowHeight) / 2.0;
                LayoutChildIntoBoundingRegion(_basePicker, new Rectangle(e.X, e.Y, e.Width, e.Height));
                LayoutChildIntoBoundingRegion(_upperGradient, new Rectangle(e.X, e.Y, e.Width, overlayHeight));
                LayoutChildIntoBoundingRegion(_lowerGradient, new Rectangle(e.X, e.Height - overlayHeight, e.Width, overlayHeight));
                if (_manLayout.Children.Contains(_lowerEdge))
                    LayoutChildIntoBoundingRegion(_lowerEdge, new Rectangle(e.X, overlayHeight, e.Width, 1.0));
                if (_manLayout.Children.Contains(_upperEdge))
                    LayoutChildIntoBoundingRegion(_upperEdge, new Rectangle(e.X, e.Height - overlayHeight, e.Width, 1.0));
            }
        }
        #endregion

        /// <summary>
        /// Reset this instance.
        /// </summary>
        public void Reset()
        {
            SelectedItem = null;
        }

        void BasePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == BasePicker.SelectedItemProperty.PropertyName)
            {
                SelectedItem = _basePicker.SelectedItem;
                //_basePicker.SelectedItem = null;
                //_basePicker._listView.SelectedItem = null;
            }
            else if (e.PropertyName == BasePicker.IndexProperty.PropertyName)
                Index = _basePicker.Index;
        }
    }
}

