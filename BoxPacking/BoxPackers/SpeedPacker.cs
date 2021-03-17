using BoxPacking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxPacking
{
    /** Speed packer algorithm
     * Create given number of boxes
     * Sort articles from highest to lowest weight and put them into a queue
     * Going from left to right in the Box array, 
     * dequeue the heaviest article and put it into a box,
     * Reverse when the bounds of the box array is reached, 
     * continue until no articles remain
     */
    public class SpeedPacker : IBoxPacker
    {
        public List<Box> Pack(List<Article> articles, int numBoxes)
        {
            List<Box> Boxes = CreateNumBoxes(numBoxes);
            
            //articles = OrderArticlesByWeight(articles);
            Queue<Article> ArticleQueue = CreateOrderedArticleQueue(articles);

            int Index = 0;
            bool IsReversed = false;
            Box Box;
            Article Article;

            while (ArticleQueue.Count() > 0)
            {
                Article = ArticleQueue.Dequeue();

                Box = IsReversed ? Boxes[--Index] : Boxes[Index++];

                Box.Add(Article);

                if (Index == Boxes.Count())
                {
                    IsReversed = true;
                }
                if (Index <= 0)
                {
                    IsReversed = false;
                }
            }

            return Boxes;
        }

        //Helpers
        List<Box> CreateNumBoxes(int numBoxes)
        {
            List<Box> Boxes = new List<Box>(numBoxes);

            for (int i = 0; i < numBoxes; i++)
                Boxes.Add(new Box());

            return Boxes;
        }

        List<Article> OrderArticlesByWeight(List<Article> articles) => articles.OrderBy(a => a.WeightInGrams).ToList(); 

        Queue<Article> CreateOrderedArticleQueue(List<Article> articles) => 
            new Queue<Article>(articles.OrderByDescending(a => a.WeightInGrams));
    }
}
