using System.Runtime.Serialization;

namespace NCas.Client
{
    /// <summary>账号信息
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>本系统唯一的Id
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>账号名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        public Account(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
