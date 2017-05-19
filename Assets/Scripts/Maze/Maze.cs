using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct Cell
{
    public int x;
    public int y;

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public struct CellString
{
    public Cell[] cells;
    public int size;

    public CellString(Cell[] cells, int size)
    {
        this.cells = cells;
        this.size = size;
    }
}

public abstract class MazeBuilder
{
    public abstract int[,] Generate();
    public MazeBuilder(int height, int width)
    {
        this.height = height;
        this.width = width;
    }

    protected int height;
    protected int width;
}

public class DeepMazeBuilder : MazeBuilder
{

    protected int[,] maze;
    private int gradually = 2;

    public DeepMazeBuilder(int height, int width, int gradually) : base(height, width)
    {
        maze = new int[height, width];
        this.gradually = gradually;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if ((i % 2 != 0 && j % 2 != 0) &&
                   (i < height - 1 && j < width - 1))
                {
                    maze[i, j] = Labyrinth.CELL;
                }
                else
                {
                    maze[i, j] = Labyrinth.WALL;
                }
            }
        }
    }

    public override int[,] Generate()
    {
        Cell startPoint = new Cell(1, 1);
        Cell currentCell = startPoint;
        CellString cellStr;
        Cell cellNeighbour;
        Stack<Cell> stack = new Stack<Cell>();
        Random rnd = new Random();
        maze[1, 1] = Labyrinth.GROUND;
        do
        {
            cellStr = GetNeighbours(currentCell);
            if (cellStr.size != 0)
            {
                int runNum = rnd.Next(0, cellStr.size);
                cellNeighbour = cellStr.cells[runNum];
                stack.Push(currentCell);
                RemoveWall(currentCell, cellNeighbour);
                currentCell = cellNeighbour;
                maze[currentCell.x, currentCell.y] = Labyrinth.GROUND;
            }
            else if (stack.Count > 0)
            {
                currentCell = stack.Pop();
            }
            else
            {
                CellString cellStrUn = GetUnvisitedCells();
                int randNum = rnd.Next(0, cellStrUn.size);
                currentCell = cellStrUn.cells[randNum];
                maze[currentCell.x, currentCell.y] = Labyrinth.GROUND;
            }


        } while (GetUnvisitedCells().size > 0);

        for (int i = 0; height > i * gradually; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i * gradually, j] = Labyrinth.GROUND;
            }
        }

        for (int i = 0; i < width; i++)
        {
            maze[0, i] = Labyrinth.WALL;
            maze[height - 1, i] = Labyrinth.WALL;
        }

        for (int i = 0; i < height; i++)
        {
            maze[i, 0] = Labyrinth.WALL;
            maze[i, width - 1] = Labyrinth.WALL;
        }

        return maze;
    }

    protected virtual CellString GetUnvisitedCells()
    {
        List<Cell> celles = new List<Cell>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (maze[i, j] == Labyrinth.CELL)
                {
                    celles.Add(new Cell(i, j));
                }
            }
        }
        CellString cellStr = new CellString(celles.ToArray(), celles.Count);
        return cellStr;
    }

    protected virtual CellString GetNeighbours(Cell c)
    {
        CellString temp;
        CellString res;
        List<Cell> listCells = new List<Cell>();
        int x = c.x, y = c.y;

        Cell up = new Cell(x, y + 2);
        Cell rt = new Cell(x + 2, y);
        Cell lt = new Cell(x - 2, y);
        Cell dw = new Cell(x, y - 2);

        temp.cells = new Cell[] { up, rt, lt, dw };
        temp.size = 4;
        for (int i = 0; i < temp.size; i++)
        {
            if (temp.cells[i].x < width && temp.cells[i].x > 0 && temp.cells[i].y < height && temp.cells[i].y > 0)
            {
                if (maze[temp.cells[i].x, temp.cells[i].y] == Labyrinth.CELL)
                {
                    listCells.Add(temp.cells[i]);
                }
            }
        }
        res.cells = listCells.ToArray();
        res.size = listCells.Count;
        return res;
    }

    protected virtual void RemoveWall(Cell first, Cell second)
    {
        int x = second.x - first.x;
        int y = second.y - first.y;
        int addX, addY;
        Cell target;
        addX = (x != 0) ? x / Math.Abs(x) : 0;
        addY = (y != 0) ? y / Math.Abs(y) : 0;
        target = new Cell(first.x + addX, first.y + addY);
        maze[target.x, target.y] = 2;
    }


}

public class Maze
{
    protected int height;
    protected int width;
    protected int[,] maze;

    public Maze(MazeBuilder builderMaze)
    {
        maze = builderMaze.Generate();
    }

    public int[,] GetArrayMaze()
    {
        return maze;
    }

}

