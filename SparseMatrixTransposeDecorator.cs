using System;

namespace knuth_scheme
{
  class SparseMatrixTransposeDecorator
  {
    private SparseMatrix Matrix;
    
    public SparseMatrixTransposeDecorator(SparseMatrix matrix)
    {
      Matrix = matrix;
    }
    
    private void ChangeInElementIndexes()
    {
      foreach (var column in Matrix.Columns) {
        if (column != null)
        {
         column.ForEach(e => {
          int t = e.Row;
          e.Row = e.Column;
          e.Column = t;
        }); 
        }
      };
    }
    
    public SparseMatrix Apply() {
      var columns = Matrix.Columns;
      Matrix.Columns = Matrix.Rows;
      Matrix.Rows = columns;
      var columnsCount = Matrix.ColumnsCount;
      Matrix.ColumnsCount = Matrix.RowsCount;
      Matrix.RowsCount = columnsCount;
      
      ChangeInElementIndexes();
      return Matrix;
    }
  }
}