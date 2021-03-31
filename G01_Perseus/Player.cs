using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace G01_Perseus
{
    public class Player : Entity
    {
        private const int INVENTORY_SIZE = 20;
        private const int ACTIVE_EQUIPMENT_SLOTS = 4;
        public static Vector2 Position { get; private set; }

        private Vector2 friction = new Vector2(0.9955f, 0.9955f);
        private Vector2 maxVelocity = new Vector2(250f, 250f);
        private Vector2 acceleration = new Vector2(4f, 4f);
        private Vector2 direction;
        private Vector2 velocity;
        private Color color;
        private Inventory inventory;
 
        private float timeLastFrame;
        private float rotation;


        public Player(Vector2 position, Color color, Texture2D texture, Vector2 scale) : base()
        {
            Position = position;
            this.color = color;
            this.scale = scale;
            this.texture = texture;

            size = texture.Bounds.Size.ToVector2() * scale;
            offset = size / 2;
            hitBox = new Rectangle(Position.ToPoint() + offset.ToPoint(), size.ToPoint());
            #region Inventory test code.
            inventory = new Inventory(INVENTORY_SIZE, ACTIVE_EQUIPMENT_SLOTS);
            inventory.AddEquipment(new BasicTurret(100, 25, 1.2f, this));
            inventory.ActivateEquipment(inventory.Equipment[0], 0);
            #endregion
        }

        public override Vector2 Size => this.size;

        public override void Update(GameTime gameTime)
        {
            float timeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            //AdjustAngleTowardsMousePosition();
            AdjustAngleTowardsVelocity();
            HandleInput();
            Movement(timeThisFrame - timeLastFrame);
            timeLastFrame = timeThisFrame;
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight) => spriteBatch.Draw(texture, hitBox.Location.ToVector2() + offset, null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, scale, SpriteEffects.None, 1.0f);

        public void Movement(float deltaTime)
        {
            if (velocity.Length() < 0.5f && velocity.Length() > -0.5f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            
            // this code clamps the velocity to values between -maxVelocity and maxVelocity.
            velocity.X = velocity.X > maxVelocity.X ? maxVelocity.X : velocity.X;
            velocity.X = velocity.X < -maxVelocity.X ? -maxVelocity.X : velocity.X;
            velocity.Y = velocity.Y > maxVelocity.Y ? maxVelocity.Y : velocity.Y;
            velocity.Y = velocity.Y < -maxVelocity.Y ? -maxVelocity.Y : velocity.Y;

            Position = Position + velocity * deltaTime;
            hitBox.Location = Position.ToPoint();
        }

        public void HandleInput()
        {
            direction = Vector2.Zero;
            direction.Y += Input.keyboardState.IsKeyDown(Input.Up) ? -1 : 0;
            direction.Y += Input.keyboardState.IsKeyDown(Input.Down) ? 1 : 0;
            direction.X += Input.keyboardState.IsKeyDown(Input.Left) ? -1 : 0;
            direction.X += Input.keyboardState.IsKeyDown(Input.Right) ? 1 : 0;

            direction = direction.LengthSquared() > 1 ? Vector2.Normalize(direction) : direction;

            if(Input.IsLeftMouseButtonClicked)
            {
                //inventory.TriggerOffensiveEquipment();
                EntityManager.AddBullet(new Bullet(Center, Input.MouseWorldPosition, 20f, Color.Red, new Point(4, 4), 10));
            }
        }

        public void AdjustAngleTowardsMousePosition()
        {
            Vector2 mousePosition = new Vector2(Input.mouseState.X, Input.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            Vector2 dPos = (Position + offset) - (mousePosition + cameraOffset);

            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        public void AdjustAngleTowardsVelocity() => rotation = (float)Math.Atan(velocity.Y / velocity.X) + MathHelper.ToRadians(90);
        public new Vector2 Center { get => Position + offset; private set => hitBox.Location = (value - offset).ToPoint(); }
        protected override void Destroy()
        {
            return;
        }
    }
}