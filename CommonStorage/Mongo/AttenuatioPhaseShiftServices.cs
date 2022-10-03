using ApiStorage.Models;
using ApiStorage.Models.Mongo;
using ApiStorage.Tools.Context;

using CommonStorage.Mongo;

using DnsClient.Internal;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStorage.Mongo
{
    public class AttenuatioPhaseShiftServices : MongoBase<AttenuatioPhaseShiftEntity>
    {
        // TODO в дальнейшем использовать разные документы для сохранения
        public AttenuatioPhaseShiftServices(IConfiguration configuration) : 
            base(configuration, nameof(AttenuatioPhaseShiftEntity))
        {        }


        //public IMongoQueryable<AttenuatioPhaseShiftEntity> GetQuery1()
        //{
        //    //_collection.a
        //}

        public async Task<List<AttenuatioPhaseShiftEntity>> FindGroupAsync(Guid groupId) =>
            await _collection.AsQueryable().Where(x => x.TestGroupId == groupId).ToListAsync();

        private IFindFluent<AttenuatioPhaseShiftEntity, AttenuatioPhaseShiftEntity> GetSettingList(
            int? pageIndex, int? pageSize,
            FilterDefinition<AttenuatioPhaseShiftEntity> filterDef = null,
            SortDefinition<AttenuatioPhaseShiftEntity> sortDef = null)
        {
            var findResult = _collection
                .Find(filterDef ?? Builders<AttenuatioPhaseShiftEntity>.Filter.Empty, null)
                .Sort(sortDef);

            if (pageIndex != null && pageSize != null)
            {
                findResult = findResult
                    .Skip((int)((pageIndex - 1) * pageSize))
                    .Limit((int?)pageSize);
            }

            return findResult;
        }

        public List<AttenuatioPhaseShiftEntity> GetSettingList() 
        {
            return _collection.AsQueryable()
                .Select(p => new AttenuatioPhaseShiftEntity()
                {
                    Id = p.Id,
                    TestGroupId = p.TestGroupId,
                    CreateDate = p.CreateDate,
                    UpdateDate = p.UpdateDate
                })
                .ToList();
        }

        public ResultParameter GetResult(Guid id)
        {
            return _collection.AsQueryable()
                .Where(p => p.Id == id)
                .Select(p => new ResultParameter()
                {
                    Parameters = p.Result.Parameters
                })
                .FirstOrDefault();
        }
    }
}
