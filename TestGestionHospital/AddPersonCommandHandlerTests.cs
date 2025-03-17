using AutoMapper;
using Moq;
using Persona.Application.Commands;
using Persona.Application.DTO;
using Persona.Application.Queries;
using Persona.Domain.Entities;
using Persona.Domain.Interface;
using FluentAssertions;



namespace TestGestionHospital
{
    public class AddPersonCommandHandlerTests
    {
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly IMapper _mapper;
        private readonly AddPersonCommandHandler _handler;

        public AddPersonCommandHandlerTests()
        {
            _personaRepositoryMock = new Mock<IPersonaRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonasDto, ListaPersona>();
            });
            _mapper = config.CreateMapper();

            _handler = new AddPersonCommandHandler(_personaRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Call_AddPersona_And_Return_True()
        {
            // Arrange
            var personaDto = new PersonasDto { numeroDocumento = "4569801", tipoDocumento = "CC", nombre = "Nuevo", apellidoUno = "Prueba",apellidoDos="aaaa",direccion="rrfrf",telefono="45432",correo="qwfaaa",fechaNacimiento=DateTime.Now, tipoUsuario = "Medico" };
            var command = new AddPersonCommand(personaDto);
            _personaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ListaPersona>()))
               .ReturnsAsync(true);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _personaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<ListaPersona>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_AddPersona_Fails()
        {
            // Arrange
            var personaDto = new PersonasDto { numeroDocumento = "4569801", tipoDocumento = "CC", nombre = "Nuevo", apellidoUno = "Prueba", tipoUsuario = "Medico" };
            var command = new AddPersonCommand(personaDto);
            _personaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ListaPersona>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

    public class DeletePersonCommandHandlerTests
    {
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly IMapper _mapper;
        private readonly DeletePersonCommandHandler _handler;

        public DeletePersonCommandHandlerTests()
        {
            _personaRepositoryMock = new Mock<IPersonaRepository>();

            var config = new MapperConfiguration(cfg => { });
            _mapper = config.CreateMapper();

            _handler = new DeletePersonCommandHandler(_personaRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_False_When_Person_Not_Found()
        {
            // Arrange
            var command = new DeletePersonCommand("4569801");
            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(command._identificacion))
                .ReturnsAsync((ListaPersona)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_Should_Delete_Person_When_Found()
        {
            // Arrange
            var persona = new ListaPersona { numeroDocumento = "4569801" };
            var command = new DeletePersonCommand("4569801");
            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(command._identificacion))
                .ReturnsAsync(persona);
            _personaRepositoryMock.Setup(repo => repo.DeleteAsync(persona))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _personaRepositoryMock.Verify(repo => repo.DeleteAsync(persona), Times.Once);
        }
    }

    public class UpdatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdatePersonCommandHandler _handler;

        public UpdatePersonCommandHandlerTests()
        {
            _personaRepositoryMock = new Mock<IPersonaRepository>();
            var config = new MapperConfiguration(cfg => { });
            _mapper = config.CreateMapper();

            _handler = new UpdatePersonCommandHandler(_personaRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Return_False_When_Person_Not_Found()
        {
            // Arrange
            var command = new UpdatePersonCommand("4569801", new PersonasDto { numeroDocumento = "4569801", tipoDocumento = "CC", nombre = "Nuevo", apellidoUno = "Prueba", tipoUsuario = "Medico" });
            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(command.Identificacion))
                .ReturnsAsync((ListaPersona)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_Should_Update_Person_When_Found()
        {
            // Arrange
            var persona = new ListaPersona { numeroDocumento = "4569801", tipoDocumento = "TI", nombre = "Viejo", apellidoUno = "Antiguo", tipoUsuario = "Paciente" };
            var personaDto = new PersonasDto { numeroDocumento = "4569801", tipoDocumento = "CC", nombre = "Nuevo", apellidoUno = "Prueba", tipoUsuario = "Medico" };
            var command = new UpdatePersonCommand("4569801", personaDto);

            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(command.Identificacion))
                .ReturnsAsync(persona);
            _personaRepositoryMock.Setup(repo => repo.UpdatePerson(persona))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal("Nuevo", persona.nombre);
            Assert.Equal("Prueba", persona.apellidoUno);
            Assert.Equal("CC", persona.tipoDocumento);
            Assert.Equal("Medico", persona.tipoUsuario);
            _personaRepositoryMock.Verify(repo => repo.UpdatePerson(persona), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Update_Fails()
        {
            // Arrange
            var persona = new ListaPersona { numeroDocumento = "4569801", tipoDocumento = "CC", nombre = "Nuevo", apellidoUno = "Prueba", tipoUsuario = "Medico" };
            var personaDto = new PersonasDto { numeroDocumento = "4569801", tipoDocumento = "CC", nombre = "Actualizado", apellidoUno = "Modificado", tipoUsuario = "Especialista" };
            var command = new UpdatePersonCommand("4569801", personaDto);

            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(command.Identificacion))
                .ReturnsAsync(persona);
            _personaRepositoryMock.Setup(repo => repo.UpdatePerson(persona))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
    public class GetAllPersonQueryHandlerTests
    {
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetAllPersonasQuerysHandler _handler;

        public GetAllPersonQueryHandlerTests()
        {
            _personaRepositoryMock = new Mock<IPersonaRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ListaPersona, PersonasDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetAllPersonasQuerysHandler(_personaRepositoryMock.Object, _mapper);
        }
        [Fact]
        public async Task Handle_Should_Call_GetList_Once()
        {
            // Arrange
            _personaRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ListaPersona>());

            var query = new GetAllPersonasQuery();

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            _personaRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }


        [Fact]
        public async Task Handle_Should_Return_List_Of_Personas()
        {
            // Arrange
            var personas = new List<ListaPersona>
        {
            new ListaPersona { numeroDocumento = "123456", tipoDocumento = "CC", nombre = "Juan", apellidoUno = "Perez", tipoUsuario = "Paciente" },
            new ListaPersona { numeroDocumento = "789012", tipoDocumento = "TI", nombre = "Maria", apellidoUno = "Gomez", tipoUsuario = "Medico" }
        };
            _personaRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(personas);

            var query = new GetAllPersonasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Juan", result[0].nombre);
            Assert.Equal("Maria", result[1].nombre);
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_List_When_No_Personas_Found()
        {
            // Arrange
            _personaRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ListaPersona>());

            var query = new GetAllPersonasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
    public class GetByDocumentPersonQueryHandlerTests
    {
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetByDocumentPersonQueryHandler _handler;

        public GetByDocumentPersonQueryHandlerTests()
        {
            _personaRepositoryMock = new Mock<IPersonaRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ListaPersona, PersonasDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetByDocumentPersonQueryHandler(_personaRepositoryMock.Object, _mapper);
        }
        [Fact]
        public async Task Handle_Should_Call_GetByDocumentoAsync_With_Correct_Id()
        {
            // Arrange
            string testId = "123456";
            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(testId)).ReturnsAsync(new ListaPersona());

            var query = new GetByDocumentPersonQuery(testId);

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            _personaRepositoryMock.Verify(repo => repo.GetByDocumentoAsync(testId), Times.Once);
        }


        [Fact]
        public async Task Handle_Should_Return_PersonasDto_When_Person_Found()
        {
            // Arrange
            var persona = new ListaPersona { numeroDocumento = "123456", tipoDocumento = "CC", nombre = "Juan", apellidoUno = "Perez", tipoUsuario = "Paciente" };
            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(persona.numeroDocumento))
                .ReturnsAsync(persona);

            var query = new GetByDocumentPersonQuery(persona.numeroDocumento);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(persona.numeroDocumento, result.numeroDocumento);
            Assert.Equal(persona.nombre, result.nombre);
            Assert.Equal(persona.apellidoUno, result.apellidoUno);
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_Person_Not_Found()
        {
            // Arrange
            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(It.IsAny<string>()))
                .ReturnsAsync((ListaPersona)null);

            var query = new GetByDocumentPersonQuery("999999"); // Un documento inexistente

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void UserDto_Should_Set_And_Get_Properties_Correctly()
        {
            // Arrange
            var userDto = new UserDto
            {
                Username = "testuser",
                Password = "securepassword",
                id = "123"
            };

            // Act & Assert
            userDto.Username.Should().Be("testuser");
            userDto.Password.Should().Be("securepassword");
            userDto.id.Should().Be("123");
        }
    }
}
