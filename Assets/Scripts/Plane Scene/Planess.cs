using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Planes : IFormattable
    {
        #region Variables

        private Vec3 m_Normal;

        private float m_Distance;

        public Vec3 normal
        {
            get
            {
                return m_Normal;
            }
            set
            {
                m_Normal = value;
            }
        }
        //
        // Resumen:
        //     The distance measured from the Plane to the origin, along the Plane's normal.
        public float distance
        {
            get
            {
                return m_Distance;
            }
            set
            {
                m_Distance = value;
            }
        }
        //
        // Resumen:
        //     Returns a copy of the plane that faces in the opposite direction.
        public Planes flipped => new Planes(-m_Normal, 0f - m_Distance);

        #endregion

        #region constants

        internal const int size = 16;


        #endregion

        #region Default Values



        #endregion

        #region Constructors

        public Planes(Vec3 inNormal, Vec3 inPoint)
        {
            m_Normal = Vector3.Normalize(inNormal);

            m_Distance = 0f - Vec3.Dot(m_Normal, inPoint);
        }
        public Planes(Vec3 inNormal, float d)
        {
            m_Normal = Vector3.Normalize(inNormal);
            m_Distance = d;
        }
        public Planes(Vec3 a, Vec3 b, Vec3 c)
        {
            m_Normal = Vector3.Normalize(Vec3.Cross(b - a, c - a));
            m_Distance = 0f - Vec3.Dot(m_Normal, a);
        }

        #endregion

        #region Operators



        #endregion

        #region Functions

        //
        // Resumen:
        //     Sets a plane using a point that lies within it along with a normal to orient
        //     it.
        //
        // Parámetros:
        //   inNormal:
        //     The plane's normal vector.
        //
        //   inPoint:
        //     A point that lies on the plane.
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            m_Normal = Vector3.Normalize(inNormal);
            m_Distance = 0f - Vec3.Dot(inNormal, inPoint);
        }
        //
        // Resumen:
        //     Sets a plane using three points that lie within it. The points go around clockwise
        //     as you look down on the top surface of the plane.
        //
        // Parámetros:
        //   a:
        //     First point in clockwise order.
        //
        //   b:
        //     Second point in clockwise order.
        //
        //   c:
        //     Third point in clockwise order.
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            m_Normal = Vector3.Normalize(Vec3.Cross(b - a, c - a));
            m_Distance = 0f - Vec3.Dot(m_Normal, a);
        }

        //
        // Resumen:
        //     Makes the plane face in the opposite direction.
        public void Flip()
        {
            m_Normal = -m_Normal;
            m_Distance = 0f - m_Distance;
        }

        //
        // Resumen:
        //     Moves the plane in space by the translation vector.
        //
        // Parámetros:
        //   translation:
        //     The offset in space to move the plane with.
        public void Translate(Vec3 translation)
        {
            m_Distance += Vec3.Dot(m_Normal, translation);
        }

        //
        // Resumen:
        //     Returns a copy of the given plane that is moved in space by the given translation.
        //
        // Parámetros:
        //   plane:
        //     The plane to move in space.
        //
        //   translation:
        //     The offset in space to move the plane with.
        //
        // Devuelve:
        //     The translated plane.
        public static Planes Translate(Planes planes, Vec3 translation)
        {
            return new Planes(planes.m_Normal, planes.m_Distance += Vec3.Dot(planes.m_Normal, translation));
        }

        //
        // Resumen:
        //     For a given point returns the closest point on the plane.
        //
        // Parámetros:
        //   point:
        //     The point to project onto the plane.
        //
        // Devuelve:
        //     A point on the plane that is closest to point.
        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            float num = Vec3.Dot(m_Normal, point) + m_Distance;
            return point - m_Normal * num;
        }

        //
        // Resumen:
        //     Returns a signed distance from plane to point.
        //
        // Parámetros:
        //   point:
        public float GetDistanceToPoint(Vec3 point)
        {
            // return Vec3.Dot(m_Normal, point) + m_Distance;
            return (Vec3.Dot(m_Normal, point) + m_Distance) / point.magnitude;
        }

        //
        // Resumen:
        //     Is a point on the positive side of the plane?
        //
        // Parámetros:
        //   point:
        public bool GetSide(Vec3 point)
        {
            //return Vec3.Dot(m_Normal, point) + m_Distance > 0f;
            return GetDistanceToPoint(point) >= 0f;
        }

        //
        // Resumen:
        //     Are two points on the same side of the plane?
        //
        // Parámetros:
        //   inPt0:
        //
        //   inPt1:
        public bool SameSide(Vector3 inPt0, Vector3 inPt1)
        {
            float distanceToPoint = GetDistanceToPoint(inPt0);
            float distanceToPoint2 = GetDistanceToPoint(inPt1);
            return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
        }

        public bool Raycast(Ray ray, out float enter)
        {
            float num = Vector3.Dot(ray.direction, m_Normal);
            float num2 = 0f - Vector3.Dot(ray.origin, m_Normal) - m_Distance;
            if (Mathf.Approximately(num, 0f))
            {
                enter = 0f;
                return false;
            }

            enter = num2 / num;
            return enter > 0f;
        }

        public override string ToString()
        {
            return ToString(null, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        public string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        #endregion

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Empty;
        }
    }

}