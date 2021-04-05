using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAAR_Game
{
    public class CollisionsCircle
    {
        public Vector2 circleCenter;
        public float circleRadius;

        public bool isCollided = false;

        public CollisionsCircle(Vector2 centre, float radius)
        {
            circleCenter = centre;
            circleRadius = radius;
        }

        public bool ColliderDetector(CollisionsCircle target)
        {
            float distanceBetweenThem =  (float)Math.Sqrt((target.circleCenter.X - circleCenter.X)* (target.circleCenter.X - circleCenter.X) + (target.circleCenter.Y - circleCenter.Y)* (target.circleCenter.Y - circleCenter.Y));
            if (distanceBetweenThem < (circleRadius + target.circleRadius)) //PYTAGORE
            {
                isCollided = true;
            }

            else
            {
                isCollided = false;
            }
            
           return isCollided;
        }

    }
}
