import { NgModule, ModuleWithProviders } from "@angular/core";
import {
  DxDataGridModule,
  DxTreeListModule,
  DxPopupModule,
  DxSwitchModule,
  DxButtonModule,
  DxTextAreaModule,
  DxTextBoxModule,
  DxSelectBoxModule,
  DxHtmlEditorModule,
  DxTemplateHost,
  DxTemplateModule,
  DxTagBoxModule,
  DxMenuModule,
  DxCheckBoxModule,
  DxNavBarModule,
  DxToolbarModule,
  DxButtonGroupModule,
  DxFormModule,
  DxRadioGroupModule,
  DxTreeViewModule,
  DxDropDownBoxModule,
  DxFileUploaderModule,
  DxLookupModule
} from "devextreme-angular";
import { CommonModule } from "@angular/common";
// import { HostService } from "./services/host.service";
import { SqlMapService } from "./services/sqlmap.service";
import { SingleCardModule } from "../layouts/single-card/single-card.component";
import { WsViewComponent } from "./components/ws-view/ws-view.component";
import { WsEditorComponent } from "./components/ws-editor/ws-editor.component";
import { StringColComponent } from "./components/cols/string-col/string-col.component";
import { DxiButtonModule } from "devextreme-angular/ui/nested/button-dxi";
import { DxiItemModule } from "devextreme-angular/ui/nested/item-dxi";
import { FormsModule } from "@angular/forms";
import { DynamicColComponent } from "./components/cols/dynamic-col.component";
import { DynamicCellComponent } from "./components/cells/dynamic-cell.component";
import { DynamicColDirective } from "./components/cols/dynamic-col.directive";
import { DynamicCellDirective } from "./components/cells/dynamic-cell.directive";
import { cellComponentRegister } from "./components/cells/cell.component.register";
import { WsSearchBarComponent } from "./components/ws-search-bar/ws-search-bar.component";
var cellComponents = cellComponentRegister.map(r => r.component);
import { DxValidatorModule } from "devextreme-angular/ui/validator";
import { DxValidationGroupModule } from "devextreme-angular/ui/validation-group";
import { MyHttpService } from './services/my-http.service';
import { RcxhApiService } from './services/rcxh-api.service';
import { NgxMdModule } from 'ngx-md';
@NgModule({
  imports: [
    NgxMdModule,
    CommonModule,
    DxTagBoxModule,
    DxTextAreaModule,
    DxDataGridModule,
    DxTreeListModule,
    DxPopupModule,
    DxSwitchModule,
    DxButtonModule,
    DxTextBoxModule,
    DxSelectBoxModule,
    DxHtmlEditorModule,
    DxTemplateModule,
    SingleCardModule,
    DxMenuModule,
    DxCheckBoxModule,
    DxNavBarModule,
    DxToolbarModule,
    DxiButtonModule,
    DxButtonGroupModule,
    DxiItemModule,
    FormsModule,
    DxFormModule,
    DxRadioGroupModule,
    DxTreeViewModule,
    DxDropDownBoxModule,
    DxValidationGroupModule,
    DxValidatorModule,
    DxFileUploaderModule,
    DxLookupModule,

  ],
  exports: [
    NgxMdModule,
    DxTagBoxModule,
    DxDataGridModule,
    DxTreeListModule,
    DxPopupModule,
    DxSwitchModule,
    DxButtonModule,
    CommonModule,
    DxTextAreaModule,
    DxTextBoxModule,
    DxSelectBoxModule,
    DxHtmlEditorModule,
    DxTemplateModule,
    SingleCardModule,
    DxMenuModule,
    DxCheckBoxModule,
    DxNavBarModule,
    DxToolbarModule,
    WsViewComponent,
    WsEditorComponent,
    StringColComponent,
    DxiButtonModule,
    DxButtonGroupModule,
    DxiItemModule,
    FormsModule,
    DxFormModule,
    DynamicColComponent,
    DynamicCellComponent,
    DxRadioGroupModule,
    DxTreeViewModule,
    DxDropDownBoxModule,
    ...cellComponents,
    WsSearchBarComponent,
    DxValidationGroupModule,
    DxValidatorModule,
    DxFileUploaderModule,
    DxLookupModule,
  ],
  declarations: [
    WsViewComponent,
    WsEditorComponent,
    StringColComponent,
    DynamicColComponent,
    DynamicCellComponent,
    DynamicColDirective,
    DynamicCellDirective,
    ...cellComponents,
    WsSearchBarComponent,
  ],
  providers: [SqlMapService, DxTemplateHost, RcxhApiService],
  entryComponents: [...cellComponents]
})
export class SharedModule {
  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [SqlMapService, MyHttpService]
    };
  }
}
