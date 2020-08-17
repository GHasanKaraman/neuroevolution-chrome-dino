using System;

namespace Mathematic
{
    //My own NN class
    public class NeuralNetwork : ICloneable
    {
        int inputSize;
        int hiddenSize1;
        int hiddenSize2;
        int outputSize;

        public double[,] weights1 = null;
        public double[,] weights2 = null;
        public double[,] weights3 = null;

        public double[,] bias1 = null;
        public double[,] bias2 = null;
        public double[,] bias3 = null;

        public NeuralNetwork(int inputLayer, int hiddenLayer1, int hiddenLayer2, int outputLayer)
        {
            inputSize = inputLayer;
            hiddenSize1 = hiddenLayer1;
            hiddenSize2 = hiddenLayer2;
            outputSize = outputLayer;

            weights1 = Matrix.Random(inputSize, hiddenSize1);
            weights2 = Matrix.Random(hiddenSize1, hiddenSize2);
            weights3 = Matrix.Random(hiddenSize2, outputSize);

            bias1 = Matrix.Random(1, hiddenLayer1);
            bias2 = Matrix.Random(1, hiddenLayer2);
            bias3 = Matrix.Random(1, outputLayer);
        }

        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        private double ReLu(double x)
        {
            if (x < 0)
            {
                return 0;
            }

            return x;
        }

        private double Tanh(double x)
        {
            return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        public double[,] Predict(double[,] input)
        {
            double[,] H1 = Matrix.Multiply(input, weights1);
            H1 = Matrix.Add(H1, Matrix.f(bias1,Negative));
            double[,] OutH1 = Matrix.f(H1, Tanh);

            double[,] H2 = Matrix.Multiply(OutH1, weights2);
            H2 = Matrix.Add(H2, Matrix.f(bias2, Negative));
            double[,] OutH2 = Matrix.f(H2, Tanh);

            double[,] O = Matrix.Multiply(OutH2, weights3);
            O = Matrix.Add(O, Matrix.f(bias3, Negative));
            double[,] OutO = Matrix.f(O, Sigmoid);

            return OutO;
        }

        public double Negative(double x)
        {
            return -x;
        }

        public static NeuralNetwork CrossOver(NeuralNetwork brain1, NeuralNetwork brain2)
        {
            NeuralNetwork newBrain = new NeuralNetwork(2, 6, 6, 4);

            for (int i = 0; i < brain1.weights1.GetLength(0); i++)
            {
                for (int j = 0; j < brain1.weights1.GetLength(1); j++)
                {
                    double rnd = UnityEngine.Random.Range(0, 1f);

                    if (rnd < 0.45)
                    {
                        newBrain.weights1[i, j] = brain1.weights1[i, j];
                    }

                    else if (rnd < 0.9)
                    {
                        newBrain.weights1[i, j] = brain2.weights1[i, j];
                    }

                    else
                    {
                        newBrain.weights1[i, j] = Mutate();
                    }
                }
            }

            for (int i = 0; i < brain1.weights2.GetLength(0); i++)
            {
                for (int j = 0; j < brain1.weights2.GetLength(1); j++)
                {
                    double rnd = UnityEngine.Random.Range(0, 1f);

                    if (rnd < 0.45)
                    {
                        newBrain.weights2[i, j] = brain1.weights2[i, j];
                    }

                    else if (rnd < 0.9)
                    {
                        newBrain.weights2[i, j] = brain2.weights2[i, j];
                    }

                    else
                    {
                        newBrain.weights2[i, j] = Mutate();
                    }
                }
            }

            for (int i = 0; i < brain1.weights3.GetLength(0); i++)
            {
                for (int j = 0; j < brain1.weights3.GetLength(1); j++)
                {
                    double rnd = UnityEngine.Random.Range(0, 1f);

                    if (rnd < 0.45)
                    {
                        newBrain.weights3[i, j] = brain1.weights3[i, j];
                    }

                    else if (rnd < 0.9)
                    {
                        newBrain.weights3[i, j] = brain2.weights3[i, j];
                    }

                    else
                    {
                        newBrain.weights3[i, j] = Mutate();
                    }
                }
            }

            for (int i = 0; i < brain1.bias1.GetLength(0); i++)
            {
                for (int j = 0; j < brain1.bias1.GetLength(1); j++)
                {
                    double rnd = UnityEngine.Random.Range(0, 1f);

                    if (rnd < 0.45)
                    {
                        newBrain.bias1[i, j] = brain1.bias1[i, j];
                    }

                    else if (rnd < 0.9)
                    {
                        newBrain.bias1[i, j] = brain2.bias1[i, j];
                    }

                    else
                    {
                        newBrain.bias1[i, j] = Mutate();
                    }
                }
            }

            for (int i = 0; i < brain1.bias2.GetLength(0); i++)
            {
                for (int j = 0; j < brain1.bias2.GetLength(1); j++)
                {
                    double rnd = UnityEngine.Random.Range(0, 1f);

                    if (rnd < 0.45)
                    {
                        newBrain.bias2[i, j] = brain1.bias2[i, j];
                    }

                    else if (rnd < 0.9)
                    {
                        newBrain.bias2[i, j] = brain2.bias2[i, j];
                    }

                    else
                    {
                        newBrain.bias2[i, j] = Mutate();
                    }
                }
            }

            for (int i = 0; i < brain1.bias3.GetLength(0); i++)
            {
                for (int j = 0; j < brain1.bias3.GetLength(1); j++)
                {
                    double rnd = UnityEngine.Random.Range(0, 1f);

                    if (rnd < 0.45)
                    {
                        newBrain.bias3[i, j] = brain1.bias3[i, j];
                    }

                    else if (rnd < 0.9)
                    {
                        newBrain.bias3[i, j] = brain2.bias3[i, j];
                    }

                    else
                    {
                        newBrain.bias3[i, j] = Mutate();
                    }
                }
            }

            return newBrain;
        }

        private static double Mutate()
        {
            double x = UnityEngine.Random.Range(0, 1f);

            if (UnityEngine.Random.Range(0, 1f) < 0.1)
            {
                double offset = UnityEngine.Random.Range(0, 1f);

                return x + offset;
            }

            return x;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public NeuralNetwork Copy()
        {
            return this.Clone() as NeuralNetwork;
        }
    }
}