using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPRLVL
{
    public static class Objects
    {
        public static Dictionary<string, string[]> ObjectList = new Dictionary<string, string[]>()
        {
            { "StartPortal", new string[] { "string dir", "bool isUfoGround", "int portalNo", "string variation" } },
            { "Door", new string[] { "int appearFrame", "string decorationKind", "bool isHideFirst", "string metagoCustom", "int metagoPortalNo", "int metagoStepShift", "string modeAppearKind", "int portalNo", "int signalNo", "int stepShift" } },
            { "AbilitySeat", new string[] { "string abilityKind" } },
            { "PopFlower", new string[] {} },
            { "AirFlowFan", new string[] { "string attribute", "string dir", "string level", "string variation", "int waitFrame" } },
            { "WarpPipe", new string[] { "string colorKind", "bool doMoveStep", "string gridKindW", "bool hasCap", "bool isQuick", "string metagoCustom", "int metagoPortalNo", "int metagoStepShift", "int portalNo", "int signalNo", "int stepShift", "string subKind", "int targetPortalNo", "string variation" } },
            { "Object", new string[] {} },
            { "ArrowBoard", new string [] { "string arrowDir" } },
            { "ThreeDWarpStar", new string[] { "int appearFrame", "string appearKind", "string heroDirDecideKind", "bool isAppearHeroLane", "bool isInvisibleFirst", "bool isNeedSavePos", "string moveKind", "int portalNo", "string reallocationKind", "int reallocationNo", "int signalNo", "string type" } }
        };
        public static Dictionary<string, string[]> ItemList = new Dictionary<string, string[]>()
        {
            { "None", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "PointStarYellow", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "PointStarGreen", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "PointStarRed", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "PointStarBlue", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "OneUp", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "MTomato", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "ReviveTomato", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "InvincibleCandy", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "GenkiDrink", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "Food", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "Sticker", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StickerRare", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "MetaPointBall", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "MetaPointBallL", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "IceCube", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "IceCubeUsed", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StockItem", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "FoodByAssistChara", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StockItemGenkiDrink", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StockItemReviveTomato", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StockItemFruitCherry", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StockItemSweetsSoftCream", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} },
            { "StockItemSweetsPudding", new string[] { "string subKind", "string itemAppearKind", "string itemCategory", "string variation"} }
        };
        public static Dictionary<string, string[]> BossList = new Dictionary<string, string[]>()
        {
            { "Boss", new string[] { } }
        };
        public static Dictionary<string, string[]> MBossList = new Dictionary<string, string[]>()
        {
            { "MBoss", new string[] { } }
        };
        public static Dictionary<string, string[]> EnemyList = new Dictionary<string, string[]>()
        {
            { "Enemy", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Helmetwaddledee", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Mechbrontoburt", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Sparky", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Sirkibble", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "ArmorWaddledee", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "RocketlauncherWaddledee", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Galbo", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Broomhatter", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Bladeknight", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } },
            { "Waddledoo", new string[] { "string dirType", "string enemyCategory", "int group", "string level", "string size", "string variation" } }
        };
    }
}
