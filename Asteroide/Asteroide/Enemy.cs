using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroide
{
	enum EnemyType
	{
		Meteor,
		Alien,
	}

	class Enemy
	{
		public int maxPv;
		public int pv;
		public int damage;
		public Position position;
		public string apparence;
		public EnemyType type;

		public HitBox hitBox;
		public ConsoleColor color;


		protected ConsoleColor _defaultColor;
		protected float _hurtAnimTime = 0;

		public void Attack()
		{
			Program.player.SubHealth(damage);
		}

		public void Hurt(int damage)
		{
			pv -= damage;
			this.color = ConsoleColor.Red;
			this._hurtAnimTime = 100f;

			if (pv <= 0)
			{
				this.Death();
			}
		}

		public void Death()
		{
			Program.enemys.Remove(this);
			
		}

		public void Draw()
		{
			if (_hurtAnimTime > 0)
			{
				_hurtAnimTime -= 6;
			}
			else
			{
				this.color = this._defaultColor;
			}

			Console.ForegroundColor = color;

			Console.SetCursorPosition(position.x, position.y);

			string[] ss = apparence.Split('$');

			for (int i = 0; i < ss.Length; i++)
			{
				Console.SetCursorPosition(position.x, position.y + i);
				Console.Write(ss[i]);
			}

			
		}

		public void Move(Program.Dir dir)
		{
			if (dir == Program.Dir.R)
			{
				if (position.x != 100)
				{
					position = new Position(position.x + 1, position.y);
				}
			}
			else if (dir == Program.Dir.L)
			{
				if (position.x != 15)
				{
					position = new Position(position.x - 1, position.y);
				}
			}
			else if (dir == Program.Dir.U)
			{
				if (position.y != 0)
				{
					position = new Position(position.x, position.y - 1);
				}
			}
			else
			{
				if (position.y != 27)
				{
					position = new Position(position.x, position.y + 1);
				}
			}


			if (position.x == Program.player.Position.x && position.y == Program.player.Position.y)
			{
				this.Attack();
			}

		}

	}
}
