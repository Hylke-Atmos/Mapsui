﻿using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Utilities;

#pragma warning disable IDISP001 // Dispose created

namespace Mapsui.Samples.Common.Maps
{
    public class GeodanOfficesSample
    {
        public static MemoryLayer CreateLayer()
        {
            var geodanAmsterdam = new Geometries.Point(122698, 483922);
            var geodanDenBosch = new Geometries.Point(148949, 411446);
            var imageStream = EmbeddedResourceLoader.Load("Images.location.png", typeof(GeodanOfficesSample));

            var layer = new MemoryLayer
            {
                DataSource = new MemoryProvider<IFeature>(new[] { geodanAmsterdam, geodanDenBosch }.ToFeatures()),
                Style = new SymbolStyle
                {
                    BitmapId = BitmapRegistry.Instance.Register(imageStream),
                    SymbolOffset = new Offset { Y = 64 },
                    SymbolScale = 0.25
                },
                Name = "Geodan Offices"
            };
            return layer;
        }
    }
}