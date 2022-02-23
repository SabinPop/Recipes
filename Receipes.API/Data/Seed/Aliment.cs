using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.API.Data.Seed
{
    public class Aliment
    {
        public string Nume { get; set; }
        public double Calorii { get; set; }
        public string Proteine { get; set; }
        public double? Lipide { get; set; }
        public string Carbohidrati { get; set; }
        public double? Fibre { get; set; }
        public string Aproximari { get; set; }
    }
}
