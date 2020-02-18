using System;
using System.Collections.Generic;
using System.Linq;

namespace knuth_scheme
{
  class SparseMatrix
  {
    public List<Node>[] Columns { get; set; }
    public List<Node>[] Rows {get; set; }
    
    public int ColumnsCount { get; set; }
    public int RowsCount { get; set; }
    
    public SparseMatrix(int columnsCount, int rowsCount)
    {
      ColumnsCount = columnsCount;
      RowsCount = rowsCount;
      Columns = new List<Node>[ColumnsCount];
      Rows = new List<Node>[RowsCount];
    }
    
    private Pair GetSmallerOrEqualInColumn(int row, int column)
    {
      return Columns[column]
        .Select((o, i) => new Pair { Value = o, Index = i })
        .Where(p => p.Value.Row <= row)
        .LastOrDefault();
    }
    
    private Pair GetSmallerOrEqualInRow(int row, int column)
    {
      return Rows[row]
        .Select((o, i) => new Pair { Value = o, Index = i })
        .Where(p => p.Value.Column <= column)
        .LastOrDefault();
    }
    
    private void UpdateValueAccordingToFound(int row, int column, Pair lastColumn, Pair lastRow, int val)
    {
      if (lastColumn.Value.Row == row)
      {
        lastColumn.Value.Value = val;
      } else
      {
        var node = new Node { Row = row, Column = column, Value = val };
        Columns[column].Insert(lastColumn.Index, node);
        Rows[row].Insert(lastRow.Index, node);
      }
    }
    
    private void CreateColumnAndRowIfNotExist(int row, int column)
    {
      if (Columns[column] == null)
      {
        Columns[column] = new List<Node>();
      }
      if (Rows[row] == null)
      {
        Rows[row] = new List<Node>();
      }
    }
    
    public void Add(int row, int column, int val)
    {
      CreateColumnAndRowIfNotExist(row, column);
      var foundColumn = GetSmallerOrEqualInColumn(row, column);
      var foundRow = GetSmallerOrEqualInRow(row, column);
      if (foundColumn != null)
      {
        UpdateValueAccordingToFound(row, column, foundColumn, foundRow, val);
      } else
      {
        var node = new Node { Row = row, Column = column, Value = val };
        Columns[column].Add(node);
        Rows[row].Add(node);
      }
    }
    
    public int Get(int row, int column)
    {
      if (Columns[column] == null)
      {
        return 0;
      }
      var found = GetSmallerOrEqualInColumn(row, column);
      return found != null && found.Value.Row == row ? found.Value.Value : 0;
    }
    
    public class Pair
    {
      public Node Value;
      public int Index;
    }
    
    public class Node
    {
      public int Row;
      public int Column;
      public int Value;
    } 
  }
}