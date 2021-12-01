using System;
using Zork.Common;
using System.Speech.Synthesis;

namespace Zork
{
    internal class ConsoleOutputService : IOutputService
    {
        SpeechSynthesizer Text = new SpeechSynthesizer();
        public void Write(object value)
        {
            Console.Write(value);
            Text.Speak(value.ToString());
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
            Text.Speak(value.ToString());
        }

        public void Clear()
        {

        }
    }
}
