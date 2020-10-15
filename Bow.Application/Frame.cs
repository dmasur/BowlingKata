using System.Collections.Generic;
using System.Linq;

namespace Bow.Application
{
    public class Frame
    {
        private readonly int frameNumber;
        private readonly List<int> PinsRolled = new List<int>();
        private Frame NextFrame;

        public Frame(int frameNumber)
        {
            this.frameNumber = frameNumber;
        }

        public int RolledPins => PinsRolled.Count;

        internal bool IsFinished => PinsRolled.Count switch
        {
            0 => false,
            1 => !IsLastFrame && PinsRolled.Sum() == 10,
            2 => !IsLastFrame || PinsRolled.Sum() < 10,
            _ => true,
        };

        private bool IsLastFrame => frameNumber == 10;

        private bool IsSpare => PinsRolled.Sum() == 10 && PinsRolled.Count == 2;

        private bool IsStrike => PinsRolled.Sum() == 10 && PinsRolled.Count == 1;

        public bool AllFinished()
        {
            if (GetNextFrame() == null)
            {
                return IsFinished;
            }
            return IsFinished && NextFrame.AllFinished();
        }

        public int Score()
        {
            var sum = PinsRolled.Sum();
            if (IsSpare && NextFrame != null)
            {
                sum += NextFrame.GetScoreForPreviousSpare();
            }

            if (IsStrike && NextFrame != null)
            {
                sum += NextFrame.GetScoreForPreviousStrike();
            }

            return sum;
        }

        public int TotalScore()
        {
            if (NextFrame == null)
            {
                return Score();
            }
            return Score() + NextFrame.TotalScore();
        }

        internal void AddRole(int i)
        {
            PinsRolled.Add(i);
        }

        internal Frame GetNextFrame()
        {
            if (IsLastFrame)
            {
                return null;
            }
            if (NextFrame == null)
            {
                NextFrame = new Frame(frameNumber + 1);
            }
            return NextFrame;
        }

        private int GetScoreForPreviousSpare()
        {
            return RolledPins >= 1 ? PinsRolled[0] : 0;
        }

        private int GetScoreForPreviousStrike()
        {
            int ForOneRolledPin()
            {
                var sum = PinsRolled[0];
                if (sum == 10 && NextFrame != null)
                {
                    sum += NextFrame.GetScoreForPreviousSpare();
                }
                return sum;
            }

            return RolledPins switch
            {
                0 => 0,
                1 => ForOneRolledPin(),
                _ => PinsRolled[0] + PinsRolled[1]
            };
        }
    }
}