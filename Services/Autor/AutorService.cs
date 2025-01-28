using System.Collections.Generic;
using API_WebLocalize.Data;
using API_WebLocalize.Dto.Autor;
using API_WebLocalize.Models;
using Microsoft.EntityFrameworkCore;

namespace API_WebLocalize.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        // Acessando o banco de dados, o que faz buscar os dados
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                //Verificar se o autor existe no banco de dados
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }
                resposta.Dados = autor;
                resposta.Mensagem = "Autor encontrado com sucesso";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {   //Buscar o autor do livro ("INCLUDE" é usado para incluir a entidade relacionada
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)  
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor encontrado com sucesso";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {

            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {

                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Autores listados com sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {   //Buscar o autor do livro ("INCLUDE" é usado para incluir a entidade relacionada
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor encontrado com sucesso";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {
                var autor = new AutorModel
                {
                    Nome = autorCriacaoDto.Nome,
                    Sobrenome = autorCriacaoDto.Sobrenome
                };

                _context.Add(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor criado com sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel <List<AutorModel >> resposta = new ResponseModel<List<AutorModel>>();

            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                autor.Nome = autorEdicaoDto.Nome;
                autor.Sobrenome = autorEdicaoDto.Sobrenome;

                _context.Update(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor editado com sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                _context.Remove(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor excluído com sucesso";

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
