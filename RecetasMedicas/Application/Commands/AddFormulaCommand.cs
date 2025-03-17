using CitasMedicas.Application.DTO;
using MediatR;
using RecetasMedicas.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasMedicas.Application.Commands
{
	public class AddFormulaCommand: IRequest<bool>
	{
        public FormulaMedicaDTO _formula { get; set; }

        public AddFormulaCommand(FormulaMedicaDTO formula)
        {
            _formula = formula;
        }
    }
}
