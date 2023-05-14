using KevinCastejon.GridHelper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grid2DHelper.APIDemo.ExtractionDemo
{
    public enum DemoType
    {
        EXTRACT_CIRCLE,
        EXTRACT_CIRCLE_OUTLINE,
        EXTRACT_RECTANGLE,
        EXTRACT_RECTANGLE_OUTLINE,
    }
    public class Grid2DMap : MonoBehaviour
    {
        [SerializeField] private int _radius = 2;
        [SerializeField] private Vector2Int _size = Vector2Int.one * 2;

        private Camera _camera;
        private Tile[,] _map = new Tile[21, 24];
        private Tile[] _extractedTiles;
        private Tile _targetTile;
        private DemoType _demoType;
        public DemoType DemoType
        {
            get
            {
                return _demoType;
            }

            set
            {
                if (_demoType == value)
                {
                    return;
                }
                _demoType = value;
                Extract();
            }
        }
        public int Radius
        {
            get
            {
                return _radius;
            }

            set
            {
                _radius = value;
                Extract();
            }
        }
        public void SetDemoType(int demoType)
        {
            DemoType = (DemoType)demoType;
        }
        public void SetCuboidSizeX(int value)
        {
            _size.x = value;
            Extract();
        }
        public void SetCuboidSizeY(int value)
        {
            _size.y = value;
            Extract();
        }
        private void Awake()
        {
            _camera = Camera.main;
            Tile[] tiles = FindObjectsOfType<Tile>();
            foreach (Tile tile in tiles)
            {
                tile.X = Mathf.RoundToInt(tile.transform.position.x);
                tile.Y = Mathf.RoundToInt(tile.transform.position.y);
                _map[tile.Y, tile.X] = tile;
            }
            _targetTile = _map[4, 8];
            _targetTile.TileMode = TileMode.TARGET;
            Extract();
        }
        private void Update()
        {
            // Detecting click on tile
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // Retrieving the Tile component
                Tile tile = hit.collider.GetComponent<Tile>();

                // If middle-click
                if (Input.GetMouseButton(2))
                {
                    SetStart(tile);
                }
            }
        }
        private void SetStart(Tile tile)
        {
            _targetTile.TileMode = TileMode.FLOOR;
            _targetTile = tile;
            Extract();
            _targetTile.TileMode = TileMode.TARGET;
        }
        private void Extract()
        {
            ClearTiles();
            switch (_demoType)
            {
                case DemoType.EXTRACT_CIRCLE:
                    ExtractCircle();
                    break;
                case DemoType.EXTRACT_CIRCLE_OUTLINE:
                    ExtractCircleOutline();
                    break;
                case DemoType.EXTRACT_RECTANGLE:
                    ExtractRectangle();
                    break;
                case DemoType.EXTRACT_RECTANGLE_OUTLINE:
                    ExtractCuboidOutline();
                    break;
                default:
                    break;
            }
        }
        private void ClearTiles()
        {
            if (_extractedTiles != null)
            {
                foreach (Tile tile in _extractedTiles)
                {
                    tile.TileMode = TileMode.FLOOR;
                }
            }
        }
        private void ExtractCircle()
        {
            _extractedTiles = Extraction.GetWalkableTilesInACircle(_map, _targetTile, _radius, false);
            foreach (Tile tile in _extractedTiles)
            {
                tile.TileMode = TileMode.EXTRACTED;
            }
        }
        private void ExtractCircleOutline()
        {
            _extractedTiles = Extraction.GetWalkableTilesOnACircleOutline(_map, _targetTile, _radius);
            foreach (Tile tile in _extractedTiles)
            {
                tile.TileMode = TileMode.EXTRACTED;
            }
        }
        private void ExtractRectangle()
        {
            _extractedTiles = Extraction.GetWalkableTilesInARectangle(_map, _targetTile, _size, false);
            foreach (Tile tile in _extractedTiles)
            {
                tile.TileMode = TileMode.EXTRACTED;
            }
        }
        private void ExtractCuboidOutline()
        {
            _extractedTiles = Extraction.GetWalkableTilesOnARectangleOutline(_map, _targetTile, _size);
            foreach (Tile tile in _extractedTiles)
            {
                tile.TileMode = TileMode.EXTRACTED;
            }
        }
    }
}