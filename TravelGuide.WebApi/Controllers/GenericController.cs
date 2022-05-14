using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelGuide.Database;
using TravelGuide.Database.Interfaces;
using TravelGuide.Database.Repositories;

namespace TravelGuide.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : class, IEntity, new()
    {
        protected IGenericRepository<TEntity> repository;

        public GenericController()
        {
            repository = new GenericRepository<TEntity>(new object(), new TravelGuideContext());
        }

        [HttpGet("GetOneByCondition/{where}")]
        public async Task<ActionResult<TEntity>> GetOneByCondition([FromRoute] string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var p = Expression.Parameter(typeof(TEntity), "i");

                    var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, query);

                    var typedExpression = (Expression<Func<TEntity, bool>>)e;

                    return Ok(await repository.SelectAsync(typedExpression));
                }
                return Ok(await repository.SelectAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetMultipleByCondition/{query}")]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetMultipleByCondition([FromRoute] string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var p = Expression.Parameter(typeof(TEntity), "i");

                    var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, query);

                    var typedExpression = (Expression<Func<TEntity, bool>>)e;

                    return Ok(await repository.SelectAllAsync(typedExpression));
                }
                return Ok(await repository.SelectAllAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] TEntity entity)
        {
            if (entity != null)
                return Ok(await repository.Update(entity));

            return BadRequest();
        }

        [HttpDelete("Delete/{entity}")]
        public async Task<ActionResult> Delete([FromRoute] string entity)
        {
            if (entity != null)
            {
                var deserializedEntity = JsonConvert.DeserializeObject<TEntity>(entity);

                return Ok(await repository.Delete(deserializedEntity));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteByCondition/{query}")]
        public async Task<ActionResult> DeleteByCondition([FromRoute] string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var p = Expression.Parameter(typeof(TEntity), "i");

                    var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, query);

                    var typedExpression = (Expression<Func<TEntity, bool>>)e;

                    await repository.DeleteByCondition(typedExpression);
                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }


}
