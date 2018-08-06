using System.Collections.Generic;
using ScrollShooter2D.render;

namespace ScrollShooter2D.managers
{
    public class ScreenManager
    {
        private Dictionary<string, Stage> stages;

        /// <summary>
        /// List of RenderObjects ready to be passed to Renderer
        /// </summary>
        public List<RenderObject> RenderReady {
            get 
            {
                List<RenderObject> renderReady = new List<RenderObject>();
            
                foreach (KeyValuePair<string,Stage> stage in stages)
                {
                    renderReady.AddRange(stage.Value.RenderReady);
                }

                return renderReady;
                
            }
        }

        public ScreenManager()
        {
            stages = new Dictionary<string, Stage>();
        }

        /// <summary>
        /// Adds new stage
        /// </summary>
        /// <param name="stage">Name of a new stage</param>
        public void AddStage(string stage)
        {
            if (stages.ContainsKey(stage))
                return;
            
            stages.Add(stage, new Stage());
        }
        
        /// <summary>
        /// Adds new stage
        /// </summary>
        /// <param name="name">Name of a stage</param>
        /// <param name="stage">Stage object to be added</param>
        public void AddStage(string name, Stage stage)
        {
            if (stages.ContainsKey(name))
                return;
            
            stages.Add(name, stage);
        }

        /// <summary>
        /// Removes stage
        /// </summary>
        /// <param name="stage">Name of stage to be removed</param>
        /// <returns>Removed stage object</returns>
        public Stage RemoveStage(string stage)
        {
            if (!stages.ContainsKey(stage))
                return null;

            Stage removed = stages[stage];

            stages.Remove(stage);

            return removed;
        }

        /// <summary>
        /// Returns stage of specified name
        /// </summary>
        /// <param name="stage">Name of a stage</param>
        /// <returns></returns>
        public Stage GetStage(string stage)
        {
            if (!stages.ContainsKey(stage))
                return null;

            return stages[stage];
        }


        /// <summary>
        /// Removes all entities
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string, Stage> stage in stages)
            {
                stage.Value.Clear();
            }

            stages.Clear();
        }
    }
}