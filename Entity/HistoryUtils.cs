using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anotar.NLog;
using QuantEdge.Entity.Entities;

namespace QuantEdge.Entity
{
    public class HistoryUtils
    {
        //Xem entity này có phải entity ở db lịch sử hay không?
        public static bool IsHistory(string entityName)
        {
            #region Daily
            if (entityName.Equals(AccountDaily.EntityName())) return true;
            if (entityName.Equals(AccountInfoDaily.EntityName())) return true;
            if (entityName.Equals(BaseSymbolDaily.EntityName())) return true;
            if (entityName.Equals(ForexRateDaily.EntityName())) return true;
            if (entityName.Equals(MemberLimitDaily.EntityName())) return true;
            if (entityName.Equals(OpenPositionDaily.EntityName())) return true;
            if (entityName.Equals(OpenPositionDetailDaily.EntityName())) return true;
            if (entityName.Equals(OrderTransactionDaily.EntityName())) return true;
            if (entityName.Equals(SymbolSettlementPriceDaily.EntityName())) return true;
            if (entityName.Equals(ForexRateInternalDaily.EntityName())) return true;
            if (entityName.Equals(ImLmeDaily.EntityName())) return true;

            #endregion

            #region Hist
            if (entityName.Equals(AlertEmailHis.EntityName())) return true;
            if (entityName.Equals(AlertSmsHis.EntityName())) return true;
            if (entityName.Equals(LinkedTransactionHist.EntityName())) return true;
            if (entityName.Equals(OpenPositionDetailHist.EntityName())) return true;
            if (entityName.Equals(OpenPositionHist.EntityName())) return true;
            if (entityName.Equals(OrderTransactionHist.EntityName())) return true;
            if (entityName.Equals(TradingDealHist.EntityName())) return true;
            if (entityName.Equals(AccountTransactionHist.EntityName())) return true;
            if (entityName.Equals(SpecAccountingHist.EntityName())) return true;
            if (entityName.Equals(ExecutionReportHist.EntityName())) return true;

            if (entityName.Equals(ApprovalAccountHist.EntityName())) return true;
            if (entityName.Equals(ApprovalDealingHist.EntityName())) return true;
            if (entityName.Equals(ApprovalMemberHist.EntityName())) return true;
            if (entityName.Equals(ApprovalOrderHist.EntityName())) return true;
            if (entityName.Equals(ApprovalPreRiskHist.EntityName())) return true;
            if (entityName.Equals(ApprovalSystemHist.EntityName())) return true;
            if (entityName.Equals(SessionHistoryMove.EntityName())) return true;
            if (entityName.Equals(ContractNoticeDateHist.EntityName())) return true;
            if (entityName.Equals(BrokerOrderHist.EntityName())) return true;
            if (entityName.Equals(SystemMessage.EntityName())) return true;
            #endregion

            #region Action
            if (entityName.Equals(SymbolSettlementPriceAction.EntityName())) return true;
            if (entityName.Equals(TradingDealAction.EntityName())) return true;
            if (entityName.Equals(SystemConfigAction.EntityName())) return true;
            if (entityName.Equals(SymbolCQGAction.EntityName())) return true;
            if (entityName.Equals(OrderTransactionAction.EntityName())) return true;
            if (entityName.Equals(ManualPriceAction.EntityName())) return true;
            if (entityName.Equals(ForexRateAction.EntityName())) return true;
            if (entityName.Equals(BaseSymbolCQGAction.EntityName())) return true;
            if (entityName.Equals(BaseSymbolAction.EntityName())) return true;
            if (entityName.Equals(ConnectionAction.EntityName())) return true;
            if (entityName.Equals(ForexRateInternalAction.EntityName())) return true;
            #endregion

            //Các bảng khác
            if (entityName.Equals(ActionLog.EntityName())) return true;
            if (entityName.Equals(StateChangedAccount.EntityName())) return true;
            if (entityName.Equals(YdspLme.EntityName())) return true;

            return false;
        }

        //Lấy KeyName cho entity
        public static string GetKeyName(string entityName)
        {
            string keyName = entityName + "Id";
            try
            {
                #region Trường hợp key đặc biệt viết tại đây
                if (entityName == ConfigApprovalTransaction.EntityName())
                    keyName = ConfigApprovalTransaction.ConfigApprovalTransactionFields.ConfigType.ToString();
                if (entityName == MemberInfo.EntityName())
                    keyName = MemberInfo.MemberInfoFields.MemberId.ToString();
                if (entityName == UserInfo.EntityName())
                    keyName = UserInfo.UserInfoFields.UserId.ToString();
                if (entityName == SessionHistory.EntityName())
                    keyName = SessionHistory.SessionHistoryFields.SessionId.ToString();
                if (entityName == AccountTransaction.EntityName())
                    keyName = AccountTransaction.AccountTransactionFields.TransactionId.ToString();
                if (entityName == AccountTransactionView.EntityName())
                    keyName = AccountTransactionView.AccountTransactionViewFields.TransactionViewId.ToString();
                if (entityName == ChangedPasswordHist.EntityName())
                    keyName = ChangedPasswordHist.ChangedPasswordHistFields.Id.ToString();
                if (entityName == MemberTradingLimit.EntityName())
                    keyName = MemberTradingLimit.MemberTradingLimitFields.Id.ToString();
                #endregion
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return keyName;


        }

        //Xem bang này có bảng lịch sử clone từ bảng này ra hay không?
        public static string GetEntityNameHist(string entityName)
        {
            if (entityName == ApprovalAccount.EntityName())
                return ApprovalAccountHist.EntityName();
            if (entityName == ApprovalDealing.EntityName())
                return ApprovalDealingHist.EntityName();
            if (entityName == ApprovalMember.EntityName())
                return ApprovalMemberHist.EntityName();
            if (entityName == ApprovalOrder.EntityName())
                return ApprovalOrderHist.EntityName();
            if (entityName == ApprovalPreRisk.EntityName())
                return ApprovalPreRiskHist.EntityName();
            if (entityName == ApprovalSystem.EntityName())
                return ApprovalSystemHist.EntityName();

            if (entityName == AccountTransaction.EntityName())
                return AccountTransactionHist.EntityName();
            if (entityName == BrokerOrder.EntityName())
                return BrokerOrderHist.EntityName();
            if (entityName == ExecutionReport.EntityName())
                return ExecutionReportHist.EntityName();

            if (entityName == OpenPositionDetail.EntityName())
                return OpenPositionDetailHist.EntityName();
            if (entityName == OpenPosition.EntityName())
                return OpenPositionHist.EntityName();
            if (entityName == OrderTransaction.EntityName())
                return OrderTransactionHist.EntityName();
            if (entityName == SpecAccounting.EntityName())
                return SpecAccountingHist.EntityName();
            if (entityName == SymbolSettlementPrice.EntityName())
                return SymbolSettlementPriceDaily.EntityName();
            if (entityName == TradingDeal.EntityName())
                return TradingDealHist.EntityName();

            return null;
        }
    }
}
