using System;
using System.Collections.Generic;
using System.Linq;

namespace Bordered_Magic_Square.Helper
{
    public static class AlterSquare
    {
        public static Square AddBorders(Square square, int ord)
        {
            int order = square.Order + 2;
            if (order - 2 >= ord)
            {
                return square;
            }
            int[,] newSquare = new int[order, order];

            Square changedSquare = new Square(order, newSquare,0);

            AddInnerSquare(ref changedSquare, square);

            if (ord % 2 == 0)
            {
                AddSidesEven(ref changedSquare);
            }
            else
            {
                AddSidesOdd(ref changedSquare);
            }
            


            if (order < ord)
            {
                changedSquare = AddBorders(changedSquare, ord);
            }
            return changedSquare;
        } //Adds borders to a magic square

        public static void AddInnerSquare(ref Square newSquare, Square square)
        {
            for (int i = 0; i < newSquare.Order - 2; ++i)
            {
                for (int j = 0; j < newSquare.Order - 2; ++j)
                {
                    square.SquareValues[i, j] += 2 * (newSquare.Order - 1);
                }
            }

            for (int i = 1; i < newSquare.Order - 1; ++i)
            {
                for (int j = 1; j < newSquare.Order - 1; ++j)
                {
                    newSquare.SquareValues[i, j] = square.SquareValues[i - 1, j - 1];
                }
            }
        } //Adds magic square to the center

        public static void AddSidesEven(ref Square square)
        {
            int s=square.Order*square.Order+1;
            int center = Helpers.GetCenter(square);
            int magicSum = Helpers.CalculateMagicSum(square.Order);
            int sumTop, sumLeft;
            Random rnd = new Random();
            int[] topSide = new int[square.Order - 2];
            int[] leftSide = new int[square.Order - 2];
            //Getting the numbers of the border
            List<int> outerNumbers = new List<int>();
            for (int i = 1; i <= 2 * (square.Order - 1); ++i)
            {
                outerNumbers.Add(i);
            }
            for (int i = (square.Order * square.Order) - 2 * (square.Order - 1) + 1; i <= square.Order * square.Order; ++i)
            {
                outerNumbers.Add(i);
            }
            //Values for the four corners
            square.SquareValues[0, 0] = square.SquareValues[1, 1] + 2 * square.Order - 6;
            square.SquareValues[0, square.Order - 1] = square.SquareValues[1, square.Order - 2] + 2 * square.Order - 2;
            square.SquareValues[square.Order - 1, 0] = s - square.SquareValues[0, square.Order - 1];
            square.SquareValues[square.Order - 1, square.Order - 1] = s - square.SquareValues[0, 0];
            outerNumbers.Remove(square.SquareValues[0, 0]); 
            outerNumbers.Remove(square.SquareValues[0, square.Order - 1]);
            outerNumbers.Remove(square.SquareValues[square.Order - 1, 0]); 
            outerNumbers.Remove(square.SquareValues[square.Order - 1, square.Order - 1]);

            List<int> bigger;
            List<int> smaller;
            int bigCount;
            int smallCount;

            //Trying different conbinations for the sides
            do
            {
                sumLeft = square.SquareValues[0, 0] + square.SquareValues[square.Order - 1, 0]; 
                sumTop = square.SquareValues[0, 0] + square.SquareValues[0, square.Order - 1];
                bigger = outerNumbers.ToArray().Where(x => x > center).ToList();
                smaller = outerNumbers.ToArray().Where(x => x < center).ToList();
                bigCount = bigger.Count;
                smallCount = smaller.Count;
                
                //Top numbers where numbers are bigger than the center + opposite
                for (int i = 0; i < (square.Order - 2)/2-1; ++i)
                {
                    topSide[i] = bigger[rnd.Next(bigCount)];
                    sumTop += topSide[i];
                    bigger.Remove(topSide[i]);
                    smaller.Remove(square.Order * square.Order + 1 - topSide[i]);
                    bigCount--;
                    smallCount--;
                }

                //Top numbers where numbers are smaller than the center + opposite
                for (int i = (square.Order - 2) / 2 - 1; i < square.Order - 2; ++i)
                {
                    topSide[i] = smaller[rnd.Next(smallCount)];
                    sumTop += topSide[i];
                    smaller.Remove(topSide[i]);
                    bigger.Remove(square.Order * square.Order + 1 - topSide[i]);
                    bigCount--;
                    smallCount--;
                }

                //Left numbers where numbers are bigger than the center + opposite
                for (int i = 0; i < (square.Order - 2) / 2; ++i)
                {
                    leftSide[i] = bigger[rnd.Next(bigCount)];
                    sumLeft += leftSide[i];
                    bigger.Remove(leftSide[i]);
                    smaller.Remove(square.Order * square.Order + 1 - leftSide[i]);
                    bigCount--;
                    smallCount--;
                }

                //Top numbers where numbers are smalles than the center + opposite
                for (int i = (square.Order - 2) / 2; i < square.Order - 2; ++i)
                {
                    leftSide[i] = smaller[rnd.Next(smallCount)];
                    sumLeft += leftSide[i];
                    smaller.Remove(leftSide[i]);
                    bigger.Remove(square.Order * square.Order + 1 - leftSide[i]);
                    bigCount--;
                    smallCount--;
                }
            } while (sumLeft != magicSum || sumTop != magicSum);
            
            //Adding the border
            for (int i = 1; i < square.Order - 1; ++i)
            {
                square.SquareValues[0, i] = topSide[i - 1];
                outerNumbers.Remove(topSide[i - 1]);
                square.SquareValues[square.Order - 1, i] = s - topSide[i - 1];
                outerNumbers.Remove(square.SquareValues[square.Order - 1, i]);
                square.SquareValues[i, 0] = leftSide[i - 1];
                outerNumbers.Remove(leftSide[i - 1]);
                square.SquareValues[i, square.Order - 1] = s - leftSide[i - 1];
                outerNumbers.Remove(square.SquareValues[i, square.Order - 1]);
            }
        } //Adds the border around the center for even order

