using System;
using System.Collections.Generic;

namespace Over_Engineered_FizzBuzz
{
    //Used to store the properites of a file so that they can be passed easily
    public struct FizzBuzzProperties
    {
        private int iterations;

        public int Iterations { get; private set; }

        private Dictionary<int, string> divisorWordPairs;

        public Dictionary<int, string> DivisorWordPairs { get; private set; }

        public FizzBuzzProperties(int amountToIterate, Dictionary<int, string> keyValues) : this()
        {
            Iterations = amountToIterate;

            DivisorWordPairs = keyValues;
        }
    }
}
