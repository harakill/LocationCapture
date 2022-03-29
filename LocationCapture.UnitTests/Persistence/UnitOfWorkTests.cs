using LocationCapture.Core;
using LocationCapture.Core.Repositories;
using LocationCapture.Persistence;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LocationCapture.UnitTests.Persistence
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Complete_WhenCall_SaveChangesAsyncIsCall()
        {
            var mockDbContext = new Mock<IApplicationDbContext>();
            var mockPlacement = new Mock<IPlacementRepository>();
            
            var mockunitOfWork = new UnitOfWork(mockDbContext.Object, mockPlacement.Object);

            await mockunitOfWork.CompleteAsync();

            mockDbContext.Verify(m => m.SaveChangesAsync());
        }
    }
}