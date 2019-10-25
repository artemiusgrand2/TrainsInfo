using System;
using System.Threading;
using System.Collections.Generic;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Requests;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;
using TrainsInfo.DataStream.Communication.Controller.Dsccp.Interfaces;
using TrainsInfo.DataStream.Communication.Controller.Dsccp.RequestProcessors;

namespace TrainsInfo.DataStream.Communication.Controller.Dsccp
{
    public class DsccpController : ICommunicationController
    {
        private readonly TimeSpan echoMessageTimeout = TimeSpan.FromSeconds(15);
        private readonly Converter converter = Converter.CreateInstance();

        private readonly IDictionary<RequestType, IRequestProcessor> processors = new Dictionary<RequestType, IRequestProcessor>()
            {
                {RequestType.TypeTable, new TypeTableRequestProcessor()}
            };

        private readonly byte[] echoMessageData;

        private IDataStream serverClient;
        private IServer server;

        private DateTime lastCommunicatedTime;
        private bool isStop;
        private Thread thread;


        public event ErrorHandler<ICommunicationController, Exception> OnError;

        public IDataStream Client
        {
            get
            {
                return serverClient;
            }
        }

        public DsccpController(IDataStream client, IServer server)
        {
            lastCommunicatedTime = DateTime.Now;
            serverClient = client;
            this.server = server;
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
            thread.Join();
        }

        public void Dispose()
        {
            serverClient.Dispose();
        }

        private void Work()
        {
            try
            {
                while (!isStop)
                {
                    object data;
                    if (serverClient.Read(out data))
                    {
                        lastCommunicatedTime = DateTime.Now;
                        var message = converter.FromBytes(data);
                        //
                        if (message.MType == MessageType.Request)
                        {
                            var request = message as Request;
                            if (request != null)
                            {
                                //Logger.Instance.Log.ObligatoryInfo("Recieved request {0} {1} lenght = {2}", request.RType, request.TimeStamp, data.Length);
                                IRequestProcessor processor;
                                if (processors.TryGetValue(request.RType, out processor))
                                {
                                    Answer answer = null;
                                    try
                                    {
                                        answer = processor.Process(request, server);
                                        //Logger.Log.ObligatoryInfo("Send answer {0}", answer.AType);
                                        WriteToClient(converter.ToBytes(answer));
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Log.LogError(e.Message, e);
                                        //throw;
                                    }
                                }
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Message has request type but it is not a request");
                            }
                        }
                        else
                        {
                            throw new NotSupportedException(string.Format(
                                "Message class is not supported. {0}", message.MType));
                        }
                    }
                    else
                    {
                        if (DateTime.Now - lastCommunicatedTime > echoMessageTimeout)
                        {
                            Logger.Log.ObligatoryInfo("Sending Dsccp Echo");
                            serverClient.Write(echoMessageData);
                            lastCommunicatedTime = DateTime.Now;
                            isStop = true;
                            if (OnError != null)
                                OnError(this, new Exception(string.Format("Client {0} disconnected", serverClient.Info)));
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                    }
                    //
                    if (serverClient.IsOnceConnect)
                    {
                        isStop = true;
                        if (OnError != null)
                            OnError(this, new Exception(string.Format("Client http {0} disconnected", serverClient.Info)));
                    }
                }
            }
            catch (Exception e)
            {
                if (OnError != null)
                {
                    OnError(this, e);
                }
            }
        }

        private void WriteToClient(byte[] data)
        {
            serverClient.Write(data);
            Logger.Log.ObligatoryInfo("Send answer lenght = {0}", data.Length, serverClient);
            lastCommunicatedTime = DateTime.Now;
        }
    }
}
