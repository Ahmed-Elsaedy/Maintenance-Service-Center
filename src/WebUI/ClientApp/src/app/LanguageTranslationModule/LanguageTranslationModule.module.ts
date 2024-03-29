import { NgModule } from "@angular/core";
import { HttpClient } from "@angular/common/http";
// import ngx-translate and the http loader
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
// ngx-translate - required for AOT compilation
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
@NgModule({
  declarations: [],
  imports: [
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
  exports: [TranslateModule],
})
export class LanguageTranslationModule {
  constructor(private translate: TranslateService) {
    // Gets Default language from browser if available, otherwise set English ad default
    //var lang = "en";
    //this.translate.addLangs(['en', 'ar']);
    //this.translate.setDefaultLang(lang);
    // const browserLang = this.translate.getBrowserLang();
    //this.translate.use(lang);
  }
}
