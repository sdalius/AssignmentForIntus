using AutoMapper;
using Microsoft.AspNetCore.Components;
using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.FilterModels;
using TestForDotNet.Services.Interface;
using TestForDotNet.ViewModel;
using UzduotysNet.Repository.Enums;

namespace TestForDotNet.Components.Pages.Orders
{
    public partial class OrderEdit : ComponentBase
    {
        [Parameter]
        public string OrderId { get; set; }

        [Inject]
        private IOrdersService _ordersService { get; set; }

        [Inject]
        private IWindowService _windowService { get; set; }

        [Inject]
        private ISubElementService _subElementService { get; set; }
        [Inject]
        private IMapper _mapper { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private OrderViewModel _orderViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ReadOrders();
            await base.OnInitializedAsync();
        }

        public async Task ReadOrders()
        {
            var orderFilter = new OrderFilterModel
            {
                Id = ParseInteger()
            };
            var orderDTO = await _ordersService.ReadSingleOrderAsync(orderFilter, true, true);
            _orderViewModel = _mapper.Map<OrderViewModel>(orderDTO);
        }

        public int? ParseInteger()
        {
            if (!int.TryParse(OrderId, out int OrderIdInt))
                return null;

            if (OrderIdInt <= 0) return null;

            return OrderIdInt;
        }

        public async Task AddWindow()
        {
            if (_orderViewModel == null)
                return;

            var newWindowModel = new WindowViewModel
            {
                OrderId = _orderViewModel.OrderId,
                Order = _mapper.Map<OrderDTO>(_orderViewModel),
                Name = string.Empty,
                SubElements = []
            };

            newWindowModel.MarkAsNew();

            _orderViewModel.Windows = _orderViewModel.Windows ?? new List<WindowViewModel>();
            _orderViewModel.Windows.Add(newWindowModel);

            await Task.CompletedTask;
        }

        public async Task AddSubElement(WindowViewModel windowViewModel)
        {
            if (_orderViewModel == null || windowViewModel == null)
                return;

            var newSubElement = new SubElementDTO
            {
                SubElementType = SubElementTypeEnum.Window,
                WindowId = windowViewModel.WindowId,
                Window = _mapper.Map<WindowDTO>(windowViewModel)
            };

            newSubElement.MarkAsNew();

            if (windowViewModel.SubElements == null)
                windowViewModel.SubElements = [newSubElement];
            else
                windowViewModel.SubElements.Add(newSubElement);

            windowViewModel.TotalSubElements++;

            _orderViewModel.IsChanged = true;

            await Task.CompletedTask;
        }

        public void OnOrderStateChanged(string orderState)
        {
            if (_orderViewModel == null)
                return;

            _orderViewModel.State = orderState;

            if (!_orderViewModel.IsNew)
            {
                _orderViewModel.IsChanged = true;
                StateHasChanged();
            }
        }

        public void OnOrderNameChanged(string orderName)
        {
            if (_orderViewModel == null)
                return;

            _orderViewModel.OrderName = orderName;

            if (_orderViewModel.IsNew)
                return;
            _orderViewModel.IsChanged = true;
            StateHasChanged();
        }

        public void OnWindowQuantityChanged(int quantity, WindowViewModel windowViewModel)
        {
            if (windowViewModel == null)
                return;

            windowViewModel.QuantityOfWindows = quantity;
            windowViewModel.SetChangedStatus();

            StateHasChanged();
        }

        public void OnSubElementTypeChanged(SubElementTypeEnum e, SubElementDTO subElementDTO, WindowViewModel windowViewModel)
        {
            if (subElementDTO == null)
                return;

            subElementDTO.SubElementType = e;
            subElementDTO.SetChangedStatus();
            windowViewModel.SetChangedStatus();

            StateHasChanged();
        }

        public void OnSubElementWidthChanged(double width, SubElementDTO subElementDTO, WindowViewModel windowViewModel)
        {
            if (subElementDTO == null)
                return;

            subElementDTO.Width = width;
            subElementDTO.SetChangedStatus();

            windowViewModel.SetChangedStatus();

            StateHasChanged();
        }

        public void OnSubElementHeigthChanged(double height, SubElementDTO subElementDTO, WindowViewModel windowViewModel)
        {
            if (subElementDTO == null)
                return;

            subElementDTO.Height = height;
            subElementDTO.SetChangedStatus();

            windowViewModel.SetChangedStatus();
            StateHasChanged();
        }

