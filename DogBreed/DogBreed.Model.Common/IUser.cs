using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.Model.Common
{
    public interface IUser
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
       
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
       
        public DateTime DateCreated { get; set; }
       
        public DateTime DateUpdated { get; set; }
    }
}
