using System;
using System.Collections.Generic;

namespace Parser
{
    public class Levels
    {
        public UserDescription user { get; set; }
        public List<Levels> friends {get; set;}

        public Levels(UserDescription user, List<Levels> friends)
        {
            this.user = user;
            this.friends = friends;
        }

        public Levels() { }

        /*internal object Serialize()
        {
            throw new NotImplementedException();
        }*/
    }
}
