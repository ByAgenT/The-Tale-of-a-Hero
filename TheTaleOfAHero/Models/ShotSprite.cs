using System;
using SpriteKit;
using CoreGraphics;


namespace TheTaleOfAHero.Models
{
    public class ShotSprite : SKSpriteNode
    {
        
        const string SHOT_ENEMY_PATH = "Enemy/EnemySpell.png";
        const string SHOT_HERO_PATH = "Hero/HeroSpell.png";

        public SpellType Type { get; set; }

        public ShotSprite(SpellType type)
        {
            Type = type;
            switch(type)
            {
                case SpellType.Hero:
                    Texture = SKTexture.FromImageNamed(SHOT_HERO_PATH);
                    break;
                case SpellType.Enemy:
                    Texture = SKTexture.FromImageNamed(SHOT_ENEMY_PATH);
                    break;
            }
            Size = Texture.Size;
            PhysicsBody = SKPhysicsBody.CreateRectangularBody(Size);
            PhysicsBody.CategoryBitMask = CollisionCategory.Spell;
            PhysicsBody.ContactTestBitMask = CollisionCategory.Enemy | CollisionCategory.Platform;
            PhysicsBody.CollisionBitMask = 0;
            PhysicsBody.AffectedByGravity = false;
            PhysicsBody.AngularDamping = 0;
            PhysicsBody.AllowsRotation = false;
        }

        /// <summary>
        /// Point the sprite to move by the vector
        /// </summary>
        /// <param name="vector">Vector.</param>
        public void AttackByVector(CGVector vector)
        {
            var time = Math.Sqrt(Math.Pow(vector.dx, 2) + Math.Pow(vector.dy, 2)) / 300;
            RunAction(SKAction.RepeatActionForever(SKAction.MoveBy(vector, time)));
        }

        /// <summary>
        /// Creates the shot at startPoint with selected type.
        /// </summary>
        /// <returns>The <see cref="T:TheTaleOfAHero.Models.ShotSprite"/>.</returns>
        /// <param name="startPoint">Start point.</param>
        /// <param name="type">Type.</param>
        public static ShotSprite CreateShotAt(CGPoint startPoint, SpellType type)
        {
            return new ShotSprite(type)
            {
                Position = startPoint
            };
        }
    }

    public enum SpellType
    {
        Hero,
        Enemy
    }
}
