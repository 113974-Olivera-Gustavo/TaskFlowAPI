namespace Taskflow.Application.Services
{
    public abstract class BaseService
    {
        protected readonly PortalContext _portalContext;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IMemoryCache _cache;
        private static ConcurrentDictionary<string, bool> _cacheKeys = new();

        protected BaseService(IMemoryCache cache)
        {
            _cache = cache;
        }

        protected BaseService(
            PortalContext portalContext,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache)
        {
            _portalContext = portalContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        private string GetUserCache()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            return user?.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        }

        protected async Task<OperationResponse<List<TDto>>> FindByConditionAsyncLongCache<TEntity, TDto>(IQueryable<TEntity> query, string cacheKey) where TEntity : class
        {        

            if (GetCache(cacheKey) is List<TDto> cachedData)
            {
                return Ok(cachedData);
            }
            else
            {
                var entityList = await query.AsNoTracking().ToListAsync();

                var entityDtoList = _mapper.Map<List<TDto>>(entityList);

                SetLongCache(cacheKey, entityDtoList);

                return Ok(entityDtoList);
            }
        }

        protected static OperationResponse<T> BadRequest<T>(string message, T? data = default) => OperationResponse<T>.CustomErrorResponse(400, message, data);

        protected static OperationResponse<T> Ok<T>(T data, int total = default) => OperationResponse<T>.SuccessResponse(data, total);

        protected static OperationResponse<T> OkMasive<T>(T data, int total = default) => OperationResponse<T>.SuccessResponseMassive(data, total);

        protected static OperationResponse<T> NotFound<T>() => OperationResponse<T>.NotFoundResponse();

        protected static OperationResponse<T> ServerErrorFile<T>(string exception, object exceptionDetails) => OperationResponse<T>.ErrorFileResponse(exception, exceptionDetails);

        protected static OperationResponse<T> InternalServerError<T>(string exception) => OperationResponse<T>.ErrorResponse(exception);

        protected object GetCache(string key)
        {
            var data = _cache.Get(key);
            return data;
        }

        protected void SetCache<T>(string key, T data) => _cache?.Set(key, data, DateTime.Now.AddMinutes(2));

        private void SetLongCache<T>(string key, T data)
        {
            _cache?.Set(key, data, DateTime.Now.AddDays(4));
            _cacheKeys.TryAdd(key, true);
        }

        protected static IEnumerable<string> GetAllKeysCache() => _cacheKeys.Keys;

        protected bool RemoveAllKeysCache()
        {
            foreach (var key in _cacheKeys)
            {
                RemoveKeyCache(key.Key);
            }

            _cacheKeys.Clear();
            return _cacheKeys.IsEmpty;
        }

        protected bool RemoveKeyCache(string key)
        {
            var data = GetCache(key);
            if (data == null) return false;

            _cache?.Remove(key);
            _cacheKeys.TryRemove(key, out _);
            return true;
        }

        protected async Task<OperationResponse<List<TDto>>> GetPagedDataAsync<TEntity, TDto>(int page, int pageSize, IQueryable<TEntity> query)
        {
            var total = await query.CountAsync();

            if (total == 0)
            {
                return NotFound<List<TDto>>();
            }

            var entitiesFilter = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var dtos = _mapper.Map<List<TDto>>(entitiesFilter);

            return Ok(dtos, total);
        }

        protected async Task<OperationResponse<TDto>> InsertAsync<TEntity, TDto>(TDto dto, DbContext context) where TEntity : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            if (entity is IEntityAuditable auditableEntity)
            {
                auditableEntity.Id = Sequence.GetSequenceForType<TEntity>();
                auditableEntity.UserIng = GetUserCache();
                auditableEntity.FecIng = DateTime.Now;
            }

            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();

            return Ok(_mapper.Map<TDto>(entity));
        }

        protected async Task<OperationResponse<List<TDto>>> InsertEntitesAsync<TEntity, TDto>(List<TDto> dtos, DbContext context) where TEntity : class
        {
            var entities = _mapper.Map<List<TEntity>>(dtos);

            foreach (var entity in entities)
            {
                if (entity is IEntityAuditable auditableEntity)
                {
                    auditableEntity.Id = Sequence.GetSequenceForType<TEntity>();
                    auditableEntity.UserIng = GetUserCache();
                    auditableEntity.FecIng = DateTime.Now;
                }

                await context.Set<TEntity>().AddAsync(entity);
            }

            await context.SaveChangesAsync();

            return Ok(_mapper.Map<List<TDto>>(entities));
        }

        protected async Task<OperationResponse<List<TDto>>> UpdateEntitiesAsync<TEntity, TDto>(List<TEntity> entities, DbContext context) where TEntity : class
        {
            foreach (var entity in entities)
            {
                if (entity is IEntityAuditable auditableEntity)
                {
                    auditableEntity.UserMod = GetUserCache();
                    auditableEntity.FecMod = DateTime.Now;
                }

                context.Set<TEntity>().Update(entity);
            }

            await context.SaveChangesAsync();

            return Ok(_mapper.Map<List<TDto>>(entities));
        }

        protected async Task<OperationResponse<TDto>> UpdateAsync<TEntity, TDto>(TEntity entity, DbContext context) where TEntity : class
        {

            if (entity is IEntityAuditable auditableEntity)
            {
                auditableEntity.UserMod = GetUserCache();
                auditableEntity.FecMod = DateTime.Now;
            }

            context.Set<TEntity>().Update(entity);

            await context.SaveChangesAsync();

            return Ok(_mapper.Map<TDto>(entity));
        }

        protected static async Task<OperationResponse<bool>> InsertWithTransactionAsync(Func<Task> insertActionsAsync, DbContext context)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await insertActionsAsync();

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return OperationResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        protected async Task<OperationResponse<List<TDto>>> DeleteEntitiesAsync<TEntity, TDto>(List<TEntity> entities, DbContext context) where TEntity : class
        {
            foreach (var entity in entities)
            {
                if (entity is IEntityAuditable auditableEntity)
                {
                    auditableEntity.UserBaja = GetUserCache();
                    auditableEntity.FecBaja = DateTime.Now;
                }

                context.Set<TEntity>().Update(entity);
            }

            await context.SaveChangesAsync();

            return Ok(_mapper.Map<List<TDto>>(entities));
        }

        protected async Task<OperationResponse<bool>> DeleteAsync<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            if (entity is IEntityAuditable auditableEntity)
            {
                auditableEntity.UserBaja = GetUserCache();
                auditableEntity.FecBaja = DateTime.Now;
            }

            context.Set<TEntity>().Update(entity);

            await context.SaveChangesAsync();

            return Ok(true);
        }

    }
}
