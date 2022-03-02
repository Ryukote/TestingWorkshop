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
            //SQLiteInMemoryContext().Create(Guid.NewGuid().ToString())
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
        public async Task WillNotGetCamera()
        {
            //arrange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            //act
            var result = await handler.GetById(Guid.NewGuid());

            //assert
            Assert.Null(result);
        }

        [Fact, Priority(2)]
        public async Task WillNotAddCamera()
        {
            //arrange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            var camera = new FakeCamera().Create();
            camera.Manufacturer = null;

            //act and assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await handler.Add(camera));
        }

        [Fact, Priority(3)]
        public async Task WillNotDeleteCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            //act
            var result = await handler.Delete(Guid.NewGuid());

            //assert
            Assert.Null(result);
        }

        [Fact, Priority(4)]
        public async Task WillAddCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            var camera = new FakeCamera().Create();

            //act
            var result = await handler.Add(camera);

            _fixture.CameraId = result.Value;
            _fixture.Camera = camera;

            //assert
            Assert.NotNull(result);
        }

        [Fact, Priority(5)]
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

        [Fact, Priority(6)]
        public async Task WillNotUpdateCamera()
        {
            //arrange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            var camera = new FakeCamera().Create();

            //act and assert
            camera.Id = _fixture.CameraId;

            camera.Manufacturer = null;

            await Assert.ThrowsAsync<DbUpdateException>(async () => await handler.Update(camera.Id, camera));
        }

        [Fact, Priority(7)]
        public async Task WillUpdateCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);
            var originalManufacturer = _fixture.Camera.Manufacturer;

            //act
            var updatedCameraManufacturer = "Canon";

            _fixture.Camera.Manufacturer = updatedCameraManufacturer;

            var updateResult = await handler.Update(_fixture.Camera.Id, _fixture.Camera);

            //assert
            Assert.NotNull(updateResult);
            Assert.NotEqual(originalManufacturer, updatedCameraManufacturer);
        }

        [Fact, Priority(8)]
        public async Task WillDeleteCamera()
        {
            //arange
            var handler = new CameraHandler(_fixture.DbContext, _fixture.Mapper);

            //act
            var deleteResult = await handler.Delete(_fixture.Camera.Id);

            var selectedResult = await handler.GetById(_fixture.Camera.Id);

            //assert
            Assert.NotNull(deleteResult.Value);
            Assert.Null(selectedResult);
        }
    }
}
