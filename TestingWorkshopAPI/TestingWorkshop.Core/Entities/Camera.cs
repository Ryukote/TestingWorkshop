using System;
using System.ComponentModel.DataAnnotations;
using TestingWorkshop.Core.Contracts;

namespace TestingWorkshop.Core.Entities
{
    public class Camera : IModel
    {
        public Guid Id { get; set; }
        [MinLength(3)]
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public bool IsAPSCSensor { get; set; }
    }
}
