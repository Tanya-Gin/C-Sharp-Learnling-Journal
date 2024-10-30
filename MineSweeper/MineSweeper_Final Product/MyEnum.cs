using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper1
{
    //背景状态
    public enum BackState
    {
        MINE=-1,//雷
        BLANK=0 //空格
    }
    public enum ForeState
    {
        NONE,//未点击
        NORMAL,//正常
        FLAG,//标记
    }
    //游戏状态
    public enum GameState
    {
        NONE,//未开始
        STOP,//游戏结束
        START,//游戏开始
        PAUSE //游戏暂停
    }
    public enum Level
    {
        SIMPLE,//简单
        NORMAL,//中等
        HARD   //困难
    }
    public class GameLevel
    {
        public int _row;
        public int _col;
        public int _mineNum;
        public GameLevel(int row, int col, int mineNum)
        {
            _row = row;
            _col = col;
            _mineNum = mineNum;
        }
    }
}

