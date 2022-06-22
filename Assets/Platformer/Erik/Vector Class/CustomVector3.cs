using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace UnityIntro.Erik.Platformer
{
    public struct CustomVector3
    {
        public CustomVector3(float _x = 0, float _y = 0, float _z = 0){
            x = _x;
            y = _y;
            z = _z;
        }
        public CustomVector3(Vector3 vec3){
            x = vec3.x;
            y = vec3.y;
            z = vec3.z;
        }
        //Axis Values
        public float x;
        public float y;
        public float z;

        #region Math Operators
        public static CustomVector3 operator +(CustomVector3 current){
            return new CustomVector3(current.x, current.y, current.z);
        }
        public static CustomVector3 operator -(CustomVector3 current){
            return new CustomVector3(-current.x, -current.y, -current.z);
        }
        public static CustomVector3 operator +(CustomVector3 current, CustomVector3 incoming){
            return new CustomVector3(current.x + incoming.x, current.y + incoming.y, current.z + incoming.z);
        }
        public static CustomVector3 operator -(CustomVector3 current, CustomVector3 incoming){
            return current + (-incoming);
        }
        public static CustomVector3 operator *(CustomVector3 current, float incoming){
            return new CustomVector3(current.x * incoming, current.y * incoming, current.z * incoming);
        }
        public static CustomVector3 operator /(CustomVector3 current, float incoming){
            if (incoming == 0)
                throw new DivideByZeroException();
            return new CustomVector3(current.x / incoming, current.y / incoming, current.z / incoming);
        }
        #endregion

        #region Conversion Operators
        public static implicit operator Vector3(CustomVector3 customVec3) => new Vector3(customVec3.x, customVec3.y, customVec3.z);
        public static explicit operator CustomVector3(Vector3 inbuiltVec3) => new CustomVector3(inbuiltVec3);
        #endregion

        #region functions 
        public static float Distance(CustomVector3 a, CustomVector3 b){
            return Mathf.Sqrt(
                (b.x - a.x) * (b.x - a.x) +
                (b.y - a.y) * (b.y - a.y) + 
                (b.z - a.z) * (b.z- a.z)
            );
        }
        public float Distance(CustomVector3 b){
            return Mathf.Sqrt(
                (b.x - x) * (b.x - x) +
                (b.y - y) * (b.y - y) + 
                (b.z - z) * (b.z - z)
            );
        }
        public float Length{
            get => (float)Math.Sqrt(x * x + y * y + z * z);
        }
        public float SqrLength{
            get => (x * x + y * y + z * z);
        }
       
        
        public CustomVector3 getClosestPoint(ICollection<CustomVector3> inp){
            CustomVector3 Vector = new();
            float dist = float.MaxValue;
            foreach (CustomVector3 item in inp){
                if(Distance(item) < dist){
                    dist = Distance(item);
                    Vector = item;
                }
            }
            return Vector;
        }

        public CustomVector3 getFurthestPoint(ICollection<CustomVector3> inp){
            CustomVector3 Vector = new();
            float dist = float.MinValue;
            foreach (CustomVector3 item in inp){
                if(Distance(item) > dist){
                    dist = Distance(item);
                    Vector = item;
                }
            }
            return Vector;
        }
        #endregion

        #region Statics
        public static CustomVector3 Up => new CustomVector3(0,1,0);
        #endregion

        #region rotation
        public CustomVector3 RotateAroundY(float degrees){
            x = (x * Mathf.Cos(degrees)) + (z * Mathf.Sin(degrees));
            //y = y;
            z = (-x * Mathf.Sin(degrees)) + (z * Mathf.Cos(degrees));
            return this;
        }
        public CustomVector3 RotateAroundX(float degrees){
            //x = x;
            y = (y * Mathf.Cos(degrees)) - (z * Mathf.Sin(degrees));
            z = (y * Mathf.Sin(degrees)) + (z * Mathf.Cos(degrees));
            return this;
        }
        public CustomVector3 RotateAroundZ(float degrees){
            x = (x * Mathf.Cos(degrees)) - (y * Mathf.Sin(degrees));
            y = (x * Mathf.Sin(degrees)) + (y * Mathf.Cos(degrees));
            //z = z;
            return this;
        }
        #endregion
    }
}
