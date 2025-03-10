using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.DTO
{
	public class CitaDto
	{

       public int idcita { get; set; }

        public int? idpaciente { get; set; }

        public int? idmedico { get; set; }

        public string lugarcita { get; set; }

        public DateTime? fechacita { get; set; }

        public string estadocita { get; set; }

    }
}