using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting.Channels;
using System.Windows.Markup;
using OpenTK;
using ScrollShooter2D.render;

namespace ScrollShooter2D.managers
{
    public enum GameStage {Level = 0, Menu} //TODO: implement menu stage
    public enum StageLayers {Background = 0, Main, Ui}
    public enum GameState {Stopped, Paused, Running, Undefined}
    
    public class GameManager
    {
        private GameState gameState = GameState.Stopped;
        public GameState GameState => gameState;

        private List<Stage> stages;
        
        private readonly ScreenManager screenManager;
        private readonly EntityManager entityManager;
        
        public Renderer Renderer { get; set; }

        private static GameManager instance;
        public  static GameManager Instance => instance ?? (instance = new GameManager());

        private readonly List<RenderObject> removalQue;
        
        private readonly string[] layerNames = {"background", "main", "ui"};
        
        private GameManager()
        {
            //TODO: stage acces methods
            screenManager = new ScreenManager();
            stages = new List<Stage>();
            
            stages.Add(new Stage());
            stages[(int)GameStage.Level].AddLayer(layerNames[(int)StageLayers.Background]);
            stages[(int)GameStage.Level].AddLayer(layerNames[(int)StageLayers.Main]);
            stages[(int)GameStage.Level].AddLayer(layerNames[(int)StageLayers.Ui]);
            
            screenManager.AddStage("game", stages[(int)GameStage.Level]);
            
            entityManager = new EntityManager();
            removalQue = new List<RenderObject>();
            
            Renderer = null;

            //TODO: remove from constructor
            //Start();
        }

        public void Update(float deltaTime)
        {
            removeEntities();

            switch (gameState)
            {
                case GameState.Running:
                    entityManager.Update(deltaTime);
                    break;
                case GameState.Paused:
                    //TODO: show Pause screen
                    break;
                case GameState.Stopped:
                    //TODO: show GameOver screen
                    break;
            }
            
            stageGlogalDraw();
        }

        #region Flow control
        
        public void Start()
        {
            //TODO: run initialization
            gameState = GameState.Running;
        }

        public void Stop()
        {
            gameState = GameState.Stopped;
        }

        public void TogglePause()
        {
            if(gameState == GameState.Paused)
                gameState = GameState.Running;
            else if(gameState == GameState.Running)
                gameState = GameState.Paused;
        }

        public void Pause()
        {
            if(gameState == GameState.Running)
                gameState = GameState.Paused;
        }

        public void Resume()
        {
            if(gameState == GameState.Paused)
                gameState = GameState.Running;
        }

        #endregion
        
        #region Entity management

        public EntityManager EntityManager => entityManager;

        /// <summary>
        /// Adds entity to specified stage layer
        /// </summary>
        /// <typeparam name="T">Game entity</typeparam>
        /// <param name="entity">Entity to be added</param>
        /// <param name="layer">Layer entity should be added to</param>
        public void AddEntity<T>(T entity, StageLayers layer)
        {
            try
            {
                entityManager.Add(entity);
                stages[(int)GameStage.Level].AddTo(layerNames[(int)layer], entity as RenderObject);
            }
            catch (Exception e)
            {
                Console.WriteLine("Passed entity is not of valid type");
            }
        }

        /// <summary>
        /// Stages entity removal from specifyed layer
        /// </summary>
        /// <typeparam name="T">Game entity</typeparam>
        /// <param name="entity">Entity to be removed</param>
        /// <param name="layer">Layer entity should be removed from</param>
        public void RemoveEntity<T>(T entity , StageLayers layer)
        {
            //TODO: remove entities from other layers
            removalQue.Add(entity as RenderObject);
        }

        /// <summary>
        /// Actually removes entities
        /// </summary>
        private void removeEntities()
        {
            foreach (RenderObject entity in removalQue)
            {
                stages[(int)GameStage.Level].RemoveFrom(layerNames[(int)StageLayers.Main], entity);
                entityManager.Remove(entity);
            }
           
            removalQue.Clear();
        }

        /// <summary>
        /// Clears all entities and sets game state to Undefined
        /// </summary>
        public void Clear()
        {
            gameState = GameState.Undefined;

            entityManager.Clear();
            screenManager.Clear();
        }

        #endregion

        #region Display
        
        private void stageGlogalDraw()
        {
            if(Renderer == null)
                throw new Exception("Game Manager has no bound renderer");
            
            Renderer.StageDraw(screenManager.RenderReady);
        }

        #endregion
    }
}