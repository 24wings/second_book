
import { View, TreeListView } from "src/app/shared/dto/View";
import { DxiDataGridColumn } from "devextreme-angular/ui/nested/base/data-grid-column-dxi";
import DevExpress from "devextreme/bundles/dx.all";
import LocalStore from "devextreme/data/local_store";
import { EditorType } from "../editor-type";
import DataSource from "devextreme/data/data_source";
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { environment } from 'src/environments/environment';
import { withAuthcationHeaderFunc } from '../util/withAuthcationHeaderFunc';
type EditorOptions = DevExpress.ui.dxTextBoxOptions;

export let articletView: View = {
    add: false,
    delete: false,
    title: "文章管理",
    dvoFullName: "Article",

    dataSource: new DataSource({
        store: AspNetData.createStore({
            key: "id",
            loadUrl: environment.ip + "/api/Hk/article/load",
            insertUrl: environment.ip + "/api/Hk/article",
            updateUrl: environment.ip + "/api/Hk/article",
            deleteUrl: environment.ip + "/api/Hk/article",
            onBeforeSend: withAuthcationHeaderFunc
        })
    }),
    viewType: "Table",
    cols: [
        {
            dataField: "title",
            caption: "标题",
            dataType: "string"
        } as DxiDataGridColumn,
        {
            dataField: "summary",
            caption: "简述"
        },
        {
            dataField: "useAudio",
            caption: "音乐",
            dataType: "boolean"
        },


        {
            dataField: "charNum",
            caption: "字数",
            dataType: "number",
        } as DxiDataGridColumn,
        {
            dataField: "price",
            caption: "价格",
            dataType: "number",

        } as DxiDataGridColumn,
        {
            dataField: "status",
            caption: "状态",
            lookup: {
                dataSource: [ // contains the same values as the "status" field provides
                    { label: '已提交', value: 0 },
                    { label: '上架', value: 1 },
                    { label: '下架', value: 2 }
                    // ...
                ]
            }

        } as any,
        {
            dataField: "summary",
            caption: "简述",
            dataType: "string"
        } as DxiDataGridColumn,
        {
            dataField: "createTime",
            caption: "发布时间",
            dataType: "datetime"
        } as DxiDataGridColumn,

    ],
    items: [
        // {
        //     dataField: "name",
        //     label: { text: "产品名字" },
        //     dataType: "string"
        // } as DevExpress.ui.dxFormSimpleItem,
        // {
        //     dataField: "productTagId",
        //     label: { text: "产品分类" },
        //     // dataType: "number",
        //     editorType: "dxLookup",
        //     editorOptions: {
        //         dxLookup: {
        //             displayExpr: "name",
        //             valueExpr: "id",
        //             dataSource: AspNetData.createStore({
        //                 key: "id",
        //                 loadUrl: environment.ip + "/api/Hk/product-tag/load",
        //             })
        //         }
        //     }
        // } as DevExpress.ui.dxFormSimpleItem,
        // {
        //     dataField: "images",
        //     label: { text: "产品图片" },
        //     editorType: "wsImage" as any
        // } as DevExpress.ui.dxFormSimpleItem,
        // {
        //     dataField: "price",
        //     label: { text: "价格" },
        //     dataType: "number",
        //     // editorOptions: { mode: "password" } as EditorOptions
        // } as DevExpress.ui.dxFormSimpleItem,
        // {
        //     dataField: "status",
        //     caption: "状态",
        //     editorType: "dxSelectBox",
        //     editorOptions: {
        //         displayExpr: "label",
        //         valueExpr: "value",
        //         dataSource: [ // contains the same values as the "status" field provides
        //             { label: '已提交', value: 0 },
        //             { label: '上架', value: 1 },
        //             { label: '下架', value: 2 }
        //             // ...
        //         ]
        //     }
        // }

    ]
};






enum ProductStatus {
    /// <summary>
    /// 已提交
    /// </summary>
    Submit,
    /// <summary>
    /// 有效
    /// </summary>
    Active,
    /// <summary>
    /// 作废
    /// </summary>
    Fail

}
