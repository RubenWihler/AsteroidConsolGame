using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroide
{
	class Meteor : Enemy
	{
		public Meteor(int maxPv, int pv, int damage, Position position)
		{
			this.maxPv = maxPv;
			this.pv = pv;
			this.damage = damage;
			this.position = position;

			this.type = EnemyType.Meteor;
			this.color = ConsoleColor.Magenta;
			this.hitBox = new HitBox(3, 1);
			this.apparence = "[#]"; //[#]
			this._defaultColor = color;
		}  


	}
}
