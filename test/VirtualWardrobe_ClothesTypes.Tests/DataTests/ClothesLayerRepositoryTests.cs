using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualWardrobe_ClothesTypes.Data;
using VirtualWardrobe_ClothesTypes.Model;

namespace VirtualWardrobe_ClothesTypes.Tests.DataTests
{
    public class ClothesLayerRepositoryTests
    {
        private readonly Mock<ClothesTypeDbContext> _mockContext;
        private readonly IQueryable<ClothesLayer> _clothesLayersSet;

        // MemberData methods
        public static List<ClothesLayer> GetRawClothesLayers()
        {
            return new List<ClothesLayer>
            {
                new ClothesLayer { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Layer1" },
                new ClothesLayer { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Layer2" },
                new ClothesLayer { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Layer3" },
                new ClothesLayer { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Layer4" }
            };
        }

        public static IEnumerable<object[]> GetExistingLayersTestMemberData()
        {
            var layers = GetRawClothesLayers();
            foreach (var layer in layers)
            {
                yield return new object[] { layer };
            }
        }

        public static IEnumerable<object[]> GetNonExistingLayersTestMemberData()
        {
            var nonExistingLayers = new List<ClothesLayer>
            {
                new ClothesLayer
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Name = "NonExistingLayer1"
                },
                new ClothesLayer
                {
                    Id = Guid.Parse("A0000000-0000-0000-0000-000000000000"),
                    Name = "NonExistingLayer2"
                },
                new ClothesLayer
                {
                    Id = Guid.Parse("A0000000-0000-0000-0000-000000000001"),
                    Name = "NonExistingLayer3"
                }
            };
            foreach (var layer in nonExistingLayers)
            {
                yield return new object[] { layer };
            }
        }

        // Constructor
        public ClothesLayerRepositoryTests()
        {
            _clothesLayersSet = GetRawClothesLayers().AsQueryable();
            var mockClothesLayersDbSet = new Mock<DbSet<ClothesLayer>>();

            mockClothesLayersDbSet.As<IQueryable<ClothesLayer>>().Setup(m => m.Provider).Returns(_clothesLayersSet.Provider);
            mockClothesLayersDbSet.As<IQueryable<ClothesLayer>>().Setup(m => m.Expression).Returns(_clothesLayersSet.Expression);
            mockClothesLayersDbSet.As<IQueryable<ClothesLayer>>().Setup(m => m.ElementType).Returns(_clothesLayersSet.ElementType);
            mockClothesLayersDbSet.As<IQueryable<ClothesLayer>>().Setup(m => m.GetEnumerator()).Returns(_clothesLayersSet.GetEnumerator());

            var options = new DbContextOptionsBuilder<ClothesTypeDbContext>().Options;
            _mockContext = new Mock<ClothesTypeDbContext>(options);
            _mockContext.Setup(c => c.ClothesLayers).Returns(mockClothesLayersDbSet.Object);
        }

        // Tests

        [Theory]
        [MemberData(nameof(GetExistingLayersTestMemberData))]
        public void Exists_ExistingLayer_ReturnsTrue(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            var resultById = repository.Exists(layer.Id);
            var resultByName = repository.Exists(layer.Name);
            var resultByObject = repository.Exists(layer);

            Assert.True(resultById);
            Assert.True(resultByName);
            Assert.True(resultByObject);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingLayersTestMemberData))]
        public void Exists_NonExistingLayer_ReturnsFalse(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            var resultById = repository.Exists(layer.Id);
            var resultByName = repository.Exists(layer.Name);
            var resultByObject = repository.Exists(layer);

            Assert.False(resultById);
            Assert.False(resultByName);
            Assert.False(resultByObject);
        }

        [Fact]
        public void GetAllClothesLayers_ReturnsAllLayers()
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            var result = repository.GetAllClothesLayers();

            foreach (var layer in _clothesLayersSet)
            {
                Assert.Contains(layer, result);
            }
        }

        [Theory]
        [MemberData(nameof(GetExistingLayersTestMemberData))]
        public void GetClothesLayerById_ExistingLayer_ReturnsLayer(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            var result = repository.GetClothesLayerById(layer.Id);

            Assert.Equal(layer, result);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingLayersTestMemberData))]
        public void GetClothesLayerById_NonExistingLayer_ThrowsException(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.GetClothesLayerById(layer.Id));
        }

        [Theory]
        [MemberData(nameof(GetNonExistingLayersTestMemberData))]
        public void CreateClothesLayer_NonExistingLayer_CreatesLayer(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            var ex = Record.Exception(() => repository.CreateClothesLayer(layer));

            _mockContext.Verify(c => c.ClothesLayers.Add(layer), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetExistingLayersTestMemberData))]
        public void CreateClothesLayer_ExistingLayer_ThrowsException(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.CreateClothesLayer(layer));
        }

        [Theory]
        [MemberData(nameof(GetNonExistingLayersTestMemberData))]
        public void UpdateClothesLayer_NonExistingLayerId_ThrowsKeyNotFoundException(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            Assert.Throws<KeyNotFoundException>(() => repository.UpdateClothesLayer(layer));
        }

        [Theory]
        [MemberData(nameof(GetExistingLayersTestMemberData))]
        public void UpdateClothesLayer_NonExistingLayerName_ThrowsInvalidOperationException(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.UpdateClothesLayer(layer));
        }

        [Theory]
        [MemberData(nameof(GetExistingLayersTestMemberData))]
        public void UpdateClothesLayer_ExistingLayer_UpdatesLayer(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);
            var updatedLayer = new ClothesLayer
            {
                Id = layer.Id,
                Name = layer.Name + "Updated"
            };

            var ex = Record.Exception(() => repository.UpdateClothesLayer(updatedLayer));

            _mockContext.Verify(c => c.Update(updatedLayer), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetExistingLayersTestMemberData))]
        public void DeleteClothesLayer_ExistingLayer_DeletesLayer(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            var ex = Record.Exception(() => repository.DeleteClothesLayer(layer.Id));

            _mockContext.Verify(c => c.ClothesLayers.Remove(layer), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingLayersTestMemberData))]
        public void DeleteClothesLayer_NonExistingLayer_ThrowsException(ClothesLayer layer)
        {
            var repository = new ClothesLayerRepository(_mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.DeleteClothesLayer(layer.Id));
        }
    }
}
