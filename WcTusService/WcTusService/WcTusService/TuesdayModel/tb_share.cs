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
    public partial class tb_share
    {
        [DataMember]
        public int pk_share_id { get; set; }
        [DataMember]
        public string nvr_shareName { get; set; }
        [DataMember]
        public string nvr_shareContents { get; set; }
        [DataMember]
        public int fk_rewardTemplate_id_f { get; set; }
        [DataMember]
        public bool bit_secondShare { get; set; }
        [DataMember]
        public int fk_rewardTemplate_id_s { get; set; }
        [DataMember]
        public double dbl_secondShareLimit { get; set; }
        public int int_firstShareTime { get; set; }
        [DataMember]
        public int int_secondShareTime { get; set; }
        [DataMember]
        public System.DateTime dtm_createTime { get; set; }
        [DataMember]
        public bool bit_isDelete { get; set; }
    }
}
