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
    public partial class tb_rewardTemplate
    {
        [DataMember]
        public int pk_rewardTemplate_id { get; set; }
        [DataMember]
        public string nvr_tmpName { get; set; }
        [DataMember]
        public bool bit_isDelete { get; set; }
    }
}
