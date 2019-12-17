using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ScrollShooter2D.game_entities;
using ScrollShooter2D.managers;
//using ScrollShooter2D.render;
//using ScrollShooter2D.render.primitives_2d;

using ScrollShooter2D.engine.graphics;
using ScrollShooter2D.engine.graphics.primitives_2d;

namespace ScrollShooter2D
{
    public class MainWindow : GameWindow
    {
        private Renderer renderer;

        private Vector2 mousePos;
        
        private int glProgram;
        //private Camera2D cam;
        private OrthographicCamera camera;

        Rectangle testRect;

        public MainWindow() : base(480, 640, GraphicsMode.Default, "Space Shooter")
        { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            glProgram = ShaderLoader.LoadProgram(new List<ShaderSrc>
            {
                new ShaderSrc(ShaderType.FragmentShader, "engine/graphics/shaders/fragment.glsl"),
                new ShaderSrc(ShaderType.VertexShader, "engine/graphics/shaders/vertex.glsl")
            });
            
            GL.UseProgram(glProgram);
            GL.ClearColor(Color4.Black);

            //cam = new Camera2D(480, 640);
            //renderer = new Renderer(glProgram, cam);
            //GameManager.Instance.Renderer = renderer;

            #region Old display objects instantiation
            
            Player player = new Player(40, 50, 10, 0);
            player.SetColor(Color4.LimeGreen);
            player.MoveTo(240, 550);
            
            Enemy enemy = new Enemy(40, 50, 1, 5);
            enemy.SetColor(Color4.Red);
            enemy.MoveTo(150, 20);

            GameManager.Instance.AddEntity(player, StageLayers.Main);
            GameManager.Instance.AddEntity(enemy, StageLayers.Main);

            #endregion

            #region New render system test

            camera = new OrthographicCamera(480, 640);
            renderer = new Renderer(glProgram, camera);

            testRect = new Rectangle(300, 300, Color4.Aqua);
            testRect.Transform.Position = new Vector3(50, 150, -1);
            testRect.Visible = true;
            renderer.StageDraw(testRect);

            #endregion

            //GameManager.Instance.Start();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            Title = $"{(int)(1 / e.Time)} FPS";

            handleGlobalInput();

            //GameManager.Instance.Update((float)e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer.StageDraw(testRect);
            renderer.Draw();
            
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            GL.Viewport(0, 0, Width, Height);
            //TODO: update resolution
            //camera.Resolution = new Vector2( Width, Height);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            testRect.Destroy();
            
            GL.DeleteProgram(glProgram);

            GameManager.Instance.Clear();
        }

        //TODO: move to controls manager
        private void handleGlobalInput()
        {
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Key.P))
                GameManager.Instance.Pause();
            if (kbState.IsKeyDown(Key.O))
                GameManager.Instance.Resume();
        }
    }
}