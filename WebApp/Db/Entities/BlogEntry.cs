using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Db.Entities;

[Table("BLogs")]
[PrimaryKey(propertyName: "Id")]
public class BlogEntry
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Created { get; set; }
    public string Modified { get; set; }
}
