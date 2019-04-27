namespace Wings.Projects.Config
{
    /// <summary>
    /// 微信配置
    /// </summary>
    public class WechatConfig
    {
        /// <summary>
        /// AppId
        /// </summary>
        /// <value></value>
        public static string AppId { get; } = "wx78e5285e4bef6c54";
        /// <summary>
        /// url
        /// </summary>
        /// <value></value>
        public static string Url { get; } = "http://115.29.64.6";
        /// <summary>
        /// 微信Token
        /// </summary>
        /// <value></value>
        public static string Token { get; } = "uWVmVXPM1mY71lYwPsSs38Zn1ys1zqux";
        /// <summary>
        /// 消息加密密钥由43位字符组成，可随机修改，字符范围为A-Z，a-z，0-9。
        /// </summary>
        /// <value></value>
        public static string EncodingAESKey { get; } = "g4BZtbw4H2Z5A8A1B94d9T5bkA5WAWn1DZ1ANb1289B";
    }
}