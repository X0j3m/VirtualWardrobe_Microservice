using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualWardrobe_ClothesTypes.Data;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Tests.DataTests
{
    public class ClothesTypeRepositoryTests
    {
        private readonly Mock<ClothesTypeDbContext> _mockContext;
        private readonly IQueryable<ClothesType> _clothesTypesSet;

        // MemberData methods
        public static List<ClothesType> GetRawClothesTypes()
        {
            var layers = ClothesLayerRepositoryTests.GetRawClothesLayers();

            return new List<ClothesType>
            {
                new ClothesType { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Type1", Layer = layers[(layers.Count-1)%1] },
                new ClothesType { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Type2", Layer = layers[(layers.Count-1)%2] },
                new ClothesType { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Type3", Layer = layers[(layers.Count-1)%3] },
                new ClothesType { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Type4", Layer = layers[(layers.Count-1)%4] }
            };
        }

        public static IEnumerable<object[]> GetExistingTypesTestMemberData()
        {
            var types = GetRawClothesTypes();
            foreach (var type in types)
            {
                yield return new object[] { type };
            }
        }

        public static IEnumerable<object[]> GetNonExistingTypesTestMemberData()
        {
            var nonExistingTypes = new List<ClothesType>
            {
                new ClothesType
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Name = "NonExistingType1",
                    Layer = ClothesLayerRepositoryTests.GetRawClothesLayers()[(ClothesLayerRepositoryTests.GetRawClothesLayers().Count-1)%1]
                },
                new ClothesType
                {
                    Id = Guid.Parse("A0000000-0000-0000-0000-000000000000"),
                    Name = "NonExistingType2",
                    Layer = ClothesLayerRepositoryTests.GetRawClothesLayers()[(ClothesLayerRepositoryTests.GetRawClothesLayers().Count-1)%2]
                },
                new ClothesType
                {
                    Id = Guid.Parse("A0000000-0000-0000-0000-000000000001"),
                    Name = "NonExistingType3",
                    Layer = ClothesLayerRepositoryTests.GetRawClothesLayers()[(ClothesLayerRepositoryTests.GetRawClothesLayers().Count-1)%3]
                }
            };
            foreach (var type in nonExistingTypes)
            {
                yield return new object[] { type };
            }
        }

        // Constructor
        public ClothesTypeRepositoryTests()
        {
            _clothesTypesSet = GetRawClothesTypes().AsQueryable();
            var mockClothesTypesDbSet = new Mock<DbSet<ClothesType>>();

            mockClothesTypesDbSet.As<IQueryable<ClothesType>>().Setup(m => m.Provider).Returns(_clothesTypesSet.Provider);
            mockClothesTypesDbSet.As<IQueryable<ClothesType>>().Setup(m => m.Expression).Returns(_clothesTypesSet.Expression);
            mockClothesTypesDbSet.As<IQueryable<ClothesType>>().Setup(m => m.ElementType).Returns(_clothesTypesSet.ElementType);
            mockClothesTypesDbSet.As<IQueryable<ClothesType>>().Setup(m => m.GetEnumerator()).Returns(_clothesTypesSet.GetEnumerator());

            var options = new DbContextOptionsBuilder<ClothesTypeDbContext>().Options;
            _mockContext = new Mock<ClothesTypeDbContext>(options);
            _mockContext.Setup(c => c.ClothesTypes).Returns(mockClothesTypesDbSet.Object);
        }

        // Tests
        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void Exists_ExistingType_ReturnsTrue(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            var resultById = repository.Exists(type.Id);
            var resultByName = repository.Exists(type.Name);
            var resultByObject = repository.Exists(type);

            Assert.True(resultById);
            Assert.True(resultByName);
            Assert.True(resultByObject);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingTypesTestMemberData))]
        public void Exists_NonExistingType_ReturnsFalse(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            var resultById = repository.Exists(type.Id);
            var resultByName = repository.Exists(type.Name);
            var resultByObject = repository.Exists(type);

            Assert.False(resultById);
            Assert.False(resultByName);
            Assert.False(resultByObject);
        }

        [Fact]
        public void GetAllClothesTypes_ReturnsAllLayers()
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            var result = repository.GetAllClothesTypes();

            foreach (var layer in _clothesTypesSet)
            {
                Assert.Contains(layer, result);
            }
        }

        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void GetClothesTypeById_ExistingType_ReturnsType(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            var result = repository.GetClothesTypeById(type.Id);

            Assert.Equal(type, result);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingTypesTestMemberData))]
        public void GetClothesTypeById_NonExistingType_ThrowsKeyNotFoundException(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            Assert.Throws<KeyNotFoundException>(() => repository.GetClothesTypeById(type.Id));
        }

        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void CreateClothesType_ExistingTypeId_ThrowsInvalidOperationException(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.CreateClothesType(type));
        }

        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void CreateClothesType_ExistingTypeName_ThrowsInvalidOperationException(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);
            var updateType = new ClothesType
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Name = type.Name,
                Layer = type.Layer
            };

            Assert.Throws<InvalidOperationException>(() => repository.CreateClothesType(updateType));
        }

        [Theory]
        [MemberData(nameof(GetNonExistingTypesTestMemberData))]
        public void CreateClothesType_ValidType_CreatesType(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            repository.CreateClothesType(type);

            _mockContext.Verify(c => c.ClothesTypes.Add(type), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingTypesTestMemberData))]
        public void UpdateClothesType_NonExistingType_ThrowsKeyNotFoundException(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            Assert.Throws<KeyNotFoundException>(() => repository.UpdateClothesType(type));
        }

        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void UpdateClothesType_ExistingTypeName_UpdatesType(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.UpdateClothesType(type));
        }

        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void UpdateClothesType_ValidType_UpdatesType(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);
            var updateType = new ClothesType
            {
                Id = type.Id,
                Name = "UpdatedName",
                Layer = type.Layer
            };

            repository.UpdateClothesType(updateType);

            _mockContext.Verify(c => c.Update(updateType), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetExistingTypesTestMemberData))]
        public void DeleteClothesType_ExistingType_DeletesType(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);

            repository.DeleteClothesType(type.Id);

            _mockContext.Verify(c => c.ClothesTypes.Remove(type), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingTypesTestMemberData))]
        public void DeleteClothesType_NonExistingType_ThrowsKeyNotFoundException(ClothesType type)
        {
            var repository = new ClothesTypeRepository(_mockContext.Object);
 
            Assert.Throws<KeyNotFoundException>(() => repository.DeleteClothesType(type.Id));
        }
    }
}