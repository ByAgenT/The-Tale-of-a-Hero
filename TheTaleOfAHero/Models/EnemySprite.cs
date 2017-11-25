using System;
using SpriteKit;
using CoreGraphics;

namespace TheTaleOfAHero.Models
{
    public class EnemySprite : SKSpriteNode
    {

        const string ENEMY_DUTY_IMAGE = "EnemyDuty.png";
        const string ENEMY_ATTACK_IMAGE = "EnemyAttack.png";

        public EnemySprite(string name) : base()
        {
            Texture = SKTexture.FromImageNamed(ENEMY_DUTY_IMAGE);
            Size = Texture.Size;
            PhysicsBody = SKPhysicsBody.CreateRectangularBody(Size);
            PhysicsBody.CategoryBitMask = CollisionCategory.Enemy;
            PhysicsBody.ContactTestBitMask = CollisionCategory.Hero | CollisionCategory.Spell | CollisionCategory.Platform;
            Name = name;

        }

        public static EnemySprite CreateEnemyAt(string name, CGPoint point) {
            return new EnemySprite(name)
            {
                Position = point
            };
        }

        // TODO: implement ability to fire
        // TODO: implement behavior
        // TODO: put all enemy behavior in this file

    }
}
