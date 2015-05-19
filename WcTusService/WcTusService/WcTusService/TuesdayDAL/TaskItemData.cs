using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 任务项
    /// </summary>
    public class TaskItemData
    {
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 根据任务得到任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<tb_taskItem> GetItemBytaskid(int id)
        {
            var con = from p in share.tb_taskItem
                      where p.fk_task_id == id&&
                      p.bit_isDelete==false
                      select p;
            if (con.Count() != 0)
            {
                return con.ToList();

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据任务项主键得到任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_taskItem GetItemByid(int id)
        {
            var con = from p in share.tb_taskItem
                      where p.pk_taskItem_id == id
                      select p;
            if (con.Count() != 0)
            {
                return con.FirstOrDefault();

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 添加任务项
        /// </summary>
        /// <param name="task"></param>
        public int AddtaskItem(tb_taskItem task)
        {
            share.tb_taskItem.Add(task);
            return share.SaveChanges();
        }

        /// <summary>
        /// 更新任务项
        /// </summary>
        /// <param name="task"></param>
        public int EdittaskItem(tb_taskItem task)
        {
            if (task != null)
            {
                share.Entry(task).State = EntityState.Modified;
                return share.SaveChanges();
            }
            else
                return 0;
        }
        /// <summary>
        /// 删除任务项
        /// </summary>
        /// <param name="id"></param>
        public int DelTaskItem(int id)
        {
            tb_taskItem ti = GetItemByid(id);
            if (ti != null)
            {
                ti.bit_isDelete = true;
                return EdittaskItem(ti);
            }
            return 0;
        }

    }
}