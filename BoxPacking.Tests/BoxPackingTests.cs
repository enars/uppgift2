using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoxPacking;
using BoxPacking.Models;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace BoxPacking.Tests
{
    /// <summary>
    /// Tests for the Box Packing Project
    /// </summary>
    [TestClass]
    public class BoxPackingTests
    {
        /**
         * Initialization method for setting up list of articles
         */
        public List<Article> InitArticleTestList(int[] weights)
        {
            int numArticles = weights.Count();
            List<Article> Articles = new List<Article>(numArticles);

            for (int i = 0; i < numArticles; i++)
                Articles.Add(new Article(weights[i]));
                
            return Articles;
        }

        ///<summary>
        /// Simple tests for Best Packer algorithm
        /// </summary>
        [TestMethod]
        public void Should_Sort_2_Boxes_With_0_Articles()
        {
            IBoxPacker Packer = new BestPacker();
            int[] Weights = { };
            int numBoxes = 0;
            List<Article> Articles = InitArticleTestList(Weights);

            List<Box> PackedBoxes = Packer.Pack(Articles, numBoxes);

            int ExpectedSum = 0;
            foreach (Box box in PackedBoxes)
            {
                Assert.IsTrue(box.TotalWeightInGrams == ExpectedSum);
            }
        }

        [TestMethod]
        public void Should_Sort_5_Boxes_With_10_Similiar_Items_Evenly()
        {
            IBoxPacker Packer = new BestPacker();
            int[] Weights = { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
            int numBoxes = 5;
            List<Article> Articles = InitArticleTestList(Weights);
            
            List<Box> PackedBoxes = Packer.Pack(Articles, numBoxes);

            int ExpectedSum = 200;
            foreach (Box box in PackedBoxes)
            {
                Assert.IsTrue(box.TotalWeightInGrams == ExpectedSum);
            }
        }

        [TestMethod]
        public void Should_SomeWhat_Evenly_Sort_3_Boxes_With_6_Different_Articles()
        {
            IBoxPacker Packer = new BestPacker();
            int[] Weights = { 1000, 300, 300, 400, 500, 500 };
            int numBoxes = 3;
            List<Article> Articles = InitArticleTestList(Weights);

            List<Box> PackedBoxes = Packer.Pack(Articles, numBoxes);

            int[] ExpectedWeightRange = { 900, 1000, 1100 };
            foreach (Box box in PackedBoxes)
            {
                Assert.IsTrue(ExpectedWeightRange.Contains(box.TotalWeightInGrams));
            }
        }


        ///<summary>
        /// Article and Box testing
        /// </summary>
        [TestMethod]
        public void Should_Fail_To_Create_Article_When_Weight_Is_Out_Of_Range()
        {
            try
            {
                Article Article = new Article(1001);
                Assert.Fail();
            }
            catch (System.ArgumentOutOfRangeException) { }
        }

        [TestMethod]
        public void Should_Return_Total_Weight_Of_Box()
        {
            Box Box = new Box();
            int[] Weights = { 100, 100, 100, 100, 100 };
            List<Article> Articles = InitArticleTestList(Weights);

            Articles.ForEach(a => Box.Add(a));

            Assert.IsTrue(Box.TotalWeightInGrams == 500);
        }

        [TestMethod]
        public void Should_Return_0_From_Empty_Box()
        {
            Box Box = new Box();

            Assert.IsTrue(Box.TotalWeightInGrams == 0);
        }
    }
}
