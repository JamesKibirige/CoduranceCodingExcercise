using System.Text.RegularExpressions;

namespace SocialMessenger.Enumerations
{
    public abstract class RegularExpressions
    {
        public static readonly RegularExpressions Message = new MessageRegEx();
        public Regex RegEx { get; set; }

        private RegularExpressions(string pattern)
        {
            RegEx = new Regex(pattern);
        }

        private class MessageRegEx : RegularExpressions
        {
            public MessageRegEx() : base(@" -> (.)+$")
            {
            }
        }
    }
}