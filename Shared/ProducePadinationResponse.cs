using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProducePadinationResponse<TEntity>
    {
        public ProducePadinationResponse(int pageIndex, int pageSize, int cout, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Cout = cout;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Cout {  get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
