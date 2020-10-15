using System;
using System.Collections.Generic;

namespace Bow.Application
{
    public class Game
    {
        private readonly Frame FirstFrame = new Frame(1);

        public void AddRoll(int pins)
        {
            if (pins < 0 || pins > 10)
            {
                throw new ArgumentException($"Wrong PinRolled Count {pins}. Should be between 1 and 10.");
            }
            GetCurrentFrame().AddRole(pins);
        }

        public bool Over()
        {
            return FirstFrame.AllFinished();
        }

        public int TotalScore()
        {
            return FirstFrame.TotalScore();
        }

        private Frame GetCurrentFrame()
        {
            var frame = FirstFrame;
            while (frame.IsFinished)
            {
                frame = frame.GetNextFrame();
            }
            return frame;
        }
    }
}