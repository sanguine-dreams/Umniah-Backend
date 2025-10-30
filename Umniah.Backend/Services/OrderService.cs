using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public  class OrderService(ICrudRepository<Order> _orderRepository, IMapper _mapper) : ICrudService<InputOrder, OutputOrder>
{
    public async Task<ServiceResponse<bool>> Create(Order input)
    {
        await _orderRepository.CreateAsync(input);
        return new ServiceResponse<bool>(true, "Order successfully created", true);
    }

    public async Task<ServiceResponse<bool>> Edit(Guid id, Order input)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
            return new ServiceResponse<bool>(false, "Order not found");

        // Update properties
        existingOrder.CustomerId = input.CustomerId;
        existingOrder.OrderDate = input.OrderDate;
        existingOrder.ProductIds = input.ProductIds;

        try
        {
            await _orderRepository.UpdateAsync(existingOrder);
            return new ServiceResponse<bool>(true, "Order successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating order: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
            return new ServiceResponse<bool>(false, "Order not found");

        try
        {
            await _orderRepository.DeleteAsync(id);
            return new ServiceResponse<bool>(true, "Order successfully deleted", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error deleting order: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<Order>> GetById(Guid id)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
            return new ServiceResponse<Order>(false, "Order not found");
            var output =_mapper.Map<OutputOrder>(existingOrder);
        return new ServiceResponse<Order>(output, "Order retrieved successfully", true);
    }

    public async Task<ServiceResponse<List<Order>>> GetAll()
    {
        var orders = await _dbContext.Orders.ToListAsync();
        return new ServiceResponse<List<Order>>(orders, "Orders retrieved successfully", true);
    }
}