using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroide
{
	class Alien : Enemy
	{
		public Alien(int maxPv, int pv, int damage, Position position)
		{
			this.maxPv = maxPv;
			this.pv = pv;
			this.damage = damage;
			this.position = position;

			this.type = EnemyType.Alien;
			this.color = ConsoleColor.Green;
			this.hitBox = new HitBox(3, 2);
			this.apparence = "-0-$'*'"; //  -0-
			this._defaultColor = color;
		}

	}
}
