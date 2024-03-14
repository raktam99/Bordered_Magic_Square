

namespace Bordered_Magic_Square.Helper
{
    public static class Helpers
    {
        public static bool isMagic(Square square)
        {
            int sumToFind = CalculateSum(square);
            int i = 0;
            int sum;
            int n = 0, m = 0;
            
            //checking if there are two of the same value
            while (n < square.Order)
            {
                for (int j = n; j < square.Order; ++j)
                {
                    for (int k = m + 1; k < square.Order; ++k)
                    {
                        if (square.SquareValues[j, k] == square.SquareValues[n, m])
                            return false;
                    }
                }
                if (m + 1 == square.Order)
                {
                    m = 0;
                    ++n;
                }
                else if (m + 1 != square.Order)
                {
                    ++m;
                }
            }

            //checking sums
            do
            {
                sum = 0;
                for (int j = 0; j < square.Order; ++j)
                {
                    sum += square.SquareValues[i, j];
                }
                ++i;
            } while (sum == sumToFind && i < square.Order);

            if (i < square.Order)
                return false;

            i = 0;

            do
            {
                sum = 0;
                for (int j = 0; j < square.Order; ++j)
                {
                    sum += square.SquareValues[j, i];
                }
                ++i;
            } while (sum == sumToFind && i < square.Order);

            if (i < square.Order)
                return false;

            i = 0;

            sum = 0;

            do
            {
                sum += square.SquareValues[i, i];
                ++i;
            } while (i < square.Order);

            if (sum != sumToFind)
                return false;

            i = 0;

            sum = 0;

            do
            {
                sum += square.SquareValues[square.Order - 1 - i, square.Order - 1 - i];
                ++i;
            } while (i < square.Order);

            if (sum < sumToFind)
                return false;

            return true;
        } //Checks if square is magic

        public static int CalculateSum(Square square)
        {
            int four = square.Order == 4 ? square.Removed*4 : 0; //Necessary if the order is four
            return CalculateMagicSum(square.Order + square.Removed * 2) //Calculates the magic sum of the original bordered square
                - (square.Removed * (square.SquareValues[square.Order / 2, 0] 
                + square.SquareValues[square.Order / 2, square.Order-1]))+four;
        } //Calculates the sum of a square

        public static int CalculateMagicSum(int order)
        {
            return (int)(((double)order / 2) * ((double)order * (double)order + 1));
        } //Calculates magic sum of an order

        public static int GetCenter(Square square)
        {
            if (square.Order % 2 == 0)
            {
                return square.SquareValues[square.Order / 2 - 1, square.Order / 2 - 1];
            }
            else
            {
                return square.SquareValues[square.Order / 2, square.Order / 2];
            }
        } //Gets the value at the center of the square
    }
}
