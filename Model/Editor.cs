﻿using System;
using System.Collections.Generic;
using LevelEditor.Model.Commands;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace LevelEditor.Model
{
    public class Editor
    {
        public struct Selection
        {
            public int X;
            public int Y;
        }

        public int SelectedTileId { get; set; }
        public Tile SelectedTile { get; set; }
        private ImageSource[] _images;
    
        private readonly Map _map;
        public List<Selection> _selectedTiles;
        private CommandController _commandController;

        public Editor()
        {
            _commandController = new CommandController();
            _selectedTiles = new List<Selection>();
            _images = new ImageSource[MainModel.ImgPaths.Count];

            for (int i = 0; i < MainModel.ImgPaths.Count; i++)
            {
                _images[i] = new BitmapImage(new Uri(MainModel.ImgPaths[i], UriKind.Relative));
                _images[i].Freeze();
            }
            _map = new Map(100, 100, _images[0]);
            SelectedTileId = 0;
            SelectedTile = new Tile(GetSelectedTileImage(), 0, 0, 0);
        }

        /// <summary>
        /// Performs the action of the currently selected tool.
        /// </summary>
        public void PerformAction()
        {
            foreach(Selection t in _selectedTiles)
            {
                SetTile(t.X, t.Y, SelectedTileId);
            }
        }
        public void SelectTile(int x, int y)
        {
            Selection sel;
            sel.X = x;
            sel.Y = y;

            if (_selectedTiles.Exists( (element) => { return element.X == x && element.Y == y; }))
                return;
            _selectedTiles.Add(sel);
        }
        public ImageSource GetSelectedTileImage()
        {
            return _images[SelectedTileId];
        }
        // Methods for the command controller
        /// <summary>
        /// Redoes an action if there is one to redo.
        /// </summary>
        public void Redo()
        {
            _commandController.Execute();
        }
        /// <summary>
        /// Undoes an action if there is one to undo.
        /// </summary>
        public void Undo()
        {
            _commandController.Undo();
        }
        // Methods for interacting with the map.
        public Tile GetTile(int x, int y)
        {
            return _map.GetTile(x, y);
        }
        public void SetTile(int x, int y, int id)
        {
            _map.SetTile(x, y, _images[id]);
        }
        public int GetMapWidth()
        {
            return _map.Width;
        }
        public int GetMapHeight()
        {
            return _map.Height;
        }
        public short GetTileSize()
        {
            return _map.TileSize;
        }
    }
}
