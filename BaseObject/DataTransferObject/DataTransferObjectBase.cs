using BaseObject.CommonObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;

namespace BaseObject.DataTransferObject
{
    [DataContract]
    public abstract class DataTransferObjectBase
    {
        #region Constant

        #endregion Constant

        #region Variable

        protected List<PropertyInfo> m_propertyAccumulateList;
        protected bool m_bolAccumulateValueStatus;
        protected RowStatusEnum m_enumRowStatus = RowStatusEnum.None;
        protected List<PropertyInfo> m_propertyUpdateList;
        protected bool m_bolUpdateValueStatus;

        //Default 為 None
        private string m_RowStatus;

        #endregion Variable

        #region Property

        [DataMember]
        public string RowStatus
        {
            get { return this.m_RowStatus; }
            set
            {
                this.m_RowStatus = value;
            }
        }

        #endregion Property

        #region Constructor

        public DataTransferObjectBase()
        {
            this.m_propertyUpdateList = new List<PropertyInfo>();
            this.m_propertyAccumulateList = new List<PropertyInfo>();
            this.ResetDefaultValue();
        }

        #endregion Constructor

        #region Function

        public void AddAccumulateProperty(PropertyInfo property)
        {
            foreach (PropertyInfo _property in this.m_propertyAccumulateList)
            {
                if (_property.Name.Equals(property.Name))
                {
                    return;
                }
            }
            m_propertyAccumulateList.Add(property);
        }

        public void AddUpdateProperty(PropertyInfo property)
        {
            foreach (PropertyInfo _property in this.m_propertyUpdateList)
            {
                if (_property.Name.Equals(property.Name))
                {
                    return;
                }
            }

            m_propertyUpdateList.Add(property);
        }

        public void BeginAccumulateValue()
        {
            this.m_bolAccumulateValueStatus = true;
        }

        public void BeginUpdateValue()
        {
            this.m_bolUpdateValueStatus = true;
        }

        public void EndAccumulateValue()
        {
            this.m_bolAccumulateValueStatus = false;
        }

        public void EndUpdateValue()
        {
            this.m_bolUpdateValueStatus = false;
        }

        public List<PropertyInfo> GetAccumulatePropertyList()
        {
            return this.m_propertyAccumulateList;
        }

        public RowStatusEnum GetRowStatus()
        {
            return this.m_enumRowStatus;
        }

        public List<PropertyInfo> GetUpdatePropertyList()
        {
            return this.m_propertyUpdateList;
        }

        /// <summary>
        /// 從DataRow取得Data Transfer Object內屬性的對應值
        /// </summary>
        /// <param name="dr"></param>
        public void GetValueFromDataRow(DataRow dr)
        {
            PropertyInfo[] _propertyArray = this.GetType().GetProperties();

            foreach (PropertyInfo _property in _propertyArray)
            {
                //欄位為Null或欄位不存在時
                if (_property.Name != "RowStatus")
                {
                    if ((!(dr[_property.Name] is DBNull)) && dr.Table.Columns.Contains(_property.Name) == true)
                    {
                        if (_property.PropertyType.Name == "Int32" && dr[_property.Name].GetType() == typeof(Boolean))
                        {
                            if ((bool)dr[_property.Name] == true)
                            {
                                _property.SetValue(this, 1, null);
                            }
                            else
                            {
                                _property.SetValue(this, 0, null);
                            }
                        }
                        else
                        {
                            //Convert.ChangeType(dr[_property.Name], _property.PropertyType)
                            //_property.SetValue(this, dr[_property.Name], null);
                            _property.SetValue(this, Convert.ChangeType(dr[_property.Name], _property.PropertyType), null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 將參數重設成預設值
        /// </summary>
        public virtual void ResetDefaultValue()
        {
            PropertyInfo[] _propertyArray = this.GetType().GetProperties();
            foreach (PropertyInfo _property in _propertyArray)
            {
                switch (_property.PropertyType.Name)
                {
                    case "Int32":
                        _property.SetValue(this, 0, null);
                        break;

                    case "Double":
                        _property.SetValue(this, (Double)0, null);
                        break;

                    case "Decimal":
                        _property.SetValue(this, (Decimal)0, null);
                        break;

                    case "String":
                        _property.SetValue(this, "", null);
                        break;

                    case "DateTime":
                        _property.SetValue(this, new DateTime(1900, 1, 1), null);
                        break;

                    case "Boolean":
                        _property.SetValue(this, false, null);
                        break;

                    default:
                        break;
                }
            }

            //初始值必須是False
            this.m_bolUpdateValueStatus = false;
            this.m_bolAccumulateValueStatus = false;
            this.m_propertyUpdateList.Clear();
            this.m_propertyAccumulateList.Clear();
        }

        public void SetRowStatus(RowStatusEnum enumRowStatus)
        {
            this.m_enumRowStatus = enumRowStatus;
        }

        #endregion Function
    }

    [DataContract]
    public abstract class DataTransferObjectBase_Query
    {
        #region Constant

        #endregion Constant

        #region Variable

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        public DataTransferObjectBase_Query()
        { }

        #endregion Constructor

        #region Function

        #endregion Function

        #region Class

        #endregion Class
    }
}