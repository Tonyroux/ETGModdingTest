using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Weapons;

namespace Mod
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "A Hero's Journey";
        public static readonly string VERSION = "0.0.3";
        public static readonly string TEXT_COLOR = "#e3de46";

        public override void Start()
        {
            ItemBuilder.Init();
            Weapons.Alternator.Add();
            Weapons.Winan.Add();
            Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);
            Log($"To spawn Hero's journey items, use prefix 'nt'", TEXT_COLOR);
        }

        public static void Log(string text, string color="FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");

        }

        public override void Exit() { }
        public override void Init() { }
    }
}
