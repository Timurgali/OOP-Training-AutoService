using System;
using System.Collections.Generic;

namespace AutoService
{
    public class Factory
    {
        private List<Component> _componentDictionary = new List<Component>() {
                                                                new Component(ComponentName.BrakingSystem, isOk:true, price:1000),
                                                                new Component(ComponentName.Carcass,       isOk:true, price:3000),
                                                                new Component(ComponentName.Engine,        isOk:true, price:3000),
                                                                new Component(ComponentName.Suspension,    isOk:true, price:1100),
                                                                new Component(ComponentName.Steering,      isOk:true, price:1600),
                                                                new Component(ComponentName.Transmission,  isOk:true, price:2000),
                                                                new Component(ComponentName.Wheel,         isOk:true, price:200),
                                                              };

        public Storage CreateStorage() => new Storage(CreateStorageSlotsList());
        public Car CreateCar() => new Car(CreateCarComponentsList());

        public Queue<Car> CreateCarQueue(int numberOfCars)
        {
            Queue<Car> carsQueue = new Queue<Car>();

            for (int i = 0; i < numberOfCars; i++)
            {
                carsQueue.Enqueue(CreateCar());
            }

            return carsQueue;
        }

        public List<Slot> CreateStorageSlotsList()
        {
            List<Slot> slots = new List<Slot>() { };

            foreach (Component component in _componentDictionary)
            {
                int minAmount = 20;
                int maxAmount = 200;
                int amount = Utils.GetRandomNumber(minAmount, maxAmount + 1);
                Slot newSlot = new Slot(component, amount);
                slots.Add(newSlot);
            }

            return slots;
        }

        public bool ShouldBeBroken => Convert.ToBoolean(Utils.GetRandomNumber(0, 2));

        public List<Component> CreateCarComponentsList()
        {
            List<Component> components = new List<Component>();
            int numberOfWheels = 4;

            foreach (Component component in _componentDictionary)
            {
                if (component.Name == ComponentName.Wheel)
                {
                    for (int i = 0; i < numberOfWheels; i++)
                    {
                        components.Add(component.Clone(ShouldBeBroken));
                    }

                    continue;
                }

                components.Add(component.Clone(ShouldBeBroken));
            }

            return components;
        }
    }
}
