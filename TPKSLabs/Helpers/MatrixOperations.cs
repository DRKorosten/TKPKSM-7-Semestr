using System;

namespace TPKSLabs.Helpers
{
    public class MatrixOperations
    {
        public static void OutPutMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("[");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int currItem = matrix[i, j];
                    Console.ForegroundColor = (currItem == 1) ? ConsoleColor.Cyan : ConsoleColor.White;
                    Console.Write(matrix[i,j] + " ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]");
                Console.WriteLine();
            }
        }
    }
}