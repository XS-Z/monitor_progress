namespace Blablabla.Hubs
{
    public class DownloadProgressHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}
        public void Send(string name, string message)
        {
            string id = Context.ConnectionId;
            int progress = 0;
            do
            {
                progress = Blabla.ExportUtility.GetExportingProgress(name);
                Clients.Client(id).updateProgress(progress);
                System.Threading.Thread.Sleep(200);
            } while (!progress.Equals(100));
        }
 
    }
}
