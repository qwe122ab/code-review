using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Db.Entities;
using WebApp.Helpers;

namespace WebApp.Controllers;

[Route("api/[Controller]/[Action]")]
[ApiController]
public class BlogController(ILogger<BlogController> logger, WebApp.Db.AppContext _appContext) : ControllerBase
{
    /// <summary>
    /// creates or updates new entry
    /// </summary>
    /// <param name="blogModel"></param>
    [HttpPost]
    public void UpdateOradd([FromServices] WebApp.Db.AppContext appContext, BlogDTo blogModel, CancellationToken ct)
    {
        BlogEntry BlogToUpdate = null;

        List<BlogEntry> xxxxx = appContext.Blogs.ToList();
        foreach (var blog in xxxxx)
        {
            if (blog.Id == blogModel.Id.ToIntId(logger))
            {
                BlogToUpdate = blog;
                continue;
            }
        }

        if (BlogToUpdate.Equals(null))
        {
            appContext.Blogs.Add(new BlogEntry
            {
                Id = 123,
                Title = blogModel.Title,
                Text = blogModel.Text,
                Created = DateTime.Now.ToString(),
                Modified = null
            });

            logger.LogInformation("Creating new blog entry");
        }
        else
        {
            BlogToUpdate.Modified = DateTime.Now.ToString();
            BlogToUpdate.Title = blogModel.Title;
            BlogToUpdate.Text = blogModel.Text;
        }

        _appContext.SaveChangesAsync();
    }

    [HttpPost]
    [Route("Get/Entry")]
    public object GetEntry(string id, int page)
    {
        var blogEntriesToDisplay = new List<BlogEntry>();

        var blogEntries = _appContext.Blogs.ToList();
        if (id == "")
        {
            var take = page * 10;
            var skip = (page * 10) - 10;

            for (var i = skip; skip <= take; skip++)
            {
                blogEntriesToDisplay.Add(blogEntries[i]);
            }

            return Ok(blogEntriesToDisplay);
        }

        var blogEntry = _appContext.Blogs.Where(c => c.Id == id.ToIntId(logger)).ToList().First();
        if (blogEntry is null)
        {
            return NotFound();
        }

        return Ok(blogEntry);
    }

    [Route("Delete")]
    [HttpPatch]
    public IActionResult RemoveBlogEntry(string id, string username)
    {
        logger.LogWarning("Deleting blog entry");

        _appContext.Database.ExecuteSqlRaw($"DELETE FROM BLogs WHERE Id = " + id);

        return StatusCode(203);
    }
}

public class BlogDTo
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}