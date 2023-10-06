﻿using Forum.DAL;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.IRepository.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostRepository(ForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public ICollection<Post> GetAll()
        {
            return _context.Posts.Include(a => a.User).Include(a => a.Likes).Include(a => a.Comments).OrderByDescending(a => a.PublishDate).ToList();

        }  
        
        public ICollection<Post> Profile(string userId)
        {
            return _context.Posts.Where(a => a.UserId == userId).Include(a => a.User).Include(a => a.Likes).Include(a => a.Comments).OrderByDescending(a => a.PublishDate).ToList();

        }


        public async Task<Post> GetById(int id)
        {
            Post post = await _context.Posts.Include(a => a.Likes).Include(a => a.User).SingleOrDefaultAsync(a => a.Id == id);
            return post;
        }


        public async Task Add(Post post, IFormFile image)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }


        public async Task Update( Post post, IFormFile image)
        {
               
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            
        }


        public async Task Delete(int id)
        {
            var post = await GetById(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }


        //public async Task<Post> GetAllComments(int id)
        //{
        //    var post = await _context.Posts.Where(a => a.Id == id)
        //        .Include(a => a.Likes).Include(a => a.User)
        //        .Include(a => a.Comments).ThenInclude(a => a.User)
        //        .Include(a => a.Comments).ThenInclude(a => a.Likes)
        //        .Include(a => a.Comments).ThenInclude(a => a.ReplyToComments).ThenInclude(a => a.Likes)
        //        .Include(a => a.Comments).ThenInclude(a => a.ReplyToComments).ThenInclude(a => a.User)
        //        .SingleOrDefaultAsync();


        //    return post;
        //}
        public Post GetAllComments(int id)
        {
            var post =  _context.Posts.Where(a => a.Id == id)
                .Include(a => a.Likes).Include(a => a.User)
                .Include(a => a.Comments).ThenInclude(a => a.User)
                .Include(a => a.Comments).ThenInclude(a => a.Likes)
                .Include(a => a.Comments).ThenInclude(a => a.ReplyToComments).ThenInclude(a => a.Likes)
                .Include(a => a.Comments).ThenInclude(a => a.ReplyToComments).ThenInclude(a => a.User)
                .SingleOrDefault();


            return post;
        }













    }
}
