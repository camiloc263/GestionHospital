using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.DTO
{
	public class RecetasDto
	{
        public string CodigoReceta { get; set; }
        public int IdPaciente { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
    }
}