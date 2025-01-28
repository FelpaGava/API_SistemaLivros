using API_WebLocalize.Data;
using API_WebLocalize.Dto.Autor;
using API_WebLocalize.Dto.Livro;
using API_WebLocalize.Models;
using Microsoft.EntityFrameworkCore;

namespace API_WebLocalize.Services.Livro
{

    //injeção de dependência no banco de dados
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            { 
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }
                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado com sucesso";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel <List<LivroModel>>();

            try
            {   //Buscar o autor do livro ("INCLUDE" é usado para incluir a entidade relacionada
                var livro = await _context.Livros
                    .Include(a => a.Autor).Where(livroBanco => livroBanco.Autor.Id == idAutor)
                    .ToListAsync();

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livros encontrado com sucesso";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
               var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localizado!";
                    return resposta;
                }
                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro encontrado!";
                    return resposta;
                }
                
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localizado!";
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros.
                    Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro excluído com sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();

                resposta.Dados = livros;
                resposta.Mensagem = "Livros listados com sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

 
    }
}
