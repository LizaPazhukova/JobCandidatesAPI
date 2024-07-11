using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Logic.Dtos;
using Logic.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Logic.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task AddOrUpdateCandidate(CandidateDto candidateDto)
        {
            Candidate newCandidate = _mapper.Map<Candidate>(candidateDto);

            var existingCandidate = _memoryCache.Get<Candidate>($"Candidate_{newCandidate.Email}");

            if (existingCandidate == null)
            {
                existingCandidate = await _candidateRepository.FindSingleAsync(c => c.Email == newCandidate.Email);
            }

            if (existingCandidate != null)
            {
                existingCandidate.FirstName = newCandidate.FirstName;
                existingCandidate.LastName = newCandidate.LastName;
                existingCandidate.CallTime = existingCandidate.CallTime;
                existingCandidate.PhoneNumber = newCandidate.PhoneNumber;
                existingCandidate.CallTime = newCandidate.CallTime;
                existingCandidate.GitHubUrl = newCandidate.GitHubUrl;
                existingCandidate.LinkedInUrl = newCandidate.LinkedInUrl;

                await _candidateRepository.UpdateAsync(existingCandidate);
            }
            else
            {
                await _candidateRepository.AddAsync(newCandidate);
                existingCandidate = newCandidate;
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(10.0);
            
            _memoryCache.Set($"Candidate_{existingCandidate.Email}", existingCandidate, expirationTime);

            await _candidateRepository.SaveChangesAsync();
        }
    }
}
