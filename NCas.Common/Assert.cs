using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCas.Common.Exceptions;

namespace NCas.Common
{
    public class Assert
    {
        public static void IsNotNull(string name, object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException(string.Format("{0}不能为空", name));
            }
        }
        public static void IsNotNullOrEmpty(string name, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(string.Format("{0}不能为空", name));
            }
        }
        public static void IsNotNullOrWhiteSpace(string name, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(string.Format("{0}不能为空", name));
            }
        }
        public static void AreEqual(string id1, string id2, string errorMessageFormat)
        {
            if (id1 != id2)
            {
                throw new ArgumentException(string.Format(errorMessageFormat, id1, id2));
            }
        }

        public static void IsNotGreater(string name, int input, int least)
        {
            if (input <= least)
            {
                throw new ArgumentException(string.Format("{0}必须大于{1}", name, least));
            }
        }

        public static void IsNotMaxLength(string name, string input, int maxLength)
        {
            if (input.Length > maxLength)
            {
                throw new ValidateException(string.Format("{0}应在{1}字符以内", name, maxLength));
            }
        }

        public static void IsNotInEnum(string name, Type type, object value)
        {
            if (!Enum.IsDefined(type, value))
            {
                throw new NotInEnumException(string.Format("{0}不在有效的范围内", name));
            }
        }
        //t2的时间是否大于t1
        public static void IsNotTimeGreater(string name1, DateTime t1, string name2, DateTime t2)
        {
            if (DateTime.Compare(t2, t1) <= 0)
            {
                throw new ValidateException(string.Format("{0}必须晚于{1}", name2, name1));
            }
        }

        //验证
        public static void IsNotEmail(string name, string email)
        {
            //if (!GUtils.Regexs.RegexValidate.IsEmailAddress(email))
            //{
            //    throw new ValidateException(string.Format("{0}不是有效的邮箱地址", name));
            //}
        }
        public static void IsNotUrl(string name, string url)
        {
            //if (!GUtils.Regexs.RegexValidate.IsURL(url))
            //{
            //    throw new ValidateException(string.Format("{0}不是有效的Url地址", name));
            //}
        }
    }
}
