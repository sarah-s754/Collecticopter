using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Collecticopter
{
    public class SpriteReactions
    {
        private List<Reaction> _reactions;

        public SpriteReactions()
        {
            _reactions = new List<Reaction>();
        }

        public void Update()
        {
            // remove reactions that are no longer current
            List<Reaction> reactionsToRemove = new List<Reaction>();

            foreach (Reaction reaction in _reactions)
            {
                if (reaction.Current() == false) reactionsToRemove.Add(reaction);
            }

            RemoveReactions(reactionsToRemove);
        }

        public void Draw()
        {
            foreach (Reaction reaction in _reactions)
            {
                reaction.Draw();
            }
        }

        // add new reaction of given type and sprite to the _reactions list
        public void AddReaction(ReactionType type, Sprite spt)
        {
            _reactions.Add(new Reaction(type, spt));
        }

        // remove each reaction in the given list from the _reactions list
        public void RemoveReactions(List<Reaction> reactions)
        {
            foreach (Reaction reaction in reactions)
            {
                _reactions.Remove(reaction);
            }
        }
    }
}
