using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 用户业务逻辑类
    /// </summary>
    public class UserManager
    {
        UserData userData = null;
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public int AddUser(tb_user user)
        {
            userData = new UserData();
            return userData.AddUser(user);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int EditUser(tb_user user)
        {
            userData = new UserData();
            return userData.EditUser(user);
        }
        /// <summary>
        /// 根据用户主键ID
        /// 查询用户信息
        /// </summary>
        /// <param name="id">用户主键ID</param>
        /// <returns></returns>
        public tb_user GetUserById(int id)
        {
            userData=new UserData();
            return userData.GetUserByID(id);
        }
        /// <summary>
        /// 根据电话号码
        /// 查询用户信息
        /// </summary>
        /// <param name="num">用户的电话号码</param>
        /// <returns></returns>
        public tb_user GetUserByPhoneNum(string num)
        {
            userData = new UserData();
            return userData.GetUserByPhoneNum(num);
        }
        /// <summary>
        /// 根据星期二用户ID
        /// 查询用户信息
        /// </summary>
        /// <param name="id">星期二用户主键ID</param>
        /// <returns></returns>
        public tb_user GetUserByTuesdayId(string id)
        {
            userData = new UserData();
            return userData.GetUserByTuesdayId(id);
        }
        /// <summary>
        /// 查看手机号是否被占用
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public Boolean IsUsedPhone(string phoneNum)
        {
            userData = new UserData();
            return userData.IsUsedPhone(phoneNum);
        }

        /// <summary>
        /// 根据微信名称或用户名或电话号码查询
        /// 用户信息
        /// </summary>
        /// <param name="nickName">微信昵称</param>
        /// <param name="name">用户名</param>
        /// <param name="phoneNum">电话号码</param>
        /// <returns></returns>
        public List<tb_user> GetUserByNameOrPhone(string nickName, string name, string phoneNum)
        {
            List<tb_user> query = new UserData().GetUserByNameOrPhone(nickName,name,phoneNum);
            if (query != null)
                return query.ToList();
            else
                return null;
        }

    }
}