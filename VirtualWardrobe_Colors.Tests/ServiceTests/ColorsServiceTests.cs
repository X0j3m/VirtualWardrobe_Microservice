using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Service;

namespace VirtualWardrobe_Colors.Tests.ServiceTests
{
    public class ColorsServiceTests
    {
        private readonly Mock<ColorsRepository> _mockColorsRepository;

        //Constructor
        public ColorsServiceTests()
        {
            _mockColorsRepository = new Mock<ColorsRepository>(new Mock<ColorsDbContext>(new DbContextOptions<ColorsDbContext>()).Object);

            _mockColorsRepository.Setup(repo => repo.GetAllColors()).Returns(new List<Model.Color>
            {
                new Model.Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Red" },
                new Model.Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Green" },
                new Model.Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Blue" },
                new Model.Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Yellow" }
            });

            _mockColorsRepository.Setup(repo => repo.GetColorById(Guid.Parse("00000000-0000-0000-0000-000000000001"))).Returns(new Model.Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Red" });
            _mockColorsRepository.Setup(repo => repo.GetColorById(Guid.Parse("99999999-9999-9999-9999-999999999999"))).Throws(new KeyNotFoundException("Color with id 99999999-9999-9999-9999-999999999999 not found."));
            
            _mockColorsRepository.Setup(repo => repo.AddColor(It.Is<Model.Color>(c => c.Id == Guid.Parse("00000000-0000-0000-0000-000000000001")))).Throws(new InvalidOperationException("Color with id 00000000-0000-0000-0000-000000000001 already exists."));
            _mockColorsRepository.Setup(repo => repo.AddColor(It.Is<Model.Color>(c => c.Id == Guid.Parse("99999999-9999-9999-9999-999999999999")))).Verifiable();
            
            _mockColorsRepository.Setup(repo => repo.UpdateColor(It.Is<Model.Color>(c => c.Id == Guid.Parse("00000000-0000-0000-0000-000000000001")))).Verifiable();
            _mockColorsRepository.Setup(repo => repo.UpdateColor(It.Is<Model.Color>(c => c.Id == Guid.Parse("99999999-9999-9999-9999-999999999999")))).Throws(new KeyNotFoundException("Color with id 99999999-9999-9999-9999-999999999999 not found."));
            
            _mockColorsRepository.Setup(repo => repo.DeleteColor(Guid.Parse("00000000-0000-0000-0000-000000000001"))).Verifiable();
            _mockColorsRepository.Setup(repo => repo.DeleteColor(Guid.Parse("99999999-9999-9999-9999-999999999999"))).Throws(new KeyNotFoundException("Color with id 99999999-9999-9999-9999-999999999999 not found."));
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
    }
}
