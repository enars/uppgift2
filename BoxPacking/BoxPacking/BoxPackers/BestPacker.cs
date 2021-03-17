using BoxPacking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxPacking
{
    /// <summary> 
    /// Best fit packing algorithm for evenly sorting weighted articles into boxes
    /// Follows steps:
    /// 1. Create list with given number of boxes
    /// 2. Sort articles by weight and put into a queue
    /// 3. Take out the first article and the last box and put the article into the box
    /// 4. Insert the box back into the list in order of priority - lowest weight last
    /// 5. Repeat step 3 and 4 until no articles remain
    /// </summary>
    public class BestPacker : IBoxPacker
    { 
        public List<Box> Pack(List<Article> articles, int numBoxes)
        {
            List<Box> Boxes = CreateNumBoxesList(numBoxes);

            articles = articles.OrderByDescending(a => a.WeightInGrams).ToList();
            Queue<Article> ArticleQueue = new Queue<Article>(articles);

            Box Box;
            Article Art;
            int BoxIndexMax = Boxes.Count - 1;
            while (ArticleQueue.Count > 0)
            {
                Art = ArticleQueue.Dequeue();
                Box = Boxes[BoxIndexMax];
                Boxes.RemoveAt(BoxIndexMax);

                Box.Add(Art);

                Boxes = InsertInPriority(Boxes, Box);
            }

            return Boxes;
        }

        /// <summary>
        /// Find priority insertion placement by iteration, stop and place when found to decrease number of iterations, else place last
        /// </summary>
        /// <param name="boxes">A list of Box objects sorted in priority of lowest weight first</param>
        /// <param name="inputBox">The Box that is to be inserted in priority</param>
        /// <returns>A new priority sorted list</returns>
        List<Box> InsertInPriority(List<Box> boxes, Box inputBox)
        {
            int i = 0;
            bool IsPlaced = false;
            foreach (Box b in boxes)
            {
                if (inputBox.TotalWeightInGrams >= b.TotalWeightInGrams)
                {
                    boxes.Insert(i, inputBox);
                    IsPlaced = true;
                    break;
                }
                else
                    i++;
            }

            if (!IsPlaced)
                boxes.Add(inputBox);

            return boxes;
        }


        /// Helper function
        List<Box> CreateNumBoxesList(int numBoxes)
        {
            List<Box> Boxes = new List<Box>(numBoxes);
            for (int i = 0; i < numBoxes; i++)
                Boxes.Add(new Box());

            return Boxes;
        }
    }
}
