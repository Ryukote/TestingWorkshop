using System;

namespace TestingWorkshop.Core.Entities
{
    public class Camera
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public bool IsAPSCSensor { get; set; }
    }
}
