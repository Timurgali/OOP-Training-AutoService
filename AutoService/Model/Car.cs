using System;
using System.Collections.Generic;

namespace AutoService
{
    public class Car
    {
        private List<Component> _components = new List<Component>();

        public Car(List<Component> components)
        {
            _components.AddRange(components);
        }

        public bool TryGetBrokenComponentActualIndex(int inputIndex, out int actualIndex)
        {
            int brokenComponentIndex = 0;
            actualIndex = 0;

            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i].IsOk)
                    continue;

                brokenComponentIndex++;

                if (inputIndex == brokenComponentIndex)
                {
                    actualIndex = i;
                    return true;
                }
            }

            Console.WriteLine("Не удалось найти деталь под этим номером");
            Utils.WaitForAction();
            return false;
        }

        public bool TryReplaceComponent(int actualIndex, Component newComponent)
        {
            if (_components[actualIndex].Name != newComponent.Name)
            {
                Console.WriteLine("Новая деталь не предназначена для ремонта этой детали");
                return false;
            }

            _components[actualIndex] = newComponent.Clone();

            return true;
        }

        public bool TryGetBrokenComponentName(int actualIndex, out ComponentName componentName)
        {
            componentName = ComponentName.Empty;

            if (actualIndex >= _components.Count || actualIndex < 0)
            {
                Console.WriteLine("Не удалось найти деталь под этим номером");
                Utils.WaitForAction();
                return false;
            }

            componentName = _components[actualIndex].Name;
            return true;
        }

        public void ShowBrokenComponets()
        {
            int index = 1;

            foreach (Component component in _components)
            {
                if (component.IsOk == false)
                {
                    Console.WriteLine($"№ {index}");
                    component.ShowInfo();
                    index++;
                }
            }
        }

        public List<Component> GetAllBrokenComponents()
        {
            List<Component> brokenComponents = new List<Component>();

            foreach (Component component in _components)
            {
                if (component.IsOk == false)
                {
                    brokenComponents.Add(component.Clone());
                }
            }

            return brokenComponents;
        }
    }
}
