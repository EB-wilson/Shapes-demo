using Shapes.Components;
using Shapes.Logic;

namespace Shapes.Utils
{
    /// <summary>
    /// 此处保存了游戏全局使用的各个变量，它们可能来自全局上下文也可能来自设置项
    /// </summary>
    public static class GlobalVars
    {
        //settings
        public static float cameraHeightOffset = 0;

        public static bool switchStatToggle = false;
        public static bool isOrthographicView = false;

        //global context
        public static World world;
        public static PlayerStatus player;
    }
}