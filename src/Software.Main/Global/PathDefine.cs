using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Software.Main
{
    public static class PathDefine
    {
        #region 文件夹创建

        public static void CreatDirectory()
        {
            Type type = typeof(PathDefine);
            type.GetProperties()
                .Where(p => Regex.IsMatch(p.Name, @"DirectoryPath$") && p.PropertyType == typeof(string))
                .ToList().ForEach(p => Directory.CreateDirectory((string)p.GetValue(type, null)));
        }

        #endregion

        #region 配方文件夹

        public static string RecipeDirectoryPath { get; } = AppDomain.CurrentDomain.BaseDirectory + "Recipe\\";

        public static string CurrentRecipeDirectoryPath
        {
            get { return RecipeDirectoryPath + $"{GlobalParam.Instance.CurrentProductType}\\"; }
        }

        public static string CurrentRecipeFilePath
        {
            get { return CurrentRecipeDirectoryPath + "Recipe.xml"; }
        }

        #endregion

        #region 缓存文件夹

        public static string CacheDirectoryPath { get; } = AppDomain.CurrentDomain.BaseDirectory + @"Cache\";

        public static string WholeStationCacheFilePath { get; } = CacheDirectoryPath + @"WholeStationRunningCache";

        #endregion

        #region 托盘文件夹

        public static string TrayDirectoryPath { get; } = AppDomain.CurrentDomain.BaseDirectory + @"Tray\";

        #endregion

        #region 数据库文件夹

        public static string DataBaseDirectoryPath { get; } = AppDomain.CurrentDomain.BaseDirectory + @"DataBase\";

        public static string DataBaseFilePath
        {
            get { return DataBaseDirectoryPath + $"{DateTime.Now.ToString("yyyyMM")}.db3"; }
        }

        #endregion

        #region 参数文件夹

        public static string ParamDirectoryPath { get; } = AppDomain.CurrentDomain.BaseDirectory + @"Param\";

        public static string GlobalParamFilePath { get; } = ParamDirectoryPath + "GlobalParam.xml";

        public static string StatisticsFilePath { get; } = ParamDirectoryPath + "Statistics.xml";

        #endregion

        #region 数据导出文件夹

        public static string ExportDataDirectoryPath { get; } = AppDomain.CurrentDomain.BaseDirectory + @"ExportData\";

        public static string ExportCSVFilePath
        {
            get { return ExportDataDirectoryPath + $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.csv"; }
        }

        #endregion
    }
}