        public static void AddSidesOdd(ref Square square)
        {
            int s = square.Order * square.Order + 1;
            Random rnd=new Random();
            List<int> outerNumbers = new List<int>();
            for (int i = 1; i <= 2 * (square.Order - 1); ++i)
            {
                outerNumbers.Add(i);
            }
            for (int i = (square.Order * square.Order) - 2 * (square.Order - 1) + 1; i <= square.Order * square.Order; ++i)
            {
                outerNumbers.Add(i);
            }
            outerNumbers.Sort();

            switch (rnd.Next(3))
            {
                case 0:
                    #region method 1
                    for (int i = 0; i < square.Order / 2 - 1; ++i)
                    {
                        square.SquareValues[square.Order - 2 - i, 0] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[square.Order - 2 - i, square.Order - 1] = s - square.SquareValues[square.Order - 2 - i, 0];
                        outerNumbers.Remove(square.SquareValues[square.Order - 2 - i, square.Order - 1]);

                        square.SquareValues[square.Order - 1, i + 1] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[0, i + 1] = s - square.SquareValues[square.Order - 1, i + 1];
                        outerNumbers.Remove(square.SquareValues[0, i + 1]);
                    }

                    square.SquareValues[square.Order - 1, square.Order / 2] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[0, square.Order / 2] = s - square.SquareValues[square.Order - 1, square.Order / 2];
                    outerNumbers.Remove(square.SquareValues[square.Order - 1, square.Order / 2 + 1]);

                    square.SquareValues[0, 0] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order - 1, square.Order - 1] = s - square.SquareValues[0, 0];
                    outerNumbers.Remove(square.SquareValues[square.Order - 1, square.Order - 1]);

                    square.SquareValues[square.Order / 2, square.Order - 1] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order / 2, 0] = s - square.SquareValues[square.Order / 2, square.Order - 1];
                    outerNumbers.Remove(square.SquareValues[square.Order / 2 + 1, square.Order - 1]);

                    square.SquareValues[0, square.Order - 1] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order - 1, 0] = s - square.SquareValues[0, square.Order - 1];
                    outerNumbers.Remove(square.SquareValues[0, square.Order - 1]);

                    for (int i = 0; i < square.Order / 2 - 1; ++i)
                    {
                        square.SquareValues[square.Order / 2 - 1 - i, square.Order - 1] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[square.Order / 2 - 1 - i, 0] = s - square.SquareValues[square.Order / 2 - 1 - i, square.Order - 1];
                        outerNumbers.Remove(square.SquareValues[square.Order / 2 - i, 0]);

                        square.SquareValues[0, square.Order / 2 + i + 1] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[square.Order - 1, square.Order / 2 + i + 1] = s - square.SquareValues[0, square.Order / 2 + i + 1];
                        outerNumbers.Remove(square.SquareValues[square.Order - 1, square.Order / 2 + i + 1]);
                    }
                    #endregion
                    break;

