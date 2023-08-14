using Microsoft.Identity.Client;

namespace WebApi.Models
{
    public class Respuesta
    {
        public Respuesta() { }
        public string Mensaje { get; set; }
        public bool Error { get; set; }
        public object Obj { get; set; }

    }
}
