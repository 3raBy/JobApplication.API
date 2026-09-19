using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Domain.Entites
{
    public class Job
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string ClosedBy { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
