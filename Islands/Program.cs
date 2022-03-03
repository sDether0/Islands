using System;
using System.Linq;


namespace Islands
{
    internal class Program
    {
        static int count = 2;
        static void Main(string[] args)
        {
            int[,] test = Generate(500, 40);
            //Print(test);
            var (x,y) = GetIslands(test);
            Console.WriteLine("Islands count = " + x);
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine($"Sqaure of {i + 1} = {y[i]}");
            }
        }
               
        public static (int,int[]) GetIslands(int[,] field)
        {
            int x = field.GetLength(0);
            int y = field.GetLength(1);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (field[i, j] == 1)
                    {
                        if ((i == 0 || j == 0 || i == x - 1 || j == y - 1))
                        {
                            field[i, j] = -1;
                            continue;
                        }
                        if (field[i - 1, j] != 0)
                        {
                            int left = field[i - 1, j];
                            field[i, j] = left;
                            if (field[i, j - 1] != 0)
                            {
                                if (field[i, j - 1] != left)
                                {
                                    int rep = field[i, j - 1];
                                    if (rep == -1)
                                    {
                                        //connect island to "earth"
                                        Replace(field, left, rep);
                                        continue;
                                    }
                                    //connect two islands
                                    Replace(field, rep, left);
                                    continue;
                                }
                                continue;
                            }
                            continue;
                        }
                        if (field[i, j - 1] != 0)
                        {
                            field[i, j] = field[i, j - 1];
                            continue;
                        }
                        field[i, j] = count++;
                    }
                }
            }
            //Print(field);
            var field2 = field.Cast<int>().ToList();
            var distinct = field2.Distinct().OrderBy(x=>x).ToList();
            int fcount = distinct.Count() - 2;
            int[] squares = new int[fcount];
            for(int i = 0; i < fcount; i++)
            {
                squares[i] = field2.FindAll(x => x == distinct[i + 2]).Count();
            }
            return (fcount,squares);
        } 

        public static void Replace(int[,] field, int rep, int neW)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == rep) field[i, j] = neW;
                }
            }
        }

        public static int[,] Generate(int x, int y) 
        {
            int[,] result = new int[x, y];
            for(int i = 0; i < x; i++)
            {
                for(var j = 0; j < y; j++)
                {
                    result[i, j] = (new Random()).Next(0,2);
                }
            }
            return result;
        }
        public static void Print(int[,] field)
        {
            Console.WriteLine();
            for (int i = 0; i < field.GetLength(1); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    Console.Write(field[j, i]+"\t");
                }
                Console.WriteLine();
            }
        }
    }
}
