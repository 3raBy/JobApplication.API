using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using JobApplication.Domain.Enums;

namespace JobApplication.Domain.Entites
{
    public class Application
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }
        public int JobId { get; set; }
        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }
        public DateTime AppliedAt { get; set; }
        public DateTime StatusUpdatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
