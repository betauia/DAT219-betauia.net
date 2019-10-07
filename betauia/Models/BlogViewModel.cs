using System;

namespace betauia.Models
{
  public class BlogViewModel
  {
    public BlogViewModel(BlogPost blogPost)
    {
      Id = blogPost.Id;
      Title = blogPost.Title;
      Image = blogPost.Image;
      Summary = blogPost.Summary;
      Content = blogPost.Content;
      CreationDate = blogPost.CreationDate.ToString("dd-MM-yyyy HH:mm:ss");
      LastEditDate = blogPost.LastEditDate.ToString("dd-MM-yyyy HH:mm:ss");
    }

    public int Id { get; set; }
    public string Title { get; set; }

    public string Image { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
    public string CreationDate { get; set; }
    public string LastEditDate { get; set; }  }
}