        public void SetOrderChangedStatus()
        {
            if (_orderViewModel == null)
                return;

            _orderViewModel.IsChanged = true;
            StateHasChanged();
        }

        public void DeleteSubElement(WindowViewModel windowViewModel, SubElementDTO subElementDTO)
        {
            if (subElementDTO == null || windowViewModel == null || windowViewModel.SubElements == null)
                return;

            if (subElementDTO.IsNew)
            {
                windowViewModel.SubElements.Remove(subElementDTO);
            }
            else
            {
                subElementDTO.MarkAsDeleted();
            }

            windowViewModel.SetChangedStatus();
            StateHasChanged();
        }

        public void DeleteWindow(WindowViewModel windowViewModel)
        {
            if (windowViewModel == null || _orderViewModel == null || _orderViewModel.Windows == null)
                return;

            if (windowViewModel.IsNew)
                _orderViewModel.Windows.Remove(windowViewModel);
            else
                windowViewModel.MarkAsDeleted();

            _orderViewModel.IsChanged = true;
            StateHasChanged();
        }

        public async Task OnSave()
        {
            if (_orderViewModel == null)
                return;

            var orderDTO = _mapper.Map<OrderDTO>(_orderViewModel);
            var ordersToUpdate = new List<OrderDTO> { orderDTO };

            if (_orderViewModel.IsNew)
                await _ordersService.CreateOrdersAsync(ordersToUpdate);
            if (_orderViewModel.IsDeleted)
                await _ordersService.DeleteOrdersAsync(ordersToUpdate);
            if (_orderViewModel.IsChanged && !_orderViewModel.IsDeleted && !_orderViewModel.IsNew)
                await _ordersService.UpdateOrdersAsync(ordersToUpdate);

            await HandleWindowAndSubElements();

            await ReadOrders();

            NavigationManager.NavigateTo("/orders");
            StateHasChanged();
        }
        public async Task HandleWindowAndSubElements()
        {
            if (_orderViewModel.Windows == null || _orderViewModel.Windows.Count <= 0)
                return;

            var newWindows = _orderViewModel.Windows.Where(x => x.IsNew).ToList();
            var updateWindows = _orderViewModel.Windows.Where(x => x.IsChanged).ToList();
            var deleteWindows = _orderViewModel.Windows.Where(x => x.IsDeleted).ToList();
            
            if (newWindows.Count != 0)
            {
                foreach (var window in newWindows)
                {
                    var savedWindow = await _windowService.CreateAsync(new List<WindowDTO> { _mapper.Map<WindowDTO>(window)});
                    if (window.SubElements == null || window.SubElements?.Count <= 0)
                        continue;

                    await UpdateSubElements(savedWindow.FirstOrDefault(), window?.SubElements ?? []);
                }
            }

            if (updateWindows.Count != 0)
            {
                foreach (var window in updateWindows)
                {
                    await _windowService.UpdateAsync([_mapper.Map<WindowDTO>(window)]);

                    if (window == null || window.SubElements == null || window.SubElements.Count <= 0)
                        return;

                    await UpdateSubElements(null, window.SubElements);
                }
            }

            if (deleteWindows.Count != 0)
            {
                await _windowService.DeleteAsync(_mapper.Map<List<WindowDTO>>(deleteWindows));
            }
        }

        public async Task UpdateSubElements(WindowDTO? windowDTO, List<SubElementDTO> subElements)
        {
            var newSubElements = subElements.Where(x => x.IsNew).ToList();
            var updateSubElements = subElements.Where(x => x.IsChanged).ToList();
            var deleteSubElements = subElements.Where(x => x.IsDeleted).ToList();

            if (newSubElements.Any(x => x.WindowId <= 0))
            {
                if (windowDTO != null)
                {
                    foreach (var subElement in newSubElements)
                        subElement.WindowId = windowDTO.WindowId;
                }
            }

            if (newSubElements.Count != 0)
                await _subElementService.CreateAsync(newSubElements);

            if (updateSubElements.Count != 0)
                await _subElementService.UpdateAsync(updateSubElements);

            if (deleteSubElements.Count != 0)
                await _subElementService.DeleteAsync(deleteSubElements);

        }
    }
}
