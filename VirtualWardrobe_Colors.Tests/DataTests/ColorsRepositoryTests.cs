using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Model;

namespace VirtualWardrobe_Colors.Tests.DataTests
{
    public class ColorsRepositoryTests
    {
        private readonly Mock<ColorsDbContext> _mockDbContext;
        private readonly IQueryable<Color> _colorsSet;

        // MemberData methods
        public static List<Color> GetRawColors()
        {
            return new List<Color>
            {
                new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Red" },
                new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Green" },
                new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Blue" },
                new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Yellow" }
            };
        }

        public static IEnumerable<object[]> GetExistingColorsTestMemberData()
        {
            var colors = GetRawColors();
            foreach (var color in colors)
            {
                yield return new object[] { color };
            }
        }

        public static IEnumerable<object[]> GetNotExistingColorsTestMemberData()
        {
            var notExistingColors = new List<Color>
            {
                new Color { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "White" },
                new Color { Id = Guid.Parse("A0000000-0000-0000-0000-000000000000"), Name = "Black" },
                new Color { Id = Guid.Parse("A0000000-0000-0000-0000-000000000001"), Name = "Gray" }
            };
            foreach (var color in notExistingColors)
            {
                yield return new object[] { color };
            }
        }

        // Constructor
        public ColorsRepositoryTests()
        {
            _colorsSet = GetRawColors().AsQueryable();
            var mockColorsSet = new Mock<DbSet<Color>>();

            mockColorsSet.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(_colorsSet.Provider);
            mockColorsSet.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(_colorsSet.Expression);
            mockColorsSet.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(_colorsSet.ElementType);
            mockColorsSet.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(_colorsSet.GetEnumerator());

            var options = new DbContextOptionsBuilder<ColorsDbContext>().Options;
            _mockDbContext = new Mock<ColorsDbContext>(options);
            _mockDbContext.Setup(db => db.Colors).Returns(mockColorsSet.Object);
        }

        // Tests
        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsRepository_Exists_ReturnsTrue(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            var resultForId = repository.Exists(color.Id);
            var resultForEntity = repository.Exists(color);

            Assert.True(resultForId);
            Assert.True(resultForEntity);
        }

        [Theory]
        [MemberData(nameof(GetNotExistingColorsTestMemberData))]
        public void ColorsRepository_Exists_ReturnsFalse(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            var resultForId = repository.Exists(color.Id);
            var resultForEntity = repository.Exists(color);

            Assert.False(resultForId);
            Assert.False(resultForEntity);
        }

        [Fact]
        public void ColorsRepository_GetAllColors_ReturnsAllColors()
        {
            var repository = new ColorsRepository(_mockDbContext.Object);
            
            var result = repository.GetAllColors();
            
            Assert.NotNull(result);
            Assert.Equal(_colorsSet.Count(), result.Count);
            foreach (var color in _colorsSet)
            {
                Assert.Contains(color, result);
            }
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsRepository_GetColorById_ReturnsColor(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);
            
            var result = repository.GetColorById(color.Id);
            
            Assert.NotNull(result);
            Assert.Equal(result, color);
        }

        [Theory]
        [MemberData(nameof(GetNotExistingColorsTestMemberData))]
        public void ColorsRepository_GetColorById_ThrowsKeyNotFoundException(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);
            
            Assert.Throws<KeyNotFoundException>(() => repository.GetColorById(color.Id));
        }

        [Theory]
        [MemberData(nameof(GetNotExistingColorsTestMemberData))]
        public void ColorsRepository_AddColor_Passes(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            var ex = Record.Exception(() => repository.AddColor(color));
           
            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsRepository_AddColor_ThrowsInvalidOperationException(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.AddColor(color));
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsRepository_UpdateColor_Passes(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);
            var updateColor = new Color { Id = color.Id, Name = color.Name + "Updated" };

            var ex = Record.Exception(() => repository.UpdateColor(updateColor));

            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetNotExistingColorsTestMemberData))]
        public void ColorsRepository_UpdateColor_ThrowsKeyNotFoundException(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            Assert.Throws<KeyNotFoundException>(() => repository.UpdateColor(color));
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsRepository_UpdateColor_ThrowsInvalidOperationException(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            Assert.Throws<InvalidOperationException>(() => repository.UpdateColor(color));
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsRepository_DeleteColor_Passes(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            var ex = Record.Exception(() => repository.DeleteColor(color.Id));

            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetNotExistingColorsTestMemberData))]
        public void ColorsRepository_DeleteColor_ThrowsKeyNotFoundException(Color color)
        {
            var repository = new ColorsRepository(_mockDbContext.Object);

            Assert.Throws<KeyNotFoundException>(() => repository.DeleteColor(color.Id));
        }
    }
}
