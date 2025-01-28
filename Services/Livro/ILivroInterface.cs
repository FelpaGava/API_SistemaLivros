using API_WebLocalize.Dto.Autor;
using API_WebLocalize.Dto.Livro;
using API_WebLocalize.Models;

namespace API_WebLocalize.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro); //busca de Livro por id
        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor); //busca do livro
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto); //criação de um novo livro
        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto); //editar um livro
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro); //deletar um livro
    }
}
 