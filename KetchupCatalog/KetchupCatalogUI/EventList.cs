using System;
using System.Collections.Generic;

namespace Gorny.KetchupCatalog.KetchupCatalogUI
{
    public class EventList<T> : List<T>
    {
        private event EventHandler _itemsChangedEvent;

        public new void Add(T element)
        {
            base.Add(element);
            _itemsChangedEvent?.Invoke(this, null);
        }

        public new void Remove(T element)
        {
            base.Remove(element);
            _itemsChangedEvent?.Invoke(this, null);
        }

        public event EventHandler ItemsChangedEvent
        {
            add => _itemsChangedEvent = value;
            remove => _itemsChangedEvent = null;
        }
    }
}
