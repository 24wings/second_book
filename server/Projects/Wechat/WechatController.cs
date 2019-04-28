using System;
using Microsoft.AspNetCore.Mvc;
using Wings.Projects.Config;
// using Senparc.CO2NET.Extensions;
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
        /// http://localhost/api/wechat/oauth2/getOauthUrl?returnUrl=http://localhost:4200/home/article/25
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet("oauth2/[action]")]
        public object getOauthUrl(string returnUrl)
        {
            Console.WriteLine("hahah");
            var state = "JeffreySu-" + DateTime.Now.Subtract(new DateTime(1970, 1, 1, 1, 0, 0, 0)).TotalMilliseconds;//随机数，用于识别请求可靠性
                                                                                                                     // Session["State"] = state;//储存随机数到Session
                                                                                                                     // var url = "http://115.29.64.6/api/wechat/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode();
            var url = "http://localhost/api/wechat/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode();
            //此页面引导用户点击授权
            var UrlUserInfo =
                OAuthApi
                .GetAuthorizeUrl(WechatConfig.AppId, url,
                state, OAuthScope.snsapi_userinfo);
            // var UrlBase =
            //     OAuthApi.GetAuthorizeUrl(WechatConfig.AppId,
            //     "https://115.29.64.6/api/wechat/oauth2/BaseCallback?returnUrl=" + returnUrl.UrlEncode(),
            //     state, OAuthScope.snsapi_base);

            return this.Redirect(UrlUserInfo);
            // return this.Redirect(url);

        }
        /// <summary>
        /// oauth2认证
        /// </summary>
        /// <returns></returns>
        [HttpGet("oauth2/[action]")]
        public object userInfoCallback(string code, string state, string returnUrl)
        {

            var res = new { code = code, state = state, returnUrl = returnUrl };

            return this.Redirect(returnUrl);

        }


    }
}