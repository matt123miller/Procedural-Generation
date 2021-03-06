﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class ProceduralObject : MonoBehaviour
    {

        #region Rect bounds and manipulation

        // Make these ints? 
        internal float Top => transform.position.z + height;
        internal float Bottom => transform.position.z;
        internal float Left => transform.position.x;
        internal float Right => transform.position.x + width;

        public Vector3 Centre
        {
            get
            {
                return _centre;
            }
            set
            {
                _centre = value;
                transform.position = new Vector3( (int)(value.x - width / 2), 0, (int)(value.z - width / 2) );
            }
        }
        public void SetPosition( Vector3 newPos )
        {
            transform.position = newPos;
            _centre = new Vector3( newPos.x + width / 2, 0, newPos.z + width / 2 );
        }

        /// <summary>
        /// This assumes both a and b are sized 1x1 unity unit.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsOverlapping(Vector3 a, Vector3 b)
        {
            bool topLeft = a.x < b.x + 1 && a.x + 1 > b.x;
            bool bottomRight = a.z + 1 > b.z && a.z < b.z + 1;

            return topLeft && bottomRight;
        }

        public static bool IsOverlapping(ProceduralObject a, ProceduralObject b)
        {
            if (!b)
            { return false; }
            bool topLeft = a.Left < b.Right && a.Right > b.Left;
            bool bottomRight = a.Top > b.Bottom && a.Bottom < b.Top;

            return topLeft && bottomRight;
        }

        public bool IsOverlapping(ProceduralObject _other)
        {
            if (!_other)
            { return false; }
            bool topLeft = this.Left < _other.Right && this.Right > _other.Left;
            bool bottomRight = this.Top > _other.Bottom && this.Bottom < _other.Top;
            
            return topLeft && bottomRight;
        }


        #endregion

        // Always treat width as X and height as Y;
        public int width, height;
        [SerializeField]
        protected Vector3 _centre;

        protected GameObject CreateWall( int x, int z )
        {
            var wall = CreatePrimitive( x, z, PrimitiveType.Cube );
            wall.transform.Translate( 0, 0.5f, 0 );
            wall.layer = Layers.Wall;
            wall.name = "Wall";
            return wall;
        }

        protected GameObject CreateDoor( int x, int z )
        {
            var tile = CreatePrimitive( x, z, PrimitiveType.Quad );
            tile.transform.Rotate( 90, 0, 0 );
            tile.layer = Layers.Door;
            tile.name = "Door";
            return tile;
        }

        protected GameObject CreateFloor( int x, int z )
        {
            var tile = CreatePrimitive( x, z, PrimitiveType.Quad );
            tile.transform.Rotate( 90, 0, 0 );
            tile.layer = Layers.Floor;
            tile.name = "Floor";
            return tile;
        }

        protected GameObject CreatePrimitive( int x, int z, PrimitiveType type )
        {
            var primitive = GameObject.CreatePrimitive( type );
            primitive.transform.position = new Vector3( x, 0, z );
            primitive.transform.SetParent( transform );
            return primitive;
        }

    }
}
