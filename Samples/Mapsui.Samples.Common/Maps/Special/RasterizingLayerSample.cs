﻿using System;
using System.Collections.Generic;
using Mapsui.Layers;
using Mapsui.Layers.Tiling;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.UI;

#pragma warning disable IDISP004 // Don't ignore created IDisposable

namespace Mapsui.Samples.Common.Maps
{
    public class RasterizingLayerSample : ISample
    {
        public string Name => "Rasterizing Layer";
        public string Category => "Special";

        public void Setup(IMapControl mapControl)
        {
            mapControl.Map = CreateMap(mapControl.PixelDensity);
        }

        public static Map CreateMap(float pixelDensity)
        {
            var map = new Map();
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            map.Layers.Add(new RasterizingLayer(CreateRandomPointLayer(), pixelDensity: pixelDensity));
            var extent = map.Layers[1].Extent!.Grow(map.Layers[1].Extent!.Width * 0.1);
            map.Home = n => n.NavigateTo(extent);
            return map;
        }

        private static MemoryLayer CreateRandomPointLayer()
        {
            var rnd = new Random(3462); // Fix the random seed so the features don't move after a refresh
            var features = new List<IFeature>();
            for (var i = 0; i < 100; i++)
            {
                features.Add(new PointFeature(new MPoint(rnd.Next(0, 5000000), rnd.Next(0, 5000000))));
            }
            var provider = new MemoryProvider<IFeature>(features);

            return new MemoryLayer
            {
                DataSource = provider,
                Style = new SymbolStyle
                {
                    SymbolType = SymbolType.Triangle,
                    Fill = new Brush(Color.Red)
                }
            };
        }
    }
}