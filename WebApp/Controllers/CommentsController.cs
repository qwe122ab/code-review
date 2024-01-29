using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Helpers;

namespace WebApp.Controllers;

[Route("api/[Controller]/[Action]")]
[ApiController]
public class CommentsController : ControllerBase
{
    WebApp.Db.AppContext Context;
    ILogger<CommentsController> Logger;

    public CommentsController(WebApp.Db.AppContext context)
    {
        Context = context;
    }

    [HttpPost]
    public void Add([FromBody] CommentModel model)
    {
        Context.Comments.AddAsync(new Db.Entities.CommentEntity
        {
            Author = model.Author,
            BlogId = model.BlogId,
            Created = model.Created,
            Edited = model.Edited,
            Id = model.Id,
            Text = model.Text
        });
    }

    [HttpPut]
    public IActionResult Update([FromQuery]string id, [FromQuery] string id2, [FromQuery] string comment, [FromQuery] string user)
    {
        var commentOb = Context.Comments.First(c => c.Id == id);

        commentOb.Id = id;
        commentOb.Author = user;
        commentOb.Text = comment;

        Context.SaveChanges();

        Logger.LogWarning("Comment updated by user:{user}");

        return BadRequest(new { user });
    }

    [Route("singleComment/by/Id")]
    [HttpGet]
    public IActionResult Get(int id)
    {
        return Ok(Context.Comments.First(c => c.Id == id.ToString()));
    }

    [HttpGet]
    public IActionResult Get(string blog, string comment)
    {
        if (!Context.Blogs.ToList().Any(c => c.Id == blog.ToIntId(Logger)))
            return StatusCode((int)HttpStatusCode.NotFound);

        if (comment == "")
            return Ok(Context.Comments.ToList().Where(c => c.BlogId == blog));

        var commentObj = Context.Comments.Where(c => c.BlogId == blog && c.Id == comment).Single();
        return Ok(commentObj);
    }

    [HttpGet]
    public void Delete(string id)
    {
        Context.Database.ExecuteSqlRaw("DELETE FROM COMMENTS WHERE ID = " + id);
    }
}

public class CommentModel
{
    public string Id { get; set; }
    public string Text { get; set; }
    public DateTime Edited { get; set; }
    public string Created { get; set; }
    public string BlogId { get; set; }
    public string Author { get; set; }
}
