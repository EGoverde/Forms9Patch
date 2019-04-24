﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Forms9Patch
{
    /// <summary>
    /// Group header template.
    /// </summary>
    public class GroupHeaderTemplate : Xamarin.Forms.DataTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Forms9Patch.GroupHeaderTemplate"/> class.
        /// </summary>
        /// <param name="groupHeaderViewType">Group header view type.</param>
        public GroupHeaderTemplate(Type groupHeaderViewType) : base(typeof(HeaderCell<>).MakeGenericType(new[] { groupHeaderViewType }))
        {
        }

     }
}
