using System.Collections.Generic;

public class SkeletonAnimationCreator: EnemyAnimationCreator
{
    protected override List<Animation> GetAnimations()
    {
        return new List<Animation>()
        {
            new Animation { name = "Attack", frameRate = 10},
            new Animation { name = "Damaged", frameRate = 10},
            new Animation { name = "Die", frameRate = 10},
            new Animation { name = "Idle", frameRate = 10},
            new Animation { name = "Move", frameRate = 10},
        };
    }
}
