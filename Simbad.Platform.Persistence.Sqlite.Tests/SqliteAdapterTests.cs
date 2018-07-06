using System;
using Simbad.Platform.Core;
using Simbad.Platform.Core.Substance.Registration;
using Simbad.Platform.Core.Tests;
using Simbad.Platform.Persistence.Tests;
using Xunit;

namespace Simbad.Platform.Persistence.Sqlite.Tests
{
    public class SqliteAdapterTests
    {
        private readonly Repository<TestEntity, Guid> _repository;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public SqliteAdapterTests()
        {
            Global.Configure()
                .RegisterIdGenertor<Guid>(() => Guid.NewGuid())
                .UseSqlitePersistence(".\\test.s3db")
                .UseEventDispatcherStub()
                .UseEntityConverterFactory<TestEntityConverterFactory>();

            EntityMapping.Configure().Add<TestEntity, TestDao, Guid>();

            _unitOfWork = new UnitOfWork<Guid>();
            _repository = new Repository<TestEntity, Guid>(_unitOfWork);
        }

        [Fact]
        public void ShouldSave()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            _repository.Save(entity);
            _unitOfWork.Commit();
        }

        [Fact]
        public void ShouldLoad()
        {
            // Arrange
            const string testString = "TEST";
            var entity = new TestEntity
            {
                TestProperty = testString
            };

            _repository.Save(entity);
            _unitOfWork.Commit();

            // Act
            var entityFromStorage = _repository.Get(entity.Id);

            // Assert
            Assert.Equal(entity.Id, entityFromStorage.Id);
            Assert.Equal(entity.TestProperty, testString);
        }
    }
}