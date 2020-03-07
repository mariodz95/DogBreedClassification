using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBreed
{
    public class DogBreedViewModel
    {
        public string Name{ get; set; }
        public float Score { get; set; }
    }

    public class ResultViewModel
    {
        public List<DogBreedViewModel> Results { get; set; }
    }
}
