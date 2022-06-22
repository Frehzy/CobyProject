using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;
using HostData.System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Data.Enum;
using System.Text.Json;

namespace HostData.Services;

public class OrderService : IOrderService
{
    protected readonly IDbRepository _dbRepository;
    private readonly IMapper _mapper;
    private readonly IProductItemService _productItemService;
    private readonly IWaiterService _waiterService;
    private readonly ITableService _tableService;
    private readonly IPaymentTypeService _paymentTypeService;
    private readonly IDiscountTypeService _discountTypeService;

    public OrderService(IDbRepository dbRepository, IMapper mapper, IProductItemService productItemService, IWaiterService waiterService, ITableService tableService, IPaymentTypeService paymentTypeService, IDiscountTypeService discountTypeService)
    {
        _dbRepository = dbRepository;
        _mapper = mapper;
        _productItemService = productItemService;
        _waiterService = waiterService;
        _tableService = tableService;
        _paymentTypeService = paymentTypeService;
        _discountTypeService = discountTypeService;
    }

    public async Task<Guid> Create(Guid entityThatChangesId, OrderModel order)
    {
        var orderEntity = new OrderEntity()
        {
            Id = order.Id,
            CreatedTime = order.CreatedTime,
            Number = order.Number,
            CloseTime = order.CloseTime,
            Status = order.Status,
            StartTime = order.StartTime,
            WaiterCreatedId = entityThatChangesId,
            Version = 1,
            IsDeleted = false
        };
        orderEntity = AddOnEntityProperty(order, orderEntity, true);

        var result = await _dbRepository.Add(orderEntity);

        await _dbRepository.SaveChangesAsync();

        Log.Information($"Added. {JsonSerializer.Serialize(order, Options.JsonSerializerOptions)}");
        return result;
    }

    public async Task Delete(Guid id)
    {
        await _dbRepository.Delete<OrderEntity>(id);
        await _dbRepository.SaveChangesAsync();
        Log.Information($"Delete with Id [{id}]");
    }

    public async Task Update(Guid entityThatChangesId, OrderModel order)
    {
        var oldEntity = await _dbRepository.GetById<OrderEntity>(order.Id);

        var newEntity = new OrderEntity()
        {
            WaiterUpdatedId = entityThatChangesId,
            WaiterCreatedId = oldEntity.WaiterCreatedId,
            IsDeleted = order.IsDeleted,
            UpdateTime = DateTime.Now,
            Id = order.Id,
            CloseTime = order.CloseTime,
            CreatedTime = order.CreatedTime,
            Number = order.Number,
            StartTime = order.StartTime,
            Status = order.Status,
            Version = order.Version
        };
        newEntity = AddOnEntityProperty(order, newEntity, false);

        await _dbRepository.Update(newEntity);
        await _dbRepository.SaveChangesAsync();
        Log.Information($"Update. New order: {JsonSerializer.Serialize(order, Options.JsonSerializerOptions)}");
    }

    public async Task<OrderModel> GetById(Guid id)
    {
        var entity = await _dbRepository.Context.Set<OrderEntity>()
                                                .Include(x => x.OrderDiscountEntities)
                                                .Include(x => x.OrderProductEntities)
                                                .Include(x => x.OrderGuestEntities)
                                                .Include(x => x.OrderPaymentEntities)
                                                .Include(x => x.OrderProductEntities)
                                                .Include(x => x.OrderTableEntities)
                                                .Include(x => x.OrderWaiterEntity)
                                                .FirstAsync(x => x.Id.Equals(id));
        return await ConvertToModel(entity);
    }

    public async Task<List<OrderModel>> Get()
    {
        var entities = await _dbRepository.Context.Set<OrderEntity>()
                                                .Include(x => x.OrderDiscountEntities)
                                                .Include(x => x.OrderProductEntities)
                                                .Include(x => x.OrderGuestEntities)
                                                .Include(x => x.OrderPaymentEntities)
                                                .Include(x => x.OrderProductEntities)
                                                .Include(x => x.OrderTableEntities)
                                                .Include(x => x.OrderWaiterEntity)
                                                .Where(x => x.IsDeleted == false).ToListAsync();
        return entities.Select(async x => await ConvertToModel(x)).Select(x => x.Result).ToList();
    }

