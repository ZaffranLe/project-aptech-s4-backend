using System;
using System.Collections.Generic;
using QuantEdge.Entity.Entities;
using QuantEdge.Message.Common;
using QuantEdge.Message.Response.DataProvider;
using QuantEdge.Message.Topic;
using Anotar.Log4Net;
using System.Linq;
using QuantEdge.Common.Enum;
using QuantEdge.Entity;
using QuantEdge.Worker.DataProviderManager.ReaderDatabase;

namespace QuantEdge.Worker.DataProviderManager.Processor.GetList%EntityName%Request
{
    public class Process_GetList%EntityName%Request : IDisposable
    {

        private Flow_GetList%EntityName%Request _flow;

        private Message.Request.DataProvider.GetList%EntityName%Request _request;
        private GetList%EntityName%Response _response;
        private List<%EntityName%> _list%EntityName% = new List<%EntityName%>();

        private readonly WorkerResponse _wrkResponse = new WorkerResponse();

        public Process_GetList%EntityName%Request()
        {
            try
            {
                _flow = new Flow_GetList%EntityName%Request
                {
                    OnListRequestEntry = OnListRequestEntry,
                    OnProcessListEntry = OnProcessListEntry,
                    OnListResponseEntry = OnListResponseEntry
                };
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        private void OnListResponseEntry()
        {
            //Gửi về client
            if (_list%EntityName% == null)
                _list%EntityName% = new List<%EntityName%>();
            _response.List%EntityName% = _list%EntityName%;
            _wrkResponse.ListMsgClient.Add(_response);
        }

        private void OnProcessListEntry()
        {
            var entityQuery = new EntityQuery
            {
                EntityName = %EntityName%.EntityName(),
                QueryAction = EntityGet.GetAllValues
            };
            entityQuery = ProcessReadDatabaseUtils.GetEntityQuery(entityQuery);

            if (entityQuery.ReturnValue != null)
            {
                _list%EntityName% = entityQuery.ReturnValue.Select(baseEntity => baseEntity as %EntityName%).ToList();
            }

            _flow.TryFireTrigger(Flow_GetList%EntityName%Request.Trigger.Next);
        }

        private void OnListRequestEntry()
        {
            //Tạo thông tin response default
            var topic = !string.IsNullOrEmpty(_request.TemporaryTopic) ? _request.TemporaryTopic : TerminalTopic.FactoryUser(_request.SenderUserId);
            _response = new GetList%EntityName%Response
            {
                SendingTopic = topic,
            };
            _flow.TryFireTrigger(Flow_GetList%EntityName%Request.Trigger.Next);
        }

        public WorkerResponse ProcessMsg(Message.Request.DataProvider.GetList%EntityName%Request message)
        {
            try
            {
                if (message == null) return _wrkResponse;
                _request = message;
                OnListRequestEntry();
                return _wrkResponse;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
                return null;
            }
        }

        public void Dispose()
        {
            _flow = null;
        }
    }
}