using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cucr.CucrSaas.App.Service;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wings.Base.Common.Attrivute;
using Wings.Base.Common.DTO;
using Wings.Projects.Web.Controllers;
using Wings.Projects.Web.Entity;
using Wings.Projects.Web.Entity.Post;
using Wings.Projects.Web.Entity.Rbac;

namespace Wings.Projects.Web.RBAC.Controllers
{
    /// <summary>
    /// 创建文章
    /// </summary>
    public class CreateArticleInput : Article
    {
        /// <summary>
        /// 标题
        /// </summary>
        /// <value></value>
        public string title { get; set; }
        /// <summary>
        /// html
        /// </summary>
        /// <value></value>
        public string html { get; set; }
        /// <summary>
        /// markdown
        /// </summary>
        /// <value></value>
        public string markdown { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        /// <value></value>
        public string author { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        /// <value></value>
        public string summary { get; set; }
        /// <summary>
        /// 首版头像
        /// </summary>
        /// <value></value>
        public string bannerImageUrl { get; set; }
        /// <summary>
        /// 文章来源类型
        /// </summary>
        /// <value></value>
        public SourceType sourceType { get; set; } = SourceType.Create;
        /// <summary>
        /// 使用阅读量
        /// </summary>
        /// <value></value>
        public bool useRead { get; set; } = false;
        /// <summary>
        /// 定制阅读量
        /// </summary>
        /// <value></value>
        public int useReadNum { get; set; } = 0;
        /// <summary>
        /// 使用正在阅读
        /// </summary>
        /// <value></value>
        public bool useReading { get; set; } = false;
        /// <summary>
        /// 正在阅读数量
        /// </summary>
        /// <value></value>
        public int useReadingNum { get; set; } = 0;
        /// <summary>
        /// 阅读次数
        /// </summary>
        /// <value></value>
        public int read { get; set; } = 0;
        /// <summary>
        /// 地址名称
        /// </summary>
        /// <value></value>
        public string addressName { get; set; }





    }
    /// <summary>
    /// 
    /// </summary>
    public class CommentCreateInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int articleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string content { get; set; }
    }
    /// <summary>
    /// 组织管理
    /// </summary>
    [Route("/api/Hk/article")]
    public class ArticleController : CurdController<Article>
    {
        private RcxhContext db { get; set; }
        /// <summary>
        /// 用户业务
        /// </summary>
        /// <value></value>
        private IUserService userService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_userService"></param>
        public ArticleController(RcxhContext _db, IUserService _userService) : base(_db)
        {
            this.userService = _userService;
            db = _db;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public object load(DataSourceLoadOptions options)
        {
            var tokenUser = this.userService.getUserFromAuthcationHeader();
            var query = (from a in this.db.articles where a.userId == tokenUser.id select a);
            return DataSourceLoader.Load(query, options);
        }
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public object insert([FromBody] CreateArticleInput input)
        {
            var article = new Article
            {
                html = input.html,
                title = input.title,
                markdown = input.markdown,
                author = input.author,
                sourceType = input.sourceType,
                bannerImageUrl = input.bannerImageUrl,
                summary = input.summary,
            };
            var tokenUser = this.userService.getUserFromAuthcationHeader();
            if (tokenUser != null)
            {
                article.userId = tokenUser.id;
                article.charNum = input.markdown.Length;
                this.db.articles.Add(article);
                this.db.SaveChanges();

            }

            return Rtn<Article>.Success(article);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public object remove(DevExtremInput input)
        {
            return this.remove(input.key, this.db.articles);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public object update([FromForm] DevExtremInput input)
        {
            return this.update(input, this.db.articles);
        }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public Rtn<Article> info(int id)
        {
            var article = this.db.articles.Find(id);
            if (article != null)
            {
                return Rtn<Article>.Success(article);
            }
            else
            {
                return Rtn<Article>.Error("文章已经删除");
            }
        }

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Rtn<Article> createComment([FromBody] CommentCreateInput input)
        {
            var article = this.db.articles.Find(input.articleId);

            if (article != null)
            {
                var newComment = new Comment { articleId = input.articleId, content = input.content };
                this.db.comments.Add(newComment);
                if (article.commentNum != null)
                {
                    article.commentNum++;
                }
                else
                {
                    article.commentNum = 1;
                }
                this.db.SaveChanges();
                return Rtn<Article>.Success(article);

            }
            else
            {
                return Rtn<Article>.Error("文章不存在");
            }

        }

    }
}