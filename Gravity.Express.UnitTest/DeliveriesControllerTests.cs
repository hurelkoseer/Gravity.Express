using Gravity.Express.API.Controllers;
using Gravity.Express.API.Models;
using Gravity.Express.Application.Common.Models;
using Gravity.Express.Application.Cqrs.Delivery.Commands.CreateDelivery;
using Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;
using Gravity.Express.Application.Filter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using Mediator;

namespace Gravity.Express.UnitTest;

public class DeliveriesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;

    private readonly DeliveriesController _controller;

    public DeliveriesControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new DeliveriesController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedDeliveryId_WhenCommandIsSuccessful()
    {
        // Arrange
        var command = new CreateDeliveryCommand
        {
            CustomerId = Guid.NewGuid(), // get customer id from DB
            DeliveryAddress = "Test Address",
            senderWarehouseAddress = "Warehouse Address",
            TrackingNumber = "123456",
            IsPassive = false
        };
        var expectedId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateDeliveryCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new CreateDeliveryCommandResponse(expectedId));

        // Act
        var actionResult = await _controller.Create(new CreateDeliveryRequest
        {
            CustomerId = command.CustomerId,
            DeliveryAddress = command.DeliveryAddress,
            senderWarehouseAddress = command.senderWarehouseAddress,
            TrackingNumber = command.TrackingNumber,
            IsPassive = command.IsPassive
        });

        // Assert
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedId = okResult.Value.Should().Be(expectedId);
    }

    [Fact]
    public async Task Get_ShouldReturnPaginatedListOfDeliveries_WhenQueryIsSuccessful()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new DeliveriesController(mediatorMock.Object);
        var filterModel = new FilterModel();
        var expectedResponse =
            new PaginatedList<GetDeliveriesQueryResponse>(new List<GetDeliveriesQueryResponse>(), 1, 1, 10);

        mediatorMock.Setup(m => m.Send(It.IsAny<GetDeliveriesQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);

        // Act
        var actionResult = await controller.Get(filterModel);

        // Assert
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedValue = okResult.Value.Should().BeEquivalentTo(expectedResponse);
    }
}
