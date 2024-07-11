using Logic.Dtos;

namespace Logic.Interfaces
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidate(CandidateDto candidate);
    }
}
