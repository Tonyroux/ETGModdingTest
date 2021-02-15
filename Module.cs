using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Alternator
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "AlternatorWeapon";
        public static readonly string VERSION = "0.0.1";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            Alternator.Add();
            Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);
        }

        public static void Log(string text, string color="FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
