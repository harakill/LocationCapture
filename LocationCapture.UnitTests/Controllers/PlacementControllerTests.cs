
using FluentAssertions;
using LocationCapture.Controllers;
using LocationCapture.Core;
using LocationCapture.Core.Domain;
using LocationCapture.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationCapture.UnitTests.Controllers
{
    [TestFixture]
    public class PlacementControllerTests
    {
        private Mock<IPlacementRepository> _mockPlacement;
        private Mock<IUnitOfWork> _mockunitOfWork;

        private PlacementController _controller;

        [SetUp]
        public void Setup()
        {
            _mockPlacement = new Mock<IPlacementRepository>();

            _mockunitOfWork = new Mock<IUnitOfWork>();
            _mockunitOfWork.SetupGet(uow => uow.Placements).Returns(_mockPlacement.Object);

            _controller = new PlacementController(_mockunitOfWork.Object);
        }

        [Test]
        public async Task GetPlacements_WhenNoPlacements_ShouldReturnNotFound()
        {
            var result = await _controller.GetPlacements();

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetPlacements_WhenCall_ShouldReturnOk()
        {
            var placement = new List<Placement>()
            {
                new Placement
                {
                    Id = 1,
                    Latitude = It.IsAny<int>(),
                    Longitude = It.IsAny<int>(),
                    TimeStamp = DateTime.Now,
                    Vehicle = It.IsAny<string>()
                },
                new Placement
                {
                    Id = 2,
                    Latitude = It.IsAny<int>(),
                    Longitude = It.IsAny<int>(),
                    TimeStamp = DateTime.Now,
                    Vehicle = It.IsAny<string>()
                }
            };

            _mockPlacement.Setup(p => p.GetAll())
                .Returns(Task.FromResult<IEnumerable<Placement>>(placement));

            var result = await _controller.GetPlacements();

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetPlacementById_WhenIdNotExist_ShouldReturnNotFound()
        {
            var result = await _controller.GetPlacementById(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetPlacementById_WhenCallWithValidId_ShouldReturnOkWithCorrectResult()
        {
            var placement = new Placement
                {
                    Id = 1,
                    Latitude = It.IsAny<int>(),
                    Longitude = It.IsAny<int>(),
                    TimeStamp = DateTime.Now,
                    Vehicle = It.IsAny<string>()
                };

            _mockPlacement.Setup(p => p.Get(placement.Id))
                .Returns(Task.FromResult(placement));

            var result = await _controller.GetPlacementById(1);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task CreatePlacement_WhenModelStateNotValid_ShouldBadRequest()
        {
            _controller.ModelState.AddModelError("key", "Error");

            var placement = new Placement
            {
                Id = 1,
                Latitude = It.IsAny<int>(),
                Longitude = It.IsAny<int>(),
                TimeStamp = DateTime.Now,
                Vehicle = It.IsAny<string>()
            };

            _mockPlacement.Setup(p => p.Add(placement))
                .Returns(Task.FromResult(placement));

            var result = await _controller.CreatePlacement(placement);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task CreatePlacement_WhenCallWithValidData_ShouldReturnOkResult()
        {
            var placement = new Placement
            {
                Latitude = It.IsAny<int>(),
                Longitude = It.IsAny<int>(),
                TimeStamp = DateTime.Now,
                Vehicle = It.IsAny<string>()
            };

            _mockPlacement.Setup(p => p.Add(placement))
                .Returns(Task.FromResult(placement));

            var result = await _controller.CreatePlacement(placement);

            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task GetSpeedOfMovement_WhenWrongIds_ShouldReturnNotFound()
        {
            var result = await _controller.GetSpeedOfMovement(-1, -2);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetSpeedOfMovement_WhenModelStateIsNotValid_ShouldBadRequest()
        {
            _controller.ModelState.AddModelError("key", "Error");

            var result = await _controller.GetSpeedOfMovement(1, 2);

            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task GetSpeedOfMovement_WhenCallWithValidData_ShouldReturnOkResult()
        {
            var dbl = 1.1;
            _mockPlacement.Setup(p => p.CalculateSpeedOfMovement(1, 2))
                .Returns(Task.FromResult(dbl));

            var result = await _controller.GetSpeedOfMovement(1, 2);

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
