//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcTusService.TuesdayModel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    [DataContract]
    public partial class tb_reward_Template_imp
    {
        [DataMember]
        public int pk_imp_id { get; set; }
        [DataMember]
        public int fk_rewardTemplate_id { get; set; }
        [DataMember]
        public double dbl_count { get; set; }
        [DataMember]
        public int fk_reward_id { get; set; }
        [DataMember]
        public bool bit_isDelete { get; set; }
    }
}
