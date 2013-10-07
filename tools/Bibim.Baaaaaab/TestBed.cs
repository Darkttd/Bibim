﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bibim.Asset.Pipeline;
using Bibim.Asset.Pipeline.Recipes;

namespace Bibim.Bab
{
    public sealed class TestBed
    {
        public static void Main(string[] args)
        {
            TimelineFX();
        }

        public static void TimelineFX()
        {
            (new ImportTimelineFX(@"E:\Works\Halak\TouhouBreak\Game\Asset\GFX\Hello.eff")).Cook(new CookingContext());
        }
    }
}
