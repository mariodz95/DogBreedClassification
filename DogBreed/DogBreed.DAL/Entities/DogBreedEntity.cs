using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.DAL.Entities
{
    public class DogBreedEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
