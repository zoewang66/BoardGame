using System;

namespace GameDesign
{
    public class Move
    {
        public int col { get; set; } = 0;
        public int row { get; set; } = 0;

        public Move() { }

        public Move(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public override string ToString()
        {
            return $"Move(row: {row}, col: {col})";
        }
    }
}