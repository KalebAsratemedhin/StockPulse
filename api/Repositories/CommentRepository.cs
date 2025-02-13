using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> Delete(int id)
        {
            var exisitingComment = await _context.Comment.FindAsync(id);

            if(exisitingComment == null)
            {
                return null;
            }

            _context.Comment.Remove(exisitingComment);
            await _context.SaveChangesAsync();
            return exisitingComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var exisitingComment = await _context.Comment.FindAsync(id);
             
            
            return exisitingComment;
            
        }

        
    }
}