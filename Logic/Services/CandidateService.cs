using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Logic.Dtos;
using Logic.Interfaces;

namespace Logic.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task AddOrUpdateCandidate(CandidateDto candidateDto)
        {
            Candidate newCandidate = _mapper.Map<Candidate>(candidateDto);

            var existingCandidate = await _candidateRepository.FindSingleAsync(c => c.Email == newCandidate.Email);

            if (existingCandidate != null)
            {
                await _candidateRepository.UpdateAsync(newCandidate);
            }
            else
            {
                await _candidateRepository.AddAsync(newCandidate);
            }

            await _candidateRepository.SaveChangesAsync();
        }
    }
}
