using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CustomMath
{
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;
        private static Vec3 zero;

        public float sqrMagnitude { get { return SqrMagnitude(new Vec3(x, y, z)); } }
        public Vector3 normalized { get { throw new NotImplementedException(); } }
        public float magnitude { get { throw new NotImplementedException(); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(scalar * v3.x, scalar * v3.y, scalar * v3.z);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }

        public static implicit operator Vec3(Vector3 v)
        {
            return new Vec3 (v.x, v.y, v.z);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }

        public static float Angle(Vec3 from, Vec3 to)
        {
            float value = (float)Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude); //Calcula la raiz cadradrada de los cuadrados de las magnitudes 
            if (value < 1E-15f) // Se compara el resutado contra kEpsilonNormalSqrt (Un numero muy pequeño)
            {
                return 0f; //En caso de que sea menor se redondea a 0
            }

            float aux = Mathf.Clamp(Dot(from, to) / value, -1f, 1f); //Hace el calculo para conseguir la magnitud normalizada y lo clampea entre 1 y -1
            return (float)Math.Acos(aux) * 57.29578f; //Multiplica el "Acos" del resultado * PI / 3

        } //https://answers.unity.com/questions/1294512/how-vectorangle-works-internally-in-unity.html
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            float num = vector.sqrMagnitude; //Asigna a "num" la magnitud al cuadrado
            if (num > maxLength * maxLength) // verifica contra el maximo al cuadrado
            {
                float num2 = (float)Math.Sqrt(num); //En caso de que sea menor, calcula la raiz cuadrada
                float num3 = vector.x / num2;
                float num4 = vector.y / num2;
                float num5 = vector.z / num2; //Y normaliza los ejes
                return new Vec3(num3 * maxLength, num4 * maxLength, num5 * maxLength); //retorna los valores normalizados multiplicados por el maximo
            }

            return vector;
        }
        public static float Magnitude(Vec3 vector)
        {
            return (float)Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;

            return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vec3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return new Vec3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            return new Vec3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            return new Vec3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            float num = Dot(onNormal, onNormal);

            if (num < Mathf.Epsilon)
            {
                return zero;
            }

            float num2 = Dot(vector, onNormal);
            return new Vec3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            float num = -2f * Dot(inNormal, inDirection);
            return new Vec3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
        }
        public void Set(float newX, float newY, float newZ)
        {
            this = new Vec3(newX, newY, newZ);
        }
        public void Scale(Vec3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }
        public void Normalize()
        {
            float mag = this.magnitude;
            this.x /= mag;
            this.y /= mag;
            this.z /= mag;
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }

}


//Ejercicio 1: SUMA vectores
//Ejercicio 2: RESTA vectores
//Ejercicio 3: Multiplica vectores
//Ejercicio 4: Producto Cruz
//Ejercicio 5: lerp de la trayectoria entre un vector y otro?
//Ejercicio 6: Toma el maximo de cada eje
//Ejercicio 7: Proyeccion
//Ejercicio 8: Suma a y b normalizada y el largo es la distancia entre a y b
//