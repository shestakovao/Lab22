using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(GetSumAndMaxArray);
            Task task2 = task1.ContinueWith(action1);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random r = new Random();
            for (int i = 0; i < n; i++) { array[i] = r.Next(0, 100); }
            return array;
        }

        static void GetSumAndMaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            int max = array[0];
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
                max = (max < array[i]) ? array[i] : max;
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine($"\nСумма = {sum}\nМаксимальное значение = {max}");
        }
    }
}