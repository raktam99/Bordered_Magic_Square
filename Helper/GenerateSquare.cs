

namespace Bordered_Magic_Square.Helper
{
    public static class GenerateSquare
    {
        public static Square Odd()
        {
            int order = 3;
            int[,] baseSquare = new int[order, order];
            int[] pos = new int[] { 0, order/2 };
            int x = 1;

            for (int i = 0; i < order; ++i)
            {
                for (int j = 0; j < order; ++j)
                {
                    baseSquare[pos[0], pos[1]] = x;
                    x++;

                    if (j + 1 != order)
                    {
                        if (pos[0] == 0)
                            pos[0] = order - 1;
                        else pos[0]--;

                        if (pos[1] == order - 1)
                            pos[1] = 0;
                        else pos[1]++;
                    }
                }
                if (pos[0] == order - 1)
                    pos[0] = 0;
                else
                    pos[0]++;
            }



            return new Square(order,baseSquare,0);
        } //Generates base magic square of order 3

        public static Square Even()
        {
            int order = 4;
            int[,] baseSquare = new int[order, order];
            int helper;


            for (int i = 0; i < order; ++i)
            {
                for (int j = 0; j < order; ++j)
                {
                    baseSquare[i, j] = order * i + j + 1;
                }
            }

            for (int i = 0; i < order / 2; ++i)
            {
                helper = baseSquare[i, i];
                baseSquare[i, i] = baseSquare[order - 1 - i, order - 1 - i];
                baseSquare[order - 1 - i, order - 1 - i] = helper;

                helper = baseSquare[i, order - 1 - i];
                baseSquare[i, order - 1 - i] = baseSquare[order - 1 - i, i];
                baseSquare[order - 1 - i, i] = helper;
            }

            return new Square(order,baseSquare,0);
        } //Generates base magic square of order 4
    }
}
