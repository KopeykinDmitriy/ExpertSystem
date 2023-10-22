using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    internal class MainMatrix
    {
        private readonly int _height = 7;
        private readonly int _width = 5;

        private List<int> matrix = new List<int>();

        public MainMatrix()
        {
            for (int i = 0; i < _height * _width; i++)
            {
                matrix.Add(0);
            }
        }

        public int GetElement(int x, int y)
        {
            if (x >= _height || y >= _width)
                throw new ArgumentException();
            return matrix[y + x * _width];
        }
        public void SetElement(int x, int y, int value)
        {
            if (x >= _height || y >= _width)
                throw new ArgumentException();
            matrix[y + x * _width] = value;
        }

        public void Education(int x, List<int> educationMatrix)
        {
            if (educationMatrix == null || educationMatrix.Count != _height)
                throw new ArgumentException();

            for (int h = 0; h < _height; h++)
                for (int w = 0; w < _width; w++)
                {
                    if (w == x)
                    {
                        SetElement(h, w, GetElement(h, w) + educationMatrix[h]);
                    }
                    else
                    {
                        SetElement(h, w, GetElement(h, w) - educationMatrix[h]);
                    }
                }
        }

        public int Expert(List<int> inputMatrix)
        {
            if (inputMatrix == null || inputMatrix.Count != _height)
                throw new ArgumentException();

            var maxValue = int.MinValue;
            var targetClass = int.MinValue;

            for (int h = 0; h < _height; h++)
                for (int w = 0; w < _width; w++)
                {
                    var value = 0;
                    value += GetElement(h, w) * inputMatrix[w];
                    if (value > maxValue)
                    {
                        maxValue = value;
                        targetClass = h;
                    }
                }
            return targetClass;
        }
    }
}
