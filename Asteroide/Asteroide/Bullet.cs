using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroide
{
	class Bullet
	{
		public float activeTime;

		public ConsoleColor color;
		public Position initialPosition;

		public Bullet(Position initialPosition, float activeTime = 1000f, ConsoleColor color = ConsoleColor.Green)
		{	
			this.initialPosition = initialPosition;
			this.activeTime = activeTime;
			this.color = color;
			
		}

		public void Shoot()
		{
			Console.ForegroundColor = color;
			for (int i = 1; i < 30; i++)
			{
				if (initialPosition.y - i < 0)
				{
					return;
				}

				Console.SetCursorPosition(initialPosition.x + 1, initialPosition.y - i);
				Console.Write("|");

				Position pos = new Position(initialPosition.x + 1, initialPosition.y - i);

				foreach (Enemy e in Program.enemys)
				{
					for (int ii = 0; ii < e.hitBox.weight; ii++)
					{
						if (e.position.x + ii == pos.x && e.position.y == pos.y)
						{
							e.Hurt(1);
							Program.deletedBullets.Add(this);
							return;
						}
					}

					
				}

			}
		}

	}
}
