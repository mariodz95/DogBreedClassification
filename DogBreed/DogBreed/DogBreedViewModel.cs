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
        public IEnumerable Score { get; set; }
    }
}
