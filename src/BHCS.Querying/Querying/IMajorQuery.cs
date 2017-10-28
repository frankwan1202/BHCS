using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IMajorQuery
    {
        IList<MajorReadModel> GetMajorList(bool isLoadDeletedData=false);
    }
}
