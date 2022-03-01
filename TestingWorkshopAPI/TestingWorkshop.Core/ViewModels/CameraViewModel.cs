using System;
using TestingWorkshop.Core.Contracts;

namespace TestingWorkshop.Core.ViewModels
{
    public class CameraViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public bool IsAPSCSensor { get; set; }
    }
}
