namespace GlobalHotkey.Actions
{
    public class ChainedAction : Action
    {
        private Action[] actions;

        public ChainedAction(params Action[] actions)
        {
            this.actions = actions;
        }

        public void execute()
        {
            foreach(Action action in actions)
            {
                action.execute();
            }
        }
    }
}
