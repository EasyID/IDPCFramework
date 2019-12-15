using System;
using System.Collections.Generic;
using System.Drawing;
using Software.Toolkit;
namespace Software.Main
{
    /// <summary>
    /// 托盘类
    /// </summary>
    public class Tray
    {

        #region field

        /// <summary>
        /// 盘中无效位置列表
        /// </summary>
        private List<Index> lstEmpty = new List<Index>();

        /// <summary>
        /// 更新点位时的委托
        /// </summary>
        public Action UpdateColor;

        /// <summary>
        /// 有效排序位置
        /// </summary>
        public Dictionary<int, Index> dic_Index = new Dictionary<int, Index>();

        #endregion

        #region property

        /// <summary>
        /// 托盘名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount { get; set; }

        /// <summary>
        /// 起始位置
        /// </summary>
        public EStartPosition StartPosition { get; set; }

        /// <summary>
        /// 排列方向
        /// </summary>
        public EDirection Direction { get; set; }

        /// <summary>
        /// 换行方式
        /// </summary>
        public ESwitchMethod SwitchMethod { get; set; }

        /// <summary>
        /// 屏蔽点位
        /// </summary>
        public string Empty
        {
            get
            {
                if (lstEmpty.Count <= 0) return "";
                else
                {
                    string strReturn = "";
                    for (int i = 0; i < lstEmpty.Count; i++)
                    {
                        strReturn += lstEmpty[i].ToString() + ",";
                    }
                    return strReturn.Trim(',');
                }

            }
        }

        /// <summary>
        /// 当前位置
        /// </summary>
        public int CurrentPos { get; set; }

        /// <summary>
        /// 起始位置
        /// </summary>
        public int StartPos { get; set; }

        /// <summary>
        /// 终点位置
        /// </summary>
        public int EndPos { get; set; }

        #endregion

        #region constructor

        public Tray() { Name = "Default"; }

        #endregion

        /// <summary>
        /// 加载托盘参数
        /// </summary>
        public void Load(string directoryPath)
        {
            string filePath = directoryPath + $"{Name}.ini";
            RowCount = INITool.ReadIniData("Data", "RowCount", 16, filePath, 20);
            ColumnCount = INITool.ReadIniData("Data", "ColumnCount", 16, filePath, 6);
            StartPosition = (EStartPosition)Enum.Parse(typeof(EStartPosition), INITool.ReadIniData("Data", "StartPosition", 16, filePath, "左上角"));
            Direction = (EDirection)Enum.Parse(typeof(EDirection), INITool.ReadIniData("Data", "Direction", 16, filePath, "行"));
            SwitchMethod = (ESwitchMethod)Enum.Parse(typeof(ESwitchMethod), INITool.ReadIniData("Data", "SwitchMethod", 16, filePath, "Z"));
            string strEmpty = INITool.ReadIniData("Data", "Empty", 1024, filePath, "");

            lstEmpty.Clear();
            List<string> lst = new List<string>();
            lst.AddRange(strEmpty.Split(','));
            lst.Remove("");
            foreach (string value in lst)
            {
                Index pos = new Index(value);
                lstEmpty.Add(pos);
            }

            SortTray(StartPosition, Direction, SwitchMethod);
        }

        /// <summary>
        /// 保存托盘参数
        /// </summary>
        /// <param name="directoryPath"></param>
        public void Save(string directoryPath)
        {
            string filePath = directoryPath + $"{Name}.ini";
            INITool.WriteIniData("Data", "RowCount", RowCount.ToString(), filePath);
            INITool.WriteIniData("Data", "ColumnCount", ColumnCount.ToString(), filePath);
            INITool.WriteIniData("Data", "StartPosition", StartPosition.ToString(), filePath);
            INITool.WriteIniData("Data", "Direction", Direction.ToString(), filePath);
            INITool.WriteIniData("Data", "SwitchMethod", SwitchMethod.ToString(), filePath);
            INITool.WriteIniData("Data", "Empty", Empty, filePath);
        }

