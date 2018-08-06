using System;
using System.Collections.Generic;
using ScrollShooter2D.render;

namespace ScrollShooter2D.managers
{
    public class Stage
    {
        private Dictionary<string, List<RenderObject>> layers;

        /// <summary>
        /// List of RenderObjects ready to be passed to Renderer
        /// </summary>
        public List<RenderObject> RenderReady
        {
            get
            {
                List<RenderObject> renderReady = new List<RenderObject>();

                foreach (KeyValuePair<string,List<RenderObject>> layer in layers)
                {
                    renderReady.AddRange(layer.Value);
                }

                return renderReady;
            }
        }

        public Stage()
        {
            layers = new Dictionary<string, List<RenderObject>>();
        }

        /// <summary>
        /// Adds new layer to stage
        /// </summary>
        /// <param name="layer">Name of layer</param>
        public void AddLayer(string layer)
        {
            if (layers.ContainsKey(layer))
                return;
            
            layers.Add(layer, new List<RenderObject>());
        }

        /// <summary>
        /// Removes layer from stage
        /// </summary>
        /// <param name="layer"></param>
        public void RemoveLayer(string layer)
        {
            if(!layers.ContainsKey(layer))
                throw new Exception("Requested layer does not exist");

            layers.Remove(layer);
        }

        /// <summary>
        /// Removes all render objects from all layers
        /// </summary>
        public void Clear()
        {
            foreach(KeyValuePair<string, List<RenderObject>> layer in layers)
            {
                layer.Value.Clear();
            }

            layers.Clear();
        }

        /// <summary>
        /// Adds render object to specified layer
        /// </summary>
        /// <param name="layer">Name of layer</param>
        /// <param name="renderObject">Render object to be added</param>
        public void AddTo(string layer, RenderObject renderObject)
        {
            if(!layers.ContainsKey(layer))
                throw new Exception("Requested layer does not exist");
            
            layers[layer].Add(renderObject);
        }

        /// <summary>
        /// Adds range of render objects to specified layer
        /// </summary>
        /// <param name="layer">Name of layer</param>
        /// <param name="renderObjects">List of render objects</param>
        public void AddRangeTo(string layer, List<RenderObject> renderObjects)
        {
            if(!layers.ContainsKey(layer))
                throw new Exception("Requested layer does not exist");
            
            layers[layer].AddRange(renderObjects);
        }

        /// <summary>
        /// Removes render object from specified layer
        /// </summary>
        /// <param name="layer">Name of layer</param>
        /// <param name="renderObject">Render object to be removed</param>
        public void RemoveFrom(string layer, RenderObject renderObject)
        {
            if(!layers.ContainsKey(layer))
                throw new Exception("Requested layer does not exist");

            layers[layer].Remove(renderObject);
        }
    }
}