using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Domain.Entites
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Application> Applications { get; set; }

    }
}
