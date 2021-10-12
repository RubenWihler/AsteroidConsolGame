using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroide
{
	class Player
	{
		private Position position;

		private int health;

		private int maxHealth;

		private int munition;

		private float _invicible;

		public Player(int health, int maxHealth, int munition, Position position)
		{
			Health = health;
			MaxHealth = maxHealth;
			Munition = munition;
			Position = position;
		}

		
		public int Health { get => health; set => health = value; }
		public int MaxHealth { get => maxHealth; set => maxHealth = value; }
		public int Munition { get => munition; set => munition = value; }
		public Position Position
		{
			get => position; 
			set
			{
				position = value;
			}
		}
		public float Invicible { get => _invicible; set => _invicible = value; }

		public void SubHealth(int i)
		{
			this.health -= i;

			if (health <= 0)
			{
				PlayerDeath();
			}
			else
			{
				var audioFile = new AudioFileReader("../../../sound/playerHurt.wav");
				var outputDevice = new WaveOutEvent();

				outputDevice.Init(audioFile);
				outputDevice.Play();

				Invicible = 333.33f;
			}
		}

		public void PlayerDeath()
		{
			Console.Beep();


		}

	}
}
