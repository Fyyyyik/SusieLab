﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using HaltmannLab.Rendering;
using KPRLVL;

namespace HaltmannLab
{
    public partial class Form1 : Form
    {
        public Level level;
        public string filePath;

        List<int> texIds = new List<int>();
        List<int> modTexIds = new List<int>();
        List<int> objTexIds = new List<int>();
        Dictionary<short, int> blockTexIds = new Dictionary<short, int>();

        Renderer renderer;
        Texturing texturing;
        Camera camera;

        BmFont font;

        private System.Timers.Timer t;

        bool moveCam = false;
        int mouseX = 0;
        int mouseY = 0;

        int moveObj;

        int tool;

        uint tileX;
        uint tileY;

        public Form1()
        {
            InitializeComponent();
            this.Text = "SusieLab";
            this.draw.Location = this.select.Location;
            this.pick.Location = this.move.Location;
            this.move.Hide();
            this.select.Hide();
            this.resetCamera.Location = new Point(this.resetCamera.Location.X + 180, this.resetCamera.Location.Y);
            this.comboBox1.Location = new Point(this.comboBox1.Location.X + 180, this.comboBox1.Location.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            this.BackColor = SystemColors.ControlDarkDark;
            editBlock.Checked = true;
            editDeco.Checked = true;
            editCol.Checked = true;
        }

        public void RefreshObjectLists()
        {
            int selIndex = 0;

            if (tabControl1.SelectedTab == objTab) selIndex = objList.SelectedIndex;
            if (tabControl1.SelectedTab == itemTab) selIndex = itemList.SelectedIndex;
            if (tabControl1.SelectedTab == bossTab) selIndex = bossList.SelectedIndex;
            if (tabControl1.SelectedTab == enemyTab) selIndex = enemyList.SelectedIndex;

            objList.Items.Clear();
            itemList.Items.Clear();
            bossList.Items.Clear();
            enemyList.Items.Clear();

            objList.BeginUpdate();
            itemList.BeginUpdate();
            bossList.BeginUpdate();
            enemyList.BeginUpdate();

            for (int i = 0; i < level.Objects.Count; i++)
            {
                objList.Items.Add(level.Objects[i]["string kind"]);
            }
            for (int i = 0; i < level.Items.Count; i++)
            {
                itemList.Items.Add(level.Items[i]["string kind"]);
            }
            for (int i = 0; i < level.Bosses.Count; i++)
            {
                bossList.Items.Add(level.Bosses[i]["string kind"]);
            }
            for (int i = 0; i < level.Enemies.Count; i++)
            {
                enemyList.Items.Add(level.Enemies[i]["string kind"]);
            }

            if (tabControl1.SelectedTab == objTab && objList.Items.Count >= selIndex + 1) objList.SelectedIndex = selIndex;
            if (tabControl1.SelectedTab == itemTab && itemList.Items.Count >= selIndex + 1) itemList.SelectedIndex = selIndex;
            if (tabControl1.SelectedTab == bossTab && bossList.Items.Count >= selIndex + 1) bossList.SelectedIndex = selIndex;
            if (tabControl1.SelectedTab == enemyTab && enemyList.Items.Count >= selIndex + 1) enemyList.SelectedIndex = selIndex;

            objList.EndUpdate();
            itemList.EndUpdate();
            bossList.EndUpdate();
            enemyList.EndUpdate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Kirby Planet Robobot Level Files|*.dat";
            open.DefaultExt = ".dat";
            open.CheckFileExists = true;
            open.Title = "Open Level File";
            if (open.ShowDialog() == DialogResult.OK)
            {
                a = true;

                filePath = open.FileName;
                objList.Items.Clear();
                itemList.Items.Clear();
                bossList.Items.Clear();
                enemyList.Items.Clear();

                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                this.Text = $"HaltmannLab - Opening {filePath}...";
                level = new Level(open.FileName);

                sizeW.Value = level.Width;
                sizeH.Value = level.Height;

                camera.pos = Vector2.Zero;
                camera.zoom = 1.1;
                RefreshObjectLists();

                this.Text = $"HaltmannLab - {filePath}";
                this.Cursor = Cursors.Arrow;
                this.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;

                a = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel newlvl = new NewLevel();
            if (newlvl.ShowDialog() == DialogResult.OK)
            {
                a = true;

                objList.Items.Clear();
                itemList.Items.Clear();
                bossList.Items.Clear();
                enemyList.Items.Clear();

                level = newlvl.level;

                camera.pos = Vector2.Zero;
                camera.zoom = 1.1;
                RefreshObjectLists();

                sizeH.Value = level.Height;
                sizeW.Value = level.Width;

                this.Text = $"HaltmannLab - New Level";

                saveAsToolStripMenuItem.Enabled = true;

                a = false;
            }
        }

        private void editObj_Click(object sender, EventArgs e)
        {
            if (objList.SelectedItem != null && level != null)
            {
                YAMLEditor editor = new YAMLEditor();
                editor.obj = level.Objects[objList.SelectedIndex];
                editor.editorType = 0;
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    level.Objects[objList.SelectedIndex] = editor.obj;
                    RefreshObjectLists();
                }
            }
        }

        private void editItem_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItem != null && level != null)
            {
                YAMLEditor editor = new YAMLEditor();
                editor.obj = level.Items[itemList.SelectedIndex];
                editor.editorType = 2;
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    level.Items[itemList.SelectedIndex] = editor.obj;
                    RefreshObjectLists();
                }
            }
        }

        private void editBoss_Click(object sender, EventArgs e)
        {
            if (bossList.SelectedItem != null && level != null)
            {
                YAMLEditor editor = new YAMLEditor();
                editor.obj = level.Bosses[bossList.SelectedIndex];
                editor.editorType = 3;
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    level.Bosses[bossList.SelectedIndex] = editor.obj;
                    RefreshObjectLists();
                }
            }
        }

        private void editEnemy_Click(object sender, EventArgs e)
        {
            if (enemyList.SelectedItem != null && level != null)
            {
                YAMLEditor editor = new YAMLEditor();
                editor.obj = level.Enemies[enemyList.SelectedIndex];
                editor.editorType = 4;
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    level.Enemies[enemyList.SelectedIndex] = editor.obj;
                    RefreshObjectLists();
                }
            }
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.ClearColor(Color.FromArgb(200, 200, 200));

            renderer = new Renderer();
            texturing = new Texturing();
            camera = new Camera(new Vector2(0, 0), 1.1);

            for (int i = 0; i < 52; i++)
            {
                texIds.Add(texturing.LoadTexture("Resources/tiles/" + i + ".png"));
            }
            texIds.Add(texturing.LoadTexture("Resources/tiles/select.png"));

            modTexIds.Add(texturing.LoadTexture("Resources/modifiers/ladder.png"));
            modTexIds.Add(texturing.LoadTexture("Resources/modifiers/water.png"));
            modTexIds.Add(texturing.LoadTexture("Resources/modifiers/spike.png"));
            modTexIds.Add(texturing.LoadTexture("Resources/modifiers/damage.png"));

            blockTexIds.Add(0xFF, texturing.LoadTexture("Resources/blocks/unknown.png"));

            objTexIds.Add(texturing.LoadTexture("Resources/obj/object.png"));
            objTexIds.Add(texturing.LoadTexture("Resources/obj/item.png"));
            objTexIds.Add(texturing.LoadTexture("Resources/obj/boss.png"));
            objTexIds.Add(texturing.LoadTexture("Resources/obj/enemy.png"));
            objTexIds.Add(texturing.LoadTexture("Resources/obj/select.png"));

            font = new BmFont(texturing, "a");

            t = new System.Timers.Timer(1000.0 / 60.0);
            t.Elapsed += t_Elapsed;
            t.Start();
        }

        private void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            glControl.Invalidate();
            t.Start();
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.FromArgb(200, 200, 200));

            renderer.Init(glControl.Width, glControl.Height);
            camera.Transform();

            if (level != null)
            {

                //Crop Tiles outside camera view.
                int tileStartX = (int)Math.Max(0,Math.Floor(
                        (camera.pos.X - glControl.Width/camera.zoom*0.5f)/16.0f
                    ));
                int tileEndX = (int)Math.Min(level.Width, Math.Ceiling(
                        (camera.pos.X + glControl.Width / camera.zoom*0.5f) / 16.0f
                    ));
                int tileStartY = (int)Math.Max(0, 1+Math.Floor(
                        -(camera.pos.Y + glControl.Height / camera.zoom*0.5f) / 16.0f
                    ));
                int tileEndY = (int)Math.Min(level.Height,1+ Math.Ceiling(
                        -(camera.pos.Y - glControl.Height / camera.zoom*0.5f) / 16.0f
                    ));

                Vector2 vec_scale = new Vector2(1.0f, 1.0f);

                //Collisions
                for (int ty = tileStartY; ty < tileEndY; ty++)
                {
                    for (int tx = tileStartX; tx < tileEndX; tx++)
                    {
                        int ix = ty * (int)level.Width + tx;
                        Collision c = level.TileCollision[ix];
                        Vector2 v = new Vector2(tx * 16f, -ty * 16f);
                        renderer.Draw(texIds[c.Shape], v, vec_scale, 17, 17);
                    }
                }

                //Blocks
                if (renderBlocksToolStripMenuItem.Checked)
                {
                    for (int ty = tileStartY; ty < tileEndY; ty++)
                    {
                        for (int tx = tileStartX; tx < tileEndX; tx++)
                        {
                            int ix = ty * (int)level.Width + tx;
                            Block b = level.TileBlock[ix];
                            Vector2 v = new Vector2(tx * 16f, -ty * 16f);

                            if (b.ID != -1)
                            {
                                if (blockTexIds.ContainsKey(b.ID))
                                {
                                    renderer.Draw(blockTexIds[b.ID], v, vec_scale, 17, 17);
                                }
                                else
                                {
                                    renderer.Draw(blockTexIds[0xFF], v, vec_scale, 17, 17);
                                }
                            }
                        }
                    }
                }

                //Modifiers
                if (renderTileModifiersToolStripMenuItem.Checked)
                {
                    for (int ty = tileStartY; ty < tileEndY; ty++)
                    {
                        for (int tx = tileStartX; tx < tileEndX; tx++)
                        {
                            int ix = ty * (int)level.Width + tx;
                            Collision c = level.TileCollision[ix];
                            Vector2 v = new Vector2(tx * 16f, -ty * 16f);
                            if ((c.Modifier & (1 << 1)) != 0)
                            {
                                renderer.Draw(modTexIds[0], v, vec_scale, 17, 17);
                            }
                            if ((c.Modifier & (1 << 3)) != 0)
                            {
                                renderer.Draw(modTexIds[1], v, vec_scale, 17, 17);
                            }
                            if ((c.Modifier & (1 << 4)) != 0)
                            {
                                renderer.Draw(modTexIds[2], v, vec_scale, 17, 17);
                            }
                            if ((c.Modifier & (1 << 6)) != 0)
                            {
                                renderer.Draw(modTexIds[3], v, vec_scale, 17, 17);
                            }
                        }
                    }
                }

                if (1 == 2)
                {
                    Vector2 v = new Vector2(tileX * 16f, -tileY * 16f);
                    renderer.Draw(texIds[52], v, vec_scale, 17, 17);
                }

                if (renderObjectPointsToolStripMenuItem.Checked)
                {
                    for (int i = 0; i < level.Objects.Count; i++)
                    {
                        try
                        {
                            int oX = int.Parse(level.Objects[i]["int x"].Replace(" ", "").Split('|')[0]);
                            int oY = int.Parse(level.Objects[i]["int y"].Replace(" ", "").Split('|')[0]);
                            int offX = int.Parse(level.Objects[i]["int x"].Replace(" ", "").Split('|')[1]);
                            int offY = int.Parse(level.Objects[i]["int y"].Replace(" ", "").Split('|')[1]);
                            renderer.Draw(objTexIds[0], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            if (i == objList.SelectedIndex)
                            {
                                renderer.Draw(objTexIds[4], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            }
                            if (renderObjectNamesToolStripMenuItem.Checked) renderer.DrawString(level.Objects[i]["string kind"], font, new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f / (float)camera.zoom, 1f / (float)camera.zoom), new Vector4(0.2f,0.6f,0.2f,1f));
                        }
                        catch { }
                    }
                }

                if (renderItemPointsToolStripMenuItem.Checked)
                {
                    for (int i = 0; i < level.Items.Count; i++)
                    {
                        try
                        {
                            int oX = int.Parse(level.Items[i]["int x"].Replace(" ", "").Split('|')[0]);
                            int oY = int.Parse(level.Items[i]["int y"].Replace(" ", "").Split('|')[0]);
                            int offX = int.Parse(level.Items[i]["int x"].Replace(" ", "").Split('|')[1]);
                            int offY = int.Parse(level.Items[i]["int y"].Replace(" ", "").Split('|')[1]);
                            renderer.Draw(objTexIds[1], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            if (i == itemList.SelectedIndex)
                            {
                                renderer.Draw(objTexIds[4], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            }
                            if (renderObjectNamesToolStripMenuItem.Checked) renderer.DrawString(level.Items[i]["string kind"], font, new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f / (float)camera.zoom, 1f / (float)camera.zoom), new Vector4(0.6f, 0.6f, 0.2f, 1f));
                        }
                        catch { }
                    }
                }

                if (renderBossPointsToolStripMenuItem.Checked)
                {
                    for (int i = 0; i < level.Bosses.Count; i++)
                    {
                        try
                        {
                            int oX = int.Parse(level.Bosses[i]["int x"].Replace(" ", "").Split('|')[0]);
                            int oY = int.Parse(level.Bosses[i]["int y"].Replace(" ", "").Split('|')[0]);
                            int offX = int.Parse(level.Bosses[i]["int x"].Replace(" ", "").Split('|')[1]);
                            int offY = int.Parse(level.Bosses[i]["int y"].Replace(" ", "").Split('|')[1]);
                            renderer.Draw(objTexIds[2], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            if (i == bossList.SelectedIndex)
                            {
                                renderer.Draw(objTexIds[4], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            }
                            if (renderObjectNamesToolStripMenuItem.Checked) renderer.DrawString(level.Bosses[i]["string kind"], font, new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f / (float)camera.zoom, 1f / (float)camera.zoom), new Vector4(0.9f, 0.1f, 0.1f, 1f));
                        }
                        catch { }
                    }
                }

                if (renderEnemyPointsToolStripMenuItem.Checked)
                {
                    for (int i = 0; i < level.Enemies.Count; i++)
                    {
                        try
                        {
                            int oX = int.Parse(level.Enemies[i]["int x"].Replace(" ", "").Split('|')[0]);
                            int oY = int.Parse(level.Enemies[i]["int y"].Replace(" ", "").Split('|')[0]);
                            int offX = int.Parse(level.Enemies[i]["int x"].Replace(" ", "").Split('|')[1]);
                            int offY = int.Parse(level.Enemies[i]["int y"].Replace(" ", "").Split('|')[1]);
                            renderer.Draw(objTexIds[3], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            if (i == enemyList.SelectedIndex)
                            {
                                renderer.Draw(objTexIds[4], new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f, 1f), 7, 7);
                            }
                            if (renderObjectNamesToolStripMenuItem.Checked) renderer.DrawString(level.Enemies[i]["string kind"], font, new Vector2(((oX * 16f) - 3f) + (offX * 0.95f), ((-oY * 16f) + 13f) - (offY * 0.95f)), new Vector2(1f / (float)camera.zoom, 1f / (float)camera.zoom), new Vector4(0.8f, 0.2f, 0.2f, 1f));
                        }
                        catch { }
                    }
                }
            }

            glControl.SwapBuffers();
        }

        public int GetUniqueWUID()
        {
            int wuid = 0;
            Random random = new Random();
            wuid = random.Next(0, int.MaxValue);

            for (int i = 0; i < level.Objects.Count; i++)
            {
                if (int.Parse(level.Objects[i]["int wuid"]) == wuid)
                {
                    random = new Random();
                    wuid = random.Next();
                    i = -1;
                }
            }

            for (int i = 0; i < level.Items.Count; i++)
            {
                if (int.Parse(level.Items[i]["int wuid"]) == wuid)
                {
                    random = new Random();
                    wuid = random.Next();
                    i = -1;
                }
            }

            for (int i = 0; i < level.Bosses.Count; i++)
            {
                if (int.Parse(level.Bosses[i]["int wuid"]) == wuid)
                {
                    random = new Random();
                    wuid = random.Next();
                    i = -1;
                }
            }

            for (int i = 0; i < level.Enemies.Count; i++)
            {
                if (int.Parse(level.Enemies[i]["int wuid"]) == wuid)
                {
                    random = new Random();
                    wuid = random.Next();
                    i = -1;
                }
            }

            return wuid;
        }

        private void addObj_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                AddObj addObj = new AddObj();
                addObj.editorType = 0;
                if (addObj.ShowDialog() == DialogResult.OK)
                {
                    addObj.obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Objects.Add(addObj.obj);
                    RefreshObjectLists();
                }
            }
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                AddObj addObj = new AddObj();
                addObj.editorType = 2;
                if (addObj.ShowDialog() == DialogResult.OK)
                {
                    addObj.obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Items.Add(addObj.obj);
                    RefreshObjectLists();
                }
            }
        }

        private void addBoss_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                AddObj addObj = new AddObj();
                addObj.editorType = 3;
                if (addObj.ShowDialog() == DialogResult.OK)
                {
                    addObj.obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Bosses.Add(addObj.obj);
                    RefreshObjectLists();
                }
            }
        }

        private void addEnemy_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                AddObj addObj = new AddObj();
                addObj.editorType = 4;
                if (addObj.ShowDialog() == DialogResult.OK)
                {
                    addObj.obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Enemies.Add(addObj.obj);
                    RefreshObjectLists();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                Save();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Kirby Planet Robobot Level Files|*.dat";
                save.DefaultExt = ".dat";
                save.Title = "Save Level File";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    filePath = save.FileName;
                    Save();
                }
            }
        }

        public void Save()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            this.Text = $"HaltmannLab - Saving {filePath}...";
            level.Write(filePath);
            this.Text = $"HaltmannLab - {filePath}";
            this.Cursor = Cursors.Arrow;
            this.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
        }

        private void delObj_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (objList.SelectedItem != null)
                {
                    level.Objects.RemoveAt(objList.SelectedIndex);
                    RefreshObjectLists();
                }
            }
        }

        private void delItem_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (itemList.SelectedItem != null)
                {
                    level.Items.RemoveAt(itemList.SelectedIndex);
                    RefreshObjectLists();
                }
            }
        }

        private void delBoss_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (bossList.SelectedItem != null)
                {
                    level.Bosses.RemoveAt(bossList.SelectedIndex);
                    RefreshObjectLists();
                }
            }
        }

        private void delEnemy_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (enemyList.SelectedItem != null)
                {
                    level.Enemies.RemoveAt(enemyList.SelectedIndex);
                    RefreshObjectLists();
                }
            }
        }

        private void stageSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                StageSettings settings = new StageSettings();
                settings.stage = level.StageData;
                settings.bg = level.Background;
                settings.tileset = level.Tileset;
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    level.StageData = settings.stage;
                    level.Background = settings.bg;
                    level.Tileset = settings.tileset;
                }
            }
        }

        private void objList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (level != null)
            {
                if (objList.SelectedItem != null)
                {
                    editObj_Click(this, new EventArgs());
                }
            }
        }

        private void itemList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (level != null)
            {
                if (itemList.SelectedItem != null)
                {
                    editItem_Click(this, new EventArgs());
                }
            }
        }

        private void bossList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (level != null)
            {
                if (bossList.SelectedItem != null)
                {
                    editBoss_Click(this, new EventArgs());
                }
            }
        }

        private void enemyList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (level != null)
            {
                if (enemyList.SelectedItem != null)
                {
                    editEnemy_Click(this, new EventArgs());
                }
            }
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                moveCam = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (tool == 0)
                {

                }
                else if (tool == 1)
                {

                }
                else if (tool == 2)
                {
                    int ix = (int)((tileY * level.Width) + tileX);

                    if (editCol.Checked)
                    {
                        Collision c = level.TileCollision[ix];
                        c.Shape = (byte)vshape.Value;
                        c.Modifier = 0;
                        if (ladder.Checked) c.Modifier += 2;
                        if (water.Checked) c.Modifier += 8;
                        if (spike.Checked) c.Modifier += 16;
                        if (lava.Checked) c.Modifier += 64;
                        c.Material = (byte)vmat.Value;
                        c.Unk = (byte)vunk.Value;
                        level.TileCollision[ix] = c;
                    }
                    if (editBlock.Checked)
                    {
                        Block b = level.TileBlock[ix];
                        b.ID = (short)vblock.Value;
                        level.TileBlock[ix] = b;
                    }
                    if (editDeco.Checked)
                    {
                        Decoration ml = level.MLandDecoration[ix];
                        Decoration bl = level.BLandDecoration[ix];
                        Decoration fl = level.FLandDecoration[ix];

                        bl.Shape = (short)d1_1.Value;
                        bl.WaterShape = (sbyte)d1_3.Value;
                        bl.Group = (sbyte)d1_4.Value;

                        ml.Shape = (short)d2_1.Value;
                        ml.WaterShape = (sbyte)d2_3.Value;
                        ml.Group = (sbyte)d2_4.Value;

                        fl.Shape = (short)d3_1.Value;
                        fl.WaterShape = (sbyte)d3_3.Value;
                        fl.Group = (sbyte)d3_4.Value;

                        level.MLandDecoration[ix] = bl;
                        level.BLandDecoration[ix] = ml;
                        level.FLandDecoration[ix] = fl;
                    }
                }
                else if (tool == 3)
                {
                    int ix = (int)((tileY * level.Width) + tileX);

                    vshape.Value = level.TileCollision[ix].Shape;

                    if ((level.TileCollision[ix].Modifier & (1 << 1)) != 0)
                    {
                        ladder.Checked = true;
                    }
                    else { ladder.Checked = false; }

                    if ((level.TileCollision[ix].Modifier & (1 << 3)) != 0)
                    {
                        water.Checked = true;
                    }
                    else { water.Checked = false; }

                    if ((level.TileCollision[ix].Modifier & (1 << 4)) != 0)
                    {
                        spike.Checked = true;
                    }
                    else { spike.Checked = false; }

                    if ((level.TileCollision[ix].Modifier & (1 << 6)) != 0)
                    {
                        lava.Checked = true;
                    }
                    else { lava.Checked = false; }

                    vmat.Value = level.TileCollision[ix].Material;

                    vunk.Value = level.TileCollision[ix].Unk;

                    vblock.Value = level.TileBlock[ix].ID;

                    d1_1.Value = level.MLandDecoration[ix].Shape;
                    d1_3.Value = level.MLandDecoration[ix].WaterShape;
                    d1_4.Value = level.MLandDecoration[ix].Group;

                    d2_1.Value = level.BLandDecoration[ix].Shape;
                    d2_3.Value = level.BLandDecoration[ix].WaterShape;
                    d2_4.Value = level.BLandDecoration[ix].Group;

                    d3_1.Value = level.FLandDecoration[ix].Shape;
                    d3_3.Value = level.FLandDecoration[ix].WaterShape;
                    d3_4.Value = level.FLandDecoration[ix].Group;
                }
            }
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (level != null)
            {
                Vector2 p = ConvertMouseCoords(new Vector2(e.X, e.Y));
                if (p.X > level.Width - 1)
                {
                    tileX = level.Width - 1;
                }
                else if (p.X > 0)
                {
                    tileX = (uint)p.X;
                }
                else { tileX = 0; }
                if (p.Y < -(level.Height - 1))
                {
                    tileY = level.Height - 1;
                }
                else if (p.Y < 0)
                {
                    tileY = (uint)-p.Y + 1;
                }
                else { tileY = 0; }
            }
            if (e.Button == MouseButtons.Right)
            {
                float moveSpeed = 1.0f / (float)camera.zoom;
                camera.pos.X += (mouseX - e.X) * moveSpeed;
                camera.pos.Y += (mouseY - e.Y) * moveSpeed;
                mouseX = e.X;
                mouseY = e.Y;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (tool == 2)
                {
                    int ix = (int)((tileY * level.Width) + tileX);

                    if (editCol.Checked)
                    {
                        Collision c = level.TileCollision[ix];
                        c.Shape = (byte)vshape.Value;
                        c.Modifier = 0;
                        if (ladder.Checked) c.Modifier += 2;
                        if (water.Checked) c.Modifier += 8;
                        if (spike.Checked) c.Modifier += 16;
                        if (lava.Checked) c.Modifier += 64;
                        c.Material = (byte)vmat.Value;
                        c.Unk = (byte)vunk.Value;
                        level.TileCollision[ix] = c;
                    }
                    if (editBlock.Checked)
                    {
                        Block b = level.TileBlock[ix];
                        b.ID = (short)vblock.Value;
                        level.TileBlock[ix] = b;
                    }
                    if (editDeco.Checked)
                    {
                        Decoration ml = level.MLandDecoration[ix];
                        Decoration bl = level.BLandDecoration[ix];
                        Decoration fl = level.FLandDecoration[ix];

                        bl.Shape = (short)d1_1.Value;
                        bl.WaterShape = (sbyte)d1_3.Value;
                        bl.Group = (sbyte)d1_4.Value;

                        ml.Shape = (short)d2_1.Value;
                        ml.WaterShape = (sbyte)d2_3.Value;
                        ml.Group = (sbyte)d2_4.Value;

                        fl.Shape = (short)d3_1.Value;
                        fl.WaterShape = (sbyte)d3_3.Value;
                        fl.Group = (sbyte)d3_4.Value;

                        level.MLandDecoration[ix] = bl;
                        level.BLandDecoration[ix] = ml;
                        level.FLandDecoration[ix] = fl;
                    }
                }
            }
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                moveCam = false;
                mouseX = 0;
                mouseY = 0;
            }
        }

        private void glControl_MouseLeave(object sender, EventArgs e)
        {
            moveCam = false;
            mouseX = 0;
            mouseY = 0;
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                camera.zoom += 0.1 * camera.zoom;
                if (camera.zoom > 4)
                {
                    camera.zoom = 4;
                }
            }
            else if (e.Delta < 0)
            {
                camera.zoom -= 0.1 * camera.zoom;
                if (camera.zoom < 0.25)
                {
                    camera.zoom = 0.25;
                }
            }
        }

        private void resetCamera_Click(object sender, EventArgs e)
        {
            camera.zoom = 1.1;
            //Move Camera into Level Bounds
            camera.pos.X = Math.Max(0, Math.Min(level.Width*16 , camera.pos.X));
            camera.pos.Y = Math.Max(-level.Height*16, Math.Min(0, camera.pos.Y));
        }

        private void UpdateLevelSize(object sender, EventArgs e)
        {
            if (!a)
            {
                Collision c = new Collision();
                Block b = new Block();
                Decoration d = new Decoration();
                b.ID = -1;
                d.Shape = -1;
                d.WaterShape = 0;
                d.Group = -1;

                if (sizeW.Value > level.Width)
                {
                    for (int h = 0; h < sizeH.Value; h++)
                    {
                        level.TileCollision.Insert(((h * (int)level.Width) + (int)level.Width) + h, c);
                        level.TileBlock.Insert(((h * (int)level.Width) + (int)level.Width) + h, b);
                        level.BLandDecoration.Insert(((h * (int)level.Width) + (int)level.Width) + h, d);
                        level.MLandDecoration.Insert(((h * (int)level.Width) + (int)level.Width) + h, d);
                        level.FLandDecoration.Insert(((h * (int)level.Width) + (int)level.Width) + h, d);
                    }
                    level.Width = (uint)sizeW.Value;
                }
                else if (sizeW.Value < level.Width)
                {
                    for (int h = 0; h < sizeH.Value - 1; h++)
                    {
                        level.TileCollision.RemoveAt(((h * (int)level.Width) + (int)level.Width) - h);
                        level.TileBlock.RemoveAt(((h * (int)level.Width) + (int)level.Width) - h);
                        level.BLandDecoration.RemoveAt(((h * (int)level.Width) + (int)level.Width) - h);
                        level.MLandDecoration.RemoveAt(((h * (int)level.Width) + (int)level.Width) - h);
                        level.FLandDecoration.RemoveAt(((h * (int)level.Width) + (int)level.Width) - h);
                    }
                    level.Width = (uint)sizeW.Value;
                }
                else if (sizeH.Value > level.Height)
                {
                    for (int w = 0; w < level.Width; w++)
                    {
                        level.TileCollision.Add(c);
                        level.TileBlock.Add(b);
                        level.BLandDecoration.Add(d);
                        level.MLandDecoration.Add(d);
                        level.FLandDecoration.Add(d);
                    }
                    level.Height = (uint)sizeH.Value;
                }
                else if (sizeH.Value < level.Height)
                {
                    level.TileCollision.RemoveRange((int)((level.Height - 1) * (int)level.Width), (int)level.Width);
                    level.TileBlock.RemoveRange((int)((level.Height - 1) * (int)level.Width), (int)level.Width);
                    level.BLandDecoration.RemoveRange((int)((level.Height - 1) * (int)level.Width), (int)level.Width);
                    level.MLandDecoration.RemoveRange((int)((level.Height - 1) * (int)level.Width), (int)level.Width);
                    level.FLandDecoration.RemoveRange((int)((level.Height - 1) * (int)level.Width), (int)level.Width);
                    level.Height = (uint)sizeH.Value;
                }
            }
        }

        private Vector2 ConvertMouseCoords(Vector2 i)
        {
            i -= new Vector2(glControl.Width, glControl.Height) / 2f;
            i /= (float)camera.zoom;
            return (camera.pos + i) / 16f;
        }

        bool a;

        private void objList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!a)
            {
                a = true;
                try
                {
                    moveObj = 0;
                    int oX = int.Parse(level.Objects[objList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[0]);
                    int oY = int.Parse(level.Objects[objList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[0]);
                    int offX = int.Parse(level.Objects[objList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[1]);
                    int offY = int.Parse(level.Objects[objList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[1]);
                    xCoord.Value = oX;
                    xOffset.Value = offX;
                    yCoord.Value = oY;
                    yOffset.Value = offY;
                } catch { }
                itemList.ClearSelected();
                bossList.ClearSelected();
                enemyList.ClearSelected();
                a = false;
            }
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!a)
            {
                a = true;
                try
                {
                    moveObj = 1;
                    int oX = int.Parse(level.Items[itemList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[0]);
                    int oY = int.Parse(level.Items[itemList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[0]);
                    int offX = int.Parse(level.Items[itemList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[1]);
                    int offY = int.Parse(level.Items[itemList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[1]);
                    xCoord.Value = oX;
                    xOffset.Value = offX;
                    yCoord.Value = oY;
                    yOffset.Value = offY;
                } catch { }
                objList.ClearSelected();
                bossList.ClearSelected();
                enemyList.ClearSelected();
                a = false;
            }
            
        }

        private void bossList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!a)
            {
                a = true;
                try
                {
                    moveObj = 2;
                    int oX = int.Parse(level.Bosses[bossList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[0]);
                    int oY = int.Parse(level.Bosses[bossList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[0]);
                    int offX = int.Parse(level.Bosses[bossList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[1]);
                    int offY = int.Parse(level.Bosses[bossList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[1]);
                    xCoord.Value = oX;
                    xOffset.Value = offX;
                    yCoord.Value = oY;
                    yOffset.Value = offY;
                } catch { }
                objList.ClearSelected();
                itemList.ClearSelected();
                enemyList.ClearSelected();
                a = false;
            }
        }

        private void enemyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!a)
            {
                a = true;
                try
                {
                    moveObj = 3;
                    int oX = int.Parse(level.Enemies[enemyList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[0]);
                    int oY = int.Parse(level.Enemies[enemyList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[0]);
                    int offX = int.Parse(level.Enemies[enemyList.SelectedIndex]["int x"].Replace(" ", "").Split('|')[1]);
                    int offY = int.Parse(level.Enemies[enemyList.SelectedIndex]["int y"].Replace(" ", "").Split('|')[1]);
                    xCoord.Value = oX;
                    xOffset.Value = offX;
                    yCoord.Value = oY;
                    yOffset.Value = offY;
                } catch { }
                objList.ClearSelected();
                itemList.ClearSelected();
                bossList.ClearSelected();
                a = false;
            }
        }

        void UpdateCoords()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        level.Objects[objList.SelectedIndex]["int x"] = $"{xCoord.Value} | {xOffset.Value}";
                        level.Objects[objList.SelectedIndex]["int y"] = $"{yCoord.Value} | {yOffset.Value}";
                        break;
                    }
                case 1:
                    {
                        level.Items[itemList.SelectedIndex]["int x"] = $"{xCoord.Value} | {xOffset.Value}";
                        level.Items[itemList.SelectedIndex]["int y"] = $"{yCoord.Value} | {yOffset.Value}";
                        break;
                    }
                case 2:
                    {
                        level.Bosses[bossList.SelectedIndex]["int x"] = $"{xCoord.Value} | {xOffset.Value}";
                        level.Bosses[bossList.SelectedIndex]["int y"] = $"{yCoord.Value} | {yOffset.Value}";
                        break;
                    }
                case 3:
                    {
                        level.Enemies[enemyList.SelectedIndex]["int x"] = $"{xCoord.Value} | {xOffset.Value}";
                        level.Enemies[enemyList.SelectedIndex]["int y"] = $"{yCoord.Value} | {yOffset.Value}";
                        break;
                    }
            }
        }

        private void xCoord_ValueChanged(object sender, EventArgs e)
        {
            if (level != null && moveObj != null)
            {
                UpdateCoords();
            }
        }

        private void xOffset_ValueChanged(object sender, EventArgs e)
        {
            if (level != null && moveObj != null)
            {
                UpdateCoords();
            }
        }

        private void yCoord_ValueChanged(object sender, EventArgs e)
        {
            if (level != null && moveObj != null)
            {
                UpdateCoords();
            }
        }

        private void yOffset_ValueChanged(object sender, EventArgs e)
        {
            if (level != null && moveObj != null)
            {
                UpdateCoords();
            }
        }

        private void select_Click(object sender, EventArgs e)
        {
            tool = 0;
            select.Enabled = false;
            move.Enabled = true;
            draw.Enabled = true;
            pick.Enabled = true;
        }

        private void move_Click(object sender, EventArgs e)
        {
            tool = 1;
            select.Enabled = true;
            move.Enabled = false;
            draw.Enabled = true;
            pick.Enabled = true;
        }

        private void draw_Click(object sender, EventArgs e)
        {
            tool = 2;
            select.Enabled = true;
            move.Enabled = true;
            draw.Enabled = false;
            pick.Enabled = true;
        }

        private void pick_Click(object sender, EventArgs e)
        {
            tool = 3;
            select.Enabled = true;
            move.Enabled = true;
            draw.Enabled = true;
            pick.Enabled = false;
        }

        private void cloneObj_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (objList.SelectedItem != null)
                {
                    Dictionary<string, string> obj = new Dictionary<string, string>();
                    foreach (KeyValuePair<string,string> pair in level.Objects[objList.SelectedIndex])
                        obj.Add(pair.Key, pair.Value);
                    obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Objects.Add(obj);
                    RefreshObjectLists();
                }
            }
        }

        private void cloneItem_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (itemList.SelectedItem != null)
                {
                    Dictionary<string, string> obj = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> pair in level.Items[itemList.SelectedIndex])
                        obj.Add(pair.Key, pair.Value);
                    obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Items.Add(obj);
                    RefreshObjectLists();
                }
            }
        }

        private void cloneBoss_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (bossList.SelectedItem != null)
                {
                    Dictionary<string, string> obj = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> pair in level.Bosses[bossList.SelectedIndex])
                        obj.Add(pair.Key, pair.Value);
                    obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Bosses.Add(obj);
                    RefreshObjectLists();
                }
            }
        }

        private void cloneEnemy_Click(object sender, EventArgs e)
        {
            if (level != null)
            {
                if (enemyList.SelectedItem != null)
                {
                    Dictionary<string, string> obj = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> pair in level.Enemies[enemyList.SelectedIndex])
                        obj.Add(pair.Key, pair.Value);
                    obj["int wuid"] = GetUniqueWUID().ToString();
                    level.Enemies.Add(obj);
                    RefreshObjectLists();
                }
            }
        }
    }
}
