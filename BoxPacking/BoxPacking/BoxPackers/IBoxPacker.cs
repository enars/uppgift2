using BoxPacking.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxPacking
{
    public interface IBoxPacker
    {
        List<Box> Pack(List<Article> articles, int numBoxes);
    }
}
