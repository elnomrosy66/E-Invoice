
public enum IsDelete { Active, Deleted}
public enum ItemType { GS1 , EGS}

public enum AccountType { Customer , supplier , Treasury , Bank  }
public enum AccountNature { Debit , Credit }
public enum JournalType { Sales , Purchase }

public enum OrderPayment { Agel , CACH }

public enum OrderType { Sale, Purcahse, SaleReturn, PurchaseReturn, Transfer , InitialBalance , Destroy }
public enum OrderEinvType { I,C,D}
public enum OrderEinvSatatus { Submitted, Valid , Invalid ,Cancled , Rejected  }
public enum OrderEinvSend { NotSent , Sent}
public enum CanonicalType { P , B , F}