using Moq;
using Xunit;
using TechSupportSystem.Services;
using TechSupportSystem.Data;
using TechSupportSystem.Models;
using TechSupportSystem.DTOs;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

public class TicketServiceTests
{
    private readonly Mock<ITicketRepo> _mockRepo;
    private readonly TicketService _service;

    public TicketServiceTests()
    {
        _mockRepo = new Mock<ITicketRepo>();
        _service = new TicketService(_mockRepo.Object);
    }

    //test that valid id returns a ticket
    [Fact]
    public async Task GetTicketByIdAsync_ValidId_ReturnsTicket()
    {
        var ticket = new Ticket { TicketId = 1, Description = "test ticket" };
        _mockRepo.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(ticket);

        var result = await _service.GetTicketByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("test ticket", result.Description);
    }

    //test that entering a missing ticket id throws an exeption
    [Fact]
    public async Task GetTicketByIdAsync_NotFound_ThrowsException()
    {
        _mockRepo.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync((Ticket)null);

        await Assert.ThrowsAsync<NullReferenceException>(() =>
            _service.GetTicketByIdAsync(1));
    }

    //test that creating a ticket returns the created ticket
    [Fact]
    public async Task CreateTicketAsync_ReturnsCreatedTicket()
    {
        var dto = new NewTicketDTO { Description = "New", Priority = "High" };

        var created = new Ticket
        {
            TicketId = 1,
            Description = "New",
            Priority = "High",
            Status = "Open",
            CreatedAt = DateTime.Now
        };

        _mockRepo.Setup(r => r.CreateTicketAsync(It.IsAny<Ticket>()))
                 .ReturnsAsync(created);

        var result = await _service.CreateTicketAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("New", result.Description);
        Assert.Equal("Open", result.Status);
    }


    //test invalid id throws exception
    [Fact]
    public async Task DeleteTicketAsync_InvalidId_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _service.DeleteTicketAsync(0));
    }

    //test that a ticket can be deleted
    [Fact]
    public async Task DeleteTicketAsync_ValidId_DeletesTicket()
    {
        var ticket = new Ticket { TicketId = 1 };
        _mockRepo.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(ticket);

        await _service.DeleteTicketAsync(1);

        _mockRepo.Verify(r => r.DeleteTicketAsync(ticket), Times.Once);
    }
}