    public async Task Remove(Guid entityThatChangesId, Guid id)
    {
        var entity = await _dbRepository.Context.Set<OrderEntity>()
                                                .Include(x => x.OrderDiscountEntities)
                                                .Include(x => x.OrderProductEntities)
                                                .Include(x => x.OrderGuestEntities)
                                                .Include(x => x.OrderPaymentEntities)
                                                .Include(x => x.OrderProductEntities)
                                                .Include(x => x.OrderTableEntities)
                                                .Include(x => x.OrderWaiterEntity)
                                                .FirstAsync(x => x.Id.Equals(id));
        entity.WaiterUpdatedId = entityThatChangesId;
        entity.UpdateTime = DateTime.Now;
        entity.IsDeleted = true;

        await _dbRepository.Update(entity);
        await _dbRepository.SaveChangesAsync();
        Log.Information($"Remove with Id [{id}]");
    }

    public async Task<OrderModel?> GetLastOrder()
    {
        var ordersModel = await Get();
        return ordersModel.LastOrDefault();
    }

    public async Task<List<OrderModel>> GetOpenOrders()
    {
        var entities = await _dbRepository.Context.Set<OrderEntity>()
                                                  .Include(x => x.OrderDiscountEntities)
                                                  .Include(x => x.OrderProductEntities)
                                                  .Include(x => x.OrderGuestEntities)
                                                  .Include(x => x.OrderPaymentEntities)
                                                  .Include(x => x.OrderProductEntities)
                                                  .Include(x => x.OrderTableEntities)
                                                  .Include(x => x.OrderWaiterEntity)
                                                  .Where(x => x.IsDeleted == false && x.Status.HasFlag(OrderStatus.Open))
                                                  .ToListAsync();
        return entities.Select(async x => await ConvertToModel(x)).Select(x => x.Result).ToList();
    }

    private OrderEntity AddOnEntityProperty(OrderModel orderModel, OrderEntity orderEntity, bool needGenerateGuid)
    {
        var newOrder = orderEntity;
        newOrder.OrderWaiterEntity = ConvertToWaiterEntity(orderModel.Waiter, needGenerateGuid ? null : orderEntity.OrderWaiterEntity.Id);
        newOrder.OrderDiscountEntities = orderModel.Discounts.Select(x => ConvertToDiscountEntity(x, needGenerateGuid ? null :x.Id)).ToList();
        newOrder.OrderGuestEntities = orderModel.Guests.Select(x => ConvertToGuestEntity(x, needGenerateGuid ? x.Id : null)).ToList();
        newOrder.OrderPaymentEntities = orderModel.Payments.Select(x => ConvertToPaymentEntity(x, needGenerateGuid ? null : x.Id)).ToList();
        newOrder.OrderProductEntities = orderModel.Products.Select(x => ConvertToProductEntity(x, needGenerateGuid ? null : x.Id)).ToList();
        newOrder.OrderTableEntities = orderModel.Tables.Select(x => ConvertToTableEntity(x, needGenerateGuid ? null : x.Id)).ToList();

        OrderWaiterEntity ConvertToWaiterEntity(WaiterModel waiter, Guid? generateGuid) =>
            new()
            {
                Id = generateGuid ?? Guid.NewGuid(),
                OrderEntity = orderEntity,
                OrderEntityId = orderEntity.Id,
                WaiterEntityId = waiter.Id
            };

        OrderDiscountEntity ConvertToDiscountEntity(DiscountModel discount, Guid? generateGuid) =>
            new()
            {
                Id = generateGuid ?? Guid.NewGuid(),
                OrderEntity = orderEntity,
                OrderEntityId = orderEntity.Id,
                DiscountSum = discount.DiscountSum,
                IsActive = discount.IsActive,
                DiscountTypeEntityId = discount.Discount.Id
            };

        OrderGuestEntity ConvertToGuestEntity(GuestModel guest, Guid? generateGuid) =>
            new()
            {
                Id = generateGuid ?? Guid.NewGuid(),
                OrderEntity = orderEntity,
                OrderEntityId = orderEntity.Id,
                Name = guest.Name,
                Rank = guest.Rank
            };

        OrderPaymentEntity ConvertToPaymentEntity(PaymentModel payment, Guid? generateGuid) =>
            new()
            {
                Id = generateGuid ?? Guid.NewGuid(),
                OrderEntity = orderEntity,
                OrderEntityId = orderEntity.Id,
                Status = payment.Status,
                Sum = payment.Sum,
                PaymentTypeEntityId = payment.Type.Id
            };

        OrderProductEntity ConvertToProductEntity(ProductModel product, Guid? generateGuid) =>
            new()
            {
                Id = generateGuid ?? Guid.NewGuid(),
                OrderEntity = orderEntity,
                OrderEntityId = orderEntity.Id,
                Status = product.Status,
                Comment = product.Comment,
                PrintTime = product.PrintTime,
                OrderGuestEntityId = product.GuestId,
                OrderWaiterEntityId = product.WaiterId,
                ProductItemEntityId = product.ProductItem.Id
            };

        OrderTableEntity ConvertToTableEntity(TableModel table, Guid? generateGuid) =>
            new()
            {
                Id = generateGuid ?? Guid.NewGuid(),
                OrderEntity = orderEntity,
                OrderEntityId = orderEntity.Id,
                TableEntityId = table.Id
            };
        return newOrder;
    }

