
using System;
using System.Linq;
using Anotar.Log4Net;
using Language.Languages;
using QuantEdge.Common.Enum;
using QuantEdge.Entity;
using QuantEdge.Entity.Entities;
using QuantEdge.Lib.Broadcast;
using QuantEdge.Lib.Memory;
using QuantEdge.Message.Common;
using QuantEdge.Message.Memory;
using QuantEdge.Message.Response.Risk;
using QuantEdge.Message.Topic;
using QuantEdge.Worker.RiskManager.Utils;

namespace QuantEdge.Worker.RiskManager.Processor.Update%EntityName%Request
{
    public class Process_Update%EntityName%Request : IDisposable
    {
        private readonly WorkerResponse _wrkResponse = new WorkerResponse();
        private ResourcesKeyEnum _resourcesKeyEnum = ResourcesKeyEnum.Success;
        private Flow_Update%EntityName%Request _flow;
        private Update%EntityName%Response _response;

        public Process_Update%EntityName%Request()
        {
            _flow = new Flow_Update%EntityName%Request
                {
                    OnInputEntry = OnInputEntry,
                    OnCheckValidateEntry = OnCheckValidateEntry,
                    OnProcessEntry = OnProcessEntry,
                    OnOutputEntry = OnOutputEntry,
                };
        }

        private Message.Request.Risk.Update%EntityName%Request _request;
        private UpdateMemoryChanged _updateMemoryChanged;

        #region IDisposable Members

        public void Dispose()
        {
            _flow = null;
        }

        #endregion

        private void OnOutputEntry()
        {
            if (_updateMemoryChanged != null)
            {
                _wrkResponse.ListMemoryChanged.Add(_updateMemoryChanged);
            }

            _response.ErrorCode = _resourcesKeyEnum;
            _wrkResponse.ListMsgClient.Add(_response);
        }

        private void OnProcessEntry()
        {
            var isApproval = RiskUtils.IsApproval(_request.SenderMemberId, _request.SenderUserId, _request.List%EntityName%);
            if (isApproval)
            {
                var approvalPreRisk = CreateApprovalPreRisk();
                if (approvalPreRisk == null)
                {
                    _resourcesKeyEnum = ResourcesKeyEnum.ErrorTargetNone;
                    _flow.TryFireTrigger(Flow_Update%EntityName%Request.Trigger.Next);
                    return;
                }

                var curent = MemoryInfo.GetExistApproval(approvalPreRisk);
                if (curent == null)
                {
                    _updateMemoryChanged.ListEntityCommand.Add(new EntityCommand
                        {
                            BaseEntity = new Entity.Entity(approvalPreRisk),
                            EntityAction = EntityAction.Insert
                        });
                }
                else
                {
                    if (curent.ActorChanged != _request.SenderUserId)
                    {
                        _resourcesKeyEnum = ResourcesKeyEnum.ErrorIsNotSameActorChange;
                        _flow.TryFireTrigger(Flow_Update%EntityName%Request.Trigger.Next);
                        return;
                    }
                    curent.CurrentValue = approvalPreRisk.CurrentValue;
                    curent.NewValue = approvalPreRisk.NewValue;
                    curent.TimeChanged = approvalPreRisk.TimeChanged;
                    curent.ActorChanged = approvalPreRisk.ActorChanged;
                    curent.AffectSessionId = MemoryInfo.GetCurrentSessionId();
                    _updateMemoryChanged.ListEntityCommand.Add(new EntityCommand
                        {
                            BaseEntity = new Entity.Entity(curent),
                            EntityAction = EntityAction.Update
                        });
                }
            }
            else
            {
                _updateMemoryChanged.ListEntityCommand.AddRange(CreateEntityCommand(_request.List%EntityName%, _request.SenderUserId));
            }

            var actionlog = ActionLogUtils.GetEntityActionLog(_request.SenderUserId, 0,
                                                ResourcesKeyEnum.Actionlog_Update%EntityName%,
                                                "", true, true);
            if (actionlog != null)
                _updateMemoryChanged.ListEntityCommand.Add(actionlog);

            _flow.TryFireTrigger(Flow_Update%EntityName%Request.Trigger.Next);
        }

		public static IEnumerable<EntityCommand> CreateEntityCommand(List<%EntityName%> listEntityUpdate, long senderUserId = 0)
        {
            var listCommand = new List<EntityCommand>();
            try
            {
                if (listEntityUpdate.Count > 0)
                {
                    foreach (var entityUpdate in listEntityUpdate)
                    {
                        var entityAction = EntityAction.Update;
                        var entityUpdateInmemo = MemoryInfo.Get%EntityName%(entityUpdate.%EntityKey%);
                        if (entityUpdateInmemo == null)
                            entityAction = EntityAction.Insert;
						entityUpdate.ActorChanged = _request.SenderUserId;
						entityUpdate.TimeChanged = TimeInfo.GetServerTime();
                        listCommand.Add(new EntityCommand
                        {
                            BaseEntity = new Entity.Entity(entityUpdate),
                            EntityAction = entityAction
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return listCommand;
        }
		
        private void OnCheckValidateEntry()
        {
            _resourcesKeyEnum = ResourcesKeyEnum.Success;
            _flow.TryFireTrigger(Flow_Update%EntityName%Request.Trigger.Next);
        }

        private void OnInputEntry()
        {
            _response = new Update%EntityName%Response
                {
                    SendingTopic = !string.IsNullOrEmpty(_request.TemporaryTopic) ? _request.TemporaryTopic : TerminalTopic.FactoryUser(_request.SenderUserId),
                    ErrorCode = ResourcesKeyEnum.Success,
                };

            _updateMemoryChanged = new UpdateMemoryChanged();
            _flow.TryFireTrigger(Flow_Update%EntityName%Request.Trigger.Next);
        }

        public WorkerResponse ProcessMsg(Message.Request.Risk.Update%EntityName%Request msgInput)
        {
            try
            {
                if (msgInput == null || msgInput.List%EntityName% == null)
                    return _wrkResponse;

                _request = msgInput;

                OnInputEntry();

                return _wrkResponse;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return null;
        }

        private ApprovalPreRisk CreateApprovalPreRisk()
        {
            try
            {
                var newValue = JsonUtils.Serialize(_request.List%EntityName%);
                var listCurrentValue = _request.List%EntityName%.
                    Select(x => MemoryInfo.Get%EntityName%(x.%EntityKey%)).Where(tl => tl != null).ToList();

                var currentValue = string.Empty;
                if (listCurrentValue.Count > 0)
                    currentValue = JsonUtils.Serialize(listCurrentValue);

                //ObjectId là type LimitMemberType dùng để check trùng và replace giá trị cũ
                var approval = new ApprovalPreRisk
                    {
                        CreateBy = _request.SenderUserId,
                        CreateDate = TimeInfo.GetServerTime(),
                        ObjectName = %EntityName%.EntityName(),
                        Status = (int)ApprovalStatus.Request,
                        ApprovalType = (int)ApprovalType.Update%EntityName%,
                        ActorChanged = _request.SenderUserId,
                        TimeChanged = TimeInfo.GetServerTime(),
                        ObjectId = %EntityName%.EntityName(),
                        CurrentValue = currentValue,
                        NewValue = newValue,
                    };
                approval = RiskManagerUtils.SetAffectValue(approval, _request.SenderMemberId);
                return approval;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }

            return null;
        }
    }
}