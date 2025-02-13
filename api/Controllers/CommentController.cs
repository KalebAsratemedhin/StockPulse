using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {

        private readonly ICommentRepository  _commentRepo;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commentsModel = await _commentRepo.GetAllAsync();

            var commentsDto = commentsModel.Select(comment => comment.ToCommentDto());

            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var result = await _commentRepo.GetByIdAsync(id);


            if(result == null)
            {
                return NotFound();
            }
            return Ok(result.ToCommentDto());
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromRoute] int id, CreateCommentDto commentDto)
        {
            var commentModel = commentDto.ToCommentModel(id);

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetOne), new {id = commentModel.Id}, commentModel.ToCommentDto());

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var exisitingComment = await _commentRepo.Delete(id);

            if(exisitingComment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}