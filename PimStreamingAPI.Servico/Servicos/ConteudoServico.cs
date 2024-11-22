using Microsoft.AspNetCore.Http;
using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Servico.Servicos
{
    public class ConteudoServico : BaseServico<Conteudo>, IConteudoServico
    {
        private readonly IRepositorioGenerico<Conteudo> _repositorio;

        public async Task<Conteudo> UploadVideoAsync(Conteudo conteudo, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                throw new Exception("Arquivo de vídeo inválido.");

            // Define o caminho onde o vídeo será salvo
            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var fileName = $"{Guid.NewGuid()}_{arquivo.FileName}";

           var caminhoArquivo = Path.Combine(pasta, fileName);
            

            // Salva o arquivo no sistema de arquivos
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Atualiza o conteúdo com o caminho do arquivo
            conteudo.CaminhoArquivo = fileName;

            // Salva no banco de dados
            await _repositorio.AdicionarAsync(conteudo);
            return conteudo;
        }

        public ConteudoServico(IRepositorioGenerico<Conteudo> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Conteudo>> ObterConteudosPorPlaylistIdAsync(int playlistId)
        {
            return await _repositorio.ObterComFiltroAsync(c => c.PlaylistID == playlistId);
        }

        public async Task<IEnumerable<Conteudo>> ObterConteudosPorCriadorIdAsync(int criadorId)
        {
            return await _repositorio.ObterComFiltroAsync(c => c.CriadorID == criadorId);
        }

    }
}
