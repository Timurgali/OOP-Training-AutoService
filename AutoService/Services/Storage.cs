using System;
using System.Collections.Generic;

namespace AutoService
{
    public class Storage
    {
        private List<Slot> _slots = new List<Slot>();

        public Storage(List<Slot> slots)
        {
            _slots.AddRange(slots);
        }

        public bool TryTakeComponent(ComponentName name, out Component component)
        {
            Slot slot;
            component = null;

            if (TryFindSlotWithName(name, out slot) == false)
                return false;

            return slot.TryTakeComponent(out component);
        }

        private bool TryFindSlotWithName(ComponentName name, out Slot foundSlot)
        {
            foundSlot = null;

            foreach (Slot slot in _slots)
            {
                if (slot.ComponentName == name)
                {
                    foundSlot = slot;
                    return true;
                }
            }

            Console.WriteLine("Не удалось найти деталь с таким именем");
            return false;
        }
    }
}
