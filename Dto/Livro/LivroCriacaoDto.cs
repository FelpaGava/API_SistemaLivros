using API_WebLocalize.Dto.Livro.Vinculo;
using API_WebLocalize.Models;

namespace API_WebLocalize.Dto.Livro
{
    public class LivroCriacaoDto
    {
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }

    }
}
