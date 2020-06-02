using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CommonicationMemory.Common;

namespace CommonicationMemory.CodeGeneration.CreateFile.CreateMessage
{
    public class CreateMessageUtils
    {
        //Tạo các file message request, response, event args
        public static void GenerateMessageRequestResponse(string BusinessLayerRootPath, DatabaseTable table)
        {
            #region CreateForder
            string forderInsert = BusinessLayerRootPath + Path.DirectorySeparatorChar + "MessageAndData\\";
            if (!Directory.Exists(forderInsert))
                Directory.CreateDirectory(forderInsert);

            string forderRequest = forderInsert + "Request\\";
            if (!Directory.Exists(forderRequest))
                Directory.CreateDirectory(forderRequest);

            string forderResponse = forderInsert + "Response\\";
            if (!Directory.Exists(forderResponse))
                Directory.CreateDirectory(forderResponse);

            string forderEventArgs = forderInsert + "EventArgs\\";
            if (!Directory.Exists(forderEventArgs))
                Directory.CreateDirectory(forderEventArgs);

            string forderEventChanged = forderEventArgs + "\\EntityChanged\\";
            if (!Directory.Exists(forderEventChanged))
                Directory.CreateDirectory(forderEventChanged);
            #endregion

            #region GetList

            #region GetListRequest
            string fileGetListRequest = forderRequest + "GetList" + table.TableName + "Request.cs";
            using (StreamWriter sw = new StreamWriter(fileGetListRequest))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using HK.CommonMessage.Utils;");
                sw.WriteLine("using HK.Entity.Enums;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using ProtoBuf;");

                sw.WriteLine("namespace HK.CommonMessage.RequestMessage.Account");
                sw.WriteLine("{");
                sw.WriteLine("[ProtoContract, Serializable]");
                sw.WriteLine("\tpublic class GetList" + table.TableName + "Request" + " : Request");
                sw.WriteLine("\t{");

                sw.WriteLine("public GetList" + table.TableName + "Request()");
                sw.WriteLine(": base(ZMQMessageType.Account)");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(1)]");
                sw.WriteLine("public RequestType RequestType { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(2)]");
                sw.WriteLine("public long RequestId { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(3)]");
                sw.WriteLine("public DateTime FromTime { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(4)]");
                sw.WriteLine("public DateTime ToTime { get; set; }");
                sw.WriteLine("");


                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region GetListResponse
            string fileGetListResponse = forderResponse + "GetList" + table.TableName + "Response.cs";
            using (StreamWriter sw = new StreamWriter(fileGetListResponse))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using HK.CommonMessage.Utils;");
                sw.WriteLine("using HK.Entity.Enums;");
                sw.WriteLine("using ProtoBuf;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using System.Collections.Generic;");

                sw.WriteLine("namespace HK.CommonMessage.ResponseMessage.Account");
                sw.WriteLine("{");
                sw.WriteLine("[ProtoContract, Serializable]");
                sw.WriteLine("\tpublic class GetList" + table.TableName + "Response" + " : Response");
                sw.WriteLine("\t{");

                sw.WriteLine("public GetList" + table.TableName + "Response()");
                sw.WriteLine(": base(ZMQMessageType.Account)");
                sw.WriteLine("{");
                sw.WriteLine("List" + table.TableName + " = new List<" + table.TableName + ">();");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(1)]");
                sw.WriteLine("public RequestType RequestType { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(2)]");
                sw.WriteLine("public long RequestId { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(3)]");
                sw.WriteLine("public List<" + table.TableName + "> List" + table.TableName + " { get; set; }");
                sw.WriteLine("");


                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #endregion

            #region Update,Insert

            #region UpdateRequest
            string fileUpdateRequest = forderRequest + "Update" + table.TableName + "Request.cs";
            using (StreamWriter sw = new StreamWriter(fileUpdateRequest))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using HK.CommonMessage.Utils;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using HK.Entity.Enums;");
                sw.WriteLine("using ProtoBuf;");

                sw.WriteLine("namespace HK.CommonMessage.RequestMessage.Account");
                sw.WriteLine("{");
                sw.WriteLine("[ProtoContract, Serializable]");
                sw.WriteLine("\tpublic class Update" + table.TableName + "Request" + " : Request");
                sw.WriteLine("\t{");

