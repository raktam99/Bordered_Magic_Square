using Bordered_Magic_Square.Helper;
using System;

namespace Bordered_Magic_Square
{
    public class Square
    {
        public int Order;

        public int[,] SquareValues;

        public int Removed;

        public Square(int order, int[,] squareValues, int removed)
        {
            Order = order;
            SquareValues = squareValues;
            Removed = removed;
        }

        public override string ToString()
        {
            string[] lines=new string[Order];
            string square="";

            for (int i = 0; i < Order; ++i)
            {
                for (int j = 0; j < Order; ++j)
                {
                    lines[i] += string.Format("{0,4}|",SquareValues[i, j]);
                }
                lines[i].Remove(lines[i].Length-1, 1);
            }

            for (int i = 0; i < Order; ++i)
            {
                square+=lines[i]+"\n";
                for (int j = 0; j < Order; j++)
                {
                    square += "-----";
                }
                square+="\n";
            }
            square += "\nOrder: " + Order;
            square += "\nSum: " + Helpers.CalculateSum(this);
            square += "\nMiddle value: " + Helpers.GetCenter(this);
            square += string.Format("\nIs it magic: {0}\n\n", Helpers.isMagic(this)==true? "Yes":"No");

            return square;
        }
    }
}
