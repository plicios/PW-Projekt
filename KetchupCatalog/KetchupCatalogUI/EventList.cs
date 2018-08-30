using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorny.KetchupCatalog.KetchupCatalogUI
{
    class EventList<T> : List<T>
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
            add
            {
                _itemsChangedEvent = value;
            }
            remove
            {
                _itemsChangedEvent = null;
            }
        }
    }
}
