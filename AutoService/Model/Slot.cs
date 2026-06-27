using System;

namespace AutoService
{
    public class Slot
    {
        private Component _component;

        public Slot(Component component, int amount)
        {
            _component = component.Clone();
            Amount = amount;
        }

        public int Amount { get; private set; }
        public ComponentName ComponentName => _component.Name;

        public bool TryTakeComponent(out Component component)
        {
            component = null;

            if (Amount <= 0)
            {
                Console.WriteLine("Деталей такого типа не осталось на складе");
                return false;
            }

            component = _component.Clone();
            Amount--;
            return true;
        }

        public void ShowInfo()
        {
            _component.ShowInfo();
            Console.WriteLine($"Количество: {Amount} шт.");
        }
    }
}
