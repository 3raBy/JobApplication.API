using System;
using System.Collections.Generic;
using System.Text;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entites;
using Microsoft.AspNetCore.Http.HttpResults;
namespace JobApplication.Application.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        //public async Task<Job?> GetJobByIdAsync(int id)
        //{
        //    return await _jobRepository.GetJobByIdAsync(id);
        //} 
        //public async Task<Job> CreateJobAsync(Job job)
        //{
            
        //}
        //public async Task<Job> UpdateJobAsync(Job newJob)
        //{
        //}
        //public async Task<bool> DeleteJobAsync(int id)
        //{
        //    return await _jobRepository.DeleteJobAsync(id);
        //}
        //public async Task<bool> CloseJobAsync(int id)
        //{
            
        //}
    }
}
