namespace GlobalHotkey.Hotkey
{
    public class Hotkey
    {
        public Keys[] keys { get; set;}
        public Action action {get;set;}

        public Hotkey(Keys[] keys, Action action)
        {
            this.keys = keys;
            this.action = action;
        }

        public Hotkey(Keys key, Action action)
        {
            this.keys = new Keys[] { key };
            this.action = action;
        }
    }
}
