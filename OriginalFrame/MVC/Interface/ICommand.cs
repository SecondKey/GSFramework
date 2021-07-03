namespace GSFramework.MVC
{
    public interface ICommand
    {
        void Execute(params object[] parameters);
    }
}
