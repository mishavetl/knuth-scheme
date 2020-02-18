using System;

namespace knuth_scheme
{
    class Program
    {
        static void Main(string[] args)
        {
            SparseMatrix matrix = new SparseMatrix(3, 3);
            matrix.Add(1, 0, 1);
            matrix.Add(1, 1, 5);
            matrix.Add(1, 2, 6);
            matrix.Add(1, 2, 7);
            
            SparseMatrix transposedMatrix = new SparseMatrixTransposeDecorator(matrix).Apply();
          
            for (int i = 0; i < transposedMatrix.RowsCount; ++i)
            {
              for (int j = 0; j < transposedMatrix.ColumnsCount; ++j)
              {
                Console.Write(matrix.Get(i, j) + " ");  
              }
              Console.WriteLine();
            }
        }
    }
}
