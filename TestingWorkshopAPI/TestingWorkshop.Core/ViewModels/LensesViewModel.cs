using System;
using TestingWorkshop.Core.Contracts;

namespace TestingWorkshop.Core.ViewModels
{
    public class LensesViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string CompatibleCameraName { get; set; }
        public bool IsAPSCSensor { get; set; }
        public int FocalLength { get; set; }
        public int Diameter { get; set; }
    }
}
