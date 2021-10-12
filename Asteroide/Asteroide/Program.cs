using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using NAudio;
using NAudio.Wave;

namespace Asteroide
{
	class Program
	{
		public enum Dir
		{
			R,
			L,
			U,
			D,
		}

		public static Player player;
		public static List<Bullet> bullets;
		public static List<Enemy> enemys;

		public static List<Bullet> deletedBullets;

		public static int level = 1;

		static void Main(string[] args)
		{
			player = new Player(10, 10, 20, new Position(15, 10));
			bullets = new List<Bullet>();
			enemys = new List<Enemy>();
			deletedBullets = new List<Bullet>();

			Console.CursorVisible = false;

			InitializeUpdate();
		} 

		static void Update()
		{
			ResetConsol();
			Input();

			RandomEnemySpawn();
			UpdateEnemys();

			UpdateBullets();
			DeleteBullets();

			DrawPlayer();



		}


		static void InitializeUpdate()
		{
			while (true)
			{
				Update();
				Thread.Sleep(6);

			}

		}

		static void Input()
		{
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo k = Console.ReadKey(true);
				if (k.Key == ConsoleKey.RightArrow)
				{
					Move(Dir.R);
				}
				else if (k.Key == ConsoleKey.LeftArrow)
				{
					
					Move(Dir.L);
					
				}
				else if (k.Key == ConsoleKey.UpArrow)
				{
					
					Move(Dir.U);
					
				}
				else if (k.Key == ConsoleKey.DownArrow)
				{
					
					Move(Dir.D);
					
				}
				else if (k.Key == ConsoleKey.Spacebar)
				{
					Shoot();
				}


			}

		}

		#region Moving

		static void Move(Dir d)
		{
			if (d == Dir.R)
			{
				if (player.Position.x != 100)
				{
					player.Position = new Position(player.Position.x + 1, player.Position.y);
				}
			}
			else if (d == Dir.L)
			{
				if (player.Position.x != 15)
				{
					player.Position = new Position(player.Position.x - 1, player.Position.y);
				}
			}
			else if (d == Dir.U)
			{
				if (player.Position.y != 0)
				{
					player.Position = new Position(player.Position.x, player.Position.y - 1);
				}
			}
			else
			{
				if (player.Position.y != 27)
				{
					player.Position = new Position(player.Position.x, player.Position.y + 1);
				}
			}
		}




		#endregion

		#region Shooting

		static void Shoot()
		{
			Bullet b = new Bullet(player.Position);
			bullets.Add(b);

			var audioFile = new AudioFileReader("../../../sound/shoot.wav");
			var outputDevice = new WaveOutEvent();
			
			outputDevice.Init(audioFile);
			outputDevice.Play();
		}

		static void DeleteBullets()
		{
			foreach (Bullet b in deletedBullets)
			{
				bullets.Remove(b);
			}

			deletedBullets.Clear();
		}



		#endregion

		#region Draw

		static void DrawPlayer()
		{
			Console.SetCursorPosition(player.Position.x, player.Position.y - 1);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(".^.");

			Console.SetCursorPosition(player.Position.x, player.Position.y);
			Console.Write("<O>");

			Console.SetCursorPosition(player.Position.x, player.Position.y + 1);
			Random r = new Random();
			int ri = r.Next(1,4);

			if (ri == 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			else if (ri == 2)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			if (ri == 3)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
			}
			else if (ri == 4)
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
			}

			ri = r.Next(1, 3);
			if (ri == 1)
			{
				Console.Write(" '");
			}
			else if (ri == 2)
			{
				Console.Write(" *");
			}
			else
			{
				Console.Write(" \"");
			}

		}

		static void UpdateBullets()
		{
			List<Bullet> newlist = new List<Bullet>();

			foreach (Bullet b in bullets)
			{
				b.activeTime -= (1000 / 6);
				if (b.activeTime > 0)
				{
					b.Shoot();
					newlist.Add(b);
				}
				
			}

			bullets = newlist;
		}

		static void ResetConsol()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			for (int i = 0; i < 27; i++)
			{
				Random r = new Random();
				int ri = r.Next(0,5);
				switch (ri)
				{
					case 0: Console.WriteLine("            |                 *                *                 *                                          |");
						break;

					case 1:
						Console.WriteLine("            |         *                  *                      *                                   *       |");
						break;

					case 2:
						Console.WriteLine("            |     *             *                  *       *           *            *      *          *       |");
						break;

					case 3:
						Console.WriteLine("            |           *                           *             *               *                *          |");
						break;

					case 4:
						Console.WriteLine("            |*                        *               *                        *                    *         |");
						break;

					case 5:
						Console.WriteLine("            |   *            *                *                    *                *                 *       |");
						break;


					default: break;
				}
			}
			Console.WriteLine("            |                                                                                 ");

			Console.ForegroundColor = ConsoleColor.Red;

			for (int i = 10; i > 10; i--)
			{
				if (player.Health >= i)
				{
					Console.Write("|");
				}
				else
				{
					Console.Write(" ");
				}
				
			}
			Console.ForegroundColor = ConsoleColor.White;

			Console.WriteLine("            \\______________________________________________________________________________________________/");
			
			
		}

		#endregion

		#region Enemy

		public static void SpawnEnemy(EnemyType t)
		{
			Random r = new Random();

			Position p = new Position(r.Next(17, 95), 3);

			if (t == EnemyType.Meteor)
			{
				Meteor e = new Meteor(2, 2, 1, p);
				enemys.Add(e);
			}
			else if (t == EnemyType.Alien)
			{
				Alien e = new Alien(5, 5, 2, p);
				enemys.Add(e);
			}
			

		}
			   
		public static void UpdateEnemys()
		{
			List<Enemy> newL = new List<Enemy>();

			foreach (Enemy e in enemys)
			{
				e.Draw();
				if (e.type == EnemyType.Meteor)
				{
					Random r = new Random();
					if (r.Next(0, 11) == 0)
					{
						e.Move(Dir.D);
					}
				}
				else if (e.type == EnemyType.Alien)
				{
					Random r = new Random();
					if (r.Next(0, 15) == 0)
					{
						e.Move(Dir.D);
					}
				}

				if (e.position.y + e.hitBox.height < 28)
				{
					newL.Add(e);
				}

			}

			enemys = newL;
		}
			   
		public static void RandomEnemySpawn()
		{
			Random r = new Random();
			int ri = r.Next(0, 110);

			if (ri <= 1 * level)
			{
				SpawnEnemy(EnemyType.Meteor);
			}

			ri = r.Next(0, 150);
			if (ri <= 1 * level)
			{
				SpawnEnemy(EnemyType.Alien);
			}
		}


		#endregion

	}
}
