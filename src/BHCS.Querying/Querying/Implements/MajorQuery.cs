using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models;

namespace BHCS.Querying.Querying.Implements
{
    public class MajorQuery : IMajorQuery
    {
        public IList<MajorReadModel> GetMajorList(bool isLoadDeletedData = false)
        {
            if (!isLoadDeletedData) return QueryEnvironment.Current.GetFromSection<Major>().Where(p => p.IsDelete == false).OrderByDescending(p=>p.CreateTime).ToList<MajorReadModel>();
            return QueryEnvironment.Current.GetFromSection<Major>().OrderByDescending(p => p.CreateTime).ToList<MajorReadModel>();
        }
    }
}
