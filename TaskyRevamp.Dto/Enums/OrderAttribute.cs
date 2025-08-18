using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Enums
{
    public class OrderAttribute : Attribute
    {
        public int Order { get; }
        public OrderAttribute(int order) => Order = order;
    }

    public static partial class EnumExtensions
    {
        public static List<T> GetOrderedValues<T>() where T : Enum
        {
            return typeof(T).GetFields()
                .Where(f => f.IsLiteral)
                .Select(f => new
                {
                    Value = (T)f.GetValue(null)!,
                    Order = f.GetCustomAttributes(typeof(OrderAttribute), false)
                             .Cast<OrderAttribute>()
                             .FirstOrDefault()?.Order ?? int.MaxValue
                })
                .OrderBy(x => x.Order)
                .Select(x => x.Value)
                .ToList();
        }
    }
}
