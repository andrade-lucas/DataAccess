﻿namespace DataAccess.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public User Author { get; set; }
    public string Sumary { get; set; }
    public string Body { get; set; }
    public string Slug { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
