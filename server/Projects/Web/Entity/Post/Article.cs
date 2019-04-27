using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wings.Projects.Web.Entity.Post
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table("article")]
    public class Article
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        /// <value></value>
        public string title { get; set; }
        /// <summary>
        /// 发布人
        /// </summary>
        /// <value></value>
        public int userId { get; set; }
        /// <summary>
        /// 文章字符长度
        /// </summary>
        /// <value></value>
        public int charNum { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        /// <value></value>
        public DateTime? createAt { get; set; } = DateTime.Now;
        /// <summary>
        /// 上次修改时间
        /// </summary>
        /// <value></value>
        public DateTime? lastModifyAt { get; set; } = DateTime.Now;
        /// <summary>
        /// markdown
        /// </summary>
        /// <value></value>
        public string markdown { get; set; } = "";
        /// <summary>
        /// html
        /// </summary>
        /// <value></value>
        public string html { get; set; } = "";

        /// <summary>
        /// 作者
        /// </summary>
        /// <value></value>
        public string author { get; set; } = "";
        /// <summary>
        /// 文章来源类型 0原创1转载
        /// </summary>
        /// <value></value>
        public SourceType? sourceType { get; set; } = SourceType.Create;
        /// <summary>
        /// 简介
        /// </summary>
        /// <value></value>
        public string summary { get; set; }
        /// <summary>
        /// 首页图片
        /// </summary>
        /// <value></value>
        public string bannerImageUrl { get; set; }
        /// <summary>
        /// 使用阅读量
        /// </summary>
        /// <value></value>
        public bool? useRead { get; set; } = false;
        /// <summary>
        /// 定制阅读量
        /// </summary>
        /// <value></value>
        public int? useReadNum { get; set; } = 0;
        /// <summary>
        /// 使用正在阅读
        /// </summary>
        /// <value></value>
        public bool? useReading { get; set; } = false;
        /// <summary>
        /// 正在阅读数量
        /// </summary>
        /// <value></value>
        public int? useReadingNum { get; set; } = 0;
        /// <summary>
        /// 阅读次数
        /// </summary>
        /// <value></value>
        public int? readNum { get; set; } = 0;
        /// <summary>
        /// 使用音频
        /// </summary>
        /// <value></value>
        public bool? useAudio { get; set; } = false;
        /// <summary>
        /// 音频地址
        /// </summary>
        /// <value></value>
        public string audioUrl { get; set; } = "";
        /// <summary>
        /// 精度
        /// </summary>
        /// <value></value>
        public decimal? lng { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal? lat { get; set; }
        /// <summary>
        /// 地址名字
        /// </summary>
        /// <value></value>
        public string addressName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <value></value>
        public string address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <value></value>
        public string contactPhone { get; set; }
        /// <summary>
        /// 启用地址
        /// </summary>
        /// <value></value>
        public bool? useAddress { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        /// <value></value>
        public int? commentNum { get; set; } = 0;

    }
    /// <summary>
    /// 来源类型
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// 原创
        /// </summary>
        Create,
        /// <summary>
        /// 转载
        /// </summary>
        Reprint

    }
}