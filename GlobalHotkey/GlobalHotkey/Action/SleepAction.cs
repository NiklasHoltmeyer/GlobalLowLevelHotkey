using System.Threading;

namespace GlobalHotkey.Actions
{
    public class SleepAction : Action
    {
        int sleepMS;

        private SleepAction(double sleepMS)
        {
            this.sleepMS = (int)sleepMS;
        }

        public static SleepAction sleepInMS(double sleepMS)
        {
            return new SleepAction(sleepMS);
        }

        public static SleepAction sleepInS(double sleepS)
        {
            return new SleepAction(sleepS * 1000.0);
        }

        public static SleepAction sleepInM(double sleepM)
        {
            return new SleepAction(sleepM * 60000.0);
        }

        public static SleepAction sleepInH(double sleepH)
        {
            return new SleepAction(sleepH * 3600000.0);
        }

        public void execute()
        {
            Thread.Sleep(sleepMS);
        }
    }
}
