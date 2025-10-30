using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public  class OrderService(ICrudRepository<Order> _orderRepository, IMapper _mapper) : ICrudService<Order, Order>
{
    public async Task<ServiceResponse<bool>> Create(Order input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true, "Order successfully created", true);
    }

    public async Task<ServiceResponse<bool>> Edit(Guid id, Order input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true, "Order successfully updated", true);
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true, "Order successfully deleted", true);
    }

    public async Task<ServiceResponse<Order>> GetById(Guid id)
    {
        var order = await _dbContext.FindAsync<Order>(id);
        return new ServiceResponse<Order>(order!, "Order retrieved successfully", true);
    }

    public async Task<ServiceResponse<List<Order>>> GetAll()
    {
        var orders = await _dbContext.Orders.ToListAsync();
        return new ServiceResponse<List<Order>>(orders, "Orders retrieved successfully", true);
    }
}