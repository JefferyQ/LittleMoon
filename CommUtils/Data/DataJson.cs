using CommUtils.Attributes;

namespace CommUtils.Data
{
    [Json(false)]
    public class DataJson
    {
        public Status Status { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }

        public object Datas { get; set; }

        public ErrCode ErrCode { get; set; }
    }

}
