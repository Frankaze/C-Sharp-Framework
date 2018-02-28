namespace BaseObject.CommonObject
{
    public enum DayOfWeekEnum
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    public enum ExcelCellAreaTypeEnum
    {
        Normal,
        Title,
        SubSummary,
        Summary
    }

    public enum ExcelCellNumericTypeEnum
    {
        NonNumeric,
        Integer,
        Percentage,
        PercentageOnSecondDecimalPoint
    }

    /// <summary>
    /// 單位代碼
    /// </summary>
    public enum OrganizationUnitCodeEnum
    {
        Office,//室
        Division,//處
        Department,//部
        Team//組
    }

    /// <summary>
    /// 權限型別狀態
    /// </summary>
    public enum PermissionTypeEnum
    {
        Control,
        Form,
        Function,
        Page,
        Report
    }

    /// <summary>
    /// 資料列狀態
    /// </summary>
    public enum RowStatusEnum
    {
        Add,
        Modify,
        Delete,
        None
    }

    /// <summary>
    /// 通用狀態
    /// </summary>
    public enum StatusEnum
    {
        Active,    //啟用
        Cancel,    //取消
        Close,     //結案 or 關閉
        Disabled,   //停用
        Inactive,  //未啟用
        Normal,   //正常
        Void,       //刪除
        Approved, //同意
        Reject,    //拒絕
        Enabled,   //可用 or 啟用
        Processing, // 處理中
        Wait //等待
    }

    /// <summary>
    /// 同步狀態
    /// </summary>
    public enum SyncStatusEnum
    {
        Synced, //已同步
        WaitToSync //待同步
    }
}