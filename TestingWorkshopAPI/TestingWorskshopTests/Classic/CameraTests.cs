using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestingWorkshop.Core.Contracts;
using TestingWorkshop.Core.Entities;
using TestingWorkshop.Core.ViewModels;
using TestingWorkshopAPI.DataAccess.Handlers;
using TestingWorkshopAPI.DataAccess.Mappers;
using TestingWorskshopTests.Setup;
using Xunit;

namespace TestingWorskshopTests.Classic
{
    //For each test we will use a new database to have everything clean for each test
    //and to avoid managing finished data after each test finish
    //The other benefit of creating a new database for each test is to ensure that Bogus library (for generating fake data) will not create same records, which will sometimes make test fail...it can happen
    public class CameraTests
    {
        private IMapper<Camera, CameraViewModel> _mapper;

        public CameraTests()
        {
            _mapper = new CameraMapper();
        }

        [Fact]
        public async Task WillNotGetCamera()
        {
            //arrange
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            //act
            var result = await handler.GetById(Guid.NewGuid());

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async Task WillNotAddCamera()
        {
            //arrange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            var camera = new FakeCamera().Create();
            camera.Manufacturer = null;

            //act and assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await handler.Add(camera));
        }

        //Problem #1
        //To make test throw exception, first we need to create a record which we are trying to update
        //With test like this, we are repeating the code and using parts of other tests to make this test work
        //This will also make this test slower
        [Fact]
        public async Task WillNotUpdateCamera()
        {
            //arrange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            var camera = new FakeCamera().Create();

            //act and assert
            var cameraId = await handler.Add(camera);
            camera.Id = cameraId.Value;

            camera.Manufacturer = null;

            await Assert.ThrowsAsync<DbUpdateException>(async () => await handler.Update(camera.Id, camera));
        }

        [Fact]
        public async Task WillNotDeleteCamera()
        {
            //arange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            //act
            var result = await handler.Delete(Guid.NewGuid());

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async Task WillAddCamera()
        {
            //arange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            var camera = new FakeCamera().Create();

            //act
            var result = await handler.Add(camera);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WillGetCameraById()
        {
            //arange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            var camera = new FakeCamera().Create();

            //act
            var result = await handler.Add(camera);

            var selectedCamera = await handler.GetById(result.Value);

            //assert
            Assert.NotNull(selectedCamera);
            Assert.NotNull(selectedCamera.Manufacturer);
            Assert.NotNull(selectedCamera.Model);
        }

        [Fact]
        public async Task WillUpdateCamera()
        {
            //arange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            var camera = new FakeCamera().Create();
            var cameraManufacturer = camera.Manufacturer;

            //act
            var result = await handler.Add(camera);

            camera.Id = result.Value;

            var updatedCameraManufacturer = "Canon";

            camera.Manufacturer = updatedCameraManufacturer;

            var updateResult = await handler.Update(camera.Id, camera);

            //assert
            Assert.NotNull(updateResult);
            Assert.NotEqual(cameraManufacturer, updatedCameraManufacturer);
        }

        //Problem #2
        //Complexity is now getting bigger
        //To be able to tell for sure that we deleted record, we need to
        //1. Add record, so we can actually delete something
        //2. Delete record
        //3. Try to get record that we have deleted to ensure we are not getting that record

        //Does this make any sense?
        //This is code repeat
        //These are not independant tests, since we are using functionalities that are being tested in other tests
        //These makes tests bigger and bigger
        //Tests are getting slower and slower
        //Amount of Assert calls are getting bigger and bigger
        [Fact]
        public async Task WillDeleteCamera()
        {
            //arange
            var context = new SQLiteInMemoryContext().Create(Guid.NewGuid().ToString());
            var handler = new CameraHandler(context, _mapper);

            var camera = new FakeCamera().Create();

            //act
            var addResult = await handler.Add(camera);
            camera.Id = addResult.Value;

            var deleteResult = await handler.Delete(camera.Id);

            var selectedResult = await handler.GetById(camera.Id);

            //assert
            Assert.NotNull(addResult);
            Assert.NotNull(deleteResult);
            Assert.Null(selectedResult);
        }
    }
}