    private async Task<OrderModel> ConvertToModel(OrderEntity orderEntity) =>
        new()
        { 
            Number = orderEntity.Number,
            IsDeleted = orderEntity.IsDeleted,
            Id = orderEntity.Id,
            Version = orderEntity.Version,
            StartTime = orderEntity.StartTime,
            CreatedTime = orderEntity.CreatedTime,
            Status = orderEntity.Status,
            CloseTime = orderEntity.CloseTime,
            Waiter = await _waiterService.GetById(orderEntity.OrderWaiterEntity.WaiterEntityId),
            Discounts = orderEntity.OrderDiscountEntities.Select(async x => new DiscountModel() 
            { 
                Id = x.Id,
                DiscountSum = x.DiscountSum,
                IsActive = x.IsActive,
                Discount = await _discountTypeService.GetById(x.DiscountTypeEntityId)
            }).Select(x => x.Result).ToList(),
            Tables = orderEntity.OrderTableEntities.Select(async x => 
            {
                var table = await _tableService.GetById(x.TableEntityId);
                return new TableModel()
                {
                    Id = table.Id,
                    Name = table.Name,
                    Number = table.Number,
                    CreatedTime = table.CreatedTime,
                    IsDeleted = table.IsDeleted
                };
            }).Select(x => x.Result).ToList(),
            Products = orderEntity.OrderProductEntities.Select(async x => 
            {
                var productItem = await _productItemService.GetById(x.ProductItemEntityId);
                return new ProductModel()
                {
                    Id = x.Id,
                    Status = x.Status,
                    Comment = x.Comment,
                    GuestId = x.OrderGuestEntityId,
                    PrintTime = x.PrintTime,
                    WaiterId = x.OrderWaiterEntityId,
                    ProductItem = productItem
                };
            }).Select(x => x.Result).ToList(),
            Payments = orderEntity.OrderPaymentEntities.Select(async x =>
            {
                var paymentType = await _paymentTypeService.GetById(x.PaymentTypeEntityId);
                return new PaymentModel()
                {
                    Id = x.Id,
                    Status = x.Status,
                    Sum = x.Sum,
                    Type = paymentType
                };
            }).Select(x => x.Result).ToList(),
            Guests = orderEntity.OrderGuestEntities.Select(x => new GuestModel()
            { 
                Id = x.Id,
                Name = x.Name,
                Rank = x.Rank
            }).ToList()
        };
}