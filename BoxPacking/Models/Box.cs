using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoxPacking.Models
{
    public class Box
    {
        private int _totalWeightInGrams = 0;
        private List<Article> _boxItems = new List<Article>();

        IReadOnlyList<Article> BoxItems => _boxItems.AsReadOnly();
        public int TotalWeightInGrams => _totalWeightInGrams;

        public void Add(Article article)
        {
            _boxItems.Add(article);
            _totalWeightInGrams += article.WeightInGrams;
        }
    }
}
