using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Views.Shared.Components.Input
{
    public class InputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(InputViewModel model)
        {
            return View(model);
        }
    }
}
