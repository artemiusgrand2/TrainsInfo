using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;


namespace TrainsInfo.Common.BusinessObjects
{
    public class ListenerController
    {

        private IListener listener;
        private ICommunicationControllerFactory controllerFactory;
        private IServer server;
        private ListenerRecord settings;

        private Thread thread;
        private bool isStop;
        private int m_maxCountThread = 50;

        private IList<ICommunicationController> clientControllers;

        public ListenerController(ListenerRecord settings, IListener listener, ICommunicationControllerFactory controllerFactory, IServer server)
        {
            this.settings = settings;
            this.listener = listener;
            this.controllerFactory = controllerFactory;
            this.server = server;

            clientControllers = new List<ICommunicationController>();
            thread = new Thread(Work);
        }

        public void Start()
        {
            if (!isStop)
            {
                isStop = false;
                thread.Start();
            }
        }

        public void Stop()
        {
            isStop = true;
            thread.Join(1000);
            foreach (ICommunicationController communicationController in clientControllers)
            {
                communicationController.Stop();
                DisposeController(communicationController);
            }
            listener.Dispose();
        }

        private void Work()
        {
            while (!isStop)
            {
                try
                {
                    IDataStream client = listener.Accept();
                    if (clientControllers.Count < m_maxCountThread)
                    {
                        Logger.Log.LogInfo("Client connect. {0}", client.Info);
                        ICommunicationController clientController = controllerFactory.Create(settings, client, server);
                        clientController.OnError += ClientControllerOnOnError;
                        clientControllers.Add(clientController);
                        clientController.Start();
                    }
                }
                catch (Exception error)
                {
                    Logger.Log.LogError(error.Message, error);
                }
            }
        }

        private void ClientControllerOnOnError(ICommunicationController sender, Exception value)
        {
            lock (clientControllers)
            {
                Logger.Log.LogError("Error in controller. {0}", value);
                DisposeController(sender);
                clientControllers.Remove(sender);
            }
        }

        private void DisposeController(ICommunicationController controller)
        {
            try
            {
              Logger.Log.LogInfo("Client disconnect. {0}", controller.Client.Info);
                controller.Dispose();
            }
            catch (Exception e)
            {
                Logger.Log.LogError("Can't free client. {0}", e);
            }
        }
    }
}