                case 1:
                    #region method 2
                    square.SquareValues[0, square.Order-2] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order - 1, square.Order - 2] = s - square.SquareValues[0, square.Order - 2];
                    outerNumbers.Remove(square.SquareValues[square.Order - 1, square.Order - 2]);

                    square.SquareValues[square.Order - 1, square.Order - 1] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[0, 0] = s - square.SquareValues[square.Order - 1, square.Order - 1];
                    outerNumbers.Remove(square.SquareValues[0, 0]);

                    for (int i = square.Order - 2; i > square.Order / 2 + 1;--i)
                    {
                        square.SquareValues[i, square.Order - 1] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[i, 0] = s - square.SquareValues[i, square.Order - 1];
                        outerNumbers.Remove(square.SquareValues[i, 0]);
                    }

                    for (int i = square.Order-3; i > square.Order/2-1; --i)
                    {
                        square.SquareValues[0, i] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[square.Order - 1, i] = s - square.SquareValues[0, i];
                        outerNumbers.Remove(square.SquareValues[square.Order - 1, i]);
                    }

                    square.SquareValues[square.Order/2+1,square.Order-1]=outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order / 2 + 1, 0] = s - square.SquareValues[square.Order / 2 + 1, square.Order - 1];
                    outerNumbers.Remove(square.SquareValues[square.Order / 2 + 1, 0]);

                    for (int i = square.Order/2; i > 0; --i)
                    {
                        square.SquareValues[i, 0] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[i, square.Order - 1] = s - square.SquareValues[i, 0];
                        outerNumbers.Remove(square.SquareValues[i, square.Order - 1]);
                    }

                    for (int i = square.Order/2-1; i > 0; --i)
                    {
                        square.SquareValues[square.Order - 1, i] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[0, i] = s - square.SquareValues[square.Order - 1, i];
                        outerNumbers.Remove(square.SquareValues[0, i]);
                    }

                    square.SquareValues[square.Order-1,0]=outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[0, square.Order - 1] = s - square.SquareValues[square.Order - 1, 0];
                    outerNumbers.Remove(square.SquareValues[0, square.Order - 1]);
                    #endregion
                    break;

                case 2:
                    #region method 3
                    for (int i = 0; i < square.Order/2; ++i)
                    {
                        square.SquareValues[square.Order - 1, i + 1] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[0, i + 1] = s - square.SquareValues[square.Order - 1, i + 1];
                        outerNumbers.Remove(square.SquareValues[0, i + 1]);

                        square.SquareValues[i,square.Order-1]=outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        if (i == 0)
                        {
                            square.SquareValues[square.Order - 1, 0] = s - square.SquareValues[i, square.Order - 1];
                            outerNumbers.Remove(square.SquareValues[square.Order - 1, 0]);
                        }
                        else
                        {
                            square.SquareValues[i, 0] = s - square.SquareValues[i, square.Order - 1];
                            outerNumbers.Remove(square.SquareValues[i, 0]);
                        }
                    }

                    square.SquareValues[square.Order/2,0]=outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order / 2, square.Order - 1] = s - square.SquareValues[square.Order / 2, 0];
                    outerNumbers.Remove(square.SquareValues[square.Order / 2, square.Order - 1]);

                    for (int i = square.Order/2+1; i < square.Order-1; ++i)
                    {
                        square.SquareValues[i, 0] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[i, square.Order - 1] = s - square.SquareValues[i, 0];
                        outerNumbers.Remove(square.SquareValues[i, square.Order - 1]);

                        square.SquareValues[0, i] = outerNumbers[0];
                        outerNumbers.RemoveAt(0);
                        square.SquareValues[square.Order - 1, i] = s - square.SquareValues[0, i];
                        outerNumbers.Remove(square.SquareValues[square.Order - 1, i]);
                    }

                    square.SquareValues[0, 0] = outerNumbers[0];
                    outerNumbers.RemoveAt(0);
                    square.SquareValues[square.Order - 1, square.Order - 1] = s - square.SquareValues[0, 0];
                    outerNumbers.Remove(square.SquareValues[square.Order - 1, square.Order - 1]);
                    #endregion
                    break;
            }

            
        }  //Adds the border around the center for odd order

        public static Square RemoveLayer(Square square)
        {
            int[,] newValues = new int[square.Order - 2, square.Order - 2];

            for (int i = 0; i < square.Order - 2; ++i)
            {
                for (int j = 0; j < square.Order - 2; ++j)
                {
                    newValues[i, j] += square.SquareValues[i+1,j+1];
                    
                }
            }

            return new Square(square.Order - 2, newValues, square.Removed + 1);
        } //Removes a layer of border
    }
}
