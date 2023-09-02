using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPRLVL;

namespace HaltmannLab
{
    public partial class AddObj : Form
    {
        public int editorType;
        public Dictionary<string, string> obj = new Dictionary<string, string>()
        {
            { "int wuid", "0" },
            { "int x", "0 | 0" },
            { "int y", "0 | 0" },
            { "string kind", "" }
        };

        public Dictionary<string, string> enemyObj = new Dictionary<string, string>()
        {
            { "int wuid", "0" },
            { "int xPos", "0 | 0" },
            { "int yPos", "0 | 0" },
            { "string dirType", "Normal" },
            { "string enemyCategory", ""},
            { "int group", "0" },
            { "string kind", "Enemy" },
            { "string level", "Lv1" },
            { "string size", "Normal" },
            { "string variation", "Wait" }
        };

        public AddObj()
        {
            InitializeComponent();
        }

        private void AddObj_Load(object sender, EventArgs e)
        {
            this.checkBox1.Hide();

            List<string> list = new List<string>();
            if (editorType == 0)
            {
                list = Objects.ObjectList.Keys.ToList();
                list.Sort();
                objectDropDown.Items.AddRange(list.ToArray());
            }
            else if (editorType == 1 || editorType == 2)
            {
                list = Objects.ItemList.Keys.ToList();
                list.Sort();
                objectDropDown.Items.AddRange(list.ToArray());
            }
            else if (editorType == 3)
            {
                list = Objects.BossList.Keys.ToList();
                list.Sort();
                objectDropDown.Items.AddRange(list.ToArray());
            }
            else if (editorType == 4)
            {
                objectDropDown.Items.AddRange(new string[] { "Enemy", "MBoss" });
                enemyDropDown.Visible = true;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if(objectDropDown.Text == "")
            {
                MessageBox.Show("Not a valid object type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(enemyDropDown.Text == "" && editorType == 4)
            {
                MessageBox.Show("Not a valid enemy kind!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(editorType != 4)
            {
                obj["string kind"] = objectDropDown.Text;
            }
            else
            {
                obj["string kind"] = enemyDropDown.Text;
            }

            if (editorType == 0)
            {
                if (Objects.ObjectList.ContainsKey(objectDropDown.Text))
                {
                    for (int i = 0; i < Objects.ObjectList[objectDropDown.Text].Length; i++)
                    {
                        string value = "";
                        string valType = Objects.ObjectList[objectDropDown.Text][i].Split(' ')[0];
                        switch (valType)
                        {
                            case "int":
                            case "float":
                                {
                                    value = "0";
                                    if (Objects.ObjectList[objectDropDown.Text][i] == "int x" || Objects.ObjectList[objectDropDown.Text][i] == "int y")
                                    {
                                        value = "0 | 0";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "int metagoStepShift")
                                    {
                                        value = "1";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "int stepShift")
                                    {
                                        value = "1";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "int waitFrame")
                                    {
                                        value = "600";
                                    }
                                    break;
                                }
                            case "bool":
                                {
                                    value = "False";
                                    if (Objects.ObjectList[objectDropDown.Text][i] == "bool doMoveStep")
                                    {
                                        value = "True";
                                    }
                                    break;
                                }
                            case "string":
                                {
                                    if (Objects.ObjectList[objectDropDown.Text][i] == "string constraintMoveGroup")
                                    {
                                        value = "-1";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string kind")
                                    {
                                        value = objectDropDown.Text;
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string dir")
                                    {
                                        switch(objectDropDown.Text)
                                        {
                                            case "StartPortal":
                                                value = "R";
                                                break;

                                            case "AirFlowFan":
                                                value = "Left";
                                                break;

                                            default:
                                                value = "Normal";
                                                break;
                                        }
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string variation")
                                    {
                                        switch(objectDropDown.Text)
                                        {
                                            case "AirFlowFan":
                                                value = "DashTutorial";
                                                break;

                                            case "WarpPipe":
                                                value = "HalfLeft";
                                                break;

                                            default:
                                                value = "Normal";
                                                break;
                                        }
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string decorationKind" && objectDropDown.Text == "Door")
                                    {
                                        value = "OneWayBlack";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string metagoCustom")
                                    {
                                        value = "Invalid";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string modeAppearKind")
                                    {
                                        value = "All";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string attribute")
                                    {
                                        value = "Air";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string level")
                                    {
                                        value = "Middle";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string colorKind")
                                    {
                                        value = "OneWay";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string gridKindW")
                                    {
                                        value = "Grid1";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string subKind")
                                    {
                                        value = "Normal";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string arrowDir")
                                    {
                                        value = "R";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string appearKind")
                                    {
                                        value = "Normal";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string heroDirDecideKind")
                                    {
                                        value = "Keep";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string moveKind")
                                    {
                                        value = "Small";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string reallocationKind")
                                    {
                                        value = "Once";
                                    }
                                    else if (Objects.ObjectList[objectDropDown.Text][i] == "string type")
                                    {
                                        value = "Start";
                                    }
                                    break;
                                }
                        }
                        obj.Add(Objects.ObjectList[objectDropDown.Text][i], value);
                    }
                }
            }
            if (editorType == 1 || editorType == 2)
            {
                if (Objects.ItemList.ContainsKey(objectDropDown.Text))
                {
                    for (int i = 0; i < Objects.ItemList[objectDropDown.Text].Length; i++)
                    {
                        string value = "";
                        string valType = Objects.ItemList[objectDropDown.Text][i].Split(' ')[0];
                        switch (valType)
                        {
                            case "int":
                            case "float":
                                {
                                    value = "0";
                                    if (Objects.ItemList[objectDropDown.Text][i] == "int x" || Objects.ItemList[objectDropDown.Text][i] == "int y")
                                    {
                                        value = "0 | 0";
                                    }
                                    break;
                                }
                            case "bool":
                                {
                                    value = "False";
                                    break;
                                }
                            case "string":
                                {
                                    if (Objects.ItemList[objectDropDown.Text][i] == "string constraintMoveGroup")
                                    {
                                        value = "-1";
                                    }
                                    else if (Objects.ItemList[objectDropDown.Text][i] == "string itemCategory")
                                    {
                                        if (editorType == 1)
                                        {
                                            value = "HelperGoItem";
                                        }
                                    }
                                    else if (Objects.ItemList[objectDropDown.Text][i] == "string kind")
                                    {
                                        value = objectDropDown.Text;
                                    }
                                    else if (Objects.ItemList[objectDropDown.Text][i] == "string subKind")
                                    {
                                        value = "FruitWatermelon";
                                    }
                                    else if (Objects.ItemList[objectDropDown.Text][i] == "string variation")
                                    {
                                        value = "Fixed";
                                    }
                                    break;
                                }
                        }
                        obj.Add(Objects.ItemList[objectDropDown.Text][i], value);
                    }
                }
            }
            else if (editorType == 3)
            {
                if (Objects.BossList.ContainsKey(objectDropDown.Text))
                {
                    for (int i = 0; i < Objects.BossList[objectDropDown.Text].Length; i++)
                    {
                        string value = "";
                        string valType = Objects.BossList[objectDropDown.Text][i].Split(' ')[0];
                        switch (valType)
                        {
                            case "int":
                            case "float":
                                {
                                    value = "0";
                                    if (Objects.BossList[objectDropDown.Text][i] == "int x" || Objects.BossList[objectDropDown.Text][i] == "int y")
                                    {
                                        value = "0 | 0";
                                    }
                                    break;
                                }
                            case "bool":
                                {
                                    value = "False";
                                    break;
                                }
                            case "string":
                                {
                                    if (Objects.BossList[objectDropDown.Text][i] == "string constraintMoveGroup")
                                    {
                                        value = "-1";
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string dirType")
                                    {
                                        value = "Normal";
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string enemyCategory")
                                    {
                                        value = "Boss";
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string kind")
                                    {
                                        value = objectDropDown.Text;
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string level")
                                    {
                                        value = "Lvl1";
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string modelKind")
                                    {
                                        value = "Normal";
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string size")
                                    {
                                        value = "Normal";
                                    }
                                    else if (Objects.BossList[objectDropDown.Text][i] == "string variation")
                                    {
                                        value = "Normal";
                                    }
                                    break;
                                }
                        }
                        obj.Add(Objects.BossList[objectDropDown.Text][i], value);
                    }
                }
            }
            else if (editorType == 4)
            {
                if (objectDropDown.Text == "Enemy")
                {
                    if (Objects.EnemyList.ContainsKey(enemyDropDown.Text))
                    {
                        for (int i = 0; i < Objects.EnemyList[enemyDropDown.Text].Length; i++)
                        {
                            string value = "";
                            string valType = Objects.EnemyList[enemyDropDown.Text][i].Split(' ')[0];
                            switch (valType)
                            {
                                case "int":
                                case "float":
                                    {
                                        value = "0";
                                        if (Objects.EnemyList[enemyDropDown.Text][i] == "int x" || Objects.EnemyList[enemyDropDown.Text][i] == "int y")
                                        {
                                            value = "0 | 0";
                                        }
                                        break;
                                    }
                                case "bool":
                                    {
                                        value = "False";
                                        break;
                                    }
                                case "string":
                                    {
                                        if (Objects.EnemyList[enemyDropDown.Text][i] == "string constraintMoveGroup")
                                        {
                                            value = "-1";
                                        }
                                        else if (Objects.EnemyList[enemyDropDown.Text][i] == "string dirType")
                                        {
                                            value = "Normal";
                                        }
                                        else if (Objects.EnemyList[enemyDropDown.Text][i] == "string enemyCategory")
                                        {
                                            value = "Enemy";
                                        }
                                        else if (Objects.EnemyList[enemyDropDown.Text][i] == "string kind") // This is useless but too lazy to remove it lol
                                        {
                                            value = enemyDropDown.Text;
                                            System.Diagnostics.Debug.WriteLine("hey");
                                        }
                                        else if (Objects.EnemyList[enemyDropDown.Text][i] == "string level")
                                        {
                                            value = "Lvl1";
                                        }
                                        else if (Objects.EnemyList[enemyDropDown.Text][i] == "string size")
                                        {
                                            value = "Normal";
                                        }
                                        else if (Objects.EnemyList[enemyDropDown.Text][i] == "string variation")
                                        {
                                            //Assign the most common behaviour based on enemy kind
                                            switch(enemyDropDown.Text)
                                            {
                                                default:
                                                    value = "Normal";
                                                    break;

                                                case "Sparky":
                                                    value = "Pursuit";
                                                    break;

                                                case "Mechbrontoburt":
                                                    value = "FlyStraight";
                                                    break;

                                                case "Sirkibble":
                                                    value = "Jump";
                                                    break;

                                                case "RocketlauncherWaddledee":
                                                    value = "WalkTurnCliff";
                                                    break;

                                                case "Galbo":
                                                    value = "AttackRotation";
                                                    break;

                                                case "Broomhatter":
                                                    value = "WalkAround";
                                                    break;

                                                case "Bladekinght":
                                                    value = "Search";
                                                    break;

                                                case "Waddledoo":
                                                    value = "WalkTurnCliff";
                                                    break;
                                            }
                                        }
                                        break;
                                    }

                                default:
                                    System.Diagnostics.Debug.WriteLine(enemyDropDown.Text + " no match found"); //Debug stuff
                                    break;
                            }
                            obj.Add(Objects.EnemyList[enemyDropDown.Text][i], value);
                        }
                    }
                }
                else if (objectDropDown.Text == "MBoss")
                {
                    if (Objects.MBossList.ContainsKey(objectDropDown.Text))
                    {
                        for (int i = 0; i < Objects.MBossList[enemyDropDown.Text].Length; i++)
                        {
                            string value = "";
                            string valType = Objects.MBossList[enemyDropDown.Text][i].Split(' ')[0];
                            switch (valType)
                            {
                                case "int":
                                case "float":
                                    {
                                        value = "0";
                                        if (Objects.MBossList[enemyDropDown.Text][i] == "int x" || Objects.MBossList[enemyDropDown.Text][i] == "int y")
                                        {
                                            value = "0 | 0";
                                        }
                                        break;
                                    }
                                case "bool":
                                    {
                                        value = "False";
                                        break;
                                    }
                                case "string":
                                    {
                                        if (Objects.MBossList[enemyDropDown.Text][i] == "string constraintMoveGroup")
                                        {
                                            value = "-1";
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string dirType")
                                        {
                                            value = "Normal";
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string enemyCategory")
                                        {
                                            value = "MBoss";
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string kind")
                                        {
                                            value = enemyDropDown.Text;
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string level")
                                        {
                                            value = "Lvl1";
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string mapDBKind")
                                        {
                                            value = enemyDropDown.Text;
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string modelKind")
                                        {
                                            value = "Normal";
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string size")
                                        {
                                            value = "Normal";
                                        }
                                        else if (Objects.MBossList[enemyDropDown.Text][i] == "string variation")
                                        {
                                            value = "Normal";
                                        }
                                        break;
                                    }
                            }
                            obj.Add(Objects.MBossList[enemyDropDown.Text][i], value);
                        }
                    }
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void objectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editorType == 4)
            {
                enemyDropDown.Items.Clear();
                if (objectDropDown.Text == "Enemy")
                {
                    List<string> list = Objects.EnemyList.Keys.ToList();
                    list.Sort();
                    enemyDropDown.Items.AddRange(list.ToArray());
                }
                else if (objectDropDown.Text == "MBoss")
                {
                    List<string> list = Objects.MBossList.Keys.ToList();
                    list.Sort();
                    enemyDropDown.Items.AddRange(list.ToArray());
                }
            }
        }
    }
}
