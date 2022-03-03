using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestingWorkshop.Core.Contracts;
using TestingWorkshop.Core.Entities;
using TestingWorkshop.Core.ViewModels;
using TestingWorkshopAPI.DataAccess;
using TestingWorkshopAPI.DataAccess.Handlers;
using TestingWorkshopAPI.DataAccess.Mappers;
using TestingWorskshopTests.Setup;
using Xunit;
using Xunit.Priority;

namespace TestingWorskshopTests.Fixtures
{
    public class CameraFixture
    {
        public TestingWorkshopContext DbContext { get; private set; }
        public IMapper<Camera, CameraViewModel> Mapper { get; set; }
        public Guid CameraId { get; set; }
        public CameraViewModel Camera { get; set; }

        public CameraFixture()
        {
            DbContext = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            Mapper = new CameraMapper();
        }
    }

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class CameraTests : IClassFixture<CameraFixture>
    {
        CameraFixture _fixture;

        public CameraTests(CameraFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact, Priority(1)]
        public async Task WillAddCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            var camera = new FakeCamera().Create();

            //act
            var result = await handler.Add(camera);

            _fixture.CameraId = result.Value;
            _fixture.Camera = camera;
            _fixture.Camera.Id = _fixture.CameraId;

            //assert
            Assert.NotNull(result);
        }

        [Fact, Priority(2)]
        public async Task WillGetCameraById()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            //act
            var selectedCamera = await handler.GetById(_fixture.CameraId);

            //assert
            Assert.NotNull(selectedCamera);
            Assert.NotNull(selectedCamera.Manufacturer);
            Assert.NotNull(selectedCamera.Model);
        }

        [Fact, Priority(3)]
        public async Task WillUpdateCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);
            var originalManufacturer = _fixture.Camera.Manufacturer;

            //act
            var updatedCameraManufacturer = "Canon";

            _fixture.Camera.Manufacturer = updatedCameraManufacturer;

            var updateResult = await handler.Update(_fixture.CameraId, _fixture.Camera);

            //assert
            Assert.NotNull(updateResult);
            Assert.NotEqual(originalManufacturer, updatedCameraManufacturer);
        }

        [Fact, Priority(4)]
        public async Task WillDeleteCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            //act
            var deleteResult = await handler.Delete(_fixture.Camera.Id);

            var selectedResult = await handler.GetById(_fixture.Camera.Id);

            //assert
            Assert.Null(selectedResult);
        }
    }
}
