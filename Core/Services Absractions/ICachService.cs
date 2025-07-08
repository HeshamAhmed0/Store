using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Absractions
{
    public  interface ICachService
    {
        public Task<string?> GetAsync(string key);
        public Task SetAsync(string key,object value,TimeSpan timeSpan);
    }
}
