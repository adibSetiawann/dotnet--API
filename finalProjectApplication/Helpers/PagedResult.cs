using System.Collections.Generic;

namespace FinalProjectApplication
{
    public class PageResult<T>
    {
        public PageResult(IEnumerable<T> data, long total)
        {
            Data = data;
            Total = total;

        }

        public PageResult()
        {
            
        }

        public IEnumerable<T> Data { get; set; }
        public long Total { get; set; }

        public static implicit operator PageResult<T>(PageResult<CustomerListDto> v)
        {
            throw new NotImplementedException();
        }
    }
}