using Moq;
using System.Collections.Generic;
using Xunit;
using Persona.Application.DTO;

using AutoMapper;
using Persona.Domain.Entities;
using Persona.Domain.Interface;
using Persona.Application.Services;

public class PersonasTest
{
    private readonly Mock<IPersonaRepository> _personaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    
    

    public PersonasTest()
    {
        _personaRepositoryMock = new Mock<IPersonaRepository>();
        _mapperMock = new Mock<IMapper>();
       
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfPersonaDto()
    {
        // Arrange
        var personas = new List<ListaPersona>
    {
        new ListaPersona { id = 3, nombre = "Juan" }
    };

        var personasDto = new List<PersonasDto>
    {
        new PersonasDto { id = 3, nombre = "Juan" }
    };

        _personaRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(personas); // 🔹 Usa ReturnsAsync() en lugar de Returns()

        _mapperMock.Setup(m => m.Map<List<PersonasDto>>(It.IsAny<List<ListaPersona>>()))
                   .Returns(personasDto);

        // Act
        var personasResult = await _personaRepositoryMock.Object.GetAll();
        Assert.NotNull(personasResult);

        // Assert
        Assert.NotNull(personasResult);
        Assert.Single(personasResult);
        Assert.Equal("Juan", personasResult[0].nombre);
    }
}