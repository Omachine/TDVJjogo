using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace Aula
{
    private Game1 game; //reference from Game1 to Player
    public class Player
    {
        private Game1 game; //reference from Game1 to Player
        public Player(Game1 game1, int x, int y) //constructor que dada a as posições guarda a sua posição
        {
            position = new Point(x, y);
        }
        // Current player position in the matrix (multiply by tileSize prior to drawing)
        private Point position; //Point = Vector2, mas são inteiros
        public Point Position => position; //auto função (equivalente a ter só get sem put)
        public Player(int x, int y) //constructor que dada a as +osições guarda a sua posição
        {
            position = new Point(x, y);
        }
    }
}
