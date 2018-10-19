﻿using Xamarin.Forms;

namespace Forms9Patch
{
    /// <summary>
    /// A simple toast that points to an element
    /// </summary>
    public class TargetedToast : BubblePopup
    {
        #region Factory
        /// <summary>
        /// Create the specified target, title and text.
        /// </summary>
        /// <param name="target">Target.</param>
        /// <param name="title">Title.</param>
        /// <param name="text">Text.</param>
        public static TargetedToast Create(VisualElement target, string title, string text)
        {
            var toast = new TargetedToast(target) { Title = title, Text = text };
            toast.IsVisible = true;
            return toast;
        }
        #endregion


        #region Properties
        /// <summary>
        /// The title property backing store.
        /// </summary>
        public static readonly new BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(TargetedToast), default(string));
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public new string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// The text property backing store.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(TargetedToast), default(string));
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(TargetedToast), Color.Black);
        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }


        /// <summary>
        /// The ok text property.
        /// </summary>
        public static readonly BindableProperty OkTextProperty = BindableProperty.Create("OkText", typeof(string), typeof(TargetedToast), "OK");
        /// <summary>
        /// Gets or sets the ok text.
        /// </summary>
        /// <value>The ok text.</value>
        public string OkText
        {
            get => (string)GetValue(OkTextProperty);
            set => SetValue(OkTextProperty, value);
        }

        /// <summary>
        /// The ok button color property.
        /// </summary>
        public static readonly BindableProperty OkButtonColorProperty = BindableProperty.Create("OkButtonColor", typeof(Color), typeof(TargetedToast), default(Color));
        /// <summary>
        /// Gets or sets the color of the ok button.
        /// </summary>
        /// <value>The color of the ok button.</value>
        public Color OkButtonColor
        {
            get => (Color)GetValue(OkButtonColorProperty);
            set => SetValue(OkButtonColorProperty, value);
        }

        /// <summary>
        /// The ok text color property.
        /// </summary>
        public static readonly BindableProperty OkTextColorProperty = BindableProperty.Create("OkTextColor", typeof(Color), typeof(TargetedToast), Color.Blue);
        /// <summary>
        /// Gets or sets the color of the ok text.
        /// </summary>
        /// <value>The color of the ok text.</value>
        public Color OkTextColor
        {
            get => (Color)GetValue(OkTextColorProperty);
            set => SetValue(OkTextColorProperty, value);
        }

        #endregion


        #region Fields 
        readonly Label _titleLabel = new Label
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.Black
        };
        readonly Label _textLabel = new Label
        {
            FontSize = 16,
            TextColor = Color.Black
            //Lines = 0,
            //VerticalOptions = LayoutOptions.Fill,
        };
        readonly Button _okButton = new Button
        {
            //HorizontalOptions = LayoutOptions.Fill
        };
        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Forms9Patch.TargetedToast"/> class.
        /// </summary>
        /// <param name="target">Target.</param>
        public TargetedToast(VisualElement target) : base(target)
        {
            _okButton.BackgroundColor = OkButtonColor;
            _okButton.TextColor = OkTextColor;
            _okButton.HtmlText = OkText;

            _okButton.Tapped += (s, args) => Cancel();
            Content = new StackLayout
            {
                Children =
                {
                    _titleLabel,
                    new ScrollView
                    {
                        Content = _textLabel
                    },
                }
            };
        }
        #endregion


        #region Property Change Management
        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (P42.Utils.Environment.IsOnMainThread)
                base.OnPropertyChanged(propertyName);
            else
                Device.BeginInvokeOnMainThread(() => base.OnPropertyChanged(propertyName));

            if (propertyName == TitleProperty.PropertyName)
                _titleLabel.HtmlText = Title;
            else if (propertyName == TextProperty.PropertyName)
                _textLabel.HtmlText = Text;
            else if (propertyName == OkTextProperty.PropertyName)
            {
                _okButton.HtmlText = OkText;
                if (!string.IsNullOrWhiteSpace(_okButton.HtmlText) && !((StackLayout)Content).Children.Contains(_okButton))
                    ((StackLayout)Content).Children.Add(_okButton);
                else if (string.IsNullOrWhiteSpace(_okButton.HtmlText) && ((StackLayout)Content).Children.Contains(_okButton))
                    ((StackLayout)Content).Children.Remove(_okButton);
            }
            else if (propertyName == OkButtonColorProperty.PropertyName)
                _okButton.BackgroundColor = OkButtonColor;
            else if (propertyName == OkTextColorProperty.PropertyName)
                _okButton.TextColor = OkTextColor;
            else if (propertyName == TextColorProperty.PropertyName)
            {
                _titleLabel.TextColor = TextColor;
                _textLabel.TextColor = TextColor;
            }
        }
        #endregion
    }
}
