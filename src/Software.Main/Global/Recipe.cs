using System;
using System.IO;

namespace Software.Main
{
    public class Recipe
    {
        #region 单例

        public static Recipe Instance { get; set; } = new Recipe();

        private Recipe() { }

        #endregion

    }
}
