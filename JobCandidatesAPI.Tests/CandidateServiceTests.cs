using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Logic.Dtos;
using Logic.Interfaces;
using Logic.MappingProfiles;
using Logic.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace JobCandidatesAPI.Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public CandidateServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = new Mapper(config);
            _memoryCache = new MemoryCache(new MemoryCacheOptions()); ;
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper, _memoryCache);
        }

        [Fact]

        public async Task AddOrUpdateCandidate_CandidateExists_UpdateCandidate()
        {
            var candidateDto = new CandidateDto()
            {
                Email = "test@gmail.com",
                FirstName = "TestNewName",
                LastName = "TestLastName",
                Comment = "TestComment",
                PhoneNumber = "+1234567890"
            };

            var existingCandidate = new Candidate()
            {
                Email = "test@gmail.com",
                FirstName = "TestName",
                LastName = "TestLastName",
                Comment = "TestComment",
                PhoneNumber = "+1234567890"
            };

            _candidateRepositoryMock.Setup(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()))
                            .ReturnsAsync(existingCandidate);

            await _candidateService.AddOrUpdateCandidate(candidateDto);

            _candidateRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Candidate>(c => c.Email == "test@gmail.com")), Times.Once());
            _candidateRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]

        public async Task AddOrUpdateCandidate_NewCandidate_AddCandidate()
        {
            var candidateDto = new CandidateDto()
            {
                Email = "test@gmail.com",
                FirstName = "TestNewName",
                LastName = "TestLastName",
                Comment = "TestComment",
                PhoneNumber = "+1234567890"
            };

            _candidateRepositoryMock.Setup(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()))
                            .ReturnsAsync((Candidate)null);

            await _candidateService.AddOrUpdateCandidate(candidateDto);

            _candidateRepositoryMock.Verify(r => r.AddAsync(It.Is<Candidate>(c => c.Email == "test@gmail.com")), Times.Once());
            _candidateRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

    }
}
