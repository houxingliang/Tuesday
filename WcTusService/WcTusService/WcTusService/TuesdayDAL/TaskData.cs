using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 任务表
    /// </summary>
    public class TaskData
    {
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task"></param>
        public int Addtask(tb_task task)
        {
            share.tb_task.Add(task);
            return share.SaveChanges();
        }
        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="task"></param>
        public int EditTask(tb_task task)
        {
            share.Entry(task).State = EntityState.Modified;
            return share.SaveChanges();
           
        }
        /// <summary>
        /// 根据主键得到任务
        /// </summary>
        /// <param name="task"></param>
        public tb_task GettaskByid(int taskid)
        {
            var ta = from p in share.tb_task
                     where p.pk_task_id == taskid
                     select p;
            if (ta.Count() != 0)
            {
                return ta.FirstOrDefault();

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到全部任务
        /// </summary>
        /// <param name="task"></param>
        public List<tb_task> GettaskList()
        {
            var ta = from p in share.tb_task
                    where p.bit_isDelete==false
                     select p;
            if (ta.Count() != 0)
            {
                return ta.ToList();

            }
            else
            {
                return null;
            }
        }
    }
}