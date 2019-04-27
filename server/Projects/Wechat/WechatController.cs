using System;
using Microsoft.AspNetCore.Mvc;
using Wings.Projects.Config;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP;

namespace Wings.Projects.Wechat
{
    /// <summary>
    /// 组织管理
    /// </summary>
    [Route("/api/wechat")]
    public class WechatController : Controller
    {

        /// <summary>
        /// 微信接入
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public object auth(string signature, int timestamp, string nonce, string echostr)
        {
            Console.WriteLine("登录");

            return echostr;
        }

        /// <summary>
        /// 微信授权
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public object oauth(string returnUrl)
        {
            Console.WriteLine("hahah");
            var state = "JeffreySu-" + DateTime.Now.Subtract(new DateTime(1970, 1, 1, 1, 0, 0, 0)).TotalMilliseconds;//随机数，用于识别请求可靠性
            // Session["State"] = state;//储存随机数到Session

            ViewData["returnUrl"] = returnUrl;

            //此页面引导用户点击授权
            ViewData["UrlUserInfo"] =
                OAuthApi
                .GetAuthorizeUrl(WechatConfig.AppId,
                "https://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode(),
                state, OAuthScope.snsapi_userinfo);
            ViewData["UrlBase"] =
                OAuthApi.GetAuthorizeUrl(WechatConfig.AppId,
                "https://sdk.weixin.senparc.com/oauth2/BaseCallback?returnUrl=" + returnUrl.UrlEncode(),
                state, OAuthScope.snsapi_base);
            return View();
        }
    }
}