using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class RandomValueGenerator
    {
        public static string GenerateName(string extension)
        {
            return Guid.NewGuid().ToString().Replace("-", "") + extension;
        }
    }
}