        /// <summary>
        /// 找出盘中有效点，按起始位置和方向排列点位
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="direct">方向</param>
        /// <param name="lineType">换行方式</param>
        public void SortTray(EStartPosition start, EDirection direct, ESwitchMethod lineType)
        {
            dic_Index.Clear();
            int i = 1;
            switch (start)
            {
                #region"左上角"
                case EStartPosition.左上角:
                    if (direct == EDirection.行)
                    {
                        for (int r = 0; r < RowCount; r++)
                        {
                            //Z型换行
                            if (r % 2 == 1 && lineType == ESwitchMethod.S)
                            {
                                for (int c = ColumnCount - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = 0; c < ColumnCount; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int c = 0; c < ColumnCount; c++)
                        {
                            if (c % 2 == 1 && lineType == ESwitchMethod.S)
                            {
                                for (int r = RowCount - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                            else
                            {
                                for (int r = 0; r < RowCount; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"左下角"
                case EStartPosition.左下角:
                    if (direct == EDirection.行)
                    {
                        for (int r = RowCount - 1; r >= 0; r--)
                        {
                            if (((RowCount % 2 == 1 && r % 2 == 1) || (RowCount % 2 == 0 && r % 2 == 0))
                                 && lineType == ESwitchMethod.S)
                            {
                                for (int c = ColumnCount - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                            else
                            {
                                for (int c = 0; c < ColumnCount; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        for (int c = 0; c < ColumnCount; c++)
                        {
                            if (c % 2 == 1 && lineType == ESwitchMethod.S)
                            {
                                for (int r = 0; r < RowCount; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = RowCount - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"右上角"
                case EStartPosition.右上角:
                    if (direct == EDirection.行)
                    {
                        for (int r = 0; r < RowCount; r++)
                        {
                            if (r % 2 == 1 && lineType == ESwitchMethod.S)
                            {
                                for (int c = 0; c < ColumnCount; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = ColumnCount - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int c = ColumnCount - 1; c >= 0; c--)
                        {
                            if (((ColumnCount % 2 == 0 && c % 2 == 0) || (ColumnCount % 2 == 1 && c % 2 == 1))
                                && lineType == ESwitchMethod.S)
                            {
                                for (int r = RowCount - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = 0; r < RowCount; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"右下角"
                case EStartPosition.右下角:
                    if (direct == EDirection.行)
                    {
                        for (int r = RowCount - 1; r >= 0; r--)
                        {
                            if (((RowCount % 2 == 1 && r % 2 == 1) || (RowCount % 2 == 0 && r % 2 == 0))
                                && lineType == ESwitchMethod.S)
                            {
                                for (int c = 0; c < ColumnCount; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = ColumnCount - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int c = ColumnCount - 1; c >= 0; c--)
                        {
                            if (((ColumnCount % 2 == 0 && c % 2 == 0) || (ColumnCount % 2 == 1 && c % 2 == 1))
                                && lineType == ESwitchMethod.S)
                            {
                                for (int r = 0; r < RowCount; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = RowCount - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                    #endregion
            }
            StartPos = 1;
            EndPos = dic_Index.Count;
        }

        /// <summary>
        /// 从指定的索引位置开始查找有效穴号，并返回该穴号位置
        /// </summary>
        /// <param name="_pos">开始查找的位置</param>
        /// <returns>返回有效穴号位置,如果返回-1则代表没有找到</returns>
        public int FindPos(Index _pos)
        {
            int result = -1;
            foreach (int i in dic_Index.Keys)
            {
                if (dic_Index[i].Equals(_pos))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 往盘中添加屏蔽位置
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        public void AddEmptyPos(int row, int col)
        {
            Index pos = new Index(row, col);
            if (!IsExistEmpty(pos))
            {
                lstEmpty.Add(pos);
            }
        }

        /// <summary>
        /// 往盘中添加屏蔽位置
        /// </summary>
        /// <param name="r_c">以字符口串"R_C"的形式赋值</param>        
        public void AddEmptyPos(string r_c)
        {
            Index pos = new Index(r_c);
            if (!IsExistEmpty(pos))
            {
                lstEmpty.Add(pos);
            }
        }

        /// <summary>
        /// 移除屏蔽位置
        /// </summary>
        /// <param name="r_c">以字符口串"R_C"的形式赋值</param>
        public void RemoveEmptyPos(string r_c)
        {
            Index pos = new Index(r_c);
            if (IsExistEmpty(pos))
            {
                lstEmpty.Remove(pos);
            }
        }

        /// <summary>
        /// 判断屏蔽位置是否存在
        /// </summary>
        public bool IsExistEmpty(Index _pos)
        {
            if (lstEmpty.Contains(_pos))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取所有屏蔽点的字符串形式
        /// </summary>
        /// <returns>以"r_c,r_c,...,r_c"形式返回字符串</returns>
        public string GetStringEmpty()
        {
            string strReturn = "";
            int iLen = lstEmpty.Count;
            for (int i = 0; i < iLen; i++)
            {
                strReturn += lstEmpty[i].ToString() + ",";
            }
            return strReturn.Trim(',');
        }

        /// <summary>
        /// 设置指定序号控件的背景颜色
        /// </summary>
        /// <param name="num">穴号</param>
        /// <param name="bColor">颜色</param>
        public void SetNumColor(int num, Color bColor)
        {
            Index index = dic_Index[num];
            index.color = bColor;
            dic_Index[num] = index;

            UpdateColor?.Invoke();
        }

        /// <summary>
        /// 托盘颜色复位
        /// </summary>
        public void ResetTrayColor(Color bColor)
        {
            foreach (var key in dic_Index.Keys)
            {
                var index = dic_Index[key];
                index.color = bColor;
                dic_Index[key] = index;
            }

            UpdateColor?.Invoke();
        }

        /// <summary>
        /// 设置托盘起始结束位
        /// </summary>
        /// <param name="_startPos">起始位置</param>
        /// <param name="_endPos">结束位置</param>
        /// <param name="fillColor">起始位到结束位的显示颜色</param>
        /// <param name="fillColor2">无效位置的显示颜色</param>
        public void SetStartEndPos(int _startPos, int _endPos, Color fillColor, Color fillColor2)
        {
            if (_startPos > _endPos)
                return;
            CurrentPos = _startPos;
            StartPos = _startPos;
            EndPos = _endPos;
            for (int i = 1; i < _startPos; i++)
            {
                SetNumColor(i, fillColor2);
            }
            int count = dic_Index.Count;
            for (int i = _endPos; i < count + 1; i++)
            {
                SetNumColor(i, fillColor2);
            }
            for (int i = _startPos; i <= _endPos; i++)
            {
                SetNumColor(i, fillColor);
            }
        }
    }

    public enum EStartPosition
    {
        左上角,
        左下角,
        右上角,
        右下角
    }

    public enum EDirection
    {
        行,
        列
    }

    public enum ESwitchMethod
    {
        Z,
        S
    }
}
