using System;
using Microsoft.Extensions.Logging;

namespace WebApp.Helpers
{
    public static class StringExt
    {
        public static int ToIntId(this string id, ILogger logger)
        {
            if (int.TryParse(id, out var value))
            {
                return value;
            }

            logger.LogInformation("failed to parse blog id");

            throw new NullReferenceException("id");
        }
    }
}
