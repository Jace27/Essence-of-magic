using System.Drawing;

namespace EssenceOfMagic
{
    public class Camera : GameObject
    {
        public Camera() { }
        public bool isTied
        {
            get
            {
                if (Tracked != null) return true;
                else return false;
            }
        }
        public GameObject Tracked = null;
        public void Link(GameObject Object)
        {
            Tracked = Object;
            Tracked.OnMove += new ObjectMoveEventHandler(OnTrackedMove);
            Location = new Location(Tracked.Location.X + GameData.BlockSize.Width / 2, Tracked.Location.Y + GameData.BlockSize.Height / 2, 0);
        }
        private void OnTrackedMove(Vector Vector)
        {
            if (Vector.dY != 0) GameData.World.Objects.Check();

            Move(Vector);
            if (Location.X != Tracked.Location.X + GameData.BlockSize.Width / 2 ||
                Location.Y != Tracked.Location.Y + GameData.BlockSize.Height / 2)
            {
                Location = new Location(Tracked.Location.X + GameData.BlockSize.Width / 2, Tracked.Location.Y + GameData.BlockSize.Height / 2, Tracked.Location.Z);
            }
        }
    }
}
