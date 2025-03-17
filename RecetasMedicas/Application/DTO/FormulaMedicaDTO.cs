using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecetasMedicas.Application.DTO
{
	public class FormulaMedicaDTO
	{
        public string CodigoReceta { get; set; }
        public int IdPaciente { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
    }
}