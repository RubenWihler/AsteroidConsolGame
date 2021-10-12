using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroide
{
	class HitBox
	{
		public int weight;
		public int height;

		public HitBox(int weight = 1, int height = 1)
		{
			this.weight = weight;
			this.height = height;
		}
	}
}
