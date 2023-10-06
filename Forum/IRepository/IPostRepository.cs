﻿using Forum.Models;

namespace Forum.IRepository
{
    public interface IPostRepository
    {

        ICollection<Post> GetAll();
        ICollection<Post> Profile(string userId);
        Task<Post> GetById(int id);
        Task Add(Post post, IFormFile image);
        Task Update( Post post, IFormFile image);
        Task Delete(int id);


        Post GetAllComments(int id);



    }

}
