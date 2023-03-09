using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace towerOfHanoi
{
    internal class Program
    {
        static int[] values = { 13, 11, 9, 7, 5, 3 };
        static string[] towerNames = { "A", "B", "C" };

        static List<Stack<int>> towers = new List<Stack<int>>() { new Stack<int>(values), new Stack<int>(), new Stack<int>() };

        static int space = 4;
        static int width = (values[0] * 3) + (4 * space);
        static int height = values.Length + 1;

        static int top = 7;
        static int left = 20;

        static void Main(string[] args)
        {
            Console.SetWindowSize(2 * left + width, 2 * top + height + 1);
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            do
            {
                while (towers[1].Count < values.Length && towers[2].Count < values.Length)
                {
                    DisplayBoard();
                    DisplayTowerValues();

                    string request = "Parancs megadása(honnan-hova, pl. ab): ";
                    Console.SetCursorPosition(left + width / 2 - (request.Length + 2) / 2, top + height + 3);
                    Console.Write(request);
                    string command = Console.ReadLine();

                    ChangeBlock(command);

                    Console.Clear();
                }

                string win = "Sikeresen megoldotta a játékot!";
                Console.SetCursorPosition(left + width / 2 - (win.Length) / 2, top + height / 2);
                Console.Write(win);

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        static void DisplayBoard()
        {
            string title = "HANOI TORNYAI";
            Console.SetCursorPosition(left + width / 2 - (title.Length) / 2, top - 3);
            Console.Write(title);

            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(left + i, top + height);
                Console.Write("─");
            }
            int towerXposition = left + space + values[0] / 2;
            for (int tower = 1; tower <= 3; tower++)
            {
                for (int i = 0; i <= height; i++)
                {
                    Console.SetCursorPosition(towerXposition, top + i);
                    if (i == height)
                    {
                        Console.Write("┴");
                        Console.SetCursorPosition(towerXposition, top + i + 1);
                        Console.Write(towerNames[tower - 1]);
                    }
                    else
                    {
                        Console.Write("│");
                    }
                }
                towerXposition += space + values[0];
            }
        }

        static void DisplayTowerValues()
        {
            int towerXposition = left + space + values[0] / 2;
            for (int tower = 0; tower < towers.Count; tower++)
            {
                Stack<int> tempTower = new Stack<int>(towers[tower]);
                int blockYposition = top + height - 1;
                for (int i = 0; i < towers[tower].Count; i++)
                {
                    for (int j = 0; j < tempTower.Peek(); j++)
                    {
                        Console.SetCursorPosition(towerXposition - tempTower.Peek()/2 + j, blockYposition);
                        Console.Write("▄");
                    }

                    tempTower.Pop();
                    blockYposition--;
                }
                towerXposition += space + values[0];
            }
        }

        static void ChangeBlock(string command)
        {
            int from = -1; 
            int next = -1;
            
            for (int i = 0; i < towerNames.Length; i++)
            {
                if (towerNames[i] == command[0].ToString().ToUpper())
                {
                    from = i;
                }
                if (towerNames[i] == command[1].ToString().ToUpper())
                {
                    next = i;
                }
            }

            if (from != -1 && next != -1 && towers[from].Count != 0)
            {
                if (!towers[next].Any() || towers[from].Peek() < towers[next].Peek())
                {
                    towers[next].Push(towers[from].Peek());
                    towers[from].Pop();
                }
            }
            else
            {
                string text = "Rossz értéket adott meg!";
                Console.SetCursorPosition(left + width / 2 - text.Length / 2, top + height + 4);
                Console.Write(text);
            }

        }
    }
}
