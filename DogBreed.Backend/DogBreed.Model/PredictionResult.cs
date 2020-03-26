using DogBreed.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.Model
{
    class PredictionResult : IPredictionResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public float Score { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Guid DogImageId { get; set; }
    }
}
