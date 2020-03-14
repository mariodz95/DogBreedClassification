using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBreed.ViewModel
{
    public class DogBreedViewModel
    {
        public string Name { get; set; }
        public float Score { get; set; }
    }

    public class ResultViewModel
    {
        public List<DogBreedViewModel> Results { get; set; }
    }

    public class ResultsViewModel
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public DateTime DateCreated { get; set; }
        public List<DogBreedViewModel> Results { get; set; }
    }
}
