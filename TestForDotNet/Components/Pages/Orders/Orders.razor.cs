using AutoMapper;
using Microsoft.AspNetCore.Components;
using TestForDotNet.Core.DTOs;
using TestForDotNet.Services.Interface;
using TestForDotNet.ViewModel;

namespace TestForDotNet.Components.Pages.Orders
{
    public partial class Orders : ComponentBase
    {
        [Inject]
        private IOrdersService _ordersService { get; set; }
        [Inject]
        private IMapper _mapper { get; set; }
        public List<OrderViewModel> OrderList { get; set; }
        protected override async Task OnInitializedAsync()
        {
            OrderList = new List<OrderViewModel>();
            await ReadOrders();
            await base.OnInitializedAsync();
        }

        public async Task ReadOrders()
        {
            var OrderListDTO = await _ordersService.ReadOrdersAsync(prefetchsubElements:true, prefetchWindows:true);
            _mapper.Map(OrderListDTO, OrderList);
        }

        public async Task ChangeOrderExpanded(OrderViewModel orderViewModel)
        {
            orderViewModel.isExpanded = !orderViewModel.isExpanded;
            await Task.CompletedTask;
        }

        public void ChangeWindowExpanded(WindowViewModel windowViewModel)
        {
            windowViewModel.isExpanded = !windowViewModel.isExpanded;
        }

        public string SwitchButtonColorWindow(WindowViewModel windowViewModel)
        {
            if (windowViewModel == null)
                return "btn btn-success btn-sm";

            return windowViewModel.isExpanded ? "btn btn-danger btn-sm" : "btn btn-success btn-sm";
        }
        public string SwitchButtonOrderWindow(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                return "btn btn-success btn-sm";

            return orderViewModel.isExpanded ? "btn btn-danger btn-sm" : "btn btn-success btn-sm";
        }

        public async Task DeleteOrder(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                return;

            orderViewModel.IsDeleted = true;

            if (orderViewModel.IsNew)
                OrderList.Remove(orderViewModel);
            else
                await _ordersService.DeleteOrdersAsync(new List<OrderDTO> { _mapper.Map<OrderDTO>(orderViewModel) });

            await OnInitializedAsync();
         }

        public async Task CreateOrder()
        {
            var newOrder = new OrderViewModel
            {
                OrderName = string.Empty,
                State = string.Empty,
                Windows = [],
                IsNew = true
            };

            await _ordersService.CreateOrdersAsync([_mapper.Map<OrderDTO>(newOrder)]);

            await OnInitializedAsync();
        }
    }
}
