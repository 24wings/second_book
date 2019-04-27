using System;
using Microsoft.AspNetCore.Mvc;

namespace Wings.Projects.Wechat {
    /// <summary>
    /// 组织管理
    /// </summary>
    [Route ("/api/wechat")]
    public class WechatController {

        /// <summary>
        /// 微信授权认证
        /// </summary>
        /// <returns></returns>
        [HttpGet ("[action]")]
        public object auth (string signature, int timestamp, string nonce, string echostr) {
            Console.WriteLine ("登录");

            return echostr;
        }
    }
}