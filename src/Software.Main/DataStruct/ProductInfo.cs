using System;

namespace Software.Main
{
    [Serializable]
    public struct ProductInfo
    {
        public string sn;

        public bool result;

        public string operateDate;

        public string remark;

        public void Clear()
        {
            sn = remark = "";
            result = false;
        }
    }
}
