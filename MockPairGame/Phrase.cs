using System;
using System.Collections.Generic;
using System.Text;

namespace MockPairGame
{
    class Phrase
    {
        public Phrase(string originalPhrase)
        {
            OriginalPhrase = originalPhrase.ToLower();
        }

        public string OriginalPhrase { get; set; }
        public string MaskedPhrase
        {
            get
            {
                char[] phraseAsChars = OriginalPhrase.ToCharArray();
                int loopLength = OriginalPhrase.Length;
                for (int i = 0; i < loopLength; i++)
                {
                    if( phraseAsChars[i] == ' ')
                    {
                        phraseAsChars[i] = '\n';
                    }
                    else if (phraseAsChars[i] == '\'')
                    {
                        continue;
                    }
                    else
                    {                        
                        phraseAsChars[i] = (char)127;
                    }                        
                }
                string maskedPhrase = new string(phraseAsChars);
                return maskedPhrase;

            }

            //set;
        }
    }
}
