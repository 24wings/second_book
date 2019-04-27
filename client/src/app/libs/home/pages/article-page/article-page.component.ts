import { Component, Input, ViewChild, ViewEncapsulation, Output, EventEmitter } from '@angular/core';
import { MyHttpService } from 'src/app/shared/services/my-http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from 'src/app/struct/entity/article';

@Component({
    selector: "article-page",
    templateUrl: "./article-page.component.html",
    styleUrls: ['./article-page.component.css'],
    // encapsulation: ViewEncapsulation.ShadowDom
    encapsulation: ViewEncapsulation.None
})
export class ArticlePageComponent {

    constructor(private myHttp: MyHttpService, private route: ActivatedRoute, private router: Router) {
        var id = this.route.snapshot.params['id']
        if (id) {
            this.loadArticleById(id)
        }
    }
    async  loadArticleById(id: number) {
        var res = await this.myHttp.Get("/api/Hk/article/info?id=" + id);
        debugger;
        if (res) {
            this.articleMeta = res;
            this.html = res.html;
            this.markdown = res.markdown

        }

    }

    @Input() articleMeta: Article = {
        summary: "",
        author: "",
        sourceType: 0,
        useAudio: false,
        audioUrl: "",
        audioName: "",
        useReadNum: 10000,
        useRead: true,
        useReading: true,
        useReadingNum: 10,
        bannerImageUrl: "",
        readNum: 0,
        readingNum: 0

    }
    duration
    @Input() markdown

    currentTime
    @Input() html
    @Output() htmlChange = new EventEmitter();
    @ViewChild('audioEl') audioEl: { nativeElement: HTMLAudioElement };
    ngOnInit() {
        this.refershEditor();
        setTimeout(() => {
            this.initPreview();
        }, 1000);
    }
    initPreview() {
        if (this.articleMeta.useAudio) {
            debugger;
            this.audioEl.nativeElement.addEventListener("play", function (e) {
                console.log(e);
            });
            this.audioEl.nativeElement.addEventListener("playing", (e) => {
                setInterval(() => this.refershAudio(), 1000);
            });
            this.audioEl.nativeElement.play();



        }
    }

    refershAudio() {
        var el = this.audioEl.nativeElement
        if (el) {
            this.duration = new Date(el.duration * 1000).format("mm:ss");

            this.currentTime = new Date(el.currentTime * 1000).format("mm:ss");
        }

    }
    refershEditor() {
        if (document.getElementById("editor")) {
            this.markdown = (document.getElementById("editor") as HTMLTextAreaElement).value;
            this.html = document.getElementsByClassName("viewer")[0].innerHTML;
            document.getElementById("editorDiv").remove();
            this.htmlChange.emit(this.html);
        }

    }
} 