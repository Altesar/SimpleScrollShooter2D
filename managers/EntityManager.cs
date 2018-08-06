using System;
using System.Collections.Generic;
using ScrollShooter2D.game_entities;
using ScrollShooter2D.render;
using ScrollShooter2D.util;

namespace ScrollShooter2D.managers
{
    internal enum EntityType { Character, Projectile, Unknown }
    
    public class EntityManager
    {
        private List<Character> characters;
        private List<Projectile> projectiles;

        public List<Character> Characters => characters;
        public List<Projectile> Projectiles => projectiles;
        
        public EntityManager()
        {
            characters = new List<Character>();
            projectiles = new List<Projectile>();
        }

        /// <summary>
        /// Adds entity to the game
        /// </summary>
        /// <typeparam name="T">Character or Projectile derivative</typeparam>
        /// <param name="entity">Entity to be added</param>
        public void Add<T>(T entity)
        {
            switch (entityType(entity))
            {
                case EntityType.Character:
                    characters.Add(entity as Character);
                    break;
                case EntityType.Projectile:
                    projectiles.Add(entity as Projectile);
                    break;
                case EntityType.Unknown:
                    throw new Exception("Invalid entity type");
            }
        }

        /// <summary>
        /// Removes entity from the game
        /// </summary>
        /// <typeparam name="T">Character or Projectile derivative</typeparam>
        /// <param name="entity">Entity to be removed</param>
        public void Remove<T>(T entity)
        {
            switch (entityType(entity))
            {
                case EntityType.Character:
                    characters.Remove(entity as Character);
                    break;
                case EntityType.Projectile:
                    projectiles.Remove(entity as Projectile);
                    break;
                case EntityType.Unknown:
                    throw new Exception("Invalid entity type");
            }

        }

        /// <summary>
        /// Removes all entities
        /// </summary>
        public void Clear()
        {
            characters.Clear();
            projectiles.Clear();
        }

        /// <summary>
        /// Returns type of entity
        /// </summary>
        /// <param name="entity">Entity to be checked</param>
        /// <returns>Type of an entity</returns>
        private EntityType entityType<T>(T entity)
        {
            EntityType type;

            if (entity is Character)
                type = EntityType.Character;
            else if (entity is Projectile)
                type = EntityType.Projectile;
            else
                type = EntityType.Unknown;

            return type;
        }

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        public void Update(float deltaTime)
        {
            updateCharacters(deltaTime);
            updateProjectiles(deltaTime);
            
            checkCollisions();
        }

        private void updateCharacters(float deltaTime)
        {
            foreach (Character character in Characters)
            {
                character.Update(deltaTime);
            }
        }

         private void updateProjectiles(float deltaTime)
        {
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Update(deltaTime);
            }
        }

        //TODO: check collisions between characters
        private void checkCollisions()
        {
            foreach (Character character in characters)
            {
                foreach (Projectile projectile in projectiles)
                {
                    if(projectile.TypeChecksEnabled && projectile.ParentType == character.GetType())
                        continue;

                    if (ColliderBox.CollidingAABB(projectile.ColliderBox, character.ColliderBox))
                    {
                        character.OnProjectileHit(projectile);
                        projectile.OnCharacterHit();
                    }
                }
            }
        }
    }
}