using System;

public static class Matrix
{
    public static int Row(double[,] matrix)
    {
        return matrix.GetLength(0);
    }

    public static int Column(double[,] matrix)
    {
        return matrix.GetLength(1);
    }

    public static double[,] Random(int row, int column)
    {
        double[,] matrix = new double[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                matrix[i, j] = UnityEngine.Random.Range(0, 1f);
            }
        }

        return matrix;
    }

    public static double[,] Add(double[,] matrix1, double[,] matrix2)
    {
        if (Row(matrix1) == Row(matrix2) && Column(matrix1) == Column(matrix2))
        {
            double[,] matrix = new double[Row(matrix1), Column(matrix1)];

            for (int i = 0; i < Row(matrix1); i++)
            {
                for (int j = 0; j < Column(matrix2); j++)
                {
                    matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return matrix;
        }

        else
        {
            throw new System.Exception("Dimension Error!");
        }
    }

    public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
    {
        if (Column(matrix1) == Row(matrix2))
        {
            int n = Column(matrix1);
            double[,] matrix = new double[Row(matrix1), Column(matrix2)];
            double temp = 0;

            for (int i = 0; i < Row(matrix1); i++)
            {
                for (int j = 0; j < Column(matrix2); j++)
                {
                    for (int l = 0; l < n; l++)
                    {
                        temp += matrix1[i, l] * matrix2[l, j];
                    }

                    matrix[i, j] = temp;
                    temp = 0;
                }
            }

            return matrix;
        }

        else
        {
            throw new System.Exception("Dimension Error!");
        }
    }

    public static double[,] f(double[,] matrix, Func<Double, Double> f)
    {
        double[,] temp = new double[Row(matrix), Column(matrix)];

        for (int i = 0; i < Row(matrix); i++)
        {
            for (int j = 0; j < Column(matrix); j++)
            {
                temp[i, j] = f(matrix[i, j]);
            }
        }

        return temp;
    }
}
