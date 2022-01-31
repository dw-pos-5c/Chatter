namespace Chatter
{
    public interface IObserver
    {
        public string ClientName { get; }

        public void ClientAttached(string name);


        public void ClientDetached(string name);
 
        public void Update(Message msg);
    }
}
