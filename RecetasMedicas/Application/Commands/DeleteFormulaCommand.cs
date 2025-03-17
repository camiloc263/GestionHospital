using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasMedicas.Application.Commands
{
	public class DeleteFormulaCommand: IRequest<bool>
	{
        public string _codigo { get; set; }

        public DeleteFormulaCommand(string codigo)
        {
            _codigo = codigo;
        }
    }
}