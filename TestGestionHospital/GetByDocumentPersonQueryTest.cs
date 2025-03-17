using AutoMapper;
using Moq;
using Persona.Application.DTO;
using Persona.Application.Queries;
using Persona.Domain.Entities;
using Persona.Domain.Interface;

namespace TestGestionHospital
{
    public class GetByDocumentPersonQueryTest
    {
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetByDocumentPersonQueryHandler _handler;

        public GetByDocumentPersonQueryTest()
        {
            _personaRepositoryMock = new Mock<IPersonaRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetByDocumentPersonQueryHandler(_personaRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByDocumentPerson_Test()
        {
            // Arrange
            string documento = "12345678";

            var persona = new ListaPersona { id = 1, nombre = "Juan", numeroDocumento = "12345678" };
            var personaDto = new PersonasDto { id = 1, nombre = "Juan", numeroDocumento = "12345678" };

            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(documento))
                                  .ReturnsAsync(persona);

            _mapperMock.Setup(m => m.Map<PersonasDto>(It.IsAny<ListaPersona>()))
                       .Returns(personaDto);

            var query = new GetByDocumentPersonQuery(documento);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(personaDto.id, result.id);
            Assert.Equal(personaDto.nombre, result.nombre);
            Assert.Equal(personaDto.numeroDocumento, result.numeroDocumento);
        }

        [Fact]
        public async Task GetByDocumentPerson_Dtest()
        {
            // Arrange
            string documento = "00000000";

            _personaRepositoryMock.Setup(repo => repo.GetByDocumentoAsync(documento))
                                  .ReturnsAsync((ListaPersona)null);

            var query = new GetByDocumentPersonQuery(documento);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
