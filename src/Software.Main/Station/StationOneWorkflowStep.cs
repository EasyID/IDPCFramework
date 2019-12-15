using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software.Main
{
    enum StationOneResetStep : int
    {
        等待PLC信号ON = 0,
        复位读码工作流,
        向PLC写入结果,
        等待PLC信号OFF
    }

    enum StationOneCodeScanStep : int
    {
        等待PLC信号ON = 0,
        初始化产品信息,
        读取条码,
        向PLC写入结果,
        等待PLC信号OFF,
        异常处理
    }

}
