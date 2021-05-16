﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class PursuerEnemyBehavior : EnemyBehavior
    {
        Timer timer = new Timer(2000);
        private Vector2 strafeVector = Vector2.Zero;
        private bool returning = false;

        public override void Update(GameTime gameTime, float rotation)
        {
            enemy.FireWeapon(gameTime);
            enemy.AdjustAngleTowardsTarget(EntityManager.Player.Position);
            Vector2 position = enemy.Position;
            if (Vector2.Distance(position, enemy.startingPosition) > enemy.leashDistance)
            {
                returning = true;
            }

            if (returning)
            {
                enemy.direction = Vector2.Normalize(enemy.startingPosition - position);
                if (Vector2.Distance(position, enemy.startingPosition) <= 10)
                {
                    returning = false;
                }
            }
            else
            {

                if (Vector2.Distance(position, EntityManager.Player.Position) > 100)
                {
                    enemy.direction = Pursue(position);
                }
                else if (Vector2.Distance(position, EntityManager.Player.Position) < 50)
                {
                    enemy.direction = Retreat(rotation);
                }
                else
                {
                    enemy.direction = Strafe(gameTime, rotation);
                }
            }
        }

        public Vector2 Pursue(Vector2 position) // Moves toward the player
        {
            //direction = Vector2.Zero;
            Vector2 vectorResult = EntityManager.Player.Position - position;
            vectorResult.Normalize();
            return vectorResult;
        }

        private void Roam() // Moves following a set / random path
        {


        }

        private Vector2 Retreat(float rotation) // Moves away from the player
        {
            Vector2 vectorResult = new Vector2((float)Math.Cos(rotation + (float)Math.PI / 2), (float)Math.Sin(rotation + (float)Math.PI / 2));
            vectorResult.Normalize();
            return vectorResult;
        }

        private Vector2 Strafe(GameTime gameTime, float rotation) // Moves horizontally in relation to the facing direction
        {

            if (timer.IsDone(gameTime))
            {
                //direction = Vector2.Zero;
                double rand = Game1.random.NextDouble();
                strafeVector = Vector2.Zero;
                if (rand > 0.49)
                {
                    strafeVector = new Vector2((float)Math.Cos(rotation + (float)Math.PI), (float)Math.Sin(rotation + (float)Math.PI));
                }
                else
                {
                    strafeVector = new Vector2((float)Math.Cos(rotation + (float)Math.PI * 2), (float)Math.Sin(rotation + (float)Math.PI * 2));
                }

                strafeVector.Normalize();
                timer.Reset(gameTime);
                return strafeVector;
            }


            return strafeVector;
        }
    }
}