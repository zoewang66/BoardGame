using System;
using System.Collections.Generic;

namespace GameDesign
{
    public class Piece
    {
        public string symbol { get; set; } = " ";  // 默认初始化为空格，避免 null 警告

        public List<string> symbolsList { get; set; } = new List<string>();

        public Piece() { }
    }
}