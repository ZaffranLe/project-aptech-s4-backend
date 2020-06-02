var  DataOperator = {
    Like: 1,
    NotLike: 2,
    IsIn: 3,
    IsNotIn: 4,
    IsEqualTo: 5,
    IsNotEqualTo: 6,
    IsGreaterThan: 7,
    IsGreaterThanOrEqualTo: 8,
    IsLessThan: 9,
    IsLessThanOrEqualTo: 10,
    IsNull: 11,
    IsNotNull: 12
};

var  EntityAction = {
    None: 0,
    Insert: 1,
    Update: 2,
    Delete: 3
};

var  EntityGet = {
    GetAllValues: 1,
    GetCustomValues: 2,
    GetCustomStore: 3
};

var  WithdrawRuleType = {
    OriginalCurrency: 1,
    DepositTotalConvert: 2,
    DepositTotalConvert_Original: 3
};

var  WorkerType = {
    None: 1,
    Database: 2,
    CommunicationMemory: 3,
    User: 4,
    System: 5,
    PreTrade: 6,
    Session: 7,
    Risk: 8,
    Dealing: 9,
    DataProvider: 10,
    Update: 11,
    SOR: 12,
    PostTrade: 13,
    Pricing: 14,
    Task: 15,
    Report: 16,
    Alert: 17,
    CqgFixConnector: 18,
    Sinker: 19,
    CqgWebApiConnector: 20,
    FTPSync: 21,
    Authentication: 22,
    WebServices: 23,
    CqgFixConverter: 24,
    CqgWebApiConverter: 25,
    Business: 26,
    PricingUpdater: 27,
    ControlLogWorker: 28,
    AlertSms: 29,
    CqgFixImport: 30,
    EquixSyncOrder: 31,
    EquixSyncFromCore: 32,
    CoreSyncCashBalance: 34,
    CoreSyncAccountInfo: 35,
    CoreSyncAssignSymbol: 36,
    CoreSyncFee: 37,
    CoreSyncFxRate: 38,
    CoreSyncHolding: 39,
    EquixSyncAvail: 40,
    EquixSyncSettlement: 41,
    CoreSyncCommodityInfo: 42
};

var MsgRequest = {
};

var MsgResponse = {
};

var MsgBroadcast = {
};

var MsgEntity = {
    Customer: 'Customer',
    Image: 'Image',
    ImportReceipt: 'ImportReceipt',
    Manufacturer: 'Manufacturer',
    OrderDetail: 'OrderDetail',
    Permission: 'Permission',
    Product: 'Product',
    ProductType: 'ProductType',
    Property: 'Property',
    Provider: 'Provider',
    Role: 'Role',
    RolePermission: 'RolePermission',
    UserInfo: 'UserInfo',
    UserLogin: 'UserLogin',
    UserRole: 'UserRole',
};

