using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EssenceOfMagic
{
    public class World
    {
        public SurfaceBlock[][] Ground { get; set; } = new SurfaceBlock[0][];
        public ObjectsCollection Objects { get; set; } = new ObjectsCollection();
        public void Click(Point Location, MouseButtons Button)
        {
            for (int i = 0; i < Objects.CountAll; i++)
            {
                GameObject go = Objects.Get(i);
                if (Location.X > go.Location.X && Location.X < go.Location.X + go.Size.Width &&
                    Location.Y > go.Location.Y && Location.Y < go.Location.Y + go.Size.Height)
                {
                    if (Button == MouseButtons.Left) go.ActionLeft?.Invoke(go, i);
                    if (Button == MouseButtons.Right) go.ActionRight?.Invoke(go, i);
                }
            }
        }
        public void MouseMove(Point Location)
        {
            if (Interface.Page == InterfacePages.Game)
            {
                for (int i = 0; i < Objects.CountAll; i++)
                {
                    GameObject go = Objects.Get(i);
                    if (Location.X > go.Location.X && Location.X < go.Location.X + go.Size.Width &&
                        Location.Y > go.Location.Y && Location.Y < go.Location.Y + go.Size.Height)
                    {
                        if (go.Name != null)
                        {
                            string Text = "";
                            if (go.ID == "shop")
                                Text += "ПКМ - торговать";
                            Interface.ShowMouseHint(go.Name, Text, new Size(165, 75));
                            return;
                        }
                    }
                }
                Interface.MouseHint = null;
            }
        }
    }
    public class ObjectsCollection
    {
        public ObjectsCollection()
        {
            objects = new GameObject[0];
            creatures = new Creature[0];
            players = new Player[0];
        }

        public GameObject[] objects { get; set; }
        public Creature[] creatures { get; set; }
        public Player[] players { get; set; }
        public int[][] index { get; set; } = new int[0][];

        private int _counta = 0;
        [System.Text.Json.Serialization.JsonIgnore]
        public int CountAll { get { _counta = objects.Length + creatures.Length + players.Length; return _counta; } }
        private int _counto = 0;
        [System.Text.Json.Serialization.JsonIgnore]
        public int CountObjects { get { _counto = objects.Length; return _counto; } }
        private int _countc = 0;
        [System.Text.Json.Serialization.JsonIgnore]
        public int CountCreatures { get { _countc = creatures.Length; return _countc; } }
        private int _countp = 0;
        [System.Text.Json.Serialization.JsonIgnore]
        public int CountPlayers { get { _countp = players.Length; return _countp; } }

        public void Get(int n, out GameObject ret)
        {
            int[] i = new int[2] { -1, -1 };
            int t = -1;
            for (int l = 0; l < index.Length; l++)
            {
                if (index[l][0] == 0)
                {
                    t++;
                    if (t == n) i = index[l];
                }
            }
            if (i[0] == -1 || i[1] == -1) { ret = null; return; }
            if (i[0] == 0 && i[1] < objects.Length) { ret = objects[i[1]]; return; }
            ret = null;
        }
        public void Get(int n, out Creature ret)
        {
            int[] i = new int[2] { -1, -1 };
            int t = -1;
            for (int l = 0; l < index.Length; l++)
            {
                if (index[l][0] == 1)
                {
                    t++;
                    if (t == n) i = index[l];
                }
            }
            if (i[0] == -1 || i[1] == -1) { ret = null; return; }
            if (i[0] == 1 && i[1] < creatures.Length) { ret = creatures[i[1]]; return; }
            ret = null;
        }
        public void Get(int n, out Player ret)
        {
            int[] i = new int[2] { -1, -1 };
            int t = -1;
            for (int l = 0; l < index.Length; l++)
            {
                if (index[l][0] == 2)
                {
                    t++;
                    if (t == n) i = index[l];
                }
            }
            if (i[0] == -1 || i[1] == -1) { ret = null; return; }
            if (i[0] == 2 && i[1] < players.Length) { ret = players[i[1]]; return; }
            ret = null;
        }
        public GameObject Get(int n)
        {
            int[] i = index[n];
            if (i != null)
            {
                if (i[0] == 0) return objects[i[1]];
                if (i[0] == 1) return creatures[i[1]];
                if (i[0] == 2) return players[i[1]];
            }
            return null;
        }
        public GameObject Get(int n, out ObjectType type)
        {
            int[] i = index[n];
            if (i[0] == 0) { type = ObjectType.Object; return objects[i[1]]; }
            if (i[0] == 1) { type = ObjectType.Creature; return creatures[i[1]]; }
            if (i[0] == 2) { type = ObjectType.Player; return players[i[1]]; }
            type = ObjectType.Unknown;
            return null;
        }

        public void Add(GameObject gameObject)
        {
            GameObject[] newarr = new GameObject[CountObjects + 1];
            int[][] newindex = new int[CountAll + 1][];

            int ina = 0, ioi = 0, ini = 0;
            if (index.Length > 0)
            {
                int isinserted = 0;
                while (ini < newindex.Length)
                {
                    GameObject temp;
                    if (ioi < index.Length)
                    {
                        if (index[ioi][0] == 0) temp = objects[index[ioi][1]];
                        else if (index[ioi][0] == 1) temp = creatures[index[ioi][1]];
                        else temp = players[index[ioi][1]];
                    }
                    else
                    {
                        temp = null;
                    }

                    if (temp != null && 
                        (temp.Location.Y + temp.Size.Height < gameObject.Location.Y + gameObject.Size.Height || 
                        (temp.Location.Y + temp.Size.Height == gameObject.Location.Y + gameObject.Size.Height && temp.Location.X < gameObject.Location.X) || 
                        isinserted > 0))
                    {
                        int ioa = 0;
                        if (index[ioi][0] == 0)
                        {
                            newarr[ina] = objects[index[ioi][1]];
                            ina++;
                            if (isinserted > 0) ioa++;
                        }
                        newindex[ini] = new int[] { index[ioi][0], index[ioi][1] + ioa };
                        ioi++;
                    }
                    else
                    if (temp == null || 
                        temp.Location.Y + temp.Size.Height > gameObject.Location.Y + gameObject.Size.Height || 
                        (temp.Location.Y == gameObject.Location.Y && temp.Location.X >= gameObject.Location.X))
                    {
                        newarr[ina] = gameObject;
                        isinserted++;
                        newindex[ini] = new int[] { 0, ina };
                        ina++;
                    }

                    ini++;
                }
            }
            else
            {
                newarr[0] = gameObject;
                newindex[0] = new int[] { 0, 0 };
            }

            objects = newarr;
            index = newindex;
        }
        public void Add(Creature gameObject)
        {
            Creature[] newarr = new Creature[CountCreatures + 1];
            int[][] newindex = new int[CountAll + 1][];

            int ina = 0, ioi = 0, ini = 0;
            if (index.Length > 0)
            {
                int isinserted = 0;
                while (ini < newindex.Length)
                {
                    GameObject temp;
                    if (ioi < index.Length)
                    {
                        if (index[ioi][0] == 0) temp = objects[index[ioi][1]];
                        else if (index[ioi][0] == 1) temp = creatures[index[ioi][1]];
                        else temp = players[index[ioi][1]];
                    }
                    else
                    {
                        temp = null;
                    }

                    if (temp != null && 
                        (temp.Location.Y + temp.Size.Height < gameObject.Location.Y + gameObject.Size.Height || 
                        (temp.Location.Y + temp.Size.Height == gameObject.Location.Y + gameObject.Size.Height && temp.Location.X < gameObject.Location.X) || 
                        isinserted > 0))
                    {
                        int ioa = 0;
                        if (index[ioi][0] == 1)
                        {
                            newarr[ina] = creatures[index[ioi][1]];
                            ina++;
                            if (isinserted > 0) ioa++;
                        }
                        newindex[ini] = new int[] { index[ioi][0], index[ioi][1] + ioa };
                        ioi++;
                    }
                    else
                    if (temp == null || 
                        temp.Location.Y + temp.Size.Height > gameObject.Location.Y + gameObject.Size.Height || 
                        (temp.Location.Y + temp.Size.Height == gameObject.Location.Y + gameObject.Size.Height && temp.Location.X >= gameObject.Location.X))
                    {
                        newarr[ina] = gameObject;
                        isinserted++;
                        newindex[ini] = new int[] { 1, ina };
                        ina++;
                    }

                    ini++;
                }
            }
            else
            {
                newarr[0] = gameObject;
                newindex[0] = new int[] { 1, 0 };
            }

            creatures = newarr;
            index = newindex;
        }
        public void Add(Player gameObject)
        {
            Player[] newarr = new Player[CountPlayers + 1];
            int[][] newindex = new int[CountAll + 1][];

            int ina = 0, ioi = 0, ini = 0;
            if (index.Length > 0)
            {
                int isinserted = 0;
                while (ini < newindex.Length)
                {
                    GameObject temp;
                    if (ioi < index.Length)
                    {
                        if (index[ioi][0] == 0) temp = objects[index[ioi][1]];
                        else if (index[ioi][0] == 1) temp = creatures[index[ioi][1]];
                        else temp = players[index[ioi][1]];
                    }
                    else
                    {
                        temp = null;
                    }

                    if (temp != null && 
                        (temp.Location.Y + temp.Size.Height < gameObject.Location.Y + gameObject.Size.Height || 
                        (temp.Location.Y + temp.Size.Height == gameObject.Location.Y + gameObject.Size.Height && temp.Location.X < gameObject.Location.X) || 
                        isinserted > 0))
                    {
                        int ioa = 0;
                        if (index[ioi][0] == 2)
                        {
                            newarr[ina] = players[index[ioi][1]];
                            ina++;
                            if (isinserted > 0) ioa++;
                        }
                        newindex[ini] = new int[] { index[ioi][0], index[ioi][1] + ioa };
                        ioi++;
                    }
                    else
                    if (temp == null || 
                        temp.Location.Y + temp.Size.Height > gameObject.Location.Y + gameObject.Size.Height || 
                        (temp.Location.Y + temp.Size.Height == gameObject.Location.Y + gameObject.Size.Height && temp.Location.X >= gameObject.Location.X))
                    {
                        newarr[ina] = gameObject;
                        isinserted++;
                        newindex[ini] = new int[] { 2, ina };
                        ina++;
                    }

                    ini++;
                }
            }
            else
            {
                newarr[0] = gameObject;
                newindex[0] = new int[] { 2, 0 };
            }

            players = newarr;
            index = newindex;
        }

        public void Check()
        {
            for (int i = 1; i < index.Length; i++)
            {
                GameObject cur1 = Get(i - 1);
                GameObject cur2 = Get(i);
                if (cur1.Location.Y + cur1.Size.Height > cur2.Location.Y + cur2.Size.Height)
                {
                    int cur = i;
                    for (int l = i - 1; l > 0; l--)
                    {
                        GameObject cur3 = Get(l);
                        if (cur3.Location.Y + cur3.Size.Height > cur2.Location.Y + cur2.Size.Height)
                        {
                            int[] temp = index[l];
                            index[l] = index[cur];
                            index[cur] = temp;
                            cur = l;
                        }
                    }
                }
            }

            int newgoi = 0;
            GameObject[] newgo = new GameObject[objects.Length];
            int newci = 0;
            Creature[] newc = new Creature[creatures.Length];
            int newpi = 0;
            Player[] newp = new Player[players.Length];
            for (int i = 0; i < index.Length; i++)
            {
                if (index[i][0] == 0)
                {
                    newgo[newgoi] = objects[index[i][1]];
                    index[i][1] = newgoi;
                    newgoi++;
                }
                if (index[i][0] == 1)
                {
                    newc[newci] = creatures[index[i][1]];
                    index[i][1] = newci;
                    newci++;
                }
                if (index[i][0] == 2)
                {
                    newp[newpi] = players[index[i][1]];
                    index[i][1] = newpi;
                    newpi++;
                }
            }
            objects = newgo;
            creatures = newc;
            players = newp;
        }

        public void Delete(int n)
        {
            int[] obji = index[n];
            int[][] newindex = new int[CountAll - 1][];
            int oi = 0, ni = 0;
            while (oi < index.Length)
            {
                if (n > oi)
                {
                    newindex[ni] = index[oi];
                    ni++;
                }
                else if (n < oi)
                {
                    newindex[ni] = index[oi];
                    if (newindex[ni][1] > obji[1] && newindex[ni][0] == obji[0])
                        newindex[ni][1]--;
                    ni++;
                }
                oi++;
            }
            index = newindex;

            if (obji[0] == 0)
            {
                GameObject[] newgo = new GameObject[CountObjects - 1];
                oi = 0; ni = 0;
                while (oi < objects.Length)
                {
                    if (oi != obji[1])
                    {
                        newgo[ni] = objects[oi];
                        ni++;
                    }
                    oi++;
                }
                objects = newgo;
            }
            if (obji[0] == 1)
            {
                Creature[] newc = new Creature[CountCreatures - 1];
                oi = 0; ni = 0;
                while (oi < creatures.Length)
                {
                    if (oi != obji[1])
                    {
                        newc[ni] = creatures[oi];
                        ni++;
                    }
                    oi++;
                }
                creatures = newc;
            }
            if (obji[0] == 2)
            {
                Player[] newp = new Player[CountPlayers - 1];
                oi = 0; ni = 0;
                while (oi < players.Length)
                {
                    if (oi != obji[1])
                    {
                        newp[ni] = players[oi];
                        ni++;
                    }
                    oi++;
                }
                players = newp;
            }
        }
    }
}