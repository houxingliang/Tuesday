﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    public class UserData
    {
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns>受影响行数</returns>
        public int AddUser(tb_user user)
        {
            if (user != null)
            {
                share.tb_user.Add(user);
                int returnNum = share.SaveChanges();
                return returnNum;
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int EditUser(tb_user user)
        {
            if (user != null)
            {
                share.Entry(user).State = EntityState.Modified;
                return share.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据主键ID查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_user GetUserByID(int id)
        {
            var user = from r in share.tb_user
                         where r.int_user_id== id
                         select r;
            if (user.ToList().Count > 0)
            {
                return user.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <returns></returns>
        public List<tb_user> GetUserList()
        {
            var users = from r in share.tb_user
                          select r;
            return users.ToList();
        }
        /// <summary>
        /// 查看手机号是否已被占用
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public bool IsUsedPhone(string phoneNum)
        {
            var query = from p in share.tb_user
                        where p.vr_phoneNum == phoneNum
                        select p;
            if (query != null)
                return true;
            else
                return false;
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
            var query = from p in share.tb_user
                        where p.nvr_wxName.Contains(nickName) ||
                        p.nvr_userName.Contains(name) ||
                        p.vr_phoneNum.Contains(phoneNum)
                        select p;
            if (query != null)
                return query.ToList();
            else
                return null;
        }
        /// <summary>
        /// 根据电话号码
        /// 查询用户信息
        /// </summary>
        /// <param name="num">用户的电话号码</param>
        /// <returns></returns>
        public tb_user GetUserByPhoneNum(string num)
        {
            var query = from p in share.tb_user
                        where p.vr_phoneNum == num
                        select p;
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 根据星期二用户ID
        /// 查询用户信息
        /// </summary>
        /// <param name="id">星期二用户主键ID</param>
        /// <returns></returns>
        public tb_user GetUserByTuesdayId(string id)
        {
            var query = from p in share.tb_user
                        where p.vr_Tuesday.Equals(id)
                        select p;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 根据微信OpenID
        /// 查询用户信息
        /// </summary>
        /// <param name="id">微信OpenID</param>
        /// <returns></returns>
        public tb_user GetUserByOpenId(string id)
        {
            var query = from p in share.tb_user
                        where p.vr_wxOpenID.Equals(id)
                        select p;
            return query.FirstOrDefault();
        }
    }
}