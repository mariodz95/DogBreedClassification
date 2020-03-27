using DogBreed.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.Model
{
    public class User : IUser
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public IDogImage DogImage { get; set; }
    }
}
