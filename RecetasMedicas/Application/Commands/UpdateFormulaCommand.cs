using MediatR;
using RecetasMedicas.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasMedicas.Application.Commands
{
	public class UpdateFormulaCommand: IRequest<bool>
	{
        public string _codigo { get; set; }
        public FormulaMedicaDTO _formulaDto;
        public UpdateFormulaCommand(string codigo, FormulaMedicaDTO formulaDto)
        {
            _codigo = codigo;
            _formulaDto = formulaDto;
        }
    }
}