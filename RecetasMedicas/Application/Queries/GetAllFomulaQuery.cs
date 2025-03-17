using MediatR;
using RecetasMedicas.Application.DTO;
using System.Collections.Generic;

namespace RecetasMedicas.Application.Queries
{
    public class GetAllFomulaQuery : IRequest<List<FormulaMedicaDTO>>
    {
    }
}