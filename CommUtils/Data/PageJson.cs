using CommUtils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommUtils.Data
{
    [Json(false)]
    public class PageJson<T> : DataJson
    {
        public new PageData<T> Data { get; set; }
    }

}
