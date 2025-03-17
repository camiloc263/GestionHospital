using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitasMedicas.Infrastructure.Repository
{
  public  interface IRabitMqRepository
    {
        Task PublicarMensaje(object message);
    }
}
