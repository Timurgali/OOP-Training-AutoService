using System;

namespace AutoService
{
    public static class Utils
    {
        private static Random s_random = new Random();

        public static bool TryReadInt(out int value)
        {
            bool canEnter = true;
            string input;

            value = 0;

            while (canEnter)
            {
                int currentTop = Console.CursorTop;
                input = Console.ReadLine();

                if (Int32.TryParse(input, out value) == true)
                    return true;

                ClearLine(currentTop);

                Console.WriteLine("Необходимо ввести число");
                canEnter = SelectOption("Ввести повторно?");

                ClearFromLine(currentTop);
            }

            return false;
        }
        public static void ClearCurrentLine()
        {
            int currentTop = Console.CursorTop;
            ClearLine(currentTop);
            Console.SetCursorPosition(0, currentTop);
        }

        public static bool SelectOption(string message = null)
        {
            const char CommandYes = '1';
            const char CommandNo = '0';

            bool isSelectionRunning = true;

            char userInput;

            if (message != null)
            {
                Console.WriteLine(message);
            }

            Console.WriteLine($"{CommandYes} - Подтвердить");
            Console.WriteLine($"{CommandNo} - Отказать");
            userInput = Console.ReadKey().KeyChar;

            ClearCurrentLine();

            while (isSelectionRunning)
            {
                switch (userInput)
                {
                    case CommandYes:
                        return true;

                    case CommandNo:
                        return false;

                    default:
                        continue;
                }
            }

            return false;
        }

        public static void ClearFromLine(int currentTop)
        {
            for (int i = currentTop; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.BufferWidth));
            }

            Console.SetCursorPosition(0, currentTop);
        }

        public static int GetRandomNumber(int minNumber = 0, int maxNumber = int.MaxValue)
        {
            return s_random.Next(minNumber, maxNumber);
        }

        public static void WaitForAction()
        {
            int currentTop = Console.CursorTop;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\nДля продолжения нажмите любую клавишу");
            Console.ResetColor();

            Console.ReadKey();

            ClearLine(currentTop + 1);
            ClearLine(currentTop + 2);

            Console.SetCursorPosition(0, currentTop);
        }

        public static void DrawLine(char symbol = '*', int number = 10) => Console.WriteLine(new string(symbol, number));

        private static void ClearLine(int topPosition)
        {
            Console.SetCursorPosition(0, topPosition);
            Console.Write(new string(' ', Console.BufferWidth));
        }
    }
}
