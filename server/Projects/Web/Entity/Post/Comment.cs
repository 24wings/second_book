using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wings.Projects.Web.Entity
{
    /// <summary>
    /// 评论表
    /// </summary>
    [Table("comment")]
    public class Comment
    {
        /// <summary>
        /// id
        /// </summary>
        /// <value></value>
        public int id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        /// <value></value>
        public int userId { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        /// <value></value>
        public int parentId { get; set; } = 0;

        /// <summary>
        /// 内容
        /// </summary>
        /// <value></value>
        public string content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        public DateTime createTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 文章id
        /// </summary>
        /// <value></value>
        public int? articleId { get; set; }

    }
}