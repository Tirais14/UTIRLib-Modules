using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UTIRLib.Mathematics
{
    public abstract class MathShape2D : IMathShape2D, IEnumerable<Vector2>
    {
        protected readonly bool autoCalculating;
        protected Vector2 centerPoint;
        protected float square;

        public Vector2 CenterPoint => centerPoint;
        public float Square => square;

        protected MathShape2D(bool autoCalculating) => this.autoCalculating = autoCalculating;

        public void Set(params Vector2[] points)
        {
            int arrayIndex = 0;
            foreach (Vector2 point in this) {
                if (points.Length >= arrayIndex) {
                    break;
                }

                point.Set(points[arrayIndex++]);
            }
        }

        public abstract void TranslateTo(Vector2 point);

        public abstract float CalculateSquare();

        public abstract bool IsPointIn(Vector2 point);

        public abstract IEnumerator<Vector2> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected void Recalculate()
        {
            CalculateSquare();
        }

        protected abstract void CalculateCenterPoint();
    }
}
