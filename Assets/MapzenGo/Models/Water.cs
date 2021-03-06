﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Helpers;
using Assets.MapzenGo.Models.Enums;
using UnityEngine;

namespace Assets.Models
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Water : MonoBehaviour
    {
        public string Id;
        public string Type;
        public string Kind;
        public string Name;
        public int SortKey;
        
        [Serializable]
        public class Settings
        {
            public WaterSettings Default = new WaterSettings();
            public List<WaterSettings> AllSettings;

            public WaterSettings GetSettingsFor(WaterType type)
            {
                return AllSettings.FirstOrDefault(x => x.Type == type) ?? Default;
            }
        }

        [Serializable]
        public class WaterSettings
        {
            public WaterType Type;
            public Material Material;
        }
    }
}
