using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

public class CategoryIdsListToStringValueConverter : ValueConverter<List<int>, string>
{
    public static string Separator { get; set; } = ",";

    public CategoryIdsListToStringValueConverter() : base(
        v => v.JoinAsString(Separator),
        v => new List<int>(v.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x))))
    {
    }
}