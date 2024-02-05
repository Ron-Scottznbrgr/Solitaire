using Godot;
using System;
using System.Collections.Generic;
using System.Linq; 

public partial class Tableau : Node
{
	private List<List<Card>> stacks = new List<List<Card>>();

	public void InitializeStacks(int numberOfStacks)
	{
		for (int i = 0; i < numberOfStacks; i++)
		{
			stacks.Add(new List<Card>());
		}
	}

	public bool TryToAddCard(Card card, int stackIndex)
	{
		if (stackIndex >= 0 && stackIndex < stacks.Count)
		{
			var targetStack = stacks[stackIndex];
			if (targetStack.Count > 0)
			{
				var topCard = targetStack.Last(); // accessible with System.Linq
				if ((card.CardColor != topCard.CardColor) && (card.CardValue == topCard.CardValue - 1)) // Assuming CardColor and CardValue are public properties
				{
					targetStack.Add(card);
					return true;
				}
			}
			else
			{
				targetStack.Add(card);
				return true;
			}
		}
		return false;
	}
}

