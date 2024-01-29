using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Db.Entities
{
    [Table("COMMENTS")]
    [PrimaryKey("Id")]
    public class CommentEntity
    {
        public string Id { get; set; }
        public string BlogId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
