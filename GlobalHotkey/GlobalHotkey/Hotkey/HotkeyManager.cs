using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace GlobalHotkey.Hotkey
{
    public class HotkeyManager
    {
        private List<Hotkey> hotKeys;
        private const short pressedFlag = -32767;
        private Thread pullThread;
        private int pullRate = 10;
        private bool pullActive;

        public HotkeyManager()
        {
            init();
        }

        public HotkeyManager(int pullRate)
        {
            this.pullRate = pullRate;
            init();
        }

        private void init()
        {
            hotKeys = new List<Hotkey>();
            this.pullActive = true;
            pullThread = new Thread(new ThreadStart(pull));
            pullThread.Start();
        }

        #region Register HKs
        public void registerHotkey(Hotkey hotkey)
        {
            if (hotkey == null || hotkey.keys.Length <= 0) return;
            else hotKeys.Add(hotkey);
        }

        public void registerHotkeys(List<Hotkey> hotkeys)
        {
            registerHotkeys(hotkeys.ToArray());
        }

        public void registerHotkeys(Hotkey[] hotkeys)
        {
            foreach(Hotkey hk in hotkeys)
                registerHotkey(hk);
        }
        #endregion
        #region unRegister HKs
        public void unregisterHotkey(Hotkey hotkey)
        {
            if (hotkey == null || hotkey.keys.Length <= 0) return;
            hotKeys.Remove(hotkey);
        }

        public void unRegisterHotkeys(List<Hotkey> hotkeys)
        {
            unregisterHotkeys(hotkeys.ToArray());
        }

        public void unregisterHotkeys(Hotkey[] hotkeys)
        {
            foreach (Hotkey hk in hotkeys)
                unregisterHotkey(hk);
        }
        #endregion
        #region GetAsyncKeyState Imports
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);
        #endregion
        private bool areHotkeysPressed(Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if (GetAsyncKeyState(key) != pressedFlag) return false;
            }
            return true;
        }
        #region Thread-Pull
        private void pull()
        {
            if (pullRate <= 0) return;

            while (pullActive)
            {
                read();
                Thread.Sleep(pullRate);
            }            
        }

        private void read()
        {
            foreach(Hotkey hk in hotKeys)
            {
                bool execute = areHotkeysPressed(hk.keys);
                if (execute)
                {
                    Thread t = new Thread(new ThreadStart(hk.action.execute));
                    t.Start();
                }
            }
        }
        #endregion 
    }
}

