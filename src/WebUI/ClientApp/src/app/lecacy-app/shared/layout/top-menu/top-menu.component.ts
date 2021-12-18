import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from 'src/app/lecacy-app/core/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Observable, Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit, OnDestroy {

  private subs: Subscription[] = [];
  public displayName: string = "";
  public picture: string = "";

  constructor(private authorizeService: AuthorizeService) { }

  ngOnInit() {
    this.subs.push(this.authorizeService.getUser().subscribe((obj: any) => {
      if (obj) {
        this.displayName = obj.name || "Administrator (Ahmed Elsaedy)";
        this.picture = obj.picture;
        
      }
    }));
  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => {
      sub.unsubscribe();
    });
  }

  logOut() {
  }

}
