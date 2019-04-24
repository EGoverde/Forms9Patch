using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Forms9Patch
{
    internal class HardwareKeyListenerBehavior : Behavior<VisualElement>
    {

        ObservableCollection<HardwareKeyListener> _hardwareKeyListeners = new ObservableCollection<HardwareKeyListener>();
        public ObservableCollection<HardwareKeyListener> HardwareKeyListeners
        {
            get => _hardwareKeyListeners;
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            HardwareKeyListenerEffect.AttachTo(bindable);
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            HardwareKeyListenerEffect.DetachFrom(bindable);
            if (HardwareKeyPage.FocusedElement == bindable)
                HardwareKeyPage.FocusedElement = null;
            HardwareKeyListeners.Clear();
            base.OnDetachingFrom(bindable);
        }

        public static HardwareKeyListenerBehavior GetFor(VisualElement visualElement)
        {
            if (visualElement == null)
                return null;
            foreach (var behavior in visualElement.Behaviors)
                if (behavior is HardwareKeyListenerBehavior hklBehavior)
                    return hklBehavior;

            var result = new HardwareKeyListenerBehavior();
            visualElement.Behaviors.Add(result);
            return result;
        }
    }
}