                sw.WriteLine("public Update" + table.TableName + "Request()");
                sw.WriteLine(": base(ZMQMessageType.Account)");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(1)]");
                sw.WriteLine("public RequestType RequestType { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(2)]");
                sw.WriteLine("public " + table.TableName + " " + table.TableName + " { get; set; }");
                sw.WriteLine("");


                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region UpdateResponse
            string fileUpdateResponse = forderResponse + "Update" + table.TableName + "Response.cs";
            using (StreamWriter sw = new StreamWriter(fileUpdateResponse))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using HK.CommonMessage.Utils;");
                sw.WriteLine("using HK.Entity.Enums;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using ProtoBuf;");
                sw.WriteLine("using System.Collections.Generic;");

                sw.WriteLine("namespace HK.CommonMessage.ResponseMessage.Account");
                sw.WriteLine("{");
                sw.WriteLine("[ProtoContract, Serializable]");
                sw.WriteLine("\tpublic class Update" + table.TableName + "Response" + " : Response");
                sw.WriteLine("\t{");

                sw.WriteLine("public Update" + table.TableName + "Response()");
                sw.WriteLine(": base(ZMQMessageType.Account)");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(1)]");
                sw.WriteLine("public RequestType RequestType { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(2)]");
                sw.WriteLine("public ResourcesKeyEnum ResourcesKeyEnum { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion
            #endregion

            #region Events Args

            #region EventChanged

            string fileEventChanged = forderEventChanged + table.TableName + "ChangedEventArgs.cs";
            using (StreamWriter sw = new StreamWriter(fileEventChanged))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using HK.Entity.Enums;");

                sw.WriteLine("namespace HK.APIHander.Events.EntityChanged");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic class " + table.TableName + "ChangedEventArgs : EventArgs");
                sw.WriteLine("\t{");

                sw.WriteLine("public " + table.TableName + " " + table.TableName + " { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("public EntityAction EntityAction { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region EventGetList

            string fileEventArgs = forderEventArgs + "GetList" + table.TableName + "EventArgs.cs";
            using (StreamWriter sw = new StreamWriter(fileEventArgs))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using System.Collections.Generic;");

                sw.WriteLine("namespace HK.APIHander.Events");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic class GetList" + table.TableName + "EventArgs : EventArgs");
                sw.WriteLine("\t{");

                sw.WriteLine("public GetList" + table.TableName + "EventArgs()");
                sw.WriteLine("{");
                sw.WriteLine("List" + table.TableName + " = new List<" + table.TableName + ">();");

                sw.WriteLine("}");

                sw.WriteLine("");
                sw.WriteLine("public List<" + table.TableName + "> List" + table.TableName + " { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("public long RequestId { get; set; }");

                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region EventUpdateInsert

            string fileEventUpdateInsert = forderEventArgs + "" + table.TableName + "EventArgs.cs";
            using (StreamWriter sw = new StreamWriter(fileEventUpdateInsert))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using HK.Entity.Enums;");

                sw.WriteLine("namespace HK.APIHander.Events");
                sw.WriteLine("{");
                sw.WriteLine("public class " + table.TableName + "EventArgs : EventArgs");
                sw.WriteLine("\t{");

                sw.WriteLine("public ResourcesKeyEnum ResourcesKeyEnum { get; set; } ");

                sw.WriteLine("public RequestType RequestType { get; set; } ");

                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #endregion
        }

        //Tạo file đăng kí protobub của request, response
        public static void GeneratorFileRequestResponse(string BusinessLayerRootPath, List<DatabaseTable> listDataTable)
        {
            #region CreateForder
            string forderInsert = BusinessLayerRootPath + Path.DirectorySeparatorChar + "MessageAndData\\";
            if (!Directory.Exists(forderInsert))
                Directory.CreateDirectory(forderInsert);

            string forderRequest = forderInsert + "Request\\";
            if (!Directory.Exists(forderRequest))
                Directory.CreateDirectory(forderRequest);

            string forderResponse = forderInsert + "Response\\";
            if (!Directory.Exists(forderResponse))
                Directory.CreateDirectory(forderResponse);

            #endregion


            var dicTableName = new Dictionary<string, string>();
            foreach (DatabaseTable table in listDataTable)
            {
                if (table.IsSelected)
                {
                    dicTableName[table.TableName] = table.TableName;
                }
            }

            #region FileRequest

            string fileRequest = forderRequest + "Request.cs";
            using (StreamWriter sw = new StreamWriter(fileRequest))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using HK.CommonMessage.RequestMessage.Account;");
                sw.WriteLine("using HK.CommonMessage.Utils;");
                sw.WriteLine("using ProtoBuf;");

                sw.WriteLine("namespace HK.CommonMessage.RequestMessage");
                sw.WriteLine("{");

                sw.WriteLine(" //ProtoInclude tag start with 100");
                sw.WriteLine("[Serializable, ProtoContract]");
                int count = 101;
                sw.WriteLine("[ProtoInclude(101, typeof (LoginRequest))]");
                foreach (string tableName in dicTableName.Values)
                {
                    count++;
                    sw.WriteLine("[ProtoInclude(" + count + ", typeof (Update" + tableName + "Request))]");
                    sw.WriteLine("[ProtoInclude(" + count + ", typeof (GetList" + tableName + "Request))]");
                }


                sw.WriteLine("public class Request");
                sw.WriteLine("{");

                #region Class Request
                sw.WriteLine("public Request()");
                sw.WriteLine("{");
                sw.WriteLine("TTL = Const.DEFAULT_PROTOBUF_TTL;");
                sw.WriteLine("}");

                sw.WriteLine("public Request(string requestKey, ZMQMessageType messageType)");
                sw.WriteLine(": this()");
                sw.WriteLine("{");
                sw.WriteLine("RequestKey = requestKey;");
                sw.WriteLine("MessageType = messageType;");
                sw.WriteLine("}");

                sw.WriteLine("public Request(ZMQMessageType messageType)");
                sw.WriteLine(": this()");
                sw.WriteLine("{");
                sw.WriteLine("MessageType = messageType;");
                sw.WriteLine("}");



                sw.WriteLine(" [ProtoMember(1)]");
                sw.WriteLine("public ZMQMessageType MessageType { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(2)]");
                sw.WriteLine("public string RequestKey { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(3)]");
                sw.WriteLine("public int TTL { get; set; }");
                sw.WriteLine("");


                sw.WriteLine("[ProtoMember(4)]");
                sw.WriteLine("public string Sender { get; set; } //GuiId authen or UserId");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(5)]");
                sw.WriteLine("public string UserLoginName { get; set; } //UserName ");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(6)]");
                sw.WriteLine("public long UserLoginId { get; set; } //UserLoginId");
                sw.WriteLine("");
                #endregion


                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region FileResponse

            string fileResponse = forderResponse + "Response.cs";
            using (StreamWriter sw = new StreamWriter(fileResponse))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using HK.CommonMessage.ResponseMessage.Account;");
                sw.WriteLine("using HK.CommonMessage.ResponseMessage.Broadcast;");
                sw.WriteLine("using HK.CommonMessage.Utils;");
                sw.WriteLine("using ProtoBuf;");

                sw.WriteLine("namespace HK.CommonMessage.ResponseMessage");
                sw.WriteLine("{");

                sw.WriteLine(" //ProtoInclude tag start with 100");
                sw.WriteLine("[Serializable, ProtoContract]");
                int count = 101;
                sw.WriteLine("[ProtoInclude(" + count + ", typeof (LoginResponse))]");
                count++;
                sw.WriteLine("[ProtoInclude(" + count + ", typeof (UpdateServerTimeBroadcast))]");
                count++;
                foreach (string tableName in dicTableName.Values)
                {
                    count++;
                    sw.WriteLine("[ProtoInclude(" + count + ", typeof (Update" + tableName + "Response))]");
                    sw.WriteLine("[ProtoInclude(" + count + ", typeof (GetList" + tableName + "Response))]");
                }


                sw.WriteLine("public class Response");
                sw.WriteLine("{");

                #region Class Request
                sw.WriteLine("public Response()");
                sw.WriteLine("{");
                sw.WriteLine("}");

                sw.WriteLine("public Response(string requestKey, ZMQMessageType messageType)");
                sw.WriteLine(": this()");
                sw.WriteLine("{");
                sw.WriteLine("RequestKey = requestKey;");
                sw.WriteLine("MessageType = messageType;");
                sw.WriteLine("}");

                sw.WriteLine("public Response(ZMQMessageType messageType)");
                sw.WriteLine(": this()");
                sw.WriteLine("{");
                sw.WriteLine("MessageType = messageType;");
                sw.WriteLine("}");



                sw.WriteLine(" [ProtoMember(1)]");
                sw.WriteLine("public ZMQMessageType MessageType { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(2)]");
                sw.WriteLine("public string RequestKey { get; set; }");
                sw.WriteLine("");

                sw.WriteLine("[ProtoMember(3)]");
                sw.WriteLine("public int TTL { get; set; }");
                sw.WriteLine("");

                #endregion


                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region File BaseEntity

            string fileBaseEntity = forderInsert + "BaseEntity.cs";
            using (StreamWriter sw = new StreamWriter(fileBaseEntity))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using HK.Entity.Entities;");
                sw.WriteLine("using ProtoBuf;");

                sw.WriteLine("namespace HK.Entity");
                sw.WriteLine("{");

                sw.WriteLine(" //ProtoInclude tag start with 100");
                sw.WriteLine("[Serializable, ProtoContract]");
                int count = 101;
                foreach (string tableName in dicTableName.Values)
                {
                    count++;
                    sw.WriteLine(" [ProtoInclude(" + count + ", typeof(" + tableName + "))]");
                }

                sw.WriteLine(@"public abstract class BaseEntity : ICloneable, IDisposable
                            {
                                public object Clone()
                                {
                                    return MemberwiseClone();
                                }

                                public void Dispose()
                                {
                                }

                                public abstract string GetName();
                            }");

                //sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion

            #region File ServiceQueue

            string fileServiceQueue = forderInsert + "ServiceQueue.cs";
            using (StreamWriter sw = new StreamWriter(fileServiceQueue))
            {
                sw.WriteLine(@"using System;
                            using HK.BrokerWorker;
                            using HK.CommonMessage.RequestMessage;
                            using HK.CommonMessage.RequestMessage.Account;
                            using HK.CommonMessage.ResponseMessage;
                            using HK.CommonMessage.ResponseMessage.Account;
                            using HK.Entity.Processor;
                            using log4net;
                            using Worker.AccountManager.Process;
                            using Worker.AccountManager.Process.GetList;
                            using Worker.AccountManager.Process.Update;");

                sw.WriteLine(@"namespace Worker.AccountManager
                            {
                                public class ServiceQueue : MessageQueue
                                {");


                sw.WriteLine(@"private static readonly ILog log = LogManager.GetLogger(typeof(ServiceQueue));

                                public static Response ProcessAccountRequest(Request request)
                                {
                                    if (request is LoginRequest)
                                    {
                                        var req = request as LoginRequest;
                                        var processLoginRequest = new ProcessLoginRequest();
                                        return processLoginRequest.ProcessLogin(req);
                                    }

                                    var response = ProcessUpdate(request);
                                    if (response != null)
                                        return response;

                                    response = ProcessGetList(request);
                                    if (response != null)
                                        return response;

                                    return null;
                                }");


                #region ProcessUpdate
                sw.WriteLine("public static Response ProcessUpdate(Request request)");
                sw.WriteLine("{");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("#region " + tableName);
                    sw.WriteLine("if (request is Update" + tableName + "Request)");
                    sw.WriteLine("{");
                    sw.WriteLine("var req = request as Update" + tableName + "Request;");
                    sw.WriteLine("var process = new ProcessUpdate" + tableName + "();");
                    sw.WriteLine("return process.Update" + tableName + "(req);");
                    sw.WriteLine("}");
                    sw.WriteLine("#endregion");
                    sw.WriteLine("");
                }

                sw.WriteLine("return null;");
                sw.WriteLine("}");
                #endregion


                #region ProcessGetList
                sw.WriteLine("public static Response ProcessGetList(Request request)");
                sw.WriteLine("{");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("#region " + tableName);

                    sw.WriteLine("if (request is GetList"+tableName+"Request)");
                    sw.WriteLine("{");
                    sw.WriteLine("var req = request as GetList"+tableName+"Request;");
                    sw.WriteLine("var process = new ProcessGetList"+tableName+"();");
                    sw.WriteLine("return process.GetList"+tableName+"(req);");
                    sw.WriteLine("}");
                    sw.WriteLine("#endregion");
                    sw.WriteLine("");
                }

                sw.WriteLine("return null;");
                sw.WriteLine("}");
                #endregion

                sw.WriteLine(@"public override void ProcessMessage(object message)
                                {
                                    try
                                    {
                                        if (message is Request)
                                        {
                                            var request = message as Request;
                                            Response response = ProcessAccountRequest(request);
                                            if (response != null)
                                            {
                                                if (string.IsNullOrEmpty(response.RequestKey))
                                                {
                                                    response.RequestKey = request.RequestKey;
                                                }

                                                if (response is LoginResponse)
                                                {
                                                    MessageDispatcher.Instance.SendToUser(request.Sender, response);
                                                    return;
                                                }
                                                MessageDispatcher.Instance.SendToUser(request.UserLoginId, response);
                                            }
                                        }
                                    }
                                    catch (Exception err)
                                    {
                                        log.Error(err.ToString());
                                    }
                                }");

                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }
            #endregion
        }

        //Tạo các file AccountClient ở client
        public static void GeneratorWorkerClientInAPIHandler(string BusinessLayerRootPath, List<DatabaseTable> listDataTable)
        {
            #region CreateForder
            string forderInsert = BusinessLayerRootPath + Path.DirectorySeparatorChar + "MessageAndData\\";
            if (!Directory.Exists(forderInsert))
                Directory.CreateDirectory(forderInsert);

            string forderWorkerClient = forderInsert + "WorkerClient\\";
            if (!Directory.Exists(forderWorkerClient))
                Directory.CreateDirectory(forderWorkerClient);

            #endregion

            var dicTableName = new Dictionary<string, string>();
            foreach (DatabaseTable table in listDataTable)
            {
                if (table.IsSelected)
                {
                    dicTableName[table.TableName] = table.TableName;
                }
            }

            #region File AccountClient

            string fileAccountClient = forderWorkerClient + "AccountClient.cs";
            using (StreamWriter sw = new StreamWriter(fileAccountClient))
            {
                string textFunction = GetTextFile("AccountClient.txt");
                sw.WriteLine(textFunction);
            }
            #endregion

            #region File AccountClient.Request

            string fileAccountClientRequest = forderWorkerClient + "AccountClient.Request.cs";
            using (StreamWriter sw = new StreamWriter(fileAccountClientRequest))
            {
                sw.WriteLine(@"using System;
                                using HK.APIHander.Events;
                                using HK.APIHander.Events.EntityChanged;
                                using HK.APIHander.NetworkClient;
                                using HK.CommonMessage.RequestMessage;
                                using HK.CommonMessage.RequestMessage.Account;
                                using HK.Entity.Entities;
                                using HK.Entity.Enums;

                                namespace HK.APIHander.WorkerClient
                                {
                                    public partial class AccountClient
                                    {
                                        public AccountClient AccountClientNotNetwork { get; set; }

                                        public void TerminalNetworkClientSendMessage(Request req)
                                        {
                                            if (Worker.AccountManager.AppGlobal.IsUseNetwork)
                                                TerminalNetworkClient.Instance.SendMessage(req);
                                            else
                                            {
                                                //not network
                                                //gán thông tin message
                                                if(req != null)
                                                {
                                                    if (string.IsNullOrEmpty(req.Sender))
                                                        req.Sender = TerminalNetworkClient.Instance.SenderName;

                                                    if (string.IsNullOrEmpty(req.UserLoginName))
                                                        req.UserLoginName = TerminalNetworkClient.Instance.UserName;

                                                    if (req.UserLoginId <= 0)
                                                        req.UserLoginId = TerminalNetworkClient.Instance.UserId;
                                                }

                                                //process message

                                                var reponse = Worker.AccountManager.AccountAdapter.ProcessMessage(req);
                                                if(reponse != null)
                                                {
                                                    AccountClientNotNetwork.ProcessMessageResponseNotNetwork(reponse);
                                                }
                                            }
                                        }
                               ");

                sw.WriteLine("#region Update");
                #region Update

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public void Update" + tableName + "(RequestType requestType, " + tableName + " " + tableName + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var req = new Update" + tableName + "Request");
                    sw.WriteLine("{");
                    sw.WriteLine("RequestType = requestType,");
                    sw.WriteLine("" + tableName + " = " + tableName + ",");
                    sw.WriteLine("};");
                    sw.WriteLine("");
                    sw.WriteLine("TerminalNetworkClientSendMessage(req);");
                    sw.WriteLine("}");
                }

                #endregion
                sw.WriteLine("#endregion");

                sw.WriteLine("#region GetList");
                #region GetList

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public void GetList" + tableName + "(RequestType requestType, DateTime fromTime, DateTime toTime, long requestId = 0)");
                    sw.WriteLine("{");
                    sw.WriteLine("var req = new GetList" + tableName + "Request");
                    sw.WriteLine("{");
                    sw.WriteLine("RequestType = requestType,");
                    sw.WriteLine("RequestId = requestId,");
                    sw.WriteLine("FromTime = fromTime,");
                    sw.WriteLine("ToTime = toTime,");
                    sw.WriteLine("};");
                    sw.WriteLine("TerminalNetworkClientSendMessage(req);");
                    sw.WriteLine("}");
                }
                #endregion
                sw.WriteLine("#endregion");

                //end class and namespace
                sw.WriteLine("}");
                sw.WriteLine("}");
            }
            #endregion

            #region File AccountClient.Response

            string fileAccountResponse = forderWorkerClient + "AccountClient.Response.cs";
            using (StreamWriter sw = new StreamWriter(fileAccountResponse))
            {
                sw.WriteLine(@"
                            using System;
                            using HK.APIHander.Events;
                            using HK.APIHander.Events.EntityChanged;
                            using HK.APIHander.OperatorHandler;
                            using HK.CommonMessage.ResponseMessage;
                            using HK.CommonMessage.ResponseMessage.Account;
                            using HK.CommonMessage.ResponseMessage.Broadcast;
                            using HK.Entity.Entities;

                                namespace HK.APIHander.WorkerClient
                                {
                                    public partial class AccountClient
                                    {");

                #region EventArgs
                sw.WriteLine("#region EventArgs");

                #region EventUpdate
                sw.WriteLine("#region EventUpdate");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public event EventHandler<" + tableName + "EventArgs> On" + tableName + "EventArgs;");
                }
                sw.WriteLine("");

                sw.WriteLine(" #endregion");
                #endregion

                #region EventGetList
                sw.WriteLine("#region EventGetList");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public event EventHandler<GetList" + tableName + "EventArgs> OnGetList" + tableName + "EventArgs;");
                }
                sw.WriteLine("");

                sw.WriteLine(" #endregion");
                #endregion

                #region Event Entity Changed
                sw.WriteLine("#region Event Entity Changed");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public event EventHandler<" + tableName + "ChangedEventArgs> On" + tableName + "ChangedEventArgs;");
                }
                sw.WriteLine("");

                sw.WriteLine(" #endregion");
                #endregion

                sw.WriteLine(" #endregion");
                sw.WriteLine("");
                sw.WriteLine("");
                #endregion


                sw.WriteLine("public Response ProcessResponse(Response message)");
                sw.WriteLine("{");

                #region ProcessResponse

                sw.WriteLine("#region Update");

                #region UpdateEntity


                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("#region #region " + tableName + "Response");
                    sw.WriteLine("");
                    sw.WriteLine("if (message is Update" + tableName + "Response)");
                    sw.WriteLine("{");
                    sw.WriteLine("var response = message as Update" + tableName + "Response;");
                    sw.WriteLine("if (On" + tableName + "EventArgs != null)");
                    sw.WriteLine("{");

                    sw.WriteLine("On" + tableName + "EventArgs(response.RequestKey, new " + tableName + "EventArgs");
                    sw.WriteLine("{");
                    sw.WriteLine("ResourcesKeyEnum = response.ResourcesKeyEnum,");
                    sw.WriteLine("});");

                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine(" #endregion");
                }

                #endregion

                sw.WriteLine(" #endregion");
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("#region GetList");

                #region GetListEntity


                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("#region #region GetList" + tableName + "Response");
                    sw.WriteLine("");
                    sw.WriteLine("if (message is GetList" + tableName + "Response)");
                    sw.WriteLine("{");

                    sw.WriteLine("var response = message as GetList" + tableName + "Response;");
                    sw.WriteLine("if (OnGetList" + tableName + "EventArgs != null)");
                    sw.WriteLine("{");

                    sw.WriteLine("OnGetList" + tableName + "EventArgs(response.RequestKey, new GetList" + tableName + "EventArgs");
                    sw.WriteLine("{");
                    sw.WriteLine("List" + tableName + " = response.List" + tableName + ",");
                    sw.WriteLine("RequestId = response.RequestId,");
                    sw.WriteLine("});");

                    sw.WriteLine("}");

                    sw.WriteLine("}");

                    sw.WriteLine(" #endregion");
                }

                #endregion

                sw.WriteLine(" #endregion");
                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                sw.WriteLine("return message;");
                sw.WriteLine("}");


                #region ProcessBroadCast

                sw.WriteLine(@"private Response ProcessBroadcast(Response message)
                                {
                                    #region UpdateServerTimeBroadcast
                                    if (message is UpdateServerTimeBroadcast)
                                    {
                                        var response = message as UpdateServerTimeBroadcast;
                                        AppGlobal.ServerTime = response.ServerTime;
                                    }
                                    #endregion
                        ");


                sw.WriteLine("#region UpdateEntityBroadcast");

                sw.WriteLine("if (message is UpdateEntityBroadcast)");
                sw.WriteLine("{");

                sw.WriteLine("var response = message as UpdateEntityBroadcast;");
                sw.WriteLine("if (response.EntityChanged != null)");
                sw.WriteLine("{");


                #region EntityChanged
                sw.WriteLine("var baseEntity = response.EntityChanged.BaseEntity;");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("#region " + tableName);

                    sw.WriteLine("if (baseEntity is " + tableName + " && On" + tableName + "ChangedEventArgs != null)");
                    sw.WriteLine("{");

                    sw.WriteLine("On" + tableName + "ChangedEventArgs(response.RequestKey, new " + tableName + "ChangedEventArgs");
                    sw.WriteLine("{");
                    sw.WriteLine("" + tableName + " = baseEntity as " + tableName + ",");
                    sw.WriteLine("EntityAction = response.EntityChanged.EntityAction,");

                    sw.WriteLine("});");
                    sw.WriteLine("");
                    sw.WriteLine("}");

                    sw.WriteLine("#endregion");
                }

                sw.WriteLine("");
                #endregion

                sw.WriteLine("}");

                sw.WriteLine("}");
                sw.WriteLine("#endregion");

                sw.WriteLine("return message;");
                sw.WriteLine("}");

                #endregion

                //end class and namespace
                sw.WriteLine("}");
                sw.WriteLine("}");
            }
            #endregion
        }

        //Tạo các file trong operator ở client
        public static void GeneratorOperator(string BusinessLayerRootPath, List<DatabaseTable> listDataTable)
        {
            #region CreateForder
            string forderInsert = BusinessLayerRootPath + Path.DirectorySeparatorChar + "MessageAndData\\";
            if (!Directory.Exists(forderInsert))
                Directory.CreateDirectory(forderInsert);

            string forderOperator = forderInsert + "OperatorHandler\\";
            if (!Directory.Exists(forderOperator))
                Directory.CreateDirectory(forderOperator);

            #endregion

            var dicTableName = new Dictionary<string, string>();
            foreach (DatabaseTable table in listDataTable)
            {
                if (table.IsSelected)
                {
                    dicTableName[table.TableName] = table.TableName;
                }
            }

            #region File Operator

            string fileOperator = forderOperator + "Operator.cs";
            using (StreamWriter sw = new StreamWriter(fileOperator))
            {
                sw.WriteLine(@"using System;
                        using HK.APIHander.WorkerClient;
                        using HK.Entity.Entities;
                        using HK.Entity.Enums;
                        using log4net;

                        namespace HK.APIHander.OperatorHandler
                        {
                            public partial class Operator
                            {");

                sw.WriteLine(@"private static readonly ILog log = LogManager.GetLogger(typeof(Operator));
                                #region singleton implement

                                public static Operator Instance
                                {
                                    get { return Nested.Instance; }
                                }

                                private class Nested
                                {
                                    internal static readonly Operator Instance = new Operator();

                                    static Nested()
                                    {
                                    }
                                }

                                #endregion");


                sw.WriteLine(@"private AccountClient _accountClient;
                                private void SetAccountClientNotNetwork()
                                {
                                    if(Worker.AccountManager.AppGlobal.IsUseNetwork) return;
                                    if (_accountClient.AccountClientNotNetwork == null)
                                        _accountClient.AccountClientNotNetwork = _accountClient;
                                }

                                public void Init()
                                {
                                    _accountClient = new AccountClient();
                                    InitAccount();
                                }

                                public void Disconnect()
                                {
                                    try
                                    {
                
                                    }
                                    catch (Exception ex)
                                    {
                                        log.Error(ex.ToString());
                                    }
                                }");

                sw.WriteLine("#region request update info");
                foreach (string tableName in dicTableName.Values)
                {
                    #region request update info
                    sw.WriteLine("public void Update" + tableName + "(RequestType requestType, " + tableName + " value)");
                    sw.WriteLine("{");
                    sw.WriteLine("SetAccountClientNotNetwork();");
                    sw.WriteLine("_accountClient.Update" + tableName + "(requestType, value);");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    #endregion
                }
                sw.WriteLine("#endregion");


                sw.WriteLine("#region request getlist info");
                foreach (string tableName in dicTableName.Values)
                {
                    #region request getlist info
                    sw.WriteLine("public void GetList" + tableName + "(RequestType requestType, DateTime fromTime, DateTime toTime, long requestId = 0)");
                    sw.WriteLine("{");
                    sw.WriteLine("SetAccountClientNotNetwork();");
                    sw.WriteLine("_accountClient.GetList" + tableName + "(requestType, fromTime, toTime, requestId);");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    #endregion
                }
                sw.WriteLine("#endregion");


                sw.WriteLine("");
                //end class and file
                sw.WriteLine("}");
                sw.WriteLine("}");
            }
            #endregion

            #region File Operator.Account

            string fileOperatorAccount = forderOperator + "Operator.Account.cs";
            using (StreamWriter sw = new StreamWriter(fileOperatorAccount))
            {
                sw.WriteLine(@"using System;
                                using HK.APIHander.Events;
                                using HK.APIHander.Events.EntityChanged;
                                using HK.APIHander.OperatorHandler.MemoryClient;

                                namespace HK.APIHander.OperatorHandler
                                {
                                    public partial class Operator
                                    {");


                #region EventArgs
                sw.WriteLine("#region EventArgs");

                #region EventUpdate
                sw.WriteLine("#region EventUpdate");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public event EventHandler<" + tableName + "EventArgs> On" + tableName + "EventArgs;");
                }
                sw.WriteLine("");

                sw.WriteLine(" #endregion");
                #endregion

                #region EventGetList
                sw.WriteLine("#region EventGetList");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public event EventHandler<GetList" + tableName + "EventArgs> OnGetList" + tableName + "EventArgs;");
                }
                sw.WriteLine("");

                sw.WriteLine(" #endregion");
                #endregion

                #region Event Entity Changed
                sw.WriteLine("#region Event Entity Changed");

                foreach (string tableName in dicTableName.Values)
                {
                    sw.WriteLine("public event EventHandler<" + tableName + "ChangedEventArgs> On" + tableName + "ChangedEventArgs;");
                }
                sw.WriteLine("");

                sw.WriteLine(" #endregion");
                #endregion

                sw.WriteLine(" #endregion");
                sw.WriteLine("");
                sw.WriteLine("");
                #endregion

                #region InitAccount
                sw.WriteLine("#region InitAccount");
                sw.WriteLine("public void InitAccount()");
                sw.WriteLine("{");

                foreach (string tableName in dicTableName.Values)
                {
                    #region init event
                    sw.WriteLine("_accountClient.On" + tableName + "EventArgs += RaiseOn" + tableName + "EventArgs;");
                    sw.WriteLine("_accountClient.OnGetList" + tableName + "EventArgs += RaiseOnGetList" + tableName + "EventArgs;");
                    sw.WriteLine("_accountClient.On" + tableName + "ChangedEventArgs += RaiseOn" + tableName + "ChangedEventArgs;");
                    sw.WriteLine("");
                    #endregion
                }

                sw.WriteLine("}");
                sw.WriteLine("#endregion");
                #endregion

                sw.WriteLine("#region Entity Changed");
                foreach (string tableName in dicTableName.Values)
                {
                    #region request Entity Changed
                    sw.WriteLine("private void RaiseOn" + tableName + "ChangedEventArgs(object sender, " + tableName + "ChangedEventArgs e)");
                    sw.WriteLine("{");
                    sw.WriteLine("MemorySet.SetMemory(e." + tableName + ");");
                    sw.WriteLine("if (On" + tableName + "ChangedEventArgs != null)");
                    sw.WriteLine("On" + tableName + "ChangedEventArgs(sender, e);");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    #endregion
                }
                sw.WriteLine("#endregion");


                sw.WriteLine("#region Update Info");
                foreach (string tableName in dicTableName.Values)
                {
                    #region Update Info
                    sw.WriteLine("private void RaiseOn" + tableName + "EventArgs(object sender, " + tableName + "EventArgs e)");
                    sw.WriteLine("{");
                    sw.WriteLine("if (On" + tableName + "EventArgs != null)");
                    sw.WriteLine("On" + tableName + "EventArgs(sender, e);");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    #endregion
                }
                sw.WriteLine("#endregion");


                sw.WriteLine("#region GetList");
                foreach (string tableName in dicTableName.Values)
                {
                    #region GetList
                    sw.WriteLine("private void RaiseOnGetList" + tableName + "EventArgs(object sender, GetList" + tableName + "EventArgs e)");
                    sw.WriteLine("{");
                    sw.WriteLine("foreach (var value in e.List" + tableName + ")");
                    sw.WriteLine("MemorySet.SetMemory(value);");
                    sw.WriteLine("if (OnGetList" + tableName + "EventArgs != null)");
                    sw.WriteLine("OnGetList" + tableName + "EventArgs(sender, e);");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    #endregion
                }
                sw.WriteLine("#endregion");

                sw.WriteLine("");
                //end class and file
                sw.WriteLine("}");
                sw.WriteLine("}");
            }
            #endregion
        }

        //Tạo các file process getlist, update insert trên worker
        public static void GeneratorProcessWorker(string BusinessLayerRootPath, List<DatabaseTable> listDataTable)
        {
            #region CreateForder
            string forderInsert = BusinessLayerRootPath + Path.DirectorySeparatorChar + "MessageAndData\\";
            if (!Directory.Exists(forderInsert))
                Directory.CreateDirectory(forderInsert);

            string forderProcess = forderInsert + "ProcessWorker\\";
            if (!Directory.Exists(forderProcess))
                Directory.CreateDirectory(forderProcess);

            string forderProcessGetList = forderProcess + "GetList\\";
            if (!Directory.Exists(forderProcessGetList))
                Directory.CreateDirectory(forderProcessGetList);

            string forderProcessUpdate = forderProcess + "Update\\";
            if (!Directory.Exists(forderProcessUpdate))
                Directory.CreateDirectory(forderProcessUpdate);


            #endregion

            var dicTableName = new Dictionary<string, string>();
            foreach (DatabaseTable table in listDataTable)
            {
                if (table.IsSelected)
                {
                    dicTableName[table.TableName] = table.TableName;
                }
            }


            #region Create Process GetList

            string textFunctionGetList = GetTextFile("ProcessGetList.txt");
            foreach (string tableName in dicTableName.Values)
            {
                string fileProcess = forderProcessGetList + "ProcessGetList" + tableName + ".cs";

                using (StreamWriter sw = new StreamWriter(fileProcess))
                {
                    string textFunction = textFunctionGetList.Replace("ENTITY_NAME", tableName);
                    sw.WriteLine(textFunction);
                }
            }
            
            #endregion

            #region Create Process UpdateInsert
            string textFunctionUpdateInsert = GetTextFile("ProcessUpdateInsert.txt");
            foreach (string tableName in dicTableName.Values)
            {
                string fileProcess = forderProcessUpdate + "ProcessUpdate" + tableName + ".cs";
                using (StreamWriter sw = new StreamWriter(fileProcess))
                {
                    string textFunction = textFunctionUpdateInsert.Replace("ENTITY_NAME", tableName);
                    sw.WriteLine(textFunction);
                }
            }

            #endregion
        }

        private static string GetTextFile(string fileName)
        {
            string textFunction = "";
            try
            {
                string fileFuncTemplate = GetAppPath() + "\\CodeGeneration\\FuncBuild\\" + fileName.Trim();
                if (File.Exists(fileFuncTemplate))
                {
                    //Nếu có template func xử lý thì insert luôn func
                    var streamReader = new StreamReader(fileFuncTemplate);
                    textFunction = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return textFunction;
        }

        private static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }
    }
}
