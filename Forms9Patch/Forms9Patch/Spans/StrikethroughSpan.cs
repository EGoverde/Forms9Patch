﻿using P42.Utils;

namespace Forms9Patch
{
	/// <summary>
	/// Forms9Patch Strikethrough span.
	/// </summary>
	class StrikethroughSpan : Span, ICopiable<StrikethroughSpan>
	{
		internal const string SpanKey = "Strikethrough";

		/// <summary>
		/// Initializes a new instance of the <see cref="Forms9Patch.StrikethroughSpan"/> class.
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		public StrikethroughSpan (int start, int end) : base (start, end) {
			Key = SpanKey;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Forms9Patch.StrikethroughSpan"/> class.
		/// </summary>
		/// <param name="span">Span.</param>
		public StrikethroughSpan (StrikethroughSpan span) : this (span.Start, span.End) {
		}

		public void PropertiesFrom(StrikethroughSpan source)
		{
			base.PropertiesFrom(source);
		}

		public override Span Copy()
		{
			return new StrikethroughSpan(Start, End);
		}
	}
}




