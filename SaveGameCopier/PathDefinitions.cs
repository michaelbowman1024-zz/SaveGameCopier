using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SaveGameCopier
{
    public class PathDefinitions
    {
        public enum GameNames
        {
            Borderlands = 1,

            [Description("Borderlands 2")]
            Borderlands2,

            [Description("Rocket League")]
            RocketLeague
        }

        public Dictionary<GameNames, string> _definitions = new Dictionary<GameNames, string>()
        {
            { GameNames.Borderlands, "Borderlands\\SaveData" },
            { GameNames.Borderlands2, "Borderlands 2\\WillowGame\\SaveData" },
            { GameNames.RocketLeague, "Rocket League\\TAGame\\SaveData\\DBE_Production" }
        };
    }
}
