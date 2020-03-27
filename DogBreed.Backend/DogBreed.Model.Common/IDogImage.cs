using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.Model.Common
{
    public interface IDogImage
    {
        public Guid Id { get; set; }
       
        public string Name { get; set; }
       
        public string Abrv { get; set; }
       
        public float Score { get; set; }
       
        public byte[] File { get; set; }
       
        public DateTime DateCreated { get; set; }
      
        public DateTime DateUpdated { get; set; }
      
        public IPredictionResult PredictionResults { get; set; }
        public Guid UserId { get; set; }
    }
}
