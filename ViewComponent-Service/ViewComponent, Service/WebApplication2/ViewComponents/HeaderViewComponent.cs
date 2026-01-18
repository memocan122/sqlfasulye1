using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly LayoutService _layoutService;
        public HeaderViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> setting = await _layoutService.GetAllSetting();
            return await Task.FromResult(View(setting));
        }
    }
}
