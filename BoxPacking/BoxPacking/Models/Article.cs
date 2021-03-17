using System;
using System.Collections.Generic;
using System.Text;

namespace BoxPacking.Models
{
    public class Article
    {
        private int _weightInGrams;

        //Constructor
        public Article(int weightInGrams)
        {
            WeightInGrams = weightInGrams;
        }

        //Get and set Article weight
        public int WeightInGrams
        {
            get { return _weightInGrams; }
            set
            {
                //Check and set if weight is ok
                if (value >= 100 && value <= 1000)
                    _weightInGrams = value;
                else
                    throw new System.ArgumentOutOfRangeException(
                        nameof(value),
                        "Article weight value out of range.");
            }
        }
    }
}
