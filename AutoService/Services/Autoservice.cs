using System;
using System.Collections.Generic;

namespace AutoService
{
    public class Autoservice
    {
        private Storage _storage;
        private Casse _casse;
        private Queue<Car> _carsQueue;
        private Factory factory = new Factory();

        public Autoservice()
        {
            int numberOfCars = 100;
            int money = 5000;

            _casse = new Casse(money);
            _storage = factory.CreateStorage();
            _carsQueue = factory.CreateCarQueue(numberOfCars);
        }

        public void Run()
        {
            const char CommandServeNextCar = '1';
            const char CommandQuit = '0';

            bool isRunning = true;
            char userInput;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine($"{CommandServeNextCar} - Ремонт следующей машины\n" +
                                          $"{CommandQuit} - Выход\n");

                Console.WriteLine();

                Console.WriteLine($"Касса: {_casse.Money}");

                userInput = Console.ReadKey().KeyChar;

                switch (userInput)
                {
                    case CommandServeNextCar:
                        RepairNextCar();
                        break;

                    case CommandQuit:
                        isRunning = false;
                        break;

                    default:
                        continue;
                }
            }
        }

        private void RepairNextCar()
        {
            const char CommandSelectComponentForReplace = '1';
            const char CommandQuit = '0';

            bool isRunning = true;
            char userInput;

            Car car = _carsQueue.Dequeue();

            float fixedPenalty = 100;
            string textForApplyingNextCar = $"Приступить к этой машине?\n" +
                                            $"Штраф за отказ обслуживания машины: {fixedPenalty} руб.";

            Console.Clear();

            car.ShowBrokenComponets();

            Console.WriteLine();
            Utils.DrawLine();

            if (Utils.SelectOption(textForApplyingNextCar) == false)
            {
                PayFixedPenalty(fixedPenalty);
                return;
            }

            while (isRunning)
            {
                Console.Clear();

                car.ShowBrokenComponets();
                Utils.DrawLine();

                Console.WriteLine($"{CommandSelectComponentForReplace} - Выбрать деталь для ремонта\n" +
                                  $"{CommandQuit} - Завершить ремонт\n");

                Console.WriteLine();

                userInput = Console.ReadKey().KeyChar;
                Utils.ClearCurrentLine();


                switch (userInput)
                {
                    case CommandSelectComponentForReplace:
                        SelectComponentForReplace(car);
                        break;

                    case CommandQuit:
                        TerminateRepairing(car, out isRunning);
                        break;

                    default:
                        continue;
                }
            }
        }

        private void TerminateRepairing(Car car, out bool isRunning)
        {
            isRunning = false;
            PayPenaltyForUnfinishedCar(car);
        }

        private void SelectComponentForReplace(Car car)
        {
            Console.Clear();
            car.ShowBrokenComponets();
            float priceMultiplyer = 1.5f;
            Utils.DrawLine();

            int currentTop = Console.CursorTop;

            Console.WriteLine("Введите номер сломанной детали");

            int userInput;

            if (Utils.TryReadInt(out userInput) == false)
                return;

            Utils.ClearFromLine(currentTop);

            int actualIndex;

            if (car.TryGetBrokenComponentActualIndex(userInput, out actualIndex) == false)
                return;

            ComponentName componentName;

            if (car.TryGetBrokenComponentName(actualIndex, out componentName) == false)
                return;

            Component component;

            if (_storage.TryTakeComponent(componentName, out component) == false)
                return;

            if (car.TryReplaceComponent(actualIndex, component) == false)
                return;

            _casse.ReceivePayment(component.Price * priceMultiplyer);
            Console.WriteLine("Ремонт детали успешно выполнен");

            Utils.WaitForAction();
        }

        private void PayFixedPenalty(float penaltyValue = 100) => _casse.Pay(penaltyValue);

        private void PayPenaltyForUnfinishedCar(Car car)
        {
            List<Component> unrepairedComponents = car.GetAllBrokenComponents();
            float penalty = 0;

            foreach (Component component in unrepairedComponents)
            {
                penalty += component.Price;
            }

            _casse.Pay(penalty);
        }
    }
}
