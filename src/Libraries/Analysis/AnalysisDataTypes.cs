﻿using System.Collections.Generic;

using Autodesk.DesignScript.Geometry;

namespace Analysis
{
    public class SurfaceAnalysisData : ISurfaceAnalysisData<UV, double>
    {
        public Surface Surface { get; set; }
        public IEnumerable<UV> CalculationLocations { get; set; }
        public Dictionary<string, IList<double>> Results { get; set; }

        public SurfaceAnalysisData(Surface surface, IEnumerable<UV> calculationLocations)
        {
            Surface = surface;
            CalculationLocations = CullCalculationLocations(surface, calculationLocations);
            Results = new Dictionary<string, IList<double>>();
        }

        public SurfaceAnalysisData(Surface surface, IEnumerable<UV> calculationLocations, Dictionary<string, IList<double>> results)
        {
            Surface = surface;
            CalculationLocations = CullCalculationLocations(surface, calculationLocations);
            Results = results;
        }

        /// <summary>
        /// Cull calculation locations that aren't within 1e-6 of the surface.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<UV> CullCalculationLocations(Surface surface, IEnumerable<UV> calculationLocations)
        {
            var pts = new List<UV>();

            foreach (var uv in calculationLocations)
            {
                var pt = surface.PointAtParameter(uv.U, uv.V);
                var dist = pt.DistanceTo(surface);
                if (dist < 1e-6 && dist > -1e-6)
                {
                    pts.Add(uv);
                }
            }

            return pts;
        }
    }

    public class VectorAnalysisData : IAnalysisData<Point, Vector>
    {
        public IEnumerable<Point> CalculationLocations { get; set; }
        public Dictionary<string, IList<Vector>> Results { get; set; }

        public VectorAnalysisData(IEnumerable<Point> points)
        {
            CalculationLocations = points;
            Results = new Dictionary<string, IList<Vector>>();
        }

        public VectorAnalysisData(IEnumerable<Point> points, Dictionary<string, IList<Vector>> results)
        {
            CalculationLocations = points;
            Results = results;
        }
    }

    public class PointAnalysisData : IAnalysisData<Point, double>
    {
        public IEnumerable<Point> CalculationLocations { get; set; }
        public Dictionary<string, IList<double>> Results { get; set; }

        public PointAnalysisData(IEnumerable<Point> points)
        {
            CalculationLocations = points;
            Results = new Dictionary<string, IList<double>>();
        }

        public PointAnalysisData(IEnumerable<Point> points, Dictionary<string, IList<double>> results)
        {
            CalculationLocations = points;
            Results = results;
        }
    }
}