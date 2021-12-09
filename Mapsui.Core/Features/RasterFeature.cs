﻿
using System;

namespace Mapsui.Layers
{
    public class RasterFeature : BaseFeature, IFeature, IDisposable
    {
#pragma warning disable IDISP008
        public MRaster? Raster { get; }
#pragma warning restore IDISP008
        public MRect? Extent => Raster;

        public RasterFeature(RasterFeature rasterFeature) : base(rasterFeature)
        {
            Raster = rasterFeature.Raster == null ? null : new MRaster(rasterFeature.Raster);
        }

        public RasterFeature(MRaster? raster)
        {
            Raster = raster;
        }

        public void CoordinateVisitor(Action<double, double, CoordinateSetter> visit)
        {
            if (Raster != null)
                foreach (var point in new[] { Raster.Min, Raster.Max })
                    visit(point.X, point.Y, (x, y) => {
                        point.X = x;
                        point.Y = x;
                    });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Raster?.Dispose();
            }
        }
    }
}
