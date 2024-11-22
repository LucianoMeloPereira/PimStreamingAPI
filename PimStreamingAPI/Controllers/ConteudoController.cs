using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.DTO.ConteudoDTO;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;
using PimStreamingAPI.Dominio.DTO.ConteudoDTO;
using Microsoft.EntityFrameworkCore;

namespace PimStreamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudoController : ControllerBase
    {
        private readonly IConteudoServico _conteudoServico;

        public ConteudoController(IConteudoServico conteudoServico)
        {
            _conteudoServico = conteudoServico;
        }

        [HttpPost("UploadVideo")]
        public async Task<ActionResult<ConteudoResponseDTO>> UploadVideo([FromForm] ConteudoUploadDTO conteudoDTO)
        {
            var novoConteudo = new Conteudo
            {
                Titulo = conteudoDTO.Titulo,
                Tipo = conteudoDTO.Tipo,
                PlaylistID = conteudoDTO.PlaylistID,
                CriadorID = conteudoDTO.CriadorID
            };

            var conteudoCriado = await _conteudoServico.UploadVideoAsync(novoConteudo, conteudoDTO.Arquivo);

            var response = new ConteudoResponseDTO
            {
                ID = conteudoCriado.ID,
                Titulo = conteudoCriado.Titulo,
                Tipo = conteudoCriado.Tipo,
                PlaylistID = conteudoCriado.PlaylistID,
                CriadorID = conteudoCriado.CriadorID,
                CaminhoArquivo = conteudoCriado.CaminhoArquivo
            };

            return CreatedAtAction(nameof(ObterConteudoPorId), new { id = response.ID }, response);
        }

        [HttpGet("DownloadVideo/{id}")]
        public async Task<IActionResult> DownloadVideo(int id)
        {
            var conteudo = await _conteudoServico.ObterPorIdAsync(id);
            if (conteudo == null || string.IsNullOrEmpty(conteudo.CaminhoArquivo))
                return NotFound("Conteúdo ou arquivo não encontrado.");

            var arquivoBytes = await System.IO.File.ReadAllBytesAsync(conteudo.CaminhoArquivo);
            var tipoArquivo = "application/octet-stream"; // Ou use um tipo específico como "video/mp4"

            return File(arquivoBytes, tipoArquivo, Path.GetFileName(conteudo.CaminhoArquivo));
        }

        [HttpGet("StreamAllVideos")]
        public async Task<ActionResult<IEnumerable<ConteudoResponseDTO>>> StreamAllVideos()
        {
            var conteudos = await _conteudoServico.ObterTodosAsync();

            if (conteudos == null || !conteudos.Any())
                return NotFound("Nenhum vídeo encontrado.");

            var response = conteudos.Select(c => new ConteudoResponseDTO
            {
                ID = c.ID,
                Titulo = c.Titulo,
                Tipo = c.Tipo,
                PlaylistID = c.PlaylistID,
                CriadorID = c.CriadorID,
                CaminhoArquivo = c.CaminhoArquivo != null
                    ? Url.Action("StreamVideo", "Conteudo", new { id = c.ID }, Request.Scheme)
                    : null // Evita retornar URLs inválidas
            });

            return Ok(response);
        }


        [HttpPost("CriarConteudo")]
        public async Task<ActionResult<ConteudoResponseDTO>> CriarConteudo([FromBody] ConteudoCreateDTO conteudoDTO)
        {
            var novoConteudo = new Conteudo
            {
                Titulo = conteudoDTO.Titulo,
                Tipo = conteudoDTO.Tipo,
                PlaylistID = conteudoDTO.PlaylistID,
                CriadorID = conteudoDTO.CriadorID
            };

            await _conteudoServico.AdicionarAsync(novoConteudo);

            var response = new ConteudoResponseDTO
            {
                ID = novoConteudo.ID,
                Titulo = novoConteudo.Titulo,
                Tipo = novoConteudo.Tipo,
                PlaylistID = novoConteudo.PlaylistID,
                CriadorID = novoConteudo.CriadorID
            };

            return CreatedAtAction(nameof(ObterConteudoPorId), new { id = response.ID }, response);
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<ConteudoResponseDTO>>> ObterTodos()
        {
            var conteudos = await _conteudoServico.ObterTodosAsync();

            var response = conteudos.Select(c => new ConteudoResponseDTO
            {
                ID = c.ID,
                Titulo = c.Titulo,
                Tipo = c.Tipo,
                PlaylistID = c.PlaylistID,
                CriadorID = c.CriadorID
            });

            return Ok(response);
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult<ConteudoResponseDTO>> ObterConteudoPorId(int id)
        {
            var conteudo = await _conteudoServico.ObterPorIdAsync(id);

            if (conteudo == null)
                return NotFound("Conteúdo não encontrado.");

            var response = new ConteudoResponseDTO
            {
                ID = conteudo.ID,
                Titulo = conteudo.Titulo,
                Tipo = conteudo.Tipo,
                PlaylistID = conteudo.PlaylistID,
                CriadorID = conteudo.CriadorID
            };

            return Ok(response);
        }

        [HttpGet("ObterPorPlaylist/{playlistId}")]
        public async Task<ActionResult<IEnumerable<ConteudoResponseDTO>>> ObterPorPlaylist(int playlistId)
        {
            var conteudos = await _conteudoServico.ObterConteudosPorPlaylistIdAsync(playlistId);

            var response = conteudos.Select(c => new ConteudoResponseDTO
            {
                ID = c.ID,
                Titulo = c.Titulo,
                Tipo = c.Tipo,
                PlaylistID = c.PlaylistID,
                CriadorID = c.CriadorID
            });

            return Ok(response);
        }

        [HttpGet("StreamVideo/{id}")]
        public  async Task<ActionResult<IEnumerable<ConteudoResponseDTO>>> StreamVideo(int id)
        {
            var conteudo = await _conteudoServico.ObterPorIdAsync(id);

            if (conteudo == null || string.IsNullOrEmpty(conteudo.CaminhoArquivo))
            {
                return NotFound("Conteúdo ou arquivo não encontrado.");
            }

            // Constrói o caminho absoluto do arquivo
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Videos", // Pasta onde os vídeos estão armazenados
                conteudo.CaminhoArquivo.Replace("/uploads/", "") // Ajusta o caminho relativo
            );

            // Verifica se o arquivo existe
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Arquivo não encontrado no servidor.");
            }

            var fileInfo = new FileInfo(filePath);
            var totalLength = fileInfo.Length; // Tamanho total do arquivo em bytes

            // Verifica se a requisição possui o cabeçalho "Range"
            if (Request.Headers.ContainsKey("Range"))
            {
                var rangeHeader = Request.Headers["Range"].ToString();
                var range = rangeHeader.Replace("bytes=", "").Split('-');

                // Determina o início e fim do intervalo solicitado
                var start = Convert.ToInt64(range[0]);
                var end = range.Length > 1 && !string.IsNullOrEmpty(range[1])
                    ? Convert.ToInt64(range[1])
                    : totalLength - 1;

                if (start >= totalLength || end >= totalLength)
                {
                    return BadRequest("Faixa inválida.");
                }

                var contentLength = end - start + 1;

                // Prepara o stream para o intervalo solicitado
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileStream.Seek(start, SeekOrigin.Begin);

                // Configura os cabeçalhos de resposta
                Response.StatusCode = 206; // Partial Content
                Response.ContentLength = contentLength;
                Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{totalLength}");
                Response.Headers.Add("Accept-Ranges", "bytes");

                return File(fileStream, "video/mp4");
            }

            // Retorna o arquivo completo se nenhum Range for solicitado
            var fullStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.ContentLength = totalLength;

            return File(fullStream, "video/mp4");
        }

        [HttpPut("AtualizarConteudo/{id}")]
        public async Task<ActionResult> AtualizarConteudo(int id, [FromBody] ConteudoCreateDTO conteudoDTO)
        {
            var conteudo = new Conteudo
            {
                ID = id,
                Titulo = conteudoDTO.Titulo,
                Tipo = conteudoDTO.Tipo,
                PlaylistID = conteudoDTO.PlaylistID,
                CriadorID = conteudoDTO.CriadorID
            };

            await _conteudoServico.AtualizarAsync(conteudo);

            return NoContent();
        }

        [HttpDelete("DeletarConteudo/{id}")]
        public async Task<ActionResult> DeletarConteudo(int id)
        {
            await _conteudoServico.RemoverAsync(id);
            return NoContent();
        }
    }
}
