using System;

namespace AutoService
{
    public class Component
    {
        public Component(ComponentName name, bool isOk, float price)
        {
            Name = name;
            IsOk = isOk;
            Price = price;
        }

        public ComponentName Name { get; private set; }
        public float Price { get; private set; }
        public bool IsOk { get; private set; }

        public Component Clone(bool isOk = true)
        {
            return new Component(Name, isOk, Price);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"название: {Name},\n" +
                              $"исправность: {(IsOk ? "ok" : "not Ok")}\n" +
                              $"цена: {Price}");
        }
    }
}
