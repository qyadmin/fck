using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonFrame
{
    public class DefaultUserData
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName;
        /// <summary>
        /// 名字
        /// </summary>
        public string UserName;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id;
        /// <summary>
        /// 今日收益
        /// </summary>
        public string TodayEarning;
        /// <summary>
        /// 余额
        /// </summary>
        public string Balance;
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPortrait;
        /// <summary>
        /// 金币
        /// </summary>
        public string Gold;
        /// <summary>
        /// 银行内的金币
        /// </summary>
        public string BankGold;

        public Dictionary<string, string> OtherDataDic = new Dictionary<string, string>();
        public void InitUserData()
        {

        }

        public void UpdateUserData()
        {

        }

        public void UpdateOtherData()
        {

        }
    }
}