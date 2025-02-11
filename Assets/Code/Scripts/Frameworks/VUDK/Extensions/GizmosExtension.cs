﻿namespace VUDK.Extensions
{
    using UnityEngine;

    public static class GizmosExtension
    {
        public static void DrawArrowRay(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void DrawArrow(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawLine(from, to);

            Vector3 direction = to - from;
            Vector3 arrowEnd = to - direction.normalized * arrowHeadLength;

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;
            Gizmos.DrawRay(arrowEnd, right * arrowHeadLength);
            Gizmos.DrawRay(arrowEnd, left * arrowHeadLength);
        }

        /// <summary>
		/// Draws a wire cube with a given rotation
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		/// <param name="rotation"></param>
		public static void DrawWireCube(Vector3 center, Vector3 size, Quaternion rotation = default)
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, size);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws a cube with a given rotation
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="rotation"></param>
        public static void DrawCube(Vector3 center, Vector3 size, Quaternion rotation = default)
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, size);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = old;
        }

        public static void DrawWireSphere(Vector3 center, float radius, Quaternion rotation = default)
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws a flat wire circle (up)
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="segments"></param>
        /// <param name="rotation"></param>
        public static void DrawWireCircle(Vector3 center, float radius, int segments = 20, Quaternion rotation = default)
        {
            DrawWireArc(center, radius, 360, segments, rotation);
        }

        /// <summary>
        /// Draws an arc with a rotation around the center
        /// </summary>
        /// <param name="center">center point</param>
        /// <param name="radius">radiu</param>
        /// <param name="angle">angle in degrees</param>
        /// <param name="segments">number of segments</param>
        /// <param name="rotation">rotation around the center</param>
        public static void DrawWireArc(Vector3 center, float radius, float angle, int segments = 20, Quaternion rotation = default)
        {
            var old = Gizmos.matrix;

            // Check if the rotation is the default Quaternion (identity), and if so, set it to Quaternion.identity
            if (rotation.Equals(default))
                rotation = Quaternion.identity;

            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Vector3 from = Vector3.forward * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
                Gizmos.DrawLine(from, to);
                from = to;
            }

            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws an arc with a rotation around an arbitraty center of rotation
        /// </summary>
        /// <param name="center">the circle's center point</param>
        /// <param name="radius">radius</param>
        /// <param name="angle">angle in degrees</param>
        /// <param name="segments">number of segments</param>
        /// <param name="rotation">rotation around the centerOfRotation</param>
        /// <param name="centerOfRotation">center of rotation</param>
        public static void DrawWireArc(Vector3 center, float radius, float angle, int segments, Quaternion rotation, Vector3 centerOfRotation)
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(centerOfRotation, rotation, Vector3.one);
            var deltaTranslation = centerOfRotation - center;
            Vector3 from = deltaTranslation + Vector3.forward * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad)) + deltaTranslation;
                Gizmos.DrawLine(from, to);
                from = to;
            }

            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws an arc with a rotation around an arbitraty center of rotation
        /// </summary>
        /// <param name="matrix">Gizmo matrix applied before drawing</param>
        /// <param name="radius">radius</param>
        /// <param name="angle">angle in degrees</param>
        /// <param name="segments">number of segments</param>
        public static void DrawWireArc(Matrix4x4 matrix, float radius, float angle, int segments)
        {
            var old = Gizmos.matrix;
            Gizmos.matrix = matrix;
            Vector3 from = Vector3.forward * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
                Gizmos.DrawLine(from, to);
                from = to;
            }

            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws a wire cylinder face up with a rotation around the center
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        public static void DrawWireCylinder(Vector3 center, float radius, float height, Quaternion rotation = default)
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            var half = height / 2;

            //draw the 4 outer lines
            Gizmos.DrawLine(Vector3.right * radius - Vector3.up * half, Vector3.right * radius + Vector3.up * half);
            Gizmos.DrawLine(-Vector3.right * radius - Vector3.up * half, -Vector3.right * radius + Vector3.up * half);
            Gizmos.DrawLine(Vector3.forward * radius - Vector3.up * half, Vector3.forward * radius + Vector3.up * half);
            Gizmos.DrawLine(-Vector3.forward * radius - Vector3.up * half, -Vector3.forward * radius + Vector3.up * half);

            //draw the 2 cricles with the center of rotation being the center of the cylinder, not the center of the circle itself
            DrawWireArc(center + Vector3.up * half, radius, 360, 20, rotation, center);
            DrawWireArc(center + Vector3.down * half, radius, 360, 20, rotation, center);
            Gizmos.matrix = old;
        }

        /// <summary>
        /// Draws a wire capsule face up
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        public static void DrawWireCapsule(Vector3 center, float radius, float height, Quaternion rotation = default)
        {
            if (rotation.Equals(default))
                rotation = Quaternion.identity;
            var old = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            var half = height / 2 - radius;

            //draw cylinder base
            DrawWireCylinder(center, radius, height - radius * 2, rotation);

            //draw upper cap
            //do some cool stuff with orthogonal matrices
            var mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.forward));
            DrawWireArc(mat, radius, 180, 20);
            mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward));
            DrawWireArc(mat, radius, 180, 20);

            //draw lower cap
            mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.forward));
            DrawWireArc(mat, radius, 180, 20);
            mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(-90, Vector3.forward));
            DrawWireArc(mat, radius, 180, 20);

            Gizmos.matrix = old;
        }

        public static void DrawCameraFrustum(Transform transform, float fov, float near, float far, float aspect)
        {
            float halfHeightNear = Mathf.Tan(Mathf.Deg2Rad * fov * 0.5f) * near;
            float halfWidthNear = halfHeightNear * aspect;

            float halfHeightFar = Mathf.Tan(Mathf.Deg2Rad * fov * 0.5f) * far;
            float halfWidthFar = halfHeightFar * aspect;
            Vector3 direction = transform.forward;

            Vector3 nearTopLeft = transform.position + direction * near - transform.up * halfHeightNear + transform.right * halfWidthNear;
            Vector3 nearTopRight = transform.position + direction * near + transform.up * halfHeightNear + transform.right * halfWidthNear;
            Vector3 nearBottomLeft = transform.position + direction * near - transform.up * halfHeightNear - transform.right * halfWidthNear;
            Vector3 nearBottomRight = transform.position + direction * near + transform.up * halfHeightNear - transform.right * halfWidthNear;

            Vector3 farTopLeft = transform.position + direction * far - transform.up * halfHeightFar + transform.right * halfWidthFar;
            Vector3 farTopRight = transform.position + direction * far + transform.up * halfHeightFar + transform.right * halfWidthFar;
            Vector3 farBottomLeft = transform.position + direction * far - transform.up * halfHeightFar - transform.right * halfWidthFar;
            Vector3 farBottomRight = transform.position + direction * far + transform.up * halfHeightFar - transform.right * halfWidthFar;

            Gizmos.DrawLine(nearTopLeft, nearTopRight);
            Gizmos.DrawLine(nearTopRight, nearBottomRight);
            Gizmos.DrawLine(nearBottomRight, nearBottomLeft);
            Gizmos.DrawLine(nearBottomLeft, nearTopLeft);

            Gizmos.DrawLine(farTopLeft, farTopRight);
            Gizmos.DrawLine(farTopRight, farBottomRight);
            Gizmos.DrawLine(farBottomRight, farBottomLeft);
            Gizmos.DrawLine(farBottomLeft, farTopLeft);

            Gizmos.DrawLine(nearTopLeft, farTopLeft);
            Gizmos.DrawLine(nearTopRight, farTopRight);
            Gizmos.DrawLine(nearBottomRight, farBottomRight);
            Gizmos.DrawLine(nearBottomLeft, farBottomLeft);
        }
    }
}