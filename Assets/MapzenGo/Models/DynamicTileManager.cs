﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Helpers;
using UnityEngine;

namespace Assets.Models
{
    public class DynamicTileManager : TileManager
    {
        [SerializeField]
        private Rect _centerCollider;
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private int _removeAfter;

        public override void Init(List<Factory> factories, World.Settings settings)
        {
            base.Init(factories, settings);
            _removeAfter = Math.Max(_removeAfter, Range * 2 + 1);
            var rect = new Vector2(settings.TileSize, settings.TileSize);
            _centerCollider = new Rect(Vector2.zero - rect / 2 , rect);
        }

        void Update()
        {
            UpdateTiles();
        }

        private void UpdateTiles()
        {
            if (!_centerCollider.Contains(_player.transform.position.ToVector2xz(), true))
            {
                //player movement in TMS tiles
                var tileDif = GetMovementVector();
                //Debug.Log(tileDif);
                //move locals
                Centralize(tileDif);
                //create new tiles
                LoadTiles(CenterTms, CenterInMercator);
                UnloadTiles(CenterTms);
            }
        }

        private void Centralize(Vector2 tileDif)
        {
            //move everything to keep current tile at 0,0
            foreach (var tile in Tiles.Values)
            {
                tile.transform.position -= new Vector3((float) (tileDif.x * TileSize), 0, (float) (-tileDif.y * TileSize));
            }
            CenterTms += tileDif.ToVector2d();
            CenterInMercator = GM.TileBounds(CenterTms, Zoom).Center;
            var difInUnity = new Vector3((float) (tileDif.x * TileSize), 0, (float) (-tileDif.y * TileSize));
            _player.position -= difInUnity;
            Camera.main.transform.position -= difInUnity;
        }

        private void UnloadTiles(Vector2d currentTms)
        {
            var rem = new List<Vector2d>();
            foreach (var key in Tiles.Keys.Where(x => x.ManhattanTo(currentTms) > _removeAfter))
            {
                rem.Add(key);
                Destroy(Tiles[key].gameObject);
            }
            foreach (var v in rem)
            {
                Tiles.Remove(v);
            }
        }

        private Vector2 GetMovementVector()
        {
            var dif = _player.transform.position.ToVector2xz();
            var tileDif = Vector2.zero;
            if (dif.x < Math.Min(_centerCollider.xMin, _centerCollider.xMax))
                tileDif.x = -1;
            else if (dif.x > Math.Max(_centerCollider.xMin, _centerCollider.xMax))
                tileDif.x = 1;

            if (dif.y < Math.Min(_centerCollider.yMin, _centerCollider.yMax))
                tileDif.y = 1;
            else if (dif.y > Math.Max(_centerCollider.yMin, _centerCollider.yMax))
                tileDif.y = -1; //invert axis  TMS vs unity
            return tileDif;
        }
    }
}
