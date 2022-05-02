using Microsoft.AspNetCore.Mvc;
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
        private IGenericRepository<TEntity> repository;

        public GenericController()
        {
            repository = new GenericRepository<TEntity>(new object(), new TravelGuideContext());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return Ok(await repository.SelectAllAsync());
        }

        [HttpGet("GetByCondition/{query}")]
        public async Task<ActionResult<TEntity>> GetByCondition([FromRoute] string query)
        {
            try
            {
                var p = Expression.Parameter(typeof(TEntity), "i");

                var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, query);

                var typedExpression = (Expression<Func<TEntity, bool>>)e;

                return Ok(await repository.SelectAllAsync(typedExpression));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
