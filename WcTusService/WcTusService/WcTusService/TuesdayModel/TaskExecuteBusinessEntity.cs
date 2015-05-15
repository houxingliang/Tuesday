using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 任务执行业务实体类
    /// </summary>
    public class TaskExecuteBusinessEntity
    {
        private tb_task task;//任务信息

        public tb_task Task
        {
            get { return task; }
            set { task = value; }
        }
        private int shareNum;//分享次数

        public int ShareNum
        {
            get { return shareNum; }
            set { shareNum = value; }
        }
    }
}