using Microsoft.EntityFrameworkCore.Storage;

namespace AccountingTM.Views.Shared.Components.Input
{
    public class InputViewModel
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public string AspFor { get; set; }
        public string Placeholder { get; set; }
    }

}
