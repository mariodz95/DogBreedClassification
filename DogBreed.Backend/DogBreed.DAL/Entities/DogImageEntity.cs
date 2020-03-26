using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.DAL.Entities
{
    public class DogImageEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public float Score { get; set; }

        public byte[] File { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
        
        public PredictionResultEntity PredictionResults { get; set; }
    }
}
