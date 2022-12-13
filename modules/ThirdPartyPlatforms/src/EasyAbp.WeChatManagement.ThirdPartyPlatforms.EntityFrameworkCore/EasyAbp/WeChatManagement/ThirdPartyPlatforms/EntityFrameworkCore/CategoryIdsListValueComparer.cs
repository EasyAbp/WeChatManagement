using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

public class CategoryIdsListValueComparer : ValueComparer<List<int>>
{
    public CategoryIdsListValueComparer()
        : base(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
            d => d.ToList())
    {
    }
}