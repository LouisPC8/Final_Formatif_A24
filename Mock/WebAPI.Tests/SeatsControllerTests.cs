using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{

    Mock<SeatsService> serviceSeatMock;
    Mock<SeatsController> controllerSeatMock;

    public SeatsControllerTests()
    {
        serviceSeatMock = new Mock<SeatsService>();
        controllerSeatMock = new Mock<SeatsController>(serviceSeatMock.Object){ CallBase = true};

        controllerSeatMock.Setup(c => c.UserId).Returns("1");
    }

    [TestMethod]
    public void ReserveSeat()
    {
        Seat seat = new Seat();
        seat.Id = 1;
        seat.Number = 1;

        serviceSeatMock.Setup(s => s.ReserveSeat(It.IsAny<String>(), It.IsAny<int>())).Returns(seat);

        var result = controllerSeatMock.Object.ReserveSeat(seat.Number).Result as OkObjectResult;

        Assert.IsNotNull(result);


    }

    [TestMethod]
    public void ReserveSeat_AlreadyTaken()
    {

        serviceSeatMock.Setup(s => s.ReserveSeat(It.IsAny<String>(), It.IsAny<int>())).Throws(new SeatAlreadyTakenException());

        var result = controllerSeatMock.Object.ReserveSeat(1).Result as UnauthorizedResult;
        Assert.IsNotNull(result);

    }
    [TestMethod]
    public void ReserveSeat_TooBig()
    {

        serviceSeatMock.Setup(s => s.ReserveSeat(It.IsAny<String>(), It.IsAny<int>())).Throws(new SeatOutOfBoundsException());

        var result = controllerSeatMock.Object.ReserveSeat(1).Result as NotFoundObjectResult;
        Assert.IsNotNull(result);

    }

    [TestMethod]
    public void ReserveSeat_AlreadySeated()
    {

        serviceSeatMock.Setup(s => s.ReserveSeat(It.IsAny<String>(), It.IsAny<int>())).Throws(new UserAlreadySeatedException());

        var result = controllerSeatMock.Object.ReserveSeat(1).Result as BadRequestResult;
        Assert.IsNotNull(result);

    }
}
