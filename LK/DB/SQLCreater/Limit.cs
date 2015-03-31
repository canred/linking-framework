using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.DB.SQLCreater
{
    public class OrderLimit
    {
        List<String> _orderColumn = new List<string>();
        List<OrderMethod> _orderMethod = new List<OrderMethod>();
        decimal? _start = null;
        decimal? _limit = null;
        public enum OrderMethod{
            ASC,
            DESC,
            NULL
        }
        public OrderLimit() { }
        public OrderLimit(string column, OrderMethod method)
        {
            _orderColumn.Add(column);
            _orderMethod.Add(method);
        }
        public void AddOrder(string column , OrderMethod method) {
            _orderColumn.Add(column);
            _orderMethod.Add(method);
        }
        public List<String> getOrderColumn()
        {
            return _orderColumn;
        }
        public List<OrderMethod> getOrderMethod() {
            return _orderMethod;
        }
        public void Clear() {
            _orderColumn = new List<string>();
            _orderMethod = new List<OrderMethod>();
        } 
        public decimal? Start {
            get {
                return _start;
            }
            set {
                _start = value;
            }
        }
        public decimal? Limit {
            get
            {
                return _limit;
            }
            set
            {
                _limit = value;
            }
        }
    }
}
