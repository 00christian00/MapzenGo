﻿using System;
using System.Collections.Generic;
using Assets;
using Assets.Helpers;
using Assets.Models;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;
using Assets.Models.Factories;

public class World : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    private TileManager _tileManager;

    private List<Factory> Factories;

    void Start ()
    {
        Factories = new List<Factory>();
        foreach (var factory in GetComponentsInChildren<Factory>())
        {
            Factories.Add(factory);
        }

        _tileManager = GetComponent<TileManager>();
        _tileManager.Init(Factories, _settings);
	}

    [Serializable]
    public class Settings
    {
        [SerializeField]
        public float Lat = 39.921864f;
        [SerializeField]
        public float Long = 32.818442f;
        [SerializeField]
        public int Range = 3;
        [SerializeField]
        public int DetailLevel = 16;
        [SerializeField]
        public bool LoadImages = false;
        [SerializeField]
        public float TileSize = 100;
        [SerializeField]
        public bool UseLayers;
    }
}
