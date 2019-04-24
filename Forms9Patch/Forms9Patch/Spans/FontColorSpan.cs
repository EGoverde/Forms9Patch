﻿using Xamarin.Forms;
using P42.Utils;

namespace Forms9Patch
{
	/// <summary>
	/// Forms9Patch Font color span.
	/// </summary>
	class FontColorSpan : Span, ICopiable<FontColorSpan>
	{
		internal const string SpanKey = "FontColor";

		Color _color;
		/// <summary>
		/// Gets or sets the font foreground color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color {
			get => _color; 
			set {
				if (_color == value)
					return;
				_color = value;
				OnPropertyChanged (Key);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Forms9Patch.FontColorSpan"/> class.
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		/// <param name="color">Color.</param>
		public FontColorSpan (int start, int end, Color color) : base (start, end) {
			Key = SpanKey;
			_color = color;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Forms9Patch.FontColorSpan"/> class.
		/// </summary>
		/// <param name="span">Span.</param>
		public FontColorSpan(FontColorSpan span) : this (span.Start, span.End, span.Color) {
		}

		public void PropertiesFrom(FontColorSpan source)
		{
			base.PropertiesFrom(source);
			Color = source.Color;
		}

		public override Span Copy()
		{
			return new FontColorSpan(Start, End, Color);
		}
	}
}

