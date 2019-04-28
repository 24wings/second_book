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
using Wings.Projects.Web;
using Cucr.CucrSaas.App.Service;
using Wings.Projects.Web.Entity.Rbac;
using System.Linq;
using Senparc.Weixin.MP.Helpers;
using Wings.Base.Common.DTO;

namespace Wings.Projects.Wechat
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenUser
    {
        public string token { get; set; }
        public int userId { get; set; }
    }
    /// <summary>
    /// 组织管理
    /// </summary>
    [Route("/api/wechat")]
    public class WechatController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        private RcxhContext db { get; set; }
        private IUserService userService { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_userService"></param>
        /// <returns></returns>
        public WechatController(RcxhContext _db, IUserService _userService)
        {
            this.userService = _userService;
            db = _db;
        }

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
            var url = "http://test.nu59n.cn/api/wechat/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode();
            //此页面引导用户点击授权
            var UrlUserInfo =
                OAuthApi
                .GetAuthorizeUrl(WechatConfig.AppId, url,
                state, OAuthScope.snsapi_userinfo);
            Console.WriteLine(url);
            Console.WriteLine(UrlUserInfo);

            // this.HttpContext.Session.Set("State",state.ge);
            // Cook
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
            Console.WriteLine("=================:" + code);
            if (code != null && code != String.Empty)
            {

                OAuthAccessTokenResult result = null;
                result = OAuthApi.GetAccessToken(WechatConfig.AppId, WechatConfig.secret, code);

                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);

                var wxUser = (from u in this.db.wxUsers where u.openid == userInfo.openid select u).FirstOrDefault();
                if (wxUser == null)
                {
                    var newUser = new User { nickname = userInfo.nickname, headimg = userInfo.headimgurl };
                    // 新用户注册
                    var newWxUser = new WxUser
                    {
                        openid = userInfo.openid,
                        nickname = userInfo.nickname,
                        headimg = userInfo.headimgurl
                    };
                    this.db.wxUsers.Add(newWxUser);
                    newUser.wxUserId = newWxUser.id;
                    this.db.users.Add(newUser);
                    Console.WriteLine("returnUrl" + returnUrl);
                    return Redirect(returnUrl + "?userId=" + newUser.id);
                    // return userInfo;
                    // return this.Redirect(returnUrl);
                }
                else
                {
                    var user = (from u in this.db.users
                                where
    u.id == wxUser.id
                                select u).FirstOrDefault();
                    return Redirect(returnUrl + "?userId=" + user.id);
                }

            }
            else
            {
                return Content("您拒绝了授权");
            }
        }

        [HttpGet("[action]")]
        public Rtn<object> jssdk()
        {
            #region v13.6.4之前的写法
            ////获取时间戳
            //var timestamp = JSSDKHelper.GetTimestamp();
            ////获取随机码
            //var nonceStr = JSSDKHelper.GetNoncestr();
            //string ticket = JsApiTicketContainer.TryGetJsApiTicket(appId, secret);
            ////获取签名
            //var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, Request.Url.AbsoluteUri);

            //ViewData["AppId"] = appId;
            //ViewData["Timestamp"] = timestamp;
            //ViewData["NonceStr"] = nonceStr;
            //ViewData["Signature"] = signature;
            #endregion

            var url = HttpContext.Request.Protocol + HttpContext.Request.Host + HttpContext.Request.Query;
            var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(WechatConfig.AppId, WechatConfig.secret, url);
            //ViewData["JsSdkUiPackage"] = jssdkUiPackage;
            //ViewData["AppId"] = appId;
            //ViewData["Timestamp"] = jssdkUiPackage.Timestamp;
            //ViewData["NonceStr"] = jssdkUiPackage.NonceStr;
            //ViewData["Signature"] = jssdkUiPackage.Signature;

            return Rtn<object>.Success(jssdkUiPackage);
        }
    }
}