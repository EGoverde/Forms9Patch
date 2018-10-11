﻿using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Forms9Patch
{
    /// <summary>
    /// Making it easier to manage HardwareKeyListeners
    /// </summary>
    public static class HardwareKeyListenerExtensions
    {
        internal static ObservableCollection<HardwareKeyListener> GetHardwareKeyListeners(this VisualElement visualElement)
        {
            var behavior = HardwareKeyListenerBehavior.GetFor(visualElement);
            return behavior.HardwareKeyListeners;
        }

        /// <summary>
        /// Gets the collection of hardware key listeners for this Xamarin.Forms.View
        /// </summary>
        /// <returns>The hardware key listeners.</returns>
        /// <param name="view">Xamarin.Forms.View.</param>
        public static ObservableCollection<HardwareKeyListener> GetHardwareKeyListeners(this View view) => GetHardwareKeyListeners(view as VisualElement);

        /// <summary>
        /// Gets the collection of hardware key listeners for this ContentPage
        /// </summary>
        /// <returns>The hardware key listeners.</returns>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        public static ObservableCollection<HardwareKeyListener> GetHardwareKeyListeners(this HardwareKeyPage page) => GetHardwareKeyListeners(page as VisualElement);

        /// <summary>
        /// Gets the collection of hardware key listeners for this ContentPage
        /// </summary>
        /// <returns>The hardware key listeners.</returns>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        public static ObservableCollection<HardwareKeyListener> GetHardwareKeyListeners(this PopupBase page) => GetHardwareKeyListeners(page as VisualElement);



        /// <summary>
        /// Clears the hardware key listeners for this visualElemnt.
        /// </summary>
        /// <param name="visualElement">Visual element.</param>
        internal static void ClearHardwareKeyListeners(this VisualElement visualElement)
        {
            var hkl = visualElement.GetHardwareKeyListeners();
            hkl.Clear();
        }

        /// <summary>
        /// Clears the hardware key listeners for this View.
        /// </summary>
        /// <param name="view">View.</param>
        public static void ClearHardwareKeyListeners(this View view) => ClearHardwareKeyListeners(view as VisualElement);

        /// <summary>
        /// Clears the hardware key listeners for this HardwareKeyPage.
        /// </summary>
        /// <param name="page">Page.</param>
        public static void ClearHardwareKeyListeners(this HardwareKeyPage page) => ClearHardwareKeyListeners(page as VisualElement);

        /// <summary>
        /// Clears the hardware key listeners for this HardwareKeyPage.
        /// </summary>
        /// <param name="page">Page.</param>
        public static void ClearHardwareKeyListeners(this PopupBase page) => ClearHardwareKeyListeners(page as VisualElement);



        internal static HardwareKeyListener AddHardwareKeyListener(this VisualElement visualElement, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys = HardwareKeyModifierKeys.None, string discoverableTitle = null, EventHandler<HardwareKeyEventArgs> onPressed = null)
        {
            var hardwareKeyListener = new HardwareKeyListener(new HardwareKey(keyInput, hardwareKeyModifierKeys, discoverableTitle), onPressed);
            var listeners = visualElement.GetHardwareKeyListeners();
            listeners.Add(hardwareKeyListener);
            return hardwareKeyListener;
        }

        /// <summary>
        /// Adds the hardware key listener to a View.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="view">View.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        /// <param name="discoverableTitle">Discoverable title.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this View view, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys, string discoverableTitle, EventHandler<HardwareKeyEventArgs> onPressed = null) => AddHardwareKeyListener(view as VisualElement, keyInput, hardwareKeyModifierKeys, discoverableTitle, onPressed);

        /// <summary>
        /// Adds the hardware key listener to a View.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="view">View.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this View view, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys, EventHandler<HardwareKeyEventArgs> onPressed = null) => AddHardwareKeyListener(view as VisualElement, keyInput, hardwareKeyModifierKeys, null, onPressed);

        /// <summary>
        /// Adds the hardware key listener to a View.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="view">View.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this View view, string keyInput, EventHandler<HardwareKeyEventArgs> onPressed) => AddHardwareKeyListener(view as VisualElement, keyInput, HardwareKeyModifierKeys.None, null, onPressed);

        /// <summary>
        /// Adds the hardware key listener to a View.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="view">View.</param>
        /// <param name="keyInput">Key input.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this View view, string keyInput) => AddHardwareKeyListener(view as VisualElement, keyInput, HardwareKeyModifierKeys.None, null, null);

        /// <summary>
        /// Adds a hardware key listener to a HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        /// <param name="discoverableTitle">Discoverable title.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this HardwareKeyPage page, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys, string discoverableTitle, EventHandler<HardwareKeyEventArgs> onPressed = null) => AddHardwareKeyListener(page as VisualElement, keyInput, hardwareKeyModifierKeys, discoverableTitle, onPressed);

        /// <summary>
        /// Adds a hardware key listener to a HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this HardwareKeyPage page, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys, EventHandler<HardwareKeyEventArgs> onPressed = null) => AddHardwareKeyListener(page as VisualElement, keyInput, hardwareKeyModifierKeys, null, onPressed);

        /// <summary>
        /// Adds a hardware key listener to a HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this HardwareKeyPage page, string keyInput, EventHandler<HardwareKeyEventArgs> onPressed) => AddHardwareKeyListener(page as VisualElement, keyInput, HardwareKeyModifierKeys.None, null, onPressed);

        /// <summary>
        /// Adds a hardware key listener to HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this HardwareKeyPage page, string keyInput) => AddHardwareKeyListener(page as VisualElement, keyInput, HardwareKeyModifierKeys.None, null, null);

        /// <summary>
        /// Adds a hardware key listener to a HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        /// <param name="discoverableTitle">Discoverable title.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this PopupBase page, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys, string discoverableTitle, EventHandler<HardwareKeyEventArgs> onPressed = null) => AddHardwareKeyListener(page as VisualElement, keyInput, hardwareKeyModifierKeys, discoverableTitle, onPressed);

        /// <summary>
        /// Adds a hardware key listener to a HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this PopupBase page, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys, EventHandler<HardwareKeyEventArgs> onPressed = null) => AddHardwareKeyListener(page as VisualElement, keyInput, hardwareKeyModifierKeys, null, onPressed);

        /// <summary>
        /// Adds a hardware key listener to a HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        /// <param name="onPressed">On pressed.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this PopupBase page, string keyInput, EventHandler<HardwareKeyEventArgs> onPressed) => AddHardwareKeyListener(page as VisualElement, keyInput, HardwareKeyModifierKeys.None, null, onPressed);

        /// <summary>
        /// Adds a hardware key listener to HardwareKeyPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Page.</param>
        /// <param name="keyInput">Key input.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this PopupBase page, string keyInput) => AddHardwareKeyListener(page as VisualElement, keyInput, HardwareKeyModifierKeys.None, null, null);


        internal static HardwareKeyListener AddHardwareKeyListener(this VisualElement visualElement, HardwareKeyListener hardwareKeyListener)
        {
            visualElement.GetHardwareKeyListeners().Add(hardwareKeyListener);
            return hardwareKeyListener;
        }

        /// <summary>
        /// Adds a hardware key listener to this Xamarin.Forms.View.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="view">Xamarin.Forms.View.</param>
        /// <param name="hardwareKeyListener">Hardware key listener.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this View view, HardwareKeyListener hardwareKeyListener) => AddHardwareKeyListener(view as VisualElement, hardwareKeyListener);

        /// <summary>
        /// Adds a hardware key listener to this ContentPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        /// <param name="hardwareKeyListener">Hardware key listener.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this HardwareKeyPage page, HardwareKeyListener hardwareKeyListener) => AddHardwareKeyListener(page as VisualElement, hardwareKeyListener);

        /// <summary>
        /// Adds a hardware key listener to this ContentPage.
        /// </summary>
        /// <returns>The hardware key listener.</returns>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        /// <param name="hardwareKeyListener">Hardware key listener.</param>
        public static HardwareKeyListener AddHardwareKeyListener(this PopupBase page, HardwareKeyListener hardwareKeyListener) => AddHardwareKeyListener(page as VisualElement, hardwareKeyListener);



        internal static void RemoveHardwareKeyListener(this VisualElement visualElement, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys = HardwareKeyModifierKeys.None)
        {
            //foreach (var listener in visualElement.GetHardwareKeyListeners())
            var listeners = visualElement.GetHardwareKeyListeners();
            for (int i = 0; i < listeners.Count; i++)
            {
                var key = listeners[i].HardwareKey;
                if (key.KeyInput == keyInput && key.ModifierKeys == hardwareKeyModifierKeys)
                {
                    listeners.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Matches a hardware key listener and, if found, removes it from this Xamarin.Forms.View.
        /// </summary>
        /// <param name="view">Xamarin.Forms.View.</param>
        /// <param name="keyInput">Key Label.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        public static void RemoveHardwareKeyListener(this View view, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys = HardwareKeyModifierKeys.None) => RemoveHardwareKeyListener(view as VisualElement, keyInput, hardwareKeyModifierKeys);

        /// <summary>
        /// Matches a hardware key listener and, if found, removes it from this ContentPage.
        /// </summary>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        /// <param name="keyInput">Key Label.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        public static void RemoveHardwareKeyListener(this HardwareKeyPage page, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys = HardwareKeyModifierKeys.None) => RemoveHardwareKeyListener(page as VisualElement, keyInput, hardwareKeyModifierKeys);

        /// <summary>
        /// Matches a hardware key listener and, if found, removes it from this ContentPage.
        /// </summary>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        /// <param name="keyInput">Key Label.</param>
        /// <param name="hardwareKeyModifierKeys">Hardware key modifier keys.</param>
        public static void RemoveHardwareKeyListener(this PopupBase page, string keyInput, HardwareKeyModifierKeys hardwareKeyModifierKeys = HardwareKeyModifierKeys.None) => RemoveHardwareKeyListener(page as VisualElement, keyInput, hardwareKeyModifierKeys);



        internal static void RemoveHardwareKeyListener(this VisualElement visualElement, HardwareKeyListener listener)
        {
            var listeners = visualElement.GetHardwareKeyListeners();
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        /// <summary>
        /// Removes a hardware key listener from this ContentPage.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="listener">Listener.</param>
        public static void RemoveHardwareKeyListener(this View view, HardwareKeyListener listener) => RemoveHardwareKeyListener(view as VisualElement, listener);

        /// <summary>
        /// Removes a hardware key listener from this Xamarin.Forms.View.
        /// </summary>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        /// <param name="listener">Listener.</param>
        public static void RemoveHardwareKeyListener(this HardwareKeyPage page, HardwareKeyListener listener) => RemoveHardwareKeyListener(page as VisualElement, listener);

        /// <summary>
        /// Removes a hardware key listener from this Xamarin.Forms.View.
        /// </summary>
        /// <param name="page">Forms9Patch.HardwareKeyPage.</param>
        /// <param name="listener">Listener.</param>
        public static void RemoveHardwareKeyListener(this PopupBase page, HardwareKeyListener listener) => RemoveHardwareKeyListener(page as VisualElement, listener);



        internal static void HardwareKeyFocus(this VisualElement visualElement) => HardwareKeyPage.FocusedElement = visualElement;

        /// <summary>
        /// Sets the HardwareKeyFocus to this view.
        /// </summary>
        /// <param name="view">View.</param>
        public static void HardwareKeyFocus(this View view) => HardwareKeyFocus(view as VisualElement);

        /// <summary>
        /// Sets the HardwareKeyFocus to this page.
        /// </summary>
        /// <param name="page">Page.</param>
        public static void HardwareKeyFocus(this HardwareKeyPage page) => HardwareKeyFocus(page as VisualElement);

        /// <summary>
        /// Sets the HardwareKeyFocus to this page.
        /// </summary>
        /// <param name="page">Page.</param>
        public static void HardwareKeyFocus(this PopupBase page) => HardwareKeyFocus(page as VisualElement);



        /// <summary>
        /// Removes the hardware key focus from this VisualElement
        /// </summary>
        /// <param name="visualElement">Visual element.</param>
        public static void HardwareKeyUnfocus(this VisualElement visualElement)
        {
            if (HardwareKeyPage.FocusedElement == visualElement)
                HardwareKeyPage.FocusedElement = null;
        }

    }
}