using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Dto;
using VirtualWardrobe_Colors.Interfaces;
using VirtualWardrobe_Colors.Model;
using VirtualWardrobe_Colors.Service;

namespace VirtualWardrobe_Colors.Tests.ServiceTests
{
    public class ColorsServiceTests
    {
        private readonly Mock<IColorsRepository> _mockColorsRepository;

        // MemberData methods
        private static List<Color> GetRawColors()
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

        public static IEnumerable<object[]> GetNonExistingColorsTestMemberData()
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

        public static IEnumerable<object[]> GetAllColorsTestMemberData()
        {
            var existing = GetExistingColorsTestMemberData();
            var nonExisting = GetNonExistingColorsTestMemberData();
            var all = existing.Concat(nonExisting);
            foreach (var item in all)
            {
                yield return item;
            }
        }

        // Constructor
        public ColorsServiceTests()
        {
            _mockColorsRepository = new Mock<IColorsRepository>();

            _mockColorsRepository.Setup(repo => repo.GetAllColors()).Returns(GetRawColors());

            _mockColorsRepository.Setup(repo => repo.GetColorById(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    var color = GetRawColors().FirstOrDefault(c => c.Id == id);
                    if (color == null)
                    {
                        throw new KeyNotFoundException($"Color with id {id} not found.");
                    }
                    return color;
                });

            _mockColorsRepository.Setup(repo => repo.Exists(It.IsAny<Guid>()))
                .Returns((Guid id) => GetRawColors().Any(c => c.Id == id));

            _mockColorsRepository.Setup(repo => repo.Exists(It.IsAny<string>()))
                .Returns((string name) => GetRawColors().Any(c => c.Name == name));

            _mockColorsRepository.Setup(repo => repo.Exists(It.IsAny<Color>()))
                .Returns((Color color) => GetRawColors().Any(c => c.Id == color.Id || c.Name == color.Name));

            _mockColorsRepository.Setup(repo => repo.AddColor(It.IsAny<Color>())).Callback((Color color) =>
                {
                    if (GetRawColors().Any(c => c.Id == color.Id) || GetRawColors().Any(c => c.Name == color.Name))
                    {
                        throw new InvalidOperationException($"Color {color} already exists.");
                    }
                });

            _mockColorsRepository.Setup(repo => repo.UpdateColor(It.IsAny<Color>())).Callback((Color color) =>
                {
                    if (!GetRawColors().Any(c => c.Id == color.Id))
                    {
                        throw new KeyNotFoundException($"Color with id {color.Id} not found.");
                    }
                    if(GetRawColors().Any(c => c.Name == color.Name))
                    {
                        throw new InvalidOperationException($"Color with name {color.Name} already exists.");
                    }
                });

            _mockColorsRepository.Setup(repo => repo.DeleteColor(It.IsAny<Guid>())).Callback((Guid id) =>
                {
                    if (!GetRawColors().Any(c => c.Id == id))
                    {
                        throw new KeyNotFoundException($"Color with id {id} not found.");
                    }
                });
        }

        // Tests
        [Fact]
        public void ColorsService_GetColors_ReturnsAllColors()
        {
            var service = new ColorsService(_mockColorsRepository.Object);

            var result = service.GetColors();

            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsService_GetColorById_ReturnsColor(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);

            var result = service.GetColor(color.Id);

            Assert.NotNull(result);
            Assert.Equal(color.Id, result.Id);
            Assert.Equal(color.Name, result.Name);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingColorsTestMemberData))]
        public void ColorsService_GetColorById_ReturnsNull(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);

            var result = service.GetColor(color.Id);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingColorsTestMemberData))]
        public void ColorsService_CreateColor_ReturnsCreatedColor(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);
            var createColorDto = new CreateColorDto(color.Name);

            var result = service.CreateColor(createColorDto);

            Assert.NotNull(result);
            Assert.Equal(createColorDto.Name, result.Name);
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsService_CreateColor_ReturnsNull(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);
            var createColorDto = new CreateColorDto(color.Name);

            var result = service.CreateColor(createColorDto);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsService_UpdateColor_ReturnsUpdatedColor(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);
            var updateColorDto = new UpdateColorDto(color.Name + " Updated");

            var result = service.UpdateColor(color.Id, updateColorDto);

            Assert.NotNull(result);
            Assert.Equal(color.Id, result.Id);
            Assert.Equal(updateColorDto.Name, result.Name);
        }

        [Theory]
        [MemberData(nameof(GetAllColorsTestMemberData))]
        public void ColorsService_UpdateColor_ReturnsNull(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);
            var updateColorDto = new UpdateColorDto(color.Name);

            var result = service.UpdateColor(color.Id, updateColorDto);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetExistingColorsTestMemberData))]
        public void ColorsService_DeleteColor_ReturnsTrue(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);

            var result = service.DeleteColor(color.Id);

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(GetNonExistingColorsTestMemberData))]
        public void ColorsService_DeleteColor_ReturnsFalse(Color color)
        {
            var service = new ColorsService(_mockColorsRepository.Object);

            var result = service.DeleteColor(color.Id);

            Assert.False(result);
        }
    }
}
