using System;
using System.Threading;
using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public class Terrain : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand();
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return true;
    }

    public int GetDrawingPriority()
    {
        return 1;
    }

    public string GetImageFileName()
    {
        return "Terrain.png";
    }
}

public class Player : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var (newX, newY) = GetNewPosition(x, y);

        if (newX < 0 || newX >= Game.MapWidth || newY < 0 
            || newY >= Game.MapHeight || Game.Map[newX,newY] is Sack)
            return new CreatureCommand();

        return new CreatureCommand
        {
            DeltaX = newX - x, 
            DeltaY = newY - y,
        };
    }

    private (int newX, int newY) GetNewPosition(int x, int y)
    {
        var newX = x;
        var newY = y;

        if (Game.KeyPressed == Key.Left) newX--;
        if (Game.KeyPressed == Key.Right) newX++;
        if (Game.KeyPressed == Key.Up) newY--;
        if (Game.KeyPressed == Key.Down) newY++;

        return (newX, newY);
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Sack || conflictedObject is Monster;
    }

    public int GetDrawingPriority()
    {
        return 2;
    }

    public string GetImageFileName()
    {
        return "Digger.png";
    }
}

public class Sack : ICreature
{
    public int FallingCells;
    public CreatureCommand Act(int x, int y)
    {
        var downY = y + 1;
        if (downY >= Game.MapHeight)
        {
            return TransformToGold();
        }

        var down = Game.Map[x,downY];
        if (down == null || ((down is Player || down is Monster) && FallingCells >= 1))
        {
            return new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 1,
                TransformTo = new Sack { FallingCells = FallingCells + 1 }
            };
        }

        return TransformToGold();
    }

    private CreatureCommand TransformToGold()
    {
        if (FallingCells > 1)
        {
            return new CreatureCommand { TransformTo = new Gold() };
        }
        else
        {
            return new CreatureCommand { TransformTo = new Sack { FallingCells = 0 } };
        }
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return false;
    }

    public int GetDrawingPriority()
    {
        return 3;
    }

    public string GetImageFileName()
    {
        return "Sack.png";
    }
}

public class Gold : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand();
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        if (conflictedObject is Player)
        {
            Game.Scores += 10;
            return true;
        }

        return conflictedObject is Monster;
    }

    public int GetDrawingPriority()
    {
        return 4;
    }

    public string GetImageFileName()
    {
        return "Gold.png";
    }
}

public class Monster : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var (playerX, playerY) = GetPlayerPosition();

        if (x < playerX && CanMove(x + 1, y))
            return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
        if (x > playerX && CanMove(x - 1, y) && playerX >= 0)
            return new CreatureCommand { DeltaX = -1, DeltaY = 0 };

        if (y < playerY && CanMove(x, y + 1))
            return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
        if (y > playerY && CanMove(x, y - 1) && playerY >= 0)
            return new CreatureCommand { DeltaX = 0, DeltaY = -1 };

        return new CreatureCommand();
    }

    private bool CanMove(int x, int y)
    {
        if (x < 0 || x >= Game.MapWidth || y < 0 || y >= Game.MapHeight)
            return false;

        var cell = Game.Map[x, y];
        return !(cell is Terrain) && !(cell is Sack) && !(cell is Monster);
    }

    private (int posX, int posY) GetPlayerPosition()
    {
        for (int i = 0; i < Game.MapWidth; i++)
        {
            for (int j = 0; j < Game.MapHeight; j++)
            {
                if (Game.Map[i, j] is Player)
                {
                    return(i, j);
                }
            }
        }
        return (-1, -1);
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Sack || conflictedObject is Monster;
    }

    public int GetDrawingPriority()
    {
        return 6;
    }

    public string GetImageFileName()
    {
        return "Monster.png";
    }
} 
