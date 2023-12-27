using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wep_api_token.ContextModel;
using wep_api_token.models;
using wep_api_token.models.DTOs;

namespace wep_api_token.Controllers
{

    [ApiController]
    [Route("api/")]
    public class BlogController : ControllerBase
    {
        private readonly UsuariosContext db;
        private readonly ILogger<BlogController> logger;
        private readonly IHostEnvironment hostEnvironment;

        public BlogController(UsuariosContext db , ILogger<BlogController> logger, IHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet("blogs")]
         public ActionResult<Response> ListBlogs()
        {
            var listado = db.Blogs.ToList();
            return Ok(listado);
        }

        [HttpGet("blog/{idBlog:int}/detail-blog")]
        public async  Task<ActionResult<Response>> BlogDetailById(int idBlog)  
        {
            var existeId = db.Blogs.Any(b => b.Id == idBlog);
            if(!existeId)
            {
                return BadRequest("No hay data");
            }
            var detailBlog = await db.DetalleBlogs.FirstOrDefaultAsync(b => b.BlogId == idBlog);
            return Ok(detailBlog);
        }

        [HttpPost("blog")]
        public async Task<ActionResult<Response>> CreatePost(BlogCreateDto blogDto)
        {

            var today = DateOnly.FromDateTime(DateTime.Now);
            Blog newBlog = new Blog
            {
                Titulo = blogDto.Titulo,
                SubTitulo = blogDto.SubTitulo,
                Autor = blogDto.Autor,
                FechaPublicacion = today
            };

            db.Blogs.Add(newBlog);
            await db.SaveChangesAsync();
            
            
            return Ok(newBlog.Id);
        }

        [HttpPost("blog/{idBlog:int}/detail-blog")]
        public async Task<ActionResult<Response>> CreateDetailBlog(int idBlog , DetailBlogCreateDto detailBlogCreateDto)
        {   
            var existe = db.Blogs.Any(blog => blog.Id == idBlog);
            if (!existe)
            {
                return BadRequest(new Response
                {
                    Success= false,
                    data  = null,
                    Message = "El id no existe"
                });
            }
            
            DetalleBlog detalleBlog = new DetalleBlog();
            detalleBlog.BlogId = idBlog;
            detalleBlog.Titulo = detailBlogCreateDto.Titulo;
            detalleBlog.Subtitulo = detailBlogCreateDto.Subtitulo;
            detalleBlog.ImagenUno = detailBlogCreateDto.ImagenUno;
            detalleBlog.ImagenDos= detailBlogCreateDto.ImagenDos;
            detalleBlog.ParrafoUno = detailBlogCreateDto.ParrafoUno;
            detalleBlog.ParrafoDos= detailBlogCreateDto.ParrafoDos;

            db.DetalleBlogs.Add(detalleBlog);
            var result = await db.SaveChangesAsync();

            return Ok(new Response
            {
                Success = true,
                data = null,
                Message = "se creo exitosamente el detalle blog"
            });

        }

        [HttpGet("blog/gg")]
        public ActionResult UpdateFile()
        {
            string path = Directory.GetCurrentDirectory();
            path = path + @"\resource";
            logger.LogInformation(path);

            if (!Directory.Exists(path))
            {
                logger.LogInformation(path);
                Directory.CreateDirectory(path);

            }
            
            return Ok(true);
        }

        [HttpPost("blog/subida-archivo")]
        public ActionResult UpdateFileTwo([FromForm] FormDto formDto) 
        {
            var currentDirectoryDirtory = Directory.GetCurrentDirectory();
            currentDirectoryDirtory += @"\resource";
            logger.LogInformation(currentDirectoryDirtory);
            if (!Directory.Exists(currentDirectoryDirtory))
            {
                Directory.CreateDirectory(currentDirectoryDirtory);
            }

            var fiplePath = Path.Combine(currentDirectoryDirtory, formDto.Archivo.FileName);

            using(var fileStream = new FileStream(fiplePath,FileMode.Create))
            {
                formDto.Archivo.CopyTo(fileStream);
            }

            return Ok("Subido");
        }
    }
}
