using API_WebLocalize.Dto.Autor;
using API_WebLocalize.Models;

namespace API_WebLocalize.Services.Autor
{
    public interface IAutorInterface
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor); //busca de Autor 
        Task<ResponseModel<AutorModel>> BuscarAutorPorLivro(int idLivro); //busca do livro
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro); //buscar autor por id do livro
        Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto); //criação de um novo autor
        Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto); //editar um autor
        Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor); //deletar um autor
    }
